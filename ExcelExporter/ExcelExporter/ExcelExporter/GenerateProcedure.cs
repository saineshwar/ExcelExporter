using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelExporter
{
    public partial class GenerateProcedure : Form
    {
        private string ConnectionString { get; set; }
        private string FolderPath { get; set; }
        public GenerateProcedure()
        {
            InitializeComponent();
        }

        public GenerateProcedure(string connectionString, string folderPath)
        {
            ConnectionString = connectionString;
            FolderPath = folderPath;
            InitializeComponent();

            if (!string.IsNullOrEmpty(connectionString))
            {
                BindDatabases();
            }
        }

        private void BindDatabases()
        {

            DataSet dsDatabases = new DataSet();
            using (var con = new SqlConnection(ConnectionString))
            {
                try
                {
                    string query = "SELECT NAME FROM sys.databases ORDER BY NAME ";
                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    var daTab = new SqlDataAdapter(cmd);

                    daTab.Fill(dsDatabases);
                    listDatabase.DataSource = dsDatabases.Tables[0];
                    listDatabase.DisplayMember = "NAME";
                    listDatabase.ValueMember = "NAME";

                    if (listDatabase.Items.Count > 0)
                    {


                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    dsDatabases.Dispose();
                }
            }
        }

        private void btnshowProcedures_Click(object sender, EventArgs e)
        {
            groupBox2.Text = @"Show Procedures";
            ConnectionString = ConnectionString + $"database={listDatabase.SelectedValue};";

            if (string.IsNullOrEmpty(Convert.ToString(listDatabase.SelectedValue)))
            {
                MessageBox.Show(string.Format("Please Select Database name"));
                return;
            }
            else
            {
                listDatabase.Enabled = false;

                using (var con = new SqlConnection(ConnectionString))
                {
                    DataSet dsTables = new DataSet();
                    try
                    {
                        //string tableQuery = "select SPECIFIC_NAME from " + Convert.ToString(listDatabase.SelectedValue) +
                        //                    ".information_schema.routines where routine_type = 'PROCEDURE' and Left(Routine_Name, 3) NOT IN ('sp_', 'xp_', 'ms_') order by Created desc";

                        string tableQuery = @"SELECT name FROM   sys.procedures pr WHERE NOT EXISTS(SELECT * FROM   sys.parameters p WHERE  p.object_id = pr.object_id)";


                        con.Open();
                        var cmd = new SqlCommand(tableQuery, con);
                        var daTab = new SqlDataAdapter(cmd);

                        daTab.Fill(dsTables);
                        listboxProcedure.DataSource = dsTables.Tables[0];
                        listboxProcedure.DisplayMember = "name";
                        listboxProcedure.ValueMember = "name";

                        if (listboxProcedure.Items.Count > 0)
                        {


                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        dsTables.Dispose();
                    }
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(listDatabase.SelectedValue)))
            {
                MessageBox.Show(string.Format("Please Select Database Name"));
                return;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(listboxProcedure.SelectedValue)))
            {
                MessageBox.Show(string.Format("Please Select Stored procedure"));
                return;
            }
            else if (ValidateStoreProcedure(Convert.ToString(listboxProcedure.SelectedValue), ConnectionString) != string.Empty)
            {
                var message = ValidateStoreProcedure(Convert.ToString(listboxProcedure.SelectedValue), ConnectionString);
                MessageBox.Show(message);
                return;
            }
            else
            {
                btnshowProcedures.Enabled = false;
                listboxProcedure.Enabled = false;

                var table = Convert.ToString(listboxProcedure.SelectedValue);
                var database = Convert.ToString(listDatabase.SelectedValue);

                DataPasser dataPasser = new DataPasser()
                {
                    DatabaseName = database,
                    TableName = table
                };

                backgroundWorker1.RunWorkerAsync(dataPasser);
                backgroundWorker1.ReportProgress(10, "Process Started");
            }
        }

        private string ValidateStoreProcedure(string selectedstoreprocedure, string connectionString)
        {
            var dsTables = new DataTable();
            var message = string.Empty;
            var query = $"sp_helptext {selectedstoreprocedure}";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                var daTab = new SqlDataAdapter(cmd);
                daTab.Fill(dsTables);

                if (dsTables.Rows.Count > 0)
                {
                    var proceduretext = ConvertDataTableToString(dsTables);

                    if (proceduretext.Contains("Insert"))
                    {
                        message = "Stored Procedure Must Only Contain Select clause";
                    }
                    else if (proceduretext.Contains("Update"))
                    {
                        message = "Stored Procedure Must Only Contain Select clause";
                    }
                    else if (proceduretext.Contains("Delete"))
                    {
                        message = "Stored Procedure Must Only Contain Select clause";
                    }
                    else if (proceduretext.Contains("@"))
                    {
                        message = "Stored Procedure Must Not Have Parameter [Should be Self Executing]";
                    }

                }
            }

            return message;
        }

        public static string ConvertDataTableToString(DataTable dataTable)
        {
            var output = new StringBuilder();

            var columnsWidths = new int[dataTable.Columns.Count];

            // Get column widths
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var length = row[i].ToString().Length;
                    if (columnsWidths[i] < length)
                        columnsWidths[i] = length;
                }
            }

            // Get Column Titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var length = dataTable.Columns[i].ColumnName.Length;
                if (columnsWidths[i] < length)
                    columnsWidths[i] = length;
            }

            // Write Column titles
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var text = dataTable.Columns[i].ColumnName;
                output.Append(PadCenter(text, columnsWidths[i] + 2));
            }
            //output.Append("|\n" + new string('=', output.Length) + "\n");

            // Write Rows
            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    var text = row[i].ToString();
                    output.Append(PadCenter(text, columnsWidths[i] + 2));
                }

            }
            return output.ToString();
        }

        private static string PadCenter(string text, int maxLength)
        {
            int diff = maxLength - text.Length;
            return new string(' ', diff / 2) + text + new string(' ', (int)(diff / 2.0 + 0.5));

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var data = e.Argument as DataPasser;
            if (data != null) ProcessMethod(data.TableName, data.DatabaseName);
        }

        public void ProcessMethod(string selectedstoreprocedure, string selectedDatabase)
        {
            backgroundWorker1.ReportProgress(20, "Started Processing");

            var storeprocedure = Convert.ToString(selectedstoreprocedure);
            var databasename = Convert.ToString(selectedDatabase);


            DataTable dsTables = new DataTable();

            DataColumn column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                AutoIncrement = true,
                AutoIncrementSeed = 1,
                AutoIncrementStep = 1,
                ColumnName = "___Id"
            };

            dsTables.Columns.Add(column);

            using (var con = new SqlConnection(ConnectionString))
            {
                string tableQuery = $"exec " + Convert.ToString(storeprocedure);
                con.Open();
                var cmd = new SqlCommand(tableQuery, con);
                cmd.CommandTimeout = 0;
                var daTab = new SqlDataAdapter(cmd);
                daTab.Fill(dsTables);
                backgroundWorker1.ReportProgress(30, "Pulling Records from Database");
            }

            if (dsTables.Rows.Count > 0)
            {
                var minRow = Convert.ToInt32(dsTables.Compute("min([___Id])", string.Empty));
                var maxRow = Convert.ToInt32(dsTables.Compute("max([___Id])", string.Empty));

                backgroundWorker1.ReportProgress(50, "Pulling Records from Database");

                var loopCount = Listgenerator(minRow, maxRow);

                for (var j = 0; j < loopCount.Count; j++)
                {
                    int value = 50 + j;
                    int counter = 1;
                    counter = counter + j;
                    backgroundWorker1.ReportProgress(value, "Generating Files " + counter);

                    var fromtemp = loopCount[j].From;
                    var totemp = loopCount[j].To;

                    IEnumerable<DataRow> myDataPage =
                        from myrow in dsTables.AsEnumerable()
                        where myrow.Field<int>("___Id") >= fromtemp && myrow.Field<int>("___Id") <= totemp
                        select myrow;

                    var newDt = myDataPage.CopyToDataTable();

                    var count = j;
                    GenerateSheet(newDt, count, storeprocedure, loopCount.Count);

                }

                backgroundWorker1.ReportProgress(100, "Process Completed");
            }

        }

        private List<PartitionValues> Listgenerator(int minRecordCount, int maxRecordCount)
        {
            var partitionList = new List<PartitionValues>();

            var tempmin = minRecordCount;

            int current = minRecordCount;

            for (int i = 1; i <= 100; i++)
            {
                if (maxRecordCount >= tempmin)
                {
                    tempmin += 50000;
                    partitionList.Add(new PartitionValues()
                    {
                        From = current,
                        To = tempmin
                    });

                    current = tempmin;
                }
                else
                {
                    break;
                }
            }

            return partitionList;

        }

        private int MinRecordCount(string primaryKey, string tablename)
        {
            try
            {
                var dsTables = new DataSet();
                int minCount = 0;
                using (var con = new SqlConnection(ConnectionString))
                {
                    var tableQuery = string.Format($"select Min({primaryKey}) as Count from " + Convert.ToString(tablename));
                    con.Open();
                    var cmd = new SqlCommand(tableQuery, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
                    daTab.Fill(dsTables);

                    if (dsTables.Tables.Count != 0)
                    {
                        minCount = Convert.ToInt32(dsTables.Tables[0].Rows[0]["Count"]);
                    }
                }
                return minCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int MaxRecordCount(string primaryKey, string tablename)
        {
            try
            {
                var dsTables = new DataSet();
                int minCount = 0;
                using (var con = new SqlConnection(ConnectionString))
                {
                    var tableQuery = string.Format($"select Max({primaryKey}) as Count from " + Convert.ToString(tablename));
                    con.Open();
                    var cmd = new SqlCommand(tableQuery, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
                    daTab.Fill(dsTables);

                    if (dsTables.Tables.Count != 0)
                    {
                        minCount = Convert.ToInt32(dsTables.Tables[0].Rows[0]["Count"]);
                    }
                }
                return minCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TspProcBar.Value = e.ProgressPercentage;
            ToolText.Text = Convert.ToString(e.UserState);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($@"Process Completed");
            listDatabase.Enabled = true;
            listboxProcedure.Enabled = true;
            btnshowProcedures.Enabled = true;
            listboxProcedure.DataSource = null;
            listboxProcedure.Items.Clear();
            btnGenerate.BackColor = Color.DodgerBlue;
            TspProcBar.Value = 0;
            ToolText.Text = "";
        }

        public void GenerateSheet(DataTable dsExcel, int count, string tableName, int totalcount)
        {
            using (ExcelPackage objExcelPackage = new ExcelPackage())
            {
                ExcelWorksheet objWorksheet = objExcelPackage.Workbook.Worksheets.Add("Result");
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1    
                objWorksheet.Cells["A1"].LoadFromDataTable(dsExcel, true);
                objWorksheet.Cells.Style.Font.SetFromFont(new Font("Calibri", 10));
                objWorksheet.Cells.AutoFitColumns();

                using (ExcelRange objRange = objWorksheet.Cells["A1:XFD1"])
                {
                    objRange.Style.Font.Bold = true;
                    objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    objRange.Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                }

                var filename = DateTime.Now.ToString("yyyyMMddHHmmss");

                string path = Convert.ToString(FolderPath + @"\" + tableName + "_" + Convert.ToString(count) + ".xlsx");
                var excelByte = objExcelPackage.GetAsByteArray();

                File.WriteAllBytes(path, excelByte);
            }

            if (count == totalcount - 1)
            {
                backgroundWorker1.ReportProgress(80, "Generating Files");
            }
        }

        private void GenerateProcedure_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show($@"Application Closed");
            Application.Exit();
        }
    }
}

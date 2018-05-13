using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelExporter
{
    public partial class MySqlGenerateTable : Form
    {

        public string ConnectionString { get; set; }

        private readonly string _folderPath;
        public MySqlGenerateTable()
        {
            InitializeComponent();
        }

        public MySqlGenerateTable(string connectionString, string folderPath)
        {
            ConnectionString = connectionString;
            _folderPath = folderPath;
            InitializeComponent();

            if (!string.IsNullOrEmpty(connectionString))
            {
                BindDatabases();
            }
        }

        private void BindDatabases()
        {

            DataSet dsDatabases = new DataSet();
            using (var con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    string query = "SHOW DATABASES";
                    con.Open();
                    var cmd = new MySqlCommand(query, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new MySqlDataAdapter(cmd);

                    daTab.Fill(dsDatabases);
                    listDatabase.DataSource = dsDatabases.Tables[0];
                    listDatabase.DisplayMember = "Database";
                    listDatabase.ValueMember = "Database";

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
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                var data = e.Argument as DataPasser;
                if (data != null) ProcessMethod(data.TableName, data.DatabaseName);
            }
            catch (System.OutOfMemoryException)
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

            btnshowtbl.Enabled = true;
            listboxTables.Enabled = true;
            listDatabase.Enabled = true;
            listboxTables.DataSource = null;
            listboxTables.Items.Clear();
            btnGenerate.BackColor = Color.DodgerBlue;
            TspProcBar.Value = 0;
            ToolText.Text = "";
        }

        private void btnshowtbl_Click(object sender, EventArgs e)
        {
            groupBox2.Text = @"Tables";
            ConnectionString = ConnectionString + $"database={listDatabase.SelectedValue};";

            if (string.IsNullOrEmpty(Convert.ToString(listDatabase.SelectedValue)))
            {
                MessageBox.Show(string.Format("Please Select Database name"));
                return;
            }
            else
            {
                listDatabase.Enabled = false;

                using (var con = new MySqlConnection(ConnectionString))
                {
                    var dsTables = new DataSet();
                    try
                    {
                        string tableQuery = "SELECT table_name FROM information_schema.tables where table_schema='" + Convert.ToString(listDatabase.SelectedValue) + "' order by table_name ;";
                        con.Open();
                        var cmd = new MySqlCommand(tableQuery, con);
                        cmd.CommandTimeout = 0;
                        var daTab = new MySqlDataAdapter(cmd);

                        daTab.Fill(dsTables);
                        listboxTables.DataSource = dsTables.Tables[0];
                        listboxTables.DisplayMember = "table_name";
                        listboxTables.ValueMember = "table_name";

                        if (listboxTables.Items.Count > 0)
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
                btnGenerate.BackColor = Color.DarkRed;
                MessageBox.Show(string.Format("Please Select Database Name"), @"Error Message");
                return;
            }
            else if (string.IsNullOrEmpty(Convert.ToString(listboxTables.SelectedValue)))
            {
                MessageBox.Show(string.Format("Please Select Table Name"), @"Error Message");
                return;
            }
            else if (ValidateDatatype(Convert.ToString(listboxTables.SelectedValue), ConnectionString) == "invalid")
            {
                MessageBox.Show(
                    $@"The Table {Convert.ToString(listboxTables.SelectedValue)} contains Binary Data Types",
                    @"Error Message");
                return;
            }
            else if (ValidateTableCount(Convert.ToString(listboxTables.SelectedValue), ConnectionString))
            {
                MessageBox.Show(
                    $@"The Table {Convert.ToString(listboxTables.SelectedValue)} does not have data to export",
                    @"Error Message");
                return;
            }
            else
            {
                btnGenerate.BackColor = Color.LimeGreen;
                btnshowtbl.Enabled = false;
                listboxTables.Enabled = false;

                if (backgroundWorker1.IsBusy)
                {
                    MessageBox.Show(@"Please Wait ... Process is under Exceution");
                    return;
                }
                else
                {
                    var table = Convert.ToString(listboxTables.SelectedValue);
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
        }

        private string ReturnprimaryKeyDatatype(string connectionString, string selectedDatabase, string selectedtablename)
        {
            var databasename = Convert.ToString(selectedDatabase);
            var tablename = Convert.ToString(selectedtablename);
            string primaryKey = string.Empty;
            string datatype = string.Empty;
            var dsTables = new DataTable();
            try
            {
                using (var con = new MySqlConnection(connectionString))
                {
                    string query =
                        " SELECT k.COLUMN_NAME FROM information_schema.TABLE_CONSTRAINTS t JOIN information_schema.KEY_COLUMN_USAGE k " +
                        " USING(CONSTRAINT_NAME, TABLE_SCHEMA, TABLE_NAME)" +
                        $" WHERE t.CONSTRAINT_TYPE = 'PRIMARY KEY' AND t.TABLE_SCHEMA = '{databasename}' AND t.TABLE_NAME = '{tablename}'; ";

                    con.Open();
                    var cmd = new MySqlCommand(query, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new MySqlDataAdapter(cmd);
                    daTab.Fill(dsTables);

                    if (dsTables.Rows.Count > 0)
                    {
                        primaryKey = Convert.ToString(dsTables.Rows[0]["COLUMN_NAME"]);
                    }
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

            var dsType = new DataTable();
            try
            {
                if (!string.IsNullOrEmpty(primaryKey))
                {
                    using (var con = new MySqlConnection(connectionString))
                    {
                        string query =
                            $"SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{selectedtablename}' AND COLUMN_NAME = '{Convert.ToString(primaryKey)}';";
                        con.Open();
                        var cmd = new MySqlCommand(query, con);
                        var daTab = new MySqlDataAdapter(cmd);
                        daTab.Fill(dsType);

                        if (dsType.Rows.Count > 0)
                        {
                            datatype = Convert.ToString(dsType.Rows[0]["DATA_TYPE"]);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dsType.Dispose();
            }

            return datatype;
        }

        private void MySQLGenerateTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show($@"Application Closed");
            Application.Exit();
        }

        private bool ValidateprimaryKeyDatatype(string primarykey, string datatype)
        {
            if (!string.IsNullOrEmpty(primarykey) && (datatype != "int" || datatype == "bigint"))
            {
                return false;
            }
            else if (!string.IsNullOrEmpty(primarykey) && string.IsNullOrEmpty(datatype))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(primarykey) && string.IsNullOrEmpty(datatype))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateTableCount(string selectedtablename, string connectionString)
        {
            try
            {
                var dsTables = new DataSet();
                using (var con = new MySqlConnection(ConnectionString))
                {
                    var tableQuery = string.Format($"select Count({1}) as TotalCount from " + Convert.ToString(selectedtablename));
                    con.Open();
                    var cmd = new MySqlCommand(tableQuery, con);
                    var daTab = new MySqlDataAdapter(cmd);
                    daTab.Fill(dsTables);

                    var totalCount = false;
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (dsTables.Tables.Count > 0)
                    {
                        totalCount = false;
                    }
                    else
                    {
                        totalCount = true;
                    }

                    return totalCount;
                }
            }
            catch (Exception)
            {
                throw;
            }
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



                string path = Convert.ToString(_folderPath + @"\" + tableName + "_" + Convert.ToString(count) + ".xlsx");
                var excelByte = objExcelPackage.GetAsByteArray();

                File.WriteAllBytes(path, excelByte);
            }

            if (count == totalcount - 1)
            {
                backgroundWorker1.ReportProgress(80, "Generating Files");
            }
        }

        private string ValidateDatatype(string selectedtablename, string connectionString)
        {
            try
            {
                var dsTables = new DataTable();
                var invalidflag = "valid";
                var query = $"SHOW fields FROM {selectedtablename}";

                using (var con = new MySqlConnection(connectionString))
                {
                    con.Open();
                    var cmd = new MySqlCommand(query, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new MySqlDataAdapter(cmd);
                    daTab.Fill(dsTables);

                    if (dsTables.Rows.Count > 0)
                    {

                        foreach (DataRow row in dsTables.Rows)
                        {
                            var columnsname = row["Field"].ToString();
                            var datatype = row["Type"].ToString();

                            if (datatype == "varbinary" || datatype == "image" || datatype == "binary" || datatype == "geography" || datatype == "xml" || datatype == "ntext" || datatype == "text")
                            {
                                invalidflag = "invalid";
                                break;
                            }
                        }
                    }
                }

                return invalidflag;
            }
            catch (Exception)
            {
                throw;
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
                var dsTables = new DataTable();
                int minCount = 0;
                using (var con = new MySqlConnection(ConnectionString))
                {
                    var tableQuery = string.Format($"select Min({primaryKey}) as Count from " + Convert.ToString(tablename));
                    con.Open();
                    var cmd = new MySqlCommand(tableQuery, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new MySqlDataAdapter(cmd);
                    daTab.Fill(dsTables);

                    if (dsTables.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dsTables.Rows[0]["Count"])))
                        {
                            minCount = Convert.ToInt32(dsTables.Rows[0]["Count"]);
                        }

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
                var dsTables = new DataTable();
                int minCount = 0;
                using (var con = new MySqlConnection(ConnectionString))
                {
                    var tableQuery = string.Format($"select Max({primaryKey}) as Count from " + Convert.ToString(tablename));
                    con.Open();
                    var cmd = new MySqlCommand(tableQuery, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new MySqlDataAdapter(cmd);
                    
                    daTab.Fill(dsTables);

                    if (dsTables.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dsTables.Rows[0]["Count"])))
                        {
                            minCount = Convert.ToInt32(dsTables.Rows[0]["Count"]);
                        }
                    }
                }
                return minCount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ProcessMethod(string selectedtablename, string selectedDatabase)
        {
            try
            {
                backgroundWorker1.ReportProgress(10, "Started Processing");

                var tableName = Convert.ToString(selectedtablename);
                var databasename = Convert.ToString(selectedDatabase);
                var tablename = Convert.ToString(selectedtablename);

                var primaryKey = ValidateprimaryKey(ConnectionString, databasename, tablename);
                var hasDatatype = ReturnprimaryKeyDatatype(ConnectionString, databasename, tablename);

                if (ValidateprimaryKeyDatatype(primaryKey, hasDatatype) == false)
                {
                    var dsTables = new DataTable();

                    DataColumn column = new DataColumn
                    {
                        DataType = System.Type.GetType("System.Int32"),
                        AutoIncrement = true,
                        AutoIncrementSeed = 1,
                        AutoIncrementStep = 1,
                        ColumnName = "___Id"
                    };

                    dsTables.Columns.Add(column);

                    using (var con = new MySqlConnection(ConnectionString))
                    {
                        string tableQuery = $"select * from " + Convert.ToString(tableName);
                        con.Open();
                        var cmd = new MySqlCommand(tableQuery, con);
                        cmd.CommandTimeout = 0;
                        var daTab = new MySqlDataAdapter(cmd);
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

                            var fromtemp = loopCount[j].From;
                            var totemp = loopCount[j].To;

                            backgroundWorker1.ReportProgress(value, "Generating Files " + counter);

                            IEnumerable<DataRow> myDataPage =
                                from myrow in dsTables.AsEnumerable()
                                where myrow.Field<int>("___Id") >= fromtemp && myrow.Field<int>("___Id") <= totemp
                                select myrow;

                            var newDt = myDataPage.CopyToDataTable();

                            var count = j;
                            GenerateSheet(newDt, count, tableName, loopCount.Count);

                        }

                        backgroundWorker1.ReportProgress(100, "Process Completed");
                    }
                }
                else
                {

                    var loopCount = Listgenerator(MinRecordCount(primaryKey, selectedtablename), MaxRecordCount(primaryKey, selectedtablename));

                    backgroundWorker1.ReportProgress(30, "Pulling Records from Database");

                    // ReSharper disable once ForCanBeConvertedToForeach
                    for (var j = 0; j < loopCount.Count; j++)
                    {
                        int value = 50 + j;
                        int counter = 1;
                        counter = counter + j;

                        backgroundWorker1.ReportProgress(value, "Generating Files " + counter);

                        var dsTables = new DataSet();

                        using (var con = new MySqlConnection(ConnectionString))
                        {
                            string tableQuery = $"select * from " + Convert.ToString(tableName) + $" where {primaryKey} between {loopCount[j].From} and {loopCount[j].To}";
                            con.Open();
                            
                            var cmd = new MySqlCommand(tableQuery, con);
                            var daTab = new MySqlDataAdapter(cmd);
                            cmd.CommandTimeout = 0;
                            daTab.Fill(dsTables);
                        }

                        if (dsTables.Tables[0].Rows.Count > 0)
                        {
                            var count = j;
                            GenerateSheet(dsTables.Tables[0], count, tableName, loopCount.Count);
                        }
                    }

                    backgroundWorker1.ReportProgress(100, "Process Completed");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ValidateprimaryKey(string connectionString, string selectedDatabase, string selectedtablename)
        {

            var databasename = Convert.ToString(selectedDatabase);
            var tablename = Convert.ToString(selectedtablename);
            string primaryKey = string.Empty;
            var dsTables = new DataTable();
            try
            {
                using (var con = new MySqlConnection(connectionString))
                {
                    string query =
                        " SELECT k.COLUMN_NAME FROM information_schema.TABLE_CONSTRAINTS t JOIN information_schema.KEY_COLUMN_USAGE k" +
                        " USING(CONSTRAINT_NAME, TABLE_SCHEMA, TABLE_NAME)" +
                        $" WHERE t.CONSTRAINT_TYPE = 'PRIMARY KEY' AND t.TABLE_SCHEMA = '{databasename}' AND t.TABLE_NAME = '{tablename}'; ";

                    con.Open();
                    var cmd = new MySqlCommand(query, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new MySqlDataAdapter(cmd);
                    daTab.Fill(dsTables);

                    if (dsTables.Rows.Count > 0)
                    {
                        primaryKey = Convert.ToString(dsTables.Rows[0]["COLUMN_NAME"]);
                    }

                    return primaryKey;
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

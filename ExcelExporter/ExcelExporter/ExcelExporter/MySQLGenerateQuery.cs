using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelExporter
{
    public partial class MySQLGenerateQuery : Form
    {
        public string ConnectionString { get; set; }

        private readonly string _folderPath;
        public MySQLGenerateQuery()
        {
            InitializeComponent();
        }
        public MySQLGenerateQuery(string connectionString, string folderPath)
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(listDatabase.SelectedValue)))
            {
                MessageBox.Show(string.Format("Please Select Database Name"));
                return;
            }
            else if (string.IsNullOrEmpty(txtQuery.Text))
            {
                MessageBox.Show(string.Format("Please Enter Query"));
                return;
            }

            else
            {
                ConnectionString = ConnectionString + $"database={listDatabase.SelectedValue};";

                var cmLibrary = new CommonLibrary.CommonLibrary();
                var validationresult = cmLibrary.ValidateQuery(txtQuery.Text);
                var validate = cmLibrary.ValidateQueryMustContain(txtQuery.Text);
                if (string.IsNullOrEmpty(validationresult) && validate == false)
                {
                    if (backgroundWorker1.IsBusy)
                    {
                        MessageBox.Show(@"Please Wait ... Process is under Exceution");
                        return;
                    }
                    else
                    {

                        backgroundWorker1.RunWorkerAsync(txtQuery.Text);
                        backgroundWorker1.ReportProgress(10, "Process Started");
                    }
                }
                else
                {
                    MessageBox.Show(@"Please Enter Valid Query");
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TspProcBar.Value = e.ProgressPercentage;
            ToolText.Text = Convert.ToString(e.UserState);
        }

        public void ProcessMethod(string query)
        {
            backgroundWorker1.ReportProgress(20, "Pulling Records from Database");

            var dsTables = new DataTable();

            try
            {
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
                    con.Open();
                    var cmd = new MySqlCommand(query, con) { CommandTimeout = 0 };
                    var daTab = new MySqlDataAdapter(cmd);
                    daTab.Fill(dsTables);
                    backgroundWorker1.ReportProgress(30, "Pulling Records from Database");
                }

                // ReSharper disable once InvertIf
                if (dsTables.Rows.Count > 0)
                {
                    var minRow = Convert.ToInt32(dsTables.Compute("min([___Id])", string.Empty));
                    var maxRow = Convert.ToInt32(dsTables.Compute("max([___Id])", string.Empty));

                    backgroundWorker1.ReportProgress(50, "Processing Data");
                    var cmLibrary = new CommonLibrary.CommonLibrary();
                    var loopCount = cmLibrary.Listgenerator(minRow, maxRow);

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
                        GenerateSheet(newDt, count, "QueryGenerated", loopCount.Count);
                    }

                    backgroundWorker1.ReportProgress(100, "Process Completed");
                }

            }
            catch (Exception)
            {
                var message = "1. Choose Proper Database" + Environment.NewLine + "2. Optimize Query" +
                              Environment.NewLine + "3. " + "check Query Once";

                MessageBox.Show(@message);


            }
            finally
            {
                dsTables.Dispose();
            }

        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var data = e.Argument as string;
            if (data != null) ProcessMethod(data);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show($@"Process Completed");
            txtQuery.Text = string.Empty;
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

                string path = Convert.ToString(_folderPath + @"\" + tableName + "_" + Convert.ToString(count) + ".xlsx");
                var excelByte = objExcelPackage.GetAsByteArray();

                File.WriteAllBytes(path, excelByte);
            }

            if (count == totalcount - 1)
            {
                backgroundWorker1.ReportProgress(80, "Generating Files");
            }
        }

        private void MySQLGenerateQuery_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void MySQLGenerateQuery_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtQuery.Text = string.Empty;
            MessageBox.Show($@"Form Closed");
            Application.Exit();
        }
    }
}

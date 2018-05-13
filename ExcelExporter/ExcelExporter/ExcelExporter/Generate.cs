using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelExporter
{
    public partial class Generate : Form
    {

        public string ConnectionString { get; set; }
        private string FolderPath { get; set; }

        public Generate()
        {
            InitializeComponent();
        }

        public Generate(string connectionString, string folderPath)
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
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
                    daTab.Fill(dsDatabases);
                    listDatabase.DataSource = dsDatabases.Tables[0];
                    listDatabase.DisplayMember = "NAME";
                    listDatabase.ValueMember = "NAME";
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

                using (var con = new SqlConnection(ConnectionString))
                {
                    var dsTables = new DataSet();
                    try
                    {
                        string tableQuery = "select * from " + Convert.ToString(listDatabase.SelectedValue) +
                                            ".sys.tables order by name";
                        con.Open();
                        var cmd = new SqlCommand(tableQuery, con);
                        cmd.CommandTimeout = 0;
                        var daTab = new SqlDataAdapter(cmd);

                        daTab.Fill(dsTables);
                        listboxTables.DataSource = dsTables.Tables[0];
                        listboxTables.DisplayMember = "Name";
                        listboxTables.ValueMember = "Name";

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
                    $@"The Table {Convert.ToString(listboxTables.SelectedValue)} contains Binary Data Types", @"Error Message");
                return;
            }
            else if (ValidateTableCount(Convert.ToString(listboxTables.SelectedValue), ConnectionString))
            {

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

        public void Validate(string database, string table, string connectionString, out string primaryKey, out string datatype)
        {
            primaryKey = "";
            datatype = "";

            var dsTables = new DataTable();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    string query =
                        "SELECT cu.CONSTRAINT_NAME, cu.COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE cu " +
                        "WHERE EXISTS(SELECT tc.* FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc " +
                        $"WHERE tc.CONSTRAINT_CATALOG = '{database}' AND tc.TABLE_NAME = '{table}' AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND tc.CONSTRAINT_NAME = cu.CONSTRAINT_NAME)";

                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
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
                using (var con = new SqlConnection(connectionString))
                {
                    string query = $"select DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{table}' and COLUMN_NAME = '{Convert.ToString(dsTables.Rows[0]["COLUMN_NAME"])}'";
                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    var daTab = new SqlDataAdapter(cmd);
                    daTab.Fill(dsType);

                    if (dsType.Rows.Count > 0)
                    {
                        datatype = Convert.ToString(dsType.Rows[0]["DATA_TYPE"]);
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

        }

        private int MinRecordCount(string primaryKey, string tablename)
        {
            try
            {
                var dsTables = new DataTable();
                int minCount = 0;
                using (var con = new SqlConnection(ConnectionString))
                {
                    var tableQuery = string.Format($"select Min({primaryKey}) as Count from " + Convert.ToString(tablename));
                    con.Open();
                    var cmd = new SqlCommand(tableQuery, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
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
                using (var con = new SqlConnection(ConnectionString))
                {
                    var tableQuery = string.Format($"select Max({primaryKey}) as Count from " + Convert.ToString(tablename));
                    con.Open();
                    var cmd = new SqlCommand(tableQuery, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            var data = e.Argument as DataPasser;
            if (data != null) ProcessMethod(data.TableName, data.DatabaseName);
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
        public void ProcessMethod(string selectedtablename, string selectedDatabase)
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

                using (var con = new SqlConnection(ConnectionString))
                {
                    string tableQuery = $"select * from " + Convert.ToString(tableName);
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

                    using (var con = new SqlConnection(ConnectionString))
                    {
                        string tableQuery = $"select * from " + Convert.ToString(tableName) + $" where {primaryKey} between {loopCount[j].From} and {loopCount[j].To}";
                        con.Open();
                        var cmd = new SqlCommand(tableQuery, con);
                        var daTab = new SqlDataAdapter(cmd);
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

        private string ValidateprimaryKey(string connectionString, string selectedDatabase, string selectedtablename)
        {

            var databasename = Convert.ToString(selectedDatabase);
            var tablename = Convert.ToString(selectedtablename);
            string primaryKey = string.Empty;
            var dsTables = new DataTable();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    string query =
                        "SELECT cu.CONSTRAINT_NAME, cu.COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE cu " +
                        "WHERE EXISTS(SELECT tc.* FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc " +
                        $"WHERE tc.CONSTRAINT_CATALOG = '{databasename}' AND tc.TABLE_NAME = '{tablename}' AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND tc.CONSTRAINT_NAME = cu.CONSTRAINT_NAME)";

                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
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

        private string ValidateDatatype(string selectedtablename, string connectionString)
        {
            var dsTables = new DataTable();
            var invalidflag = "valid";
            var query =
                $"SELECT c.name as columnsname,t.name as datatype FROM sys.columns c JOIN sys.types t ON c.user_type_id = t.user_type_id WHERE c.object_id = Object_id('{selectedtablename}')";

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var cmd = new SqlCommand(query, con);
                cmd.CommandTimeout = 0;
                var daTab = new SqlDataAdapter(cmd);
                daTab.Fill(dsTables);

                if (dsTables.Rows.Count > 0)
                {

                    foreach (DataRow row in dsTables.Rows)
                    {
                        var columnsname = row["columnsname"].ToString();
                        var datatype = row["datatype"].ToString();

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

        private string ReturnprimaryKeyDatatype(string connectionString, string selectedDatabase, string selectedtablename)
        {
            var databasename = Convert.ToString(selectedDatabase);
            var tablename = Convert.ToString(selectedtablename);
            string primaryKey = string.Empty;
            string datatype = string.Empty;
            var dsTables = new DataTable();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    string query =
                        "SELECT cu.CONSTRAINT_NAME, cu.COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE cu " +
                        "WHERE EXISTS(SELECT tc.* FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc " +
                        $"WHERE tc.CONSTRAINT_CATALOG = '{databasename}' AND tc.TABLE_NAME = '{tablename}' AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND tc.CONSTRAINT_NAME = cu.CONSTRAINT_NAME)";

                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    cmd.CommandTimeout = 0;
                    var daTab = new SqlDataAdapter(cmd);
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
                    using (var con = new SqlConnection(connectionString))
                    {
                        string query =
                            $"select DATA_TYPE from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '{selectedtablename}' and COLUMN_NAME = '{Convert.ToString(primaryKey)}'";
                        con.Open();
                        var cmd = new SqlCommand(query, con);
                        cmd.CommandTimeout = 0;
                        var daTab = new SqlDataAdapter(cmd);
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
            var dsTables = new DataSet();
            using (var con = new SqlConnection(ConnectionString))
            {
                var tableQuery = string.Format($"select Count({1}) as TotalCount from " + Convert.ToString(selectedtablename));
                con.Open();
                var cmd = new SqlCommand(tableQuery, con);
                cmd.CommandTimeout = 0;
                var daTab = new SqlDataAdapter(cmd);
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

        public void GenerateSheet(DataTable dsExcel, int count, string tableName, int totalcount)
        {
            try
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
            catch (Exception)
            {
                MessageBox.Show(@"Folder You have Choosen Does have Access");
            }
        }

        private void Generate_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show($@"Application Closed");
            Application.Exit();
        }
    }
}

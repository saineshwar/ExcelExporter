using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace ExcelExporter
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
            txtServerName.Text = "0.0.0.0";
            txtLogin.Text = "";
            txtPassword.Text = "";
            BindDropdown();
            txt_my_HostAddress.Text = @"0.0.0.0";
            txt_my_Username.Text = @"";
            txt_my_Password.Text = @"";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(comAuthen.SelectedValue) == "SQL Server Authentication")
            {

                if (txtServerName.Text == "")
                {
                    MessageBox.Show(string.Format("Enter ServerName"));
                }
                else if (txtLogin.Text == "")
                {
                    MessageBox.Show(string.Format("Enter Login"));
                }
                else if (Convert.ToString(comAuthen.SelectedValue) == "")
                {
                    MessageBox.Show(string.Format("Choose Authentication"));
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show(string.Format("Enter Password"));
                }
                else if (rbttables.Checked == false && rbtstoreprocedure.Checked == false && rbtquery.Checked == false)
                {
                    MessageBox.Show(string.Format("Choose Export Type"));
                }
                else if (txtLocation.Text == "")
                {
                    MessageBox.Show(string.Format("Choose path to Export Files! Go to Export Path Settings For Setting Path!"));
                }
                else
                {
                    var connectionString = $"Data Source={txtServerName.Text}; UID={txtLogin.Text}; password={txtPassword.Text};";
                    var connectionStatus = IsServerConnected(connectionString, txtServerName.Text);
                    var folderPath = txtLocation.Text;

                    if (connectionStatus)
                    {

                        if (rbttables.Checked == true)
                        {
                            Generate generate = new Generate(connectionString, folderPath);
                            this.Hide();
                            generate.ShowDialog();

                        }

                        if (rbtstoreprocedure.Checked == true)
                        {
                            GenerateProcedure generate = new GenerateProcedure(connectionString, folderPath);
                            this.Hide();
                            generate.ShowDialog();

                        }

                        if (rbtquery.Checked == true)
                        {

                            GenerateQuery generate = new GenerateQuery(connectionString, folderPath);
                            this.Hide();
                            generate.ShowDialog();

                        }

                    }
                }
            }
            else if (Convert.ToString(comAuthen.SelectedValue) == "Windows Authentication")
            {
                if (txtServerName.Text == "")
                {
                    MessageBox.Show(string.Format("Enter ServerName"));
                }
                else if (Convert.ToString(comAuthen.SelectedValue) == "")
                {
                    MessageBox.Show(string.Format("Choose Authentication"));
                }
                else
                {
                    var connectionString = "";
                    connectionString = $"Data Source={txtServerName.Text}; Integrated Security=true";
                    var connectionStatus = IsServerConnected(connectionString, txtServerName.Text);
                }
            }
        }

        private static bool IsServerConnected(string connectionString, string serverName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    MessageBox.Show(string.Format($"Cannot Connect to {serverName}"));
                    return false;
                }
            }
        }

        private static bool IsMySqlServerConnected(string connectionString, string serverName)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    MessageBox.Show(string.Format($"Cannot Connect to {serverName}"));
                    return false;
                }
            }
        }

        private void BindDropdown()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Text");
                dt.Columns.Add("Value");
                dt.Rows.Add("SQL Server Authentication", "SQL Server Authentication");
                dt.Rows.Add("Windows Authentication", "Windows Authentication");
                comAuthen.ValueMember = "Text";
                comAuthen.DisplayMember = "Text";
                comAuthen.DataSource = dt;
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void comAuthen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(comAuthen.SelectedValue) == "SQL Server Authentication")
            {
                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
            }
            else
            {
                txtLogin.Enabled = false;
                txtPassword.Enabled = false;
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (tabAbout.SelectedTab == tabPage1)
            {
                if (txtServerName.Text == "")
                {
                    MessageBox.Show(string.Format("Enter ServerName"));
                }
                else if (txtLogin.Text == "")
                {
                    MessageBox.Show(string.Format("Enter Login"));
                }
                else if (Convert.ToString(comAuthen.SelectedValue) == "")
                {
                    MessageBox.Show(string.Format("Choose Authentication"));
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show(string.Format("Enter Password"));
                }

                var connectionString =
                    $"Data Source={txtServerName.Text}; UID={txtLogin.Text}; password={txtPassword.Text};";
                var connectionStatus = IsServerConnected(connectionString, txtServerName.Text);

                if (connectionStatus)
                {
                    MessageBox.Show(string.Format("Successfully Made the Microsoft SQL Server Connection"), @"Message");
                }
            }

            if (tabAbout.SelectedTab == tabPage2)
            {
                if (txt_my_HostAddress.Text == "")
                {
                    MessageBox.Show(string.Format("Enter HostAddress"));
                }
                else if (txt_my_Username.Text == "")
                {
                    MessageBox.Show(string.Format("Enter Username"));
                }
                else if (txt_my_Password.Text == "")
                {
                    MessageBox.Show(string.Format("Enter Password"));
                }

                var connectionString =
                    $"server={txt_my_HostAddress.Text}; UID={txt_my_Username.Text}; password={txt_my_Password.Text};";
                var connectionStatus = IsMySqlServerConnected(connectionString, txt_my_HostAddress.Text);

                if (connectionStatus)
                {
                    MessageBox.Show(string.Format("Successfully Made the MySQL Connection"), @"Message");
                }
            }
        }

        private void btnConnect_mySql_Click(object sender, EventArgs e)
        {
            if (txt_my_HostAddress.Text == "")
            {
                MessageBox.Show(string.Format("Enter HostAddress"));
            }
            else if (txt_my_Username.Text == "")
            {
                MessageBox.Show(string.Format("Enter Username"));
            }
            else if (txt_my_Password.Text == "")
            {
                MessageBox.Show(string.Format("Enter Password"));
            }
            else if (txtLocation.Text == "")
            {
                MessageBox.Show(string.Format("Choose path to Export Files! Go to Export Path Settings For Setting Path!"));
            }
            else
            {
                var connectionString =
                    $"server={txt_my_HostAddress.Text}; UID={txt_my_Username.Text}; password={txt_my_Password.Text};";
                var connectionStatus = IsMySqlServerConnected(connectionString, txt_my_HostAddress.Text);
                var folderPath = txtLocation.Text;

                if (connectionStatus)
                {
                    if (rbt_my_Tables.Checked == true)
                    {
                        MySqlGenerateTable generate = new MySqlGenerateTable(connectionString, folderPath);
                        this.Hide();
                        generate.ShowDialog();

                    }
                    if (rbt_my_storedprocedure.Checked == true)
                    {
                        MySQLGenerateProcedure generate = new MySQLGenerateProcedure(connectionString, folderPath);
                        this.Hide();
                        generate.ShowDialog();

                    }

                    if (rbt_my_Query.Checked == true)
                    {
                        MySQLGenerateQuery generate = new MySQLGenerateQuery(connectionString, folderPath);
                        this.Hide();
                        generate.ShowDialog();
                    }
                }
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void btnSetpath_Click(object sender, EventArgs e)
        {
            // Create a new instance of FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
            // A new folder button will display in FolderBrowserDialog.
            folderBrowserDlg.ShowNewFolderButton = true;
            //Show FolderBrowserDialog
            DialogResult dlgResult = folderBrowserDlg.ShowDialog();
            if (dlgResult.Equals(DialogResult.OK))
            {
                if (HasWriteAccessToFile(folderBrowserDlg.SelectedPath))
                {
                    //Show selected folder path in textbox1.
                    txtLocation.Text = folderBrowserDlg.SelectedPath;
                    //Browsing start from root folder.
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder;
                }
                else
                {
                    MessageBox.Show(string.Format("Folder You have Choosen Does have Access"));
                }
            }
        }

        private static bool HasWriteAccessToFile(string filePath)
        {
            bool isWriteAccess = false;
            try
            {
                AuthorizationRuleCollection collection =
                    Directory.GetAccessControl(filePath)
                        .GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (FileSystemAccessRule rule in collection)
                {
                    if (rule.AccessControlType == AccessControlType.Allow)
                    {
                        isWriteAccess = true;
                        break;
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                isWriteAccess = false;
            }
            catch (Exception)
            {
                isWriteAccess = false;
            }

            return isWriteAccess;
        }

    }
}

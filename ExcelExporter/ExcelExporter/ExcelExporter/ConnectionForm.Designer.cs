namespace ExcelExporter
{
    partial class ConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txt_my_HostAddress = new System.Windows.Forms.TextBox();
            this.txt_my_Username = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txt_my_Password = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbt_my_Query = new System.Windows.Forms.RadioButton();
            this.rbt_my_storedprocedure = new System.Windows.Forms.RadioButton();
            this.rbt_my_Tables = new System.Windows.Forms.RadioButton();
            this.btnConnect_mySql = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtquery = new System.Windows.Forms.RadioButton();
            this.rbtstoreprocedure = new System.Windows.Forms.RadioButton();
            this.rbttables = new System.Windows.Forms.RadioButton();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comAuthen = new System.Windows.Forms.ComboBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.tabAbout = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSetpath = new System.Windows.Forms.Button();
            this.labelOutput = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 50;
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 10;
            // 
            // txt_my_HostAddress
            // 
            this.txt_my_HostAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_my_HostAddress.Location = new System.Drawing.Point(140, 27);
            this.txt_my_HostAddress.MaxLength = 50;
            this.txt_my_HostAddress.Name = "txt_my_HostAddress";
            this.txt_my_HostAddress.Size = new System.Drawing.Size(218, 23);
            this.txt_my_HostAddress.TabIndex = 27;
            this.toolTip1.SetToolTip(this.txt_my_HostAddress, "Enter Host Address");
            // 
            // txt_my_Username
            // 
            this.txt_my_Username.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_my_Username.Location = new System.Drawing.Point(140, 56);
            this.txt_my_Username.MaxLength = 50;
            this.txt_my_Username.Name = "txt_my_Username";
            this.txt_my_Username.Size = new System.Drawing.Size(218, 23);
            this.txt_my_Username.TabIndex = 30;
            this.toolTip1.SetToolTip(this.txt_my_Username, "Enter Username");
            // 
            // txtServerName
            // 
            this.txtServerName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerName.Location = new System.Drawing.Point(136, 27);
            this.txtServerName.MaxLength = 50;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(218, 23);
            this.txtServerName.TabIndex = 17;
            this.toolTip1.SetToolTip(this.txtServerName, "Enter Server Name ( PCName or IP address )");
            // 
            // txtLogin
            // 
            this.txtLogin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogin.Location = new System.Drawing.Point(136, 106);
            this.txtLogin.MaxLength = 50;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(218, 23);
            this.txtLogin.TabIndex = 20;
            this.toolTip1.SetToolTip(this.txtLogin, "Enter your Sql Login");
            // 
            // txt_my_Password
            // 
            this.txt_my_Password.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_my_Password.Location = new System.Drawing.Point(140, 89);
            this.txt_my_Password.MaxLength = 50;
            this.txt_my_Password.Name = "txt_my_Password";
            this.txt_my_Password.PasswordChar = '*';
            this.txt_my_Password.ShortcutsEnabled = false;
            this.txt_my_Password.Size = new System.Drawing.Size(218, 23);
            this.txt_my_Password.TabIndex = 32;
            this.toolTip1.SetToolTip(this.txt_my_Password, "Enter Password");
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(136, 139);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(218, 23);
            this.txtPassword.TabIndex = 22;
            this.toolTip1.SetToolTip(this.txtPassword, "Enter Password");
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTestConnection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConnection.Location = new System.Drawing.Point(12, 402);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(112, 23);
            this.btnTestConnection.TabIndex = 24;
            this.btnTestConnection.Text = "TestConnection";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.LightGray;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.btnConnect_mySql);
            this.tabPage2.Controls.Add(this.txt_my_Password);
            this.tabPage2.Controls.Add(this.txt_my_Username);
            this.tabPage2.Controls.Add(this.txt_my_HostAddress);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(427, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MySQL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbt_my_Query);
            this.groupBox2.Controls.Add(this.rbt_my_storedprocedure);
            this.groupBox2.Controls.Add(this.rbt_my_Tables);
            this.groupBox2.Location = new System.Drawing.Point(26, 143);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 56);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Type";
            // 
            // rbt_my_Query
            // 
            this.rbt_my_Query.AutoSize = true;
            this.rbt_my_Query.Location = new System.Drawing.Point(276, 20);
            this.rbt_my_Query.Name = "rbt_my_Query";
            this.rbt_my_Query.Size = new System.Drawing.Size(56, 17);
            this.rbt_my_Query.TabIndex = 2;
            this.rbt_my_Query.TabStop = true;
            this.rbt_my_Query.Text = "Query";
            this.rbt_my_Query.UseVisualStyleBackColor = true;
            // 
            // rbt_my_storedprocedure
            // 
            this.rbt_my_storedprocedure.AutoSize = true;
            this.rbt_my_storedprocedure.Location = new System.Drawing.Point(130, 20);
            this.rbt_my_storedprocedure.Name = "rbt_my_storedprocedure";
            this.rbt_my_storedprocedure.Size = new System.Drawing.Size(120, 17);
            this.rbt_my_storedprocedure.TabIndex = 1;
            this.rbt_my_storedprocedure.TabStop = true;
            this.rbt_my_storedprocedure.Text = "Stored procedures";
            this.rbt_my_storedprocedure.UseVisualStyleBackColor = true;
            // 
            // rbt_my_Tables
            // 
            this.rbt_my_Tables.AutoSize = true;
            this.rbt_my_Tables.Location = new System.Drawing.Point(16, 20);
            this.rbt_my_Tables.Name = "rbt_my_Tables";
            this.rbt_my_Tables.Size = new System.Drawing.Size(57, 17);
            this.rbt_my_Tables.TabIndex = 0;
            this.rbt_my_Tables.TabStop = true;
            this.rbt_my_Tables.Text = "Tables";
            this.rbt_my_Tables.UseVisualStyleBackColor = true;
            // 
            // btnConnect_mySql
            // 
            this.btnConnect_mySql.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnConnect_mySql.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect_mySql.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect_mySql.Location = new System.Drawing.Point(26, 228);
            this.btnConnect_mySql.Name = "btnConnect_mySql";
            this.btnConnect_mySql.Size = new System.Drawing.Size(112, 23);
            this.btnConnect_mySql.TabIndex = 33;
            this.btnConnect_mySql.Text = "Connect";
            this.btnConnect_mySql.UseVisualStyleBackColor = false;
            this.btnConnect_mySql.Click += new System.EventHandler(this.btnConnect_mySql_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 35;
            this.label3.Text = "Username :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "Password :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 17);
            this.label6.TabIndex = 29;
            this.label6.Text = "Host address:";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnConnect);
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.txtLogin);
            this.tabPage1.Controls.Add(this.txtServerName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.comAuthen);
            this.tabPage1.Controls.Add(this.lblPassword);
            this.tabPage1.Controls.Add(this.lblServer);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(427, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Microsoft SQL";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtquery);
            this.groupBox1.Controls.Add(this.rbtstoreprocedure);
            this.groupBox1.Controls.Add(this.rbttables);
            this.groupBox1.Location = new System.Drawing.Point(22, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 56);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Type";
            // 
            // rbtquery
            // 
            this.rbtquery.AutoSize = true;
            this.rbtquery.Location = new System.Drawing.Point(276, 20);
            this.rbtquery.Name = "rbtquery";
            this.rbtquery.Size = new System.Drawing.Size(56, 17);
            this.rbtquery.TabIndex = 2;
            this.rbtquery.TabStop = true;
            this.rbtquery.Text = "Query";
            this.rbtquery.UseVisualStyleBackColor = true;
            // 
            // rbtstoreprocedure
            // 
            this.rbtstoreprocedure.AutoSize = true;
            this.rbtstoreprocedure.Location = new System.Drawing.Point(130, 20);
            this.rbtstoreprocedure.Name = "rbtstoreprocedure";
            this.rbtstoreprocedure.Size = new System.Drawing.Size(120, 17);
            this.rbtstoreprocedure.TabIndex = 1;
            this.rbtstoreprocedure.TabStop = true;
            this.rbtstoreprocedure.Text = "Stored procedures";
            this.rbtstoreprocedure.UseVisualStyleBackColor = true;
            // 
            // rbttables
            // 
            this.rbttables.AutoSize = true;
            this.rbttables.Location = new System.Drawing.Point(16, 20);
            this.rbttables.Name = "rbttables";
            this.rbttables.Size = new System.Drawing.Size(57, 17);
            this.rbttables.TabIndex = 0;
            this.rbttables.TabStop = true;
            this.rbttables.Text = "Tables";
            this.rbttables.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(20, 267);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(112, 23);
            this.btnConnect.TabIndex = 23;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "Login :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Authentication :";
            // 
            // comAuthen
            // 
            this.comAuthen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comAuthen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comAuthen.FormattingEnabled = true;
            this.comAuthen.Location = new System.Drawing.Point(136, 65);
            this.comAuthen.Name = "comAuthen";
            this.comAuthen.Size = new System.Drawing.Size(218, 23);
            this.comAuthen.TabIndex = 18;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(19, 139);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(71, 17);
            this.lblPassword.TabIndex = 21;
            this.lblPassword.Text = "Password :";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(19, 30);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(88, 17);
            this.lblServer.TabIndex = 19;
            this.lblServer.Text = "Server name :";
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.tabPage1);
            this.tabAbout.Controls.Add(this.tabPage2);
            this.tabAbout.Controls.Add(this.tabPage3);
            this.tabAbout.Controls.Add(this.tabPage4);
            this.tabAbout.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAbout.Location = new System.Drawing.Point(12, 24);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.SelectedIndex = 0;
            this.tabAbout.Size = new System.Drawing.Size(435, 356);
            this.tabAbout.TabIndex = 17;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.LightGray;
            this.tabPage3.Controls.Add(this.btnSetpath);
            this.tabPage3.Controls.Add(this.labelOutput);
            this.tabPage3.Controls.Add(this.txtLocation);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(427, 330);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Export Path Settings";
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // btnSetpath
            // 
            this.btnSetpath.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSetpath.Location = new System.Drawing.Point(9, 85);
            this.btnSetpath.Name = "btnSetpath";
            this.btnSetpath.Size = new System.Drawing.Size(96, 24);
            this.btnSetpath.TabIndex = 21;
            this.btnSetpath.Text = "Choose Folder";
            this.btnSetpath.UseVisualStyleBackColor = false;
            this.btnSetpath.Click += new System.EventHandler(this.btnSetpath_Click);
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.labelOutput.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutput.Location = new System.Drawing.Point(6, 14);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(80, 17);
            this.labelOutput.TabIndex = 20;
            this.labelOutput.Text = "Output Path:";
            // 
            // txtLocation
            // 
            this.txtLocation.Enabled = false;
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.ForeColor = System.Drawing.Color.Red;
            this.txtLocation.Location = new System.Drawing.Point(9, 44);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(412, 22);
            this.txtLocation.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.LightGray;
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(427, 330);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "AboutUs";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(19, 132);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(325, 15);
            this.label14.TabIndex = 8;
            this.label14.Text = "Connect me in various ways just google \"Saineshwar Bageri\"";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(126, 104);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 13);
            this.label13.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(19, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Connect me:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(126, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Saineshwar Bageri";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Author:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(126, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Beta Version.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Version:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(126, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Complete (Open Source).";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Edition:";
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(465, 437);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.tabAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Connection Properties";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbt_my_Query;
        private System.Windows.Forms.RadioButton rbt_my_storedprocedure;
        private System.Windows.Forms.RadioButton rbt_my_Tables;
        private System.Windows.Forms.Button btnConnect_mySql;
        private System.Windows.Forms.TextBox txt_my_Password;
        private System.Windows.Forms.TextBox txt_my_Username;
        private System.Windows.Forms.TextBox txt_my_HostAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtquery;
        private System.Windows.Forms.RadioButton rbtstoreprocedure;
        private System.Windows.Forms.RadioButton rbttables;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comAuthen;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TabControl tabAbout;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSetpath;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.FolderBrowserDialog OpenFileDialog;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
    }
}
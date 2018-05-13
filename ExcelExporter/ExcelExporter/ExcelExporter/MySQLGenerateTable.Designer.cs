namespace ExcelExporter
{
    partial class MySqlGenerateTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MySqlGenerateTable));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnshowtbl = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listboxTables = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listDatabase = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TspProcBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolText = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(331, 384);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(192, 41);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Generate Excel";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnshowtbl
            // 
            this.btnshowtbl.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnshowtbl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnshowtbl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnshowtbl.Location = new System.Drawing.Point(346, 170);
            this.btnshowtbl.Name = "btnshowtbl";
            this.btnshowtbl.Size = new System.Drawing.Size(163, 41);
            this.btnshowtbl.TabIndex = 7;
            this.btnshowtbl.Text = "Show Tables";
            this.btnshowtbl.UseVisualStyleBackColor = false;
            this.btnshowtbl.Click += new System.EventHandler(this.btnshowtbl_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listboxTables);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(558, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 334);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // listboxTables
            // 
            this.listboxTables.BackColor = System.Drawing.Color.White;
            this.listboxTables.FormattingEnabled = true;
            this.listboxTables.ItemHeight = 15;
            this.listboxTables.Location = new System.Drawing.Point(19, 22);
            this.listboxTables.Name = "listboxTables";
            this.listboxTables.Size = new System.Drawing.Size(246, 289);
            this.listboxTables.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listDatabase);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(19, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 334);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Databases";
            // 
            // listDatabase
            // 
            this.listDatabase.BackColor = System.Drawing.Color.White;
            this.listDatabase.FormattingEnabled = true;
            this.listDatabase.ItemHeight = 15;
            this.listDatabase.Location = new System.Drawing.Point(20, 29);
            this.listDatabase.Name = "listDatabase";
            this.listDatabase.Size = new System.Drawing.Size(246, 289);
            this.listDatabase.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TspProcBar,
            this.toolStripStatusLabel1,
            this.ToolText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(864, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TspProcBar
            // 
            this.TspProcBar.BackColor = System.Drawing.SystemColors.Control;
            this.TspProcBar.ForeColor = System.Drawing.Color.LimeGreen;
            this.TspProcBar.Name = "TspProcBar";
            this.TspProcBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.ForestGreen;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // ToolText
            // 
            this.ToolText.Name = "ToolText";
            this.ToolText.Size = new System.Drawing.Size(0, 17);
            // 
            // MySqlGenerateTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 454);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnshowtbl);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MySqlGenerateTable";
            this.Text = "MySQLGenerateTable";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MySQLGenerateTable_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnshowtbl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listboxTables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listDatabase;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar TspProcBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel ToolText;
    }
}
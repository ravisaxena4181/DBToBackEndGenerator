namespace WinDBToBackEndGenerator
{
    partial class frmMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtbScript = new System.Windows.Forms.RichTextBox();
            this.btnGenerateScripts = new System.Windows.Forms.Button();
            this.txtClassPrefix = new System.Windows.Forms.TextBox();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.chkSingleDBScript = new System.Windows.Forms.CheckBox();
            this.chkDataAccessObjects = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkClassEntities = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSqlProcedures = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstTables = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.cboDBTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 140);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1598, 475);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtbScript);
            this.tabPage1.Controls.Add(this.btnGenerateScripts);
            this.tabPage1.Controls.Add(this.txtClassPrefix);
            this.tabPage1.Controls.Add(this.txtNamespace);
            this.tabPage1.Controls.Add(this.txtPath);
            this.tabPage1.Controls.Add(this.chkSingleDBScript);
            this.tabPage1.Controls.Add(this.chkDataAccessObjects);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.chkClassEntities);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.chkSqlProcedures);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lstTables);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1590, 446);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtbScript
            // 
            this.rtbScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbScript.Location = new System.Drawing.Point(466, 181);
            this.rtbScript.Name = "rtbScript";
            this.rtbScript.Size = new System.Drawing.Size(1118, 259);
            this.rtbScript.TabIndex = 7;
            this.rtbScript.Text = "";
            // 
            // btnGenerateScripts
            // 
            this.btnGenerateScripts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateScripts.Location = new System.Drawing.Point(1217, 30);
            this.btnGenerateScripts.Name = "btnGenerateScripts";
            this.btnGenerateScripts.Size = new System.Drawing.Size(152, 32);
            this.btnGenerateScripts.TabIndex = 6;
            this.btnGenerateScripts.Text = "Generate Scripts";
            this.btnGenerateScripts.UseVisualStyleBackColor = true;
            this.btnGenerateScripts.Click += new System.EventHandler(this.btnGenerateScripts_Click);
            // 
            // txtClassPrefix
            // 
            this.txtClassPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClassPrefix.Location = new System.Drawing.Point(1114, 112);
            this.txtClassPrefix.Name = "txtClassPrefix";
            this.txtClassPrefix.Size = new System.Drawing.Size(255, 26);
            this.txtClassPrefix.TabIndex = 5;
            // 
            // txtNamespace
            // 
            this.txtNamespace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNamespace.Location = new System.Drawing.Point(664, 112);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(317, 26);
            this.txtNamespace.TabIndex = 5;
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(563, 79);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(806, 26);
            this.txtPath.TabIndex = 5;
            // 
            // chkSingleDBScript
            // 
            this.chkSingleDBScript.AutoSize = true;
            this.chkSingleDBScript.Location = new System.Drawing.Point(518, 144);
            this.chkSingleDBScript.Name = "chkSingleDBScript";
            this.chkSingleDBScript.Size = new System.Drawing.Size(234, 21);
            this.chkSingleDBScript.TabIndex = 4;
            this.chkSingleDBScript.Text = "Create Single File for SQL Script";
            this.chkSingleDBScript.UseVisualStyleBackColor = true;
            // 
            // chkDataAccessObjects
            // 
            this.chkDataAccessObjects.AutoSize = true;
            this.chkDataAccessObjects.Location = new System.Drawing.Point(881, 37);
            this.chkDataAccessObjects.Name = "chkDataAccessObjects";
            this.chkDataAccessObjects.Size = new System.Drawing.Size(183, 21);
            this.chkDataAccessObjects.TabIndex = 4;
            this.chkDataAccessObjects.Text = "Data Access Objects-C#";
            this.chkDataAccessObjects.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(987, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Class Prefix";
            // 
            // chkClassEntities
            // 
            this.chkClassEntities.AutoSize = true;
            this.chkClassEntities.Location = new System.Drawing.Point(695, 37);
            this.chkClassEntities.Name = "chkClassEntities";
            this.chkClassEntities.Size = new System.Drawing.Size(151, 21);
            this.chkClassEntities.TabIndex = 3;
            this.chkClassEntities.Text = "Models/Entities- C#";
            this.chkClassEntities.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkClassEntities.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(514, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Class Namespace ";
            // 
            // chkSqlProcedures
            // 
            this.chkSqlProcedures.AutoSize = true;
            this.chkSqlProcedures.Location = new System.Drawing.Point(518, 37);
            this.chkSqlProcedures.Name = "chkSqlProcedures";
            this.chkSqlProcedures.Size = new System.Drawing.Size(144, 21);
            this.chkSqlProcedures.TabIndex = 2;
            this.chkSqlProcedures.Text = "SQL Proc (CRUD)";
            this.chkSqlProcedures.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(514, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tables";
            // 
            // lstTables
            // 
            this.lstTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstTables.FormattingEnabled = true;
            this.lstTables.ItemHeight = 16;
            this.lstTables.Location = new System.Drawing.Point(16, 37);
            this.lstTables.Name = "lstTables";
            this.lstTables.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstTables.Size = new System.Drawing.Size(444, 404);
            this.lstTables.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1590, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtConnectionString);
            this.groupBox1.Controls.Add(this.cboDBTemplate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1598, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL DB Connection";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(1440, 60);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(152, 32);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionString.Location = new System.Drawing.Point(385, 28);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(1207, 26);
            this.txtConnectionString.TabIndex = 2;
            // 
            // cboDBTemplate
            // 
            this.cboDBTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDBTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDBTemplate.FormattingEnabled = true;
            this.cboDBTemplate.Location = new System.Drawing.Point(192, 28);
            this.cboDBTemplate.Name = "cboDBTemplate";
            this.cboDBTemplate.Size = new System.Drawing.Size(187, 28);
            this.cboDBTemplate.TabIndex = 1;
            this.cboDBTemplate.SelectedIndexChanged += new System.EventHandler(this.cboDBTemplate_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database Connection";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1622, 628);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.Text = ".Net Object Generator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.ComboBox cboDBTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstTables;
        private System.Windows.Forms.Button btnGenerateScripts;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.CheckBox chkDataAccessObjects;
        private System.Windows.Forms.CheckBox chkClassEntities;
        private System.Windows.Forms.CheckBox chkSqlProcedures;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbScript;
        private System.Windows.Forms.TextBox txtClassPrefix;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSingleDBScript;
    }
}


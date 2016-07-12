namespace ORM.UI
{
    partial class ORMMain
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
            this.ofdClasses = new System.Windows.Forms.OpenFileDialog();
            this.fbTarget = new System.Windows.Forms.FolderBrowserDialog();
            this.fbProject = new System.Windows.Forms.FolderBrowserDialog();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.rbTextFile = new System.Windows.Forms.RadioButton();
            this.rbSQLDirect = new System.Windows.Forms.RadioButton();
            this.grpSQL = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblTables = new System.Windows.Forms.Label();
            this.lblDatabases = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblServers = new System.Windows.Forms.Label();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.btnGetSQLStucture = new System.Windows.Forms.Button();
            this.grpTextFile = new System.Windows.Forms.GroupBox();
            this.btnSourceFile = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtHomeFolder = new System.Windows.Forms.TextBox();
            this.txtCompanyNamespace = new System.Windows.Forms.TextBox();
            this.btnProjectFolder = new System.Windows.Forms.Button();
            this.txtClassname = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.chkSort = new System.Windows.Forms.CheckBox();
            this.chkPrefix = new System.Windows.Forms.CheckBox();
            this.chkBE = new System.Windows.Forms.CheckBox();
            this.chkDAL = new System.Windows.Forms.CheckBox();
            this.chkBLL = new System.Windows.Forms.CheckBox();
            this.chkGUI = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.xTabBE = new System.Windows.Forms.TabPage();
            this.grpBE = new System.Windows.Forms.GroupBox();
            this.btnBEFile = new System.Windows.Forms.Button();
            this.txtPrefixBE = new System.Windows.Forms.TextBox();
            this.txtBaseClass = new System.Windows.Forms.TextBox();
            this.txtBEFile = new System.Windows.Forms.TextBox();
            this.txtConstants = new System.Windows.Forms.TextBox();
            this.txtBENamespace = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xTabBLL = new System.Windows.Forms.TabPage();
            this.grpBL = new System.Windows.Forms.GroupBox();
            this.btnBLLFile = new System.Windows.Forms.Button();
            this.txtPrefixBLL = new System.Windows.Forms.TextBox();
            this.txtBLLFile = new System.Windows.Forms.TextBox();
            this.txtBLLNamespace = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.xTabDAL = new System.Windows.Forms.TabPage();
            this.grpDAL = new System.Windows.Forms.GroupBox();
            this.chkInheritDAO = new System.Windows.Forms.CheckBox();
            this.btnDALFile = new System.Windows.Forms.Button();
            this.txtDAO = new System.Windows.Forms.TextBox();
            this.txtPrefixDAL = new System.Windows.Forms.TextBox();
            this.txtDALFile = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDALNamespace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.xTabUI = new System.Windows.Forms.TabPage();
            this.chkDevExpress = new System.Windows.Forms.CheckBox();
            this.chkFilterMgr = new System.Windows.Forms.CheckBox();
            this.grpGUI = new System.Windows.Forms.GroupBox();
            this.chkLayout = new System.Windows.Forms.CheckBox();
            this.chkInheritGUI = new System.Windows.Forms.CheckBox();
            this.btnGUIFile = new System.Windows.Forms.Button();
            this.txtGUI = new System.Windows.Forms.TextBox();
            this.txtParentName = new System.Windows.Forms.TextBox();
            this.txtPrefixGUI = new System.Windows.Forms.TextBox();
            this.txtGUIFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtGUINamespace = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.xTabWeb = new System.Windows.Forms.TabPage();
            this.grpAspx = new System.Windows.Forms.GroupBox();
            this.btnAspxFile = new System.Windows.Forms.Button();
            this.txtPrefixAspx = new System.Windows.Forms.TextBox();
            this.txtAspxFile = new System.Windows.Forms.TextBox();
            this.txtNamespaceAspx = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkAspx = new System.Windows.Forms.CheckBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtTextBox = new System.Windows.Forms.TextBox();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.txtButton = new System.Windows.Forms.TextBox();
            this.txtGrid = new System.Windows.Forms.TextBox();
            this.txtLinkButton = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.grpSource.SuspendLayout();
            this.grpSQL.SuspendLayout();
            this.grpTextFile.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.xTabBE.SuspendLayout();
            this.grpBE.SuspendLayout();
            this.xTabBLL.SuspendLayout();
            this.grpBL.SuspendLayout();
            this.xTabDAL.SuspendLayout();
            this.grpDAL.SuspendLayout();
            this.xTabUI.SuspendLayout();
            this.grpGUI.SuspendLayout();
            this.xTabWeb.SuspendLayout();
            this.grpAspx.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdClasses
            // 
            this.ofdClasses.DefaultExt = "cs";
            this.ofdClasses.InitialDirectory = "D:\\Dev 2010\\WORKman nTier\\WORKman XPODAL\\";
            this.ofdClasses.RestoreDirectory = true;
            this.ofdClasses.Title = "Select BE Classes";
            // 
            // fbTarget
            // 
            this.fbTarget.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fbTarget.SelectedPath = "D:\\Dev 2010";
            // 
            // fbProject
            // 
            this.fbProject.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fbProject.SelectedPath = "D:\\Dev 2010";
            // 
            // grpSource
            // 
            this.grpSource.Controls.Add(this.rbTextFile);
            this.grpSource.Controls.Add(this.rbSQLDirect);
            this.grpSource.Location = new System.Drawing.Point(111, 1);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(334, 39);
            this.grpSource.TabIndex = 28;
            this.grpSource.TabStop = false;
            // 
            // rbTextFile
            // 
            this.rbTextFile.AutoSize = true;
            this.rbTextFile.Location = new System.Drawing.Point(187, 14);
            this.rbTextFile.Name = "rbTextFile";
            this.rbTextFile.Size = new System.Drawing.Size(129, 17);
            this.rbTextFile.TabIndex = 1;
            this.rbTextFile.Text = "From text definition file";
            this.rbTextFile.UseVisualStyleBackColor = true;
            // 
            // rbSQLDirect
            // 
            this.rbSQLDirect.AutoSize = true;
            this.rbSQLDirect.Checked = true;
            this.rbSQLDirect.Location = new System.Drawing.Point(18, 13);
            this.rbSQLDirect.Name = "rbSQLDirect";
            this.rbSQLDirect.Size = new System.Drawing.Size(100, 17);
            this.rbSQLDirect.TabIndex = 0;
            this.rbSQLDirect.TabStop = true;
            this.rbSQLDirect.Text = "Direct from SQL";
            this.rbSQLDirect.UseVisualStyleBackColor = true;
            this.rbSQLDirect.CheckedChanged += new System.EventHandler(this.rbSQLDirect_CheckedChanged);
            // 
            // grpSQL
            // 
            this.grpSQL.Controls.Add(this.txtPassword);
            this.grpSQL.Controls.Add(this.txtUsername);
            this.grpSQL.Controls.Add(this.lblTables);
            this.grpSQL.Controls.Add(this.lblDatabases);
            this.grpSQL.Controls.Add(this.label16);
            this.grpSQL.Controls.Add(this.label15);
            this.grpSQL.Controls.Add(this.lblServers);
            this.grpSQL.Controls.Add(this.cbTables);
            this.grpSQL.Controls.Add(this.cbDatabases);
            this.grpSQL.Controls.Add(this.cbServers);
            this.grpSQL.Controls.Add(this.btnGetSQLStucture);
            this.grpSQL.Location = new System.Drawing.Point(18, 46);
            this.grpSQL.Name = "grpSQL";
            this.grpSQL.Size = new System.Drawing.Size(521, 147);
            this.grpSQL.TabIndex = 0;
            this.grpSQL.TabStop = false;
            this.grpSQL.Text = "SQL";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(385, 90);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(110, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "gentil";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(385, 60);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(110, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "sa";
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(25, 118);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(45, 13);
            this.lblTables.TabIndex = 37;
            this.lblTables.Text = "Tables: ";
            // 
            // lblDatabases
            // 
            this.lblDatabases.AutoSize = true;
            this.lblDatabases.Location = new System.Drawing.Point(6, 88);
            this.lblDatabases.Name = "lblDatabases";
            this.lblDatabases.Size = new System.Drawing.Size(64, 13);
            this.lblDatabases.TabIndex = 36;
            this.lblDatabases.Text = "Databases: ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(320, 93);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "Password: ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(313, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "User Name: ";
            // 
            // lblServers
            // 
            this.lblServers.AutoSize = true;
            this.lblServers.Location = new System.Drawing.Point(21, 58);
            this.lblServers.Name = "lblServers";
            this.lblServers.Size = new System.Drawing.Size(49, 13);
            this.lblServers.TabIndex = 35;
            this.lblServers.Text = "Servers: ";
            // 
            // cbTables
            // 
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(74, 114);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(226, 21);
            this.cbTables.TabIndex = 5;
            this.cbTables.SelectedIndexChanged += new System.EventHandler(this.cbTables_SelectedIndexChanged);
            // 
            // cbDatabases
            // 
            this.cbDatabases.FormattingEnabled = true;
            this.cbDatabases.Location = new System.Drawing.Point(74, 85);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(226, 21);
            this.cbDatabases.TabIndex = 4;
            this.cbDatabases.SelectedIndexChanged += new System.EventHandler(this.cbDatabases_SelectedIndexChanged);
            // 
            // cbServers
            // 
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(74, 55);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(226, 21);
            this.cbServers.TabIndex = 3;
            this.cbServers.Leave += new System.EventHandler(this.cbServers_Leave);
            // 
            // btnGetSQLStucture
            // 
            this.btnGetSQLStucture.Location = new System.Drawing.Point(18, 19);
            this.btnGetSQLStucture.Name = "btnGetSQLStucture";
            this.btnGetSQLStucture.Size = new System.Drawing.Size(485, 23);
            this.btnGetSQLStucture.TabIndex = 0;
            this.btnGetSQLStucture.Text = "Get SQL Information";
            this.btnGetSQLStucture.UseVisualStyleBackColor = true;
            this.btnGetSQLStucture.Click += new System.EventHandler(this.btnGetSQLStucture_Click);
            // 
            // grpTextFile
            // 
            this.grpTextFile.Controls.Add(this.btnSourceFile);
            this.grpTextFile.Controls.Add(this.label23);
            this.grpTextFile.Controls.Add(this.txtSourceFile);
            this.grpTextFile.Location = new System.Drawing.Point(18, 197);
            this.grpTextFile.Name = "grpTextFile";
            this.grpTextFile.Size = new System.Drawing.Size(521, 57);
            this.grpTextFile.TabIndex = 1;
            this.grpTextFile.TabStop = false;
            this.grpTextFile.Text = "Definition File";
            // 
            // btnSourceFile
            // 
            this.btnSourceFile.Location = new System.Drawing.Point(489, 22);
            this.btnSourceFile.Name = "btnSourceFile";
            this.btnSourceFile.Size = new System.Drawing.Size(24, 21);
            this.btnSourceFile.TabIndex = 39;
            this.btnSourceFile.Text = "...";
            this.btnSourceFile.UseVisualStyleBackColor = true;
            this.btnSourceFile.Click += new System.EventHandler(this.btnSourceFile_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 27);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(81, 13);
            this.label23.TabIndex = 38;
            this.label23.Text = "Sources File/s: ";
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Location = new System.Drawing.Point(93, 23);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(391, 20);
            this.txtSourceFile.TabIndex = 0;
            this.txtSourceFile.TextChanged += new System.EventHandler(this.txtSourceFile_TextChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(68, 259);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(109, 13);
            this.label24.TabIndex = 39;
            this.label24.Text = "Project Home Folder: ";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(108, 311);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(69, 13);
            this.label25.TabIndex = 39;
            this.label25.Text = "Class Name: ";
            // 
            // txtHomeFolder
            // 
            this.txtHomeFolder.Location = new System.Drawing.Point(183, 256);
            this.txtHomeFolder.Name = "txtHomeFolder";
            this.txtHomeFolder.Size = new System.Drawing.Size(307, 20);
            this.txtHomeFolder.TabIndex = 2;
            this.txtHomeFolder.Leave += new System.EventHandler(this.txtHomeFolder_Leave);
            // 
            // txtCompanyNamespace
            // 
            this.txtCompanyNamespace.Location = new System.Drawing.Point(183, 282);
            this.txtCompanyNamespace.Name = "txtCompanyNamespace";
            this.txtCompanyNamespace.Size = new System.Drawing.Size(307, 20);
            this.txtCompanyNamespace.TabIndex = 3;
            this.txtCompanyNamespace.Leave += new System.EventHandler(this.txtCompanyNamespace_Leave);
            // 
            // btnProjectFolder
            // 
            this.btnProjectFolder.Location = new System.Drawing.Point(494, 256);
            this.btnProjectFolder.Name = "btnProjectFolder";
            this.btnProjectFolder.Size = new System.Drawing.Size(24, 21);
            this.btnProjectFolder.TabIndex = 4;
            this.btnProjectFolder.Text = "...";
            this.btnProjectFolder.UseVisualStyleBackColor = true;
            this.btnProjectFolder.Click += new System.EventHandler(this.btnProjectFolder_Click);
            // 
            // txtClassname
            // 
            this.txtClassname.Location = new System.Drawing.Point(183, 307);
            this.txtClassname.Name = "txtClassname";
            this.txtClassname.Size = new System.Drawing.Size(240, 20);
            this.txtClassname.TabIndex = 5;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(22, 285);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(155, 13);
            this.label26.TabIndex = 39;
            this.label26.Text = "Company/Project Namespace: ";
            // 
            // chkSort
            // 
            this.chkSort.AutoSize = true;
            this.chkSort.Checked = true;
            this.chkSort.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSort.Location = new System.Drawing.Point(171, 333);
            this.chkSort.Name = "chkSort";
            this.chkSort.Size = new System.Drawing.Size(215, 17);
            this.chkSort.TabIndex = 6;
            this.chkSort.Text = "Create Filter/Sort methods for every field";
            this.chkSort.UseVisualStyleBackColor = true;
            // 
            // chkPrefix
            // 
            this.chkPrefix.AutoSize = true;
            this.chkPrefix.Checked = true;
            this.chkPrefix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrefix.Location = new System.Drawing.Point(171, 356);
            this.chkPrefix.Name = "chkPrefix";
            this.chkPrefix.Size = new System.Drawing.Size(130, 17);
            this.chkPrefix.TabIndex = 7;
            this.chkPrefix.Text = "Prefix Class Filenames";
            this.chkPrefix.UseVisualStyleBackColor = true;
            this.chkPrefix.CheckStateChanged += new System.EventHandler(this.chkPrefix_CheckedChanged);
            // 
            // chkBE
            // 
            this.chkBE.AutoSize = true;
            this.chkBE.Checked = true;
            this.chkBE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBE.Location = new System.Drawing.Point(34, 387);
            this.chkBE.Name = "chkBE";
            this.chkBE.Size = new System.Drawing.Size(126, 17);
            this.chkBE.TabIndex = 8;
            this.chkBE.Text = "Generate BE Classes";
            this.chkBE.UseVisualStyleBackColor = true;
            this.chkBE.CheckStateChanged += new System.EventHandler(this.chkBE_CheckedChanged);
            // 
            // chkDAL
            // 
            this.chkDAL.AutoSize = true;
            this.chkDAL.Checked = true;
            this.chkDAL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDAL.Location = new System.Drawing.Point(34, 412);
            this.chkDAL.Name = "chkDAL";
            this.chkDAL.Size = new System.Drawing.Size(133, 17);
            this.chkDAL.TabIndex = 10;
            this.chkDAL.Text = "Generate DAL Classes";
            this.chkDAL.UseVisualStyleBackColor = true;
            this.chkDAL.CheckStateChanged += new System.EventHandler(this.chkDAL_CheckedChanged);
            // 
            // chkBLL
            // 
            this.chkBLL.AutoSize = true;
            this.chkBLL.Checked = true;
            this.chkBLL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBLL.Location = new System.Drawing.Point(209, 387);
            this.chkBLL.Name = "chkBLL";
            this.chkBLL.Size = new System.Drawing.Size(131, 17);
            this.chkBLL.TabIndex = 9;
            this.chkBLL.Text = "Generate BLL Classes";
            this.chkBLL.UseVisualStyleBackColor = true;
            this.chkBLL.CheckStateChanged += new System.EventHandler(this.chkBLL_CheckedChanged);
            // 
            // chkGUI
            // 
            this.chkGUI.AutoSize = true;
            this.chkGUI.Checked = true;
            this.chkGUI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGUI.Location = new System.Drawing.Point(209, 412);
            this.chkGUI.Name = "chkGUI";
            this.chkGUI.Size = new System.Drawing.Size(131, 17);
            this.chkGUI.TabIndex = 11;
            this.chkGUI.Text = "Generate GUI Classes";
            this.chkGUI.UseVisualStyleBackColor = true;
            this.chkGUI.CheckStateChanged += new System.EventHandler(this.chkGUI_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.xTabBE);
            this.tabControl1.Controls.Add(this.xTabBLL);
            this.tabControl1.Controls.Add(this.xTabDAL);
            this.tabControl1.Controls.Add(this.xTabUI);
            this.tabControl1.Controls.Add(this.xTabWeb);
            this.tabControl1.Location = new System.Drawing.Point(14, 443);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(529, 180);
            this.tabControl1.TabIndex = 12;
            // 
            // xTabBE
            // 
            this.xTabBE.Controls.Add(this.grpBE);
            this.xTabBE.Location = new System.Drawing.Point(4, 22);
            this.xTabBE.Name = "xTabBE";
            this.xTabBE.Padding = new System.Windows.Forms.Padding(3);
            this.xTabBE.Size = new System.Drawing.Size(521, 154);
            this.xTabBE.TabIndex = 0;
            this.xTabBE.Text = "BE Classes";
            this.xTabBE.UseVisualStyleBackColor = true;
            // 
            // grpBE
            // 
            this.grpBE.BackColor = System.Drawing.Color.PowderBlue;
            this.grpBE.Controls.Add(this.btnBEFile);
            this.grpBE.Controls.Add(this.txtPrefixBE);
            this.grpBE.Controls.Add(this.txtBaseClass);
            this.grpBE.Controls.Add(this.txtBEFile);
            this.grpBE.Controls.Add(this.txtConstants);
            this.grpBE.Controls.Add(this.txtBENamespace);
            this.grpBE.Controls.Add(this.label14);
            this.grpBE.Controls.Add(this.label6);
            this.grpBE.Controls.Add(this.label5);
            this.grpBE.Controls.Add(this.label2);
            this.grpBE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBE.Location = new System.Drawing.Point(3, 3);
            this.grpBE.Name = "grpBE";
            this.grpBE.Size = new System.Drawing.Size(515, 148);
            this.grpBE.TabIndex = 45;
            this.grpBE.TabStop = false;
            this.grpBE.Text = "Business Entities";
            // 
            // btnBEFile
            // 
            this.btnBEFile.Location = new System.Drawing.Point(485, 86);
            this.btnBEFile.Name = "btnBEFile";
            this.btnBEFile.Size = new System.Drawing.Size(24, 21);
            this.btnBEFile.TabIndex = 4;
            this.btnBEFile.Text = "...";
            this.btnBEFile.UseVisualStyleBackColor = true;
            this.btnBEFile.Click += new System.EventHandler(this.btnBEFile_Click);
            // 
            // txtPrefixBE
            // 
            this.txtPrefixBE.Location = new System.Drawing.Point(375, 25);
            this.txtPrefixBE.Name = "txtPrefixBE";
            this.txtPrefixBE.Size = new System.Drawing.Size(104, 20);
            this.txtPrefixBE.TabIndex = 1;
            // 
            // txtBaseClass
            // 
            this.txtBaseClass.Location = new System.Drawing.Point(104, 117);
            this.txtBaseClass.Name = "txtBaseClass";
            this.txtBaseClass.Size = new System.Drawing.Size(265, 20);
            this.txtBaseClass.TabIndex = 5;
            this.txtBaseClass.Visible = false;
            // 
            // txtBEFile
            // 
            this.txtBEFile.Location = new System.Drawing.Point(104, 87);
            this.txtBEFile.Name = "txtBEFile";
            this.txtBEFile.Size = new System.Drawing.Size(375, 20);
            this.txtBEFile.TabIndex = 3;
            // 
            // txtConstants
            // 
            this.txtConstants.Location = new System.Drawing.Point(104, 58);
            this.txtConstants.Name = "txtConstants";
            this.txtConstants.Size = new System.Drawing.Size(372, 20);
            this.txtConstants.TabIndex = 2;
            this.txtConstants.Text = "\\Constants\\";
            // 
            // txtBENamespace
            // 
            this.txtBENamespace.Location = new System.Drawing.Point(104, 25);
            this.txtBENamespace.Name = "txtBENamespace";
            this.txtBENamespace.Size = new System.Drawing.Size(262, 20);
            this.txtBENamespace.TabIndex = 0;
            this.txtBENamespace.Leave += new System.EventHandler(this.txtBENamespace_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 121);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "Base Class Name: ";
            this.label14.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "BE Location: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Constants: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "BE Namespace: ";
            // 
            // xTabBLL
            // 
            this.xTabBLL.Controls.Add(this.grpBL);
            this.xTabBLL.Location = new System.Drawing.Point(4, 22);
            this.xTabBLL.Name = "xTabBLL";
            this.xTabBLL.Padding = new System.Windows.Forms.Padding(3);
            this.xTabBLL.Size = new System.Drawing.Size(521, 154);
            this.xTabBLL.TabIndex = 1;
            this.xTabBLL.Text = "BLL Classes";
            this.xTabBLL.UseVisualStyleBackColor = true;
            // 
            // grpBL
            // 
            this.grpBL.BackColor = System.Drawing.Color.Khaki;
            this.grpBL.Controls.Add(this.btnBLLFile);
            this.grpBL.Controls.Add(this.txtPrefixBLL);
            this.grpBL.Controls.Add(this.txtBLLFile);
            this.grpBL.Controls.Add(this.txtBLLNamespace);
            this.grpBL.Controls.Add(this.label7);
            this.grpBL.Controls.Add(this.label13);
            this.grpBL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBL.Location = new System.Drawing.Point(3, 3);
            this.grpBL.Name = "grpBL";
            this.grpBL.Size = new System.Drawing.Size(515, 148);
            this.grpBL.TabIndex = 46;
            this.grpBL.TabStop = false;
            this.grpBL.Text = "Business Logic";
            // 
            // btnBLLFile
            // 
            this.btnBLLFile.Location = new System.Drawing.Point(481, 57);
            this.btnBLLFile.Name = "btnBLLFile";
            this.btnBLLFile.Size = new System.Drawing.Size(24, 21);
            this.btnBLLFile.TabIndex = 3;
            this.btnBLLFile.Text = "...";
            this.btnBLLFile.UseVisualStyleBackColor = true;
            this.btnBLLFile.Click += new System.EventHandler(this.btnBLLFile_Click);
            // 
            // txtPrefixBLL
            // 
            this.txtPrefixBLL.Location = new System.Drawing.Point(375, 25);
            this.txtPrefixBLL.Name = "txtPrefixBLL";
            this.txtPrefixBLL.Size = new System.Drawing.Size(104, 20);
            this.txtPrefixBLL.TabIndex = 1;
            // 
            // txtBLLFile
            // 
            this.txtBLLFile.Location = new System.Drawing.Point(104, 59);
            this.txtBLLFile.Name = "txtBLLFile";
            this.txtBLLFile.Size = new System.Drawing.Size(375, 20);
            this.txtBLLFile.TabIndex = 2;
            // 
            // txtBLLNamespace
            // 
            this.txtBLLNamespace.Location = new System.Drawing.Point(104, 25);
            this.txtBLLNamespace.Name = "txtBLLNamespace";
            this.txtBLLNamespace.Size = new System.Drawing.Size(262, 20);
            this.txtBLLNamespace.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "BLL Location: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 40;
            this.label13.Text = "BL Namespace: ";
            // 
            // xTabDAL
            // 
            this.xTabDAL.Controls.Add(this.grpDAL);
            this.xTabDAL.Location = new System.Drawing.Point(4, 22);
            this.xTabDAL.Name = "xTabDAL";
            this.xTabDAL.Padding = new System.Windows.Forms.Padding(3);
            this.xTabDAL.Size = new System.Drawing.Size(521, 154);
            this.xTabDAL.TabIndex = 2;
            this.xTabDAL.Text = "DAL Classes";
            this.xTabDAL.UseVisualStyleBackColor = true;
            // 
            // grpDAL
            // 
            this.grpDAL.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.grpDAL.Controls.Add(this.chkInheritDAO);
            this.grpDAL.Controls.Add(this.btnDALFile);
            this.grpDAL.Controls.Add(this.txtDAO);
            this.grpDAL.Controls.Add(this.txtPrefixDAL);
            this.grpDAL.Controls.Add(this.txtDALFile);
            this.grpDAL.Controls.Add(this.label8);
            this.grpDAL.Controls.Add(this.txtDALNamespace);
            this.grpDAL.Controls.Add(this.label1);
            this.grpDAL.Controls.Add(this.label4);
            this.grpDAL.Controls.Add(this.label12);
            this.grpDAL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDAL.Location = new System.Drawing.Point(3, 3);
            this.grpDAL.Name = "grpDAL";
            this.grpDAL.Size = new System.Drawing.Size(515, 148);
            this.grpDAL.TabIndex = 46;
            this.grpDAL.TabStop = false;
            this.grpDAL.Text = "Data Access Layer";
            // 
            // chkInheritDAO
            // 
            this.chkInheritDAO.AutoSize = true;
            this.chkInheritDAO.Checked = true;
            this.chkInheritDAO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInheritDAO.Location = new System.Drawing.Point(104, 93);
            this.chkInheritDAO.Name = "chkInheritDAO";
            this.chkInheritDAO.Size = new System.Drawing.Size(157, 17);
            this.chkInheritDAO.TabIndex = 3;
            this.chkInheritDAO.Text = "Inherit from base DAO class";
            this.chkInheritDAO.UseVisualStyleBackColor = true;
            this.chkInheritDAO.CheckedChanged += new System.EventHandler(this.chkInheritDAO_CheckedChanged);
            // 
            // btnDALFile
            // 
            this.btnDALFile.Location = new System.Drawing.Point(485, 60);
            this.btnDALFile.Name = "btnDALFile";
            this.btnDALFile.Size = new System.Drawing.Size(24, 21);
            this.btnDALFile.TabIndex = 2;
            this.btnDALFile.Text = "...";
            this.btnDALFile.UseVisualStyleBackColor = true;
            this.btnDALFile.Click += new System.EventHandler(this.btnDALFile_Click);
            // 
            // txtDAO
            // 
            this.txtDAO.Location = new System.Drawing.Point(104, 122);
            this.txtDAO.Name = "txtDAO";
            this.txtDAO.Size = new System.Drawing.Size(265, 20);
            this.txtDAO.TabIndex = 5;
            this.txtDAO.Text = "DataAccessObject";
            // 
            // txtPrefixDAL
            // 
            this.txtPrefixDAL.Location = new System.Drawing.Point(363, 91);
            this.txtPrefixDAL.Name = "txtPrefixDAL";
            this.txtPrefixDAL.Size = new System.Drawing.Size(116, 20);
            this.txtPrefixDAL.TabIndex = 4;
            // 
            // txtDALFile
            // 
            this.txtDALFile.Location = new System.Drawing.Point(104, 61);
            this.txtDALFile.Name = "txtDALFile";
            this.txtDALFile.Size = new System.Drawing.Size(375, 20);
            this.txtDALFile.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "DAO Base Class: ";
            // 
            // txtDALNamespace
            // 
            this.txtDALNamespace.Location = new System.Drawing.Point(104, 25);
            this.txtDALNamespace.Name = "txtDALNamespace";
            this.txtDALNamespace.Size = new System.Drawing.Size(262, 20);
            this.txtDALNamespace.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "DAL Prefix: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "DAL Location: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "DAL Namespace: ";
            // 
            // xTabUI
            // 
            this.xTabUI.Controls.Add(this.chkDevExpress);
            this.xTabUI.Controls.Add(this.chkFilterMgr);
            this.xTabUI.Controls.Add(this.grpGUI);
            this.xTabUI.Location = new System.Drawing.Point(4, 22);
            this.xTabUI.Name = "xTabUI";
            this.xTabUI.Padding = new System.Windows.Forms.Padding(3);
            this.xTabUI.Size = new System.Drawing.Size(521, 154);
            this.xTabUI.TabIndex = 3;
            this.xTabUI.Text = "Winforms UI Classes";
            this.xTabUI.UseVisualStyleBackColor = true;
            // 
            // chkDevExpress
            // 
            this.chkDevExpress.AutoSize = true;
            this.chkDevExpress.Location = new System.Drawing.Point(252, 7);
            this.chkDevExpress.Name = "chkDevExpress";
            this.chkDevExpress.Size = new System.Drawing.Size(146, 17);
            this.chkDevExpress.TabIndex = 1;
            this.chkDevExpress.Text = "Use DevExpress Controls";
            this.chkDevExpress.UseVisualStyleBackColor = true;
            this.chkDevExpress.CheckedChanged += new System.EventHandler(this.chkDevExpress_CheckedChanged);
            // 
            // chkFilterMgr
            // 
            this.chkFilterMgr.AutoSize = true;
            this.chkFilterMgr.Checked = true;
            this.chkFilterMgr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFilterMgr.Location = new System.Drawing.Point(14, 7);
            this.chkFilterMgr.Name = "chkFilterMgr";
            this.chkFilterMgr.Size = new System.Drawing.Size(150, 17);
            this.chkFilterMgr.TabIndex = 0;
            this.chkFilterMgr.Text = "Generate Filter Mgr Code?";
            this.chkFilterMgr.UseVisualStyleBackColor = true;
            // 
            // grpGUI
            // 
            this.grpGUI.BackColor = System.Drawing.Color.Pink;
            this.grpGUI.Controls.Add(this.chkLayout);
            this.grpGUI.Controls.Add(this.chkInheritGUI);
            this.grpGUI.Controls.Add(this.btnGUIFile);
            this.grpGUI.Controls.Add(this.txtGUI);
            this.grpGUI.Controls.Add(this.txtParentName);
            this.grpGUI.Controls.Add(this.txtPrefixGUI);
            this.grpGUI.Controls.Add(this.txtGUIFile);
            this.grpGUI.Controls.Add(this.label3);
            this.grpGUI.Controls.Add(this.label27);
            this.grpGUI.Controls.Add(this.txtGUINamespace);
            this.grpGUI.Controls.Add(this.label9);
            this.grpGUI.Controls.Add(this.label10);
            this.grpGUI.Controls.Add(this.label11);
            this.grpGUI.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpGUI.Location = new System.Drawing.Point(3, 29);
            this.grpGUI.Name = "grpGUI";
            this.grpGUI.Size = new System.Drawing.Size(515, 122);
            this.grpGUI.TabIndex = 2;
            this.grpGUI.TabStop = false;
            this.grpGUI.Text = "Data Access Layer";
            // 
            // chkLayout
            // 
            this.chkLayout.AutoSize = true;
            this.chkLayout.Location = new System.Drawing.Point(382, 16);
            this.chkLayout.Name = "chkLayout";
            this.chkLayout.Size = new System.Drawing.Size(116, 17);
            this.chkLayout.TabIndex = 1;
            this.chkLayout.Text = "Use Layout Control";
            this.chkLayout.UseVisualStyleBackColor = true;
            // 
            // chkInheritGUI
            // 
            this.chkInheritGUI.AutoSize = true;
            this.chkInheritGUI.Checked = true;
            this.chkInheritGUI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInheritGUI.Location = new System.Drawing.Point(104, 65);
            this.chkInheritGUI.Name = "chkInheritGUI";
            this.chkInheritGUI.Size = new System.Drawing.Size(153, 17);
            this.chkInheritGUI.TabIndex = 4;
            this.chkInheritGUI.Text = "Inherit from base GUI class";
            this.chkInheritGUI.UseVisualStyleBackColor = true;
            this.chkInheritGUI.CheckStateChanged += new System.EventHandler(this.chkInheritGUI_CheckedChanged);
            // 
            // btnGUIFile
            // 
            this.btnGUIFile.Location = new System.Drawing.Point(485, 38);
            this.btnGUIFile.Name = "btnGUIFile";
            this.btnGUIFile.Size = new System.Drawing.Size(24, 21);
            this.btnGUIFile.TabIndex = 3;
            this.btnGUIFile.Text = "...";
            this.btnGUIFile.UseVisualStyleBackColor = true;
            this.btnGUIFile.Click += new System.EventHandler(this.btnGUIFile_Click);
            // 
            // txtGUI
            // 
            this.txtGUI.Location = new System.Drawing.Point(104, 86);
            this.txtGUI.Name = "txtGUI";
            this.txtGUI.Size = new System.Drawing.Size(153, 20);
            this.txtGUI.TabIndex = 6;
            this.txtGUI.Text = "GUIBaseObject";
            // 
            // txtParentName
            // 
            this.txtParentName.Location = new System.Drawing.Point(363, 87);
            this.txtParentName.Name = "txtParentName";
            this.txtParentName.Size = new System.Drawing.Size(116, 20);
            this.txtParentName.TabIndex = 7;
            this.txtParentName.Text = "Main";
            // 
            // txtPrefixGUI
            // 
            this.txtPrefixGUI.Location = new System.Drawing.Point(363, 63);
            this.txtPrefixGUI.Name = "txtPrefixGUI";
            this.txtPrefixGUI.Size = new System.Drawing.Size(116, 20);
            this.txtPrefixGUI.TabIndex = 5;
            // 
            // txtGUIFile
            // 
            this.txtGUIFile.Location = new System.Drawing.Point(104, 39);
            this.txtGUIFile.Name = "txtGUIFile";
            this.txtGUIFile.Size = new System.Drawing.Size(375, 20);
            this.txtGUIFile.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "GUI Base Class: ";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(265, 90);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(92, 13);
            this.label27.TabIndex = 41;
            this.label27.Text = "App Parent Main: ";
            // 
            // txtGUINamespace
            // 
            this.txtGUINamespace.Location = new System.Drawing.Point(104, 16);
            this.txtGUINamespace.Name = "txtGUINamespace";
            this.txtGUINamespace.Size = new System.Drawing.Size(262, 20);
            this.txtGUINamespace.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(295, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "GUI Prefix: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 41;
            this.label10.Text = "GUI Location: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "GUI Namespace: ";
            // 
            // xTabWeb
            // 
            this.xTabWeb.Controls.Add(this.grpAspx);
            this.xTabWeb.Location = new System.Drawing.Point(4, 22);
            this.xTabWeb.Name = "xTabWeb";
            this.xTabWeb.Size = new System.Drawing.Size(521, 154);
            this.xTabWeb.TabIndex = 4;
            this.xTabWeb.Text = "Aspx Web Forms";
            this.xTabWeb.UseVisualStyleBackColor = true;
            // 
            // grpAspx
            // 
            this.grpAspx.Controls.Add(this.label29);
            this.grpAspx.Controls.Add(this.label28);
            this.grpAspx.Controls.Add(this.label22);
            this.grpAspx.Controls.Add(this.label21);
            this.grpAspx.Controls.Add(this.label20);
            this.grpAspx.Controls.Add(this.label19);
            this.grpAspx.Controls.Add(this.txtLinkButton);
            this.grpAspx.Controls.Add(this.txtGrid);
            this.grpAspx.Controls.Add(this.txtButton);
            this.grpAspx.Controls.Add(this.txtLabel);
            this.grpAspx.Controls.Add(this.txtTextBox);
            this.grpAspx.Controls.Add(this.txtTitle);
            this.grpAspx.Controls.Add(this.btnAspxFile);
            this.grpAspx.Controls.Add(this.txtPrefixAspx);
            this.grpAspx.Controls.Add(this.txtAspxFile);
            this.grpAspx.Controls.Add(this.txtNamespaceAspx);
            this.grpAspx.Controls.Add(this.label17);
            this.grpAspx.Controls.Add(this.label18);
            this.grpAspx.Location = new System.Drawing.Point(3, 3);
            this.grpAspx.Name = "grpAspx";
            this.grpAspx.Size = new System.Drawing.Size(515, 148);
            this.grpAspx.TabIndex = 0;
            this.grpAspx.TabStop = false;
            this.grpAspx.Text = "Aspx Page";
            this.grpAspx.Enter += new System.EventHandler(this.grpAspx_Enter);
            // 
            // btnAspxFile
            // 
            this.btnAspxFile.Location = new System.Drawing.Point(480, 61);
            this.btnAspxFile.Name = "btnAspxFile";
            this.btnAspxFile.Size = new System.Drawing.Size(24, 21);
            this.btnAspxFile.TabIndex = 45;
            this.btnAspxFile.Text = "...";
            this.btnAspxFile.UseVisualStyleBackColor = true;
            this.btnAspxFile.Click += new System.EventHandler(this.btnAspxFile_Click);
            // 
            // txtPrefixAspx
            // 
            this.txtPrefixAspx.Location = new System.Drawing.Point(374, 29);
            this.txtPrefixAspx.Name = "txtPrefixAspx";
            this.txtPrefixAspx.Size = new System.Drawing.Size(104, 20);
            this.txtPrefixAspx.TabIndex = 43;
            // 
            // txtAspxFile
            // 
            this.txtAspxFile.Location = new System.Drawing.Point(103, 63);
            this.txtAspxFile.Name = "txtAspxFile";
            this.txtAspxFile.Size = new System.Drawing.Size(375, 20);
            this.txtAspxFile.TabIndex = 44;
            // 
            // txtNamespaceAspx
            // 
            this.txtNamespaceAspx.Location = new System.Drawing.Point(103, 29);
            this.txtNamespaceAspx.Name = "txtNamespaceAspx";
            this.txtNamespaceAspx.Size = new System.Drawing.Size(262, 20);
            this.txtNamespaceAspx.TabIndex = 42;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(22, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 13);
            this.label17.TabIndex = 47;
            this.label17.Text = "Aspx Location: ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(96, 13);
            this.label18.TabIndex = 46;
            this.label18.Text = "Aspx Namespace: ";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(18, 636);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(56, 42);
            this.btnSet.TabIndex = 13;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(104, 636);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(56, 42);
            this.btnTest.TabIndex = 14;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(197, 636);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(116, 42);
            this.btnBuild.TabIndex = 15;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(427, 636);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 42);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkAspx
            // 
            this.chkAspx.AutoSize = true;
            this.chkAspx.Checked = true;
            this.chkAspx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAspx.Location = new System.Drawing.Point(359, 387);
            this.chkAspx.Name = "chkAspx";
            this.chkAspx.Size = new System.Drawing.Size(150, 17);
            this.chkAspx.TabIndex = 40;
            this.chkAspx.Text = "Generate Aspx WebForms";
            this.chkAspx.UseVisualStyleBackColor = true;
            this.chkAspx.CheckStateChanged += new System.EventHandler(this.chkAspx_CheckStateChanged);
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(71, 89);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(100, 20);
            this.txtTitle.TabIndex = 48;
            this.txtTitle.Text = "modal-title";
            // 
            // txtTextBox
            // 
            this.txtTextBox.Location = new System.Drawing.Point(71, 122);
            this.txtTextBox.Name = "txtTextBox";
            this.txtTextBox.Size = new System.Drawing.Size(100, 20);
            this.txtTextBox.TabIndex = 49;
            this.txtTextBox.Text = "form-control";
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(237, 122);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(100, 20);
            this.txtLabel.TabIndex = 50;
            this.txtLabel.Text = "control-label";
            // 
            // txtButton
            // 
            this.txtButton.Location = new System.Drawing.Point(237, 89);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(100, 20);
            this.txtButton.TabIndex = 51;
            this.txtButton.Text = "btn btn-default";
            // 
            // txtGrid
            // 
            this.txtGrid.Location = new System.Drawing.Point(392, 89);
            this.txtGrid.Name = "txtGrid";
            this.txtGrid.Size = new System.Drawing.Size(100, 20);
            this.txtGrid.TabIndex = 52;
            this.txtGrid.Text = "table table-hover table-bordered";
            // 
            // txtLinkButton
            // 
            this.txtLinkButton.Location = new System.Drawing.Point(392, 122);
            this.txtLinkButton.Name = "txtLinkButton";
            this.txtLinkButton.Size = new System.Drawing.Size(100, 20);
            this.txtLinkButton.TabIndex = 53;
            this.txtLinkButton.Text = "btn-link";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 95);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 13);
            this.label19.TabIndex = 54;
            this.label19.Text = "CssTitle";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(9, 122);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(63, 13);
            this.label20.TabIndex = 55;
            this.label20.Text = "CssTextBox";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(177, 95);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(55, 13);
            this.label21.TabIndex = 56;
            this.label21.Text = "CssButton";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(177, 122);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(50, 13);
            this.label22.TabIndex = 57;
            this.label22.Text = "CssLabel";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(343, 95);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(43, 13);
            this.label28.TabIndex = 58;
            this.label28.Text = "CssGrid";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(342, 122);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(43, 13);
            this.label29.TabIndex = 59;
            this.label29.Text = "CssLkB";
            // 
            // ORMMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(557, 690);
            this.ControlBox = false;
            this.Controls.Add(this.chkAspx);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chkGUI);
            this.Controls.Add(this.chkDAL);
            this.Controls.Add(this.chkPrefix);
            this.Controls.Add(this.chkBLL);
            this.Controls.Add(this.chkBE);
            this.Controls.Add(this.chkSort);
            this.Controls.Add(this.btnProjectFolder);
            this.Controls.Add(this.txtClassname);
            this.Controls.Add(this.txtCompanyNamespace);
            this.Controls.Add(this.txtHomeFolder);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.grpTextFile);
            this.Controls.Add(this.grpSQL);
            this.Controls.Add(this.grpSource);
            this.Name = "ORMMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ORM Class Builder";
            this.Load += new System.EventHandler(this.ORMMain_Load);
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.grpSQL.ResumeLayout(false);
            this.grpSQL.PerformLayout();
            this.grpTextFile.ResumeLayout(false);
            this.grpTextFile.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.xTabBE.ResumeLayout(false);
            this.grpBE.ResumeLayout(false);
            this.grpBE.PerformLayout();
            this.xTabBLL.ResumeLayout(false);
            this.grpBL.ResumeLayout(false);
            this.grpBL.PerformLayout();
            this.xTabDAL.ResumeLayout(false);
            this.grpDAL.ResumeLayout(false);
            this.grpDAL.PerformLayout();
            this.xTabUI.ResumeLayout(false);
            this.xTabUI.PerformLayout();
            this.grpGUI.ResumeLayout(false);
            this.grpGUI.PerformLayout();
            this.xTabWeb.ResumeLayout(false);
            this.grpAspx.ResumeLayout(false);
            this.grpAspx.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdClasses;
        private System.Windows.Forms.FolderBrowserDialog fbTarget;
        private System.Windows.Forms.FolderBrowserDialog fbProject;
        private System.Windows.Forms.GroupBox grpSource;
        private System.Windows.Forms.RadioButton rbTextFile;
        private System.Windows.Forms.RadioButton rbSQLDirect;
        private System.Windows.Forms.GroupBox grpSQL;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.Label lblDatabases;
        private System.Windows.Forms.Label lblServers;
        private System.Windows.Forms.ComboBox cbTables;
        private System.Windows.Forms.ComboBox cbDatabases;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.Button btnGetSQLStucture;
        private System.Windows.Forms.GroupBox grpTextFile;
        private System.Windows.Forms.Button btnSourceFile;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtHomeFolder;
        private System.Windows.Forms.TextBox txtCompanyNamespace;
        private System.Windows.Forms.Button btnProjectFolder;
        private System.Windows.Forms.TextBox txtClassname;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox chkSort;
        private System.Windows.Forms.CheckBox chkPrefix;
        private System.Windows.Forms.CheckBox chkBE;
        private System.Windows.Forms.CheckBox chkDAL;
        private System.Windows.Forms.CheckBox chkBLL;
        private System.Windows.Forms.CheckBox chkGUI;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage xTabBE;
        private System.Windows.Forms.GroupBox grpBE;
        private System.Windows.Forms.Button btnBEFile;
        private System.Windows.Forms.TextBox txtPrefixBE;
        private System.Windows.Forms.TextBox txtBaseClass;
        private System.Windows.Forms.TextBox txtBEFile;
        private System.Windows.Forms.TextBox txtConstants;
        private System.Windows.Forms.TextBox txtBENamespace;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage xTabBLL;
        private System.Windows.Forms.GroupBox grpBL;
        private System.Windows.Forms.Button btnBLLFile;
        private System.Windows.Forms.TextBox txtPrefixBLL;
        private System.Windows.Forms.TextBox txtBLLFile;
        private System.Windows.Forms.TextBox txtBLLNamespace;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage xTabDAL;
        private System.Windows.Forms.GroupBox grpDAL;
        private System.Windows.Forms.CheckBox chkInheritDAO;
        private System.Windows.Forms.Button btnDALFile;
        private System.Windows.Forms.TextBox txtDAO;
        private System.Windows.Forms.TextBox txtPrefixDAL;
        private System.Windows.Forms.TextBox txtDALFile;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDALNamespace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage xTabUI;
        private System.Windows.Forms.CheckBox chkDevExpress;
        private System.Windows.Forms.CheckBox chkFilterMgr;
        private System.Windows.Forms.GroupBox grpGUI;
        private System.Windows.Forms.CheckBox chkLayout;
        private System.Windows.Forms.CheckBox chkInheritGUI;
        private System.Windows.Forms.Button btnGUIFile;
        private System.Windows.Forms.TextBox txtGUI;
        private System.Windows.Forms.TextBox txtParentName;
        private System.Windows.Forms.TextBox txtPrefixGUI;
        private System.Windows.Forms.TextBox txtGUIFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtGUINamespace;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage xTabWeb;
        private System.Windows.Forms.CheckBox chkAspx;
        private System.Windows.Forms.GroupBox grpAspx;
        private System.Windows.Forms.Button btnAspxFile;
        private System.Windows.Forms.TextBox txtPrefixAspx;
        private System.Windows.Forms.TextBox txtAspxFile;
        private System.Windows.Forms.TextBox txtNamespaceAspx;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtTextBox;
        private System.Windows.Forms.TextBox txtLinkButton;
        private System.Windows.Forms.TextBox txtGrid;
        private System.Windows.Forms.TextBox txtButton;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
    }
}


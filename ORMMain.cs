using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using pMap = ORM.UI.PropertyMap;
using System.Diagnostics;
using Microsoft.SqlServer.Management.Smo;
using T = Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data;
using Microsoft.Win32;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ORM.UI
{
    public partial class ORMMain : Form
    {
        #region Properties
        private Boolean isSQL = true;
        private Server sqlServer;
        private Database dbase;
        private T.Table table;
        private String BEfolderName;
        private String BLLfolderName;
        private String DALfolderName;
        private String GUIfolderName;
        private String AspxfolderName;
        private string strSourcePath;
        private String targetPath;
        private String className;
        private String pluralClassName;
        private String lowerClass;
        private String key;
        private Boolean useLayoutControl;
        private String _tableName = "_tableName";
        //private String operatorType = "operatorType";
        //private String sortField    = "sortField";
        //private String fieldName    = "fieldName";
        //private String value        = "EditValue";
        private String srLine;
        private List<pMap> fieldList = new List<pMap>();
        private Int32 n = 1;
        private Int32 fCount;
        //private Int16 x;
        //private Int16 y;
        private Boolean boolRightAlign = false;
        private Boolean enumExists = false;
        private String parentName;
        #endregion

        public ORMMain()
        {
            InitializeComponent();
            fbTarget.SelectedPath = @"D:\Dev 2010\";   // ofdClasses.InitialDirectory;
        }
        private void ORMMain_Load(object sender, EventArgs e)
        {
            //Text += String.Format("Version: {0}", Application.ProductVersion);
            AcceptButton = btnBuild;
        }

        private string ToTitleCase(string Word)
        {
            //fachi
            Word = Word.ToUpper().Replace("ID_","").Replace("_", " ");
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Word.ToLower());
        }

        #region buttons
        private void btnSet_Click(object sender, EventArgs e)
        {
            txtBENamespace.Text = txtCompanyNamespace.Text.Trim() + ".BusinessEntities";
            txtConstants.Text = txtHomeFolder.Text.Trim() + txtBENamespace.Text.Trim() + "\\Constants\\";
            txtBEFile.Text = txtHomeFolder.Text.Trim() + txtBENamespace.Text.Trim() + "\\";

            txtBLLNamespace.Text = txtCompanyNamespace.Text.Trim() + ".BusinessLogicLayer";
            txtBLLFile.Text = txtHomeFolder.Text.Trim() + txtBLLNamespace.Text.Trim() + "\\";

            txtDALNamespace.Text = txtCompanyNamespace.Text.Trim() + ".DataAccessLayer";
            txtDALFile.Text = txtHomeFolder.Text.Trim() + txtDALNamespace.Text.Trim() + "\\";

            txtGUINamespace.Text = txtCompanyNamespace.Text.Trim() + ".UserInterface";
            txtGUIFile.Text = txtHomeFolder.Text.Trim() + txtGUINamespace.Text.Trim() + "\\";

            txtNamespaceAspx.Text = txtCompanyNamespace.Text.Trim() + ".Webforms";
            txtAspxFile.Text = txtHomeFolder.Text.Trim() + txtGUINamespace.Text.Trim() + "\\";

        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            BLLfolderName = txtBLLFile.Text.Trim();
            BEfolderName = txtBEFile.Text.Trim();
            DALfolderName = txtDALFile.Text.Trim();
            GUIfolderName = txtGUIFile.Text.Trim();
            AspxfolderName = txtAspxFile.Text.Trim();

            if (chkBE.Checked)
            {
                if (!Directory.Exists(BEfolderName))
                {
                    MessageBox.Show(BEfolderName, "This folder does not exist. Click OK to create it");
                    Directory.CreateDirectory(BEfolderName);
                    Directory.CreateDirectory(BEfolderName + "\\Constants");
                }
                if (!Directory.Exists(BEfolderName + "\\Constants"))
                {
                    MessageBox.Show(BEfolderName + "\\Constants", "This folder does not exist. Click OK to create it");
                    Directory.CreateDirectory(BEfolderName + "\\Constants");
                }

            }

            if (chkBLL.Checked)
            {
                if (!Directory.Exists(BLLfolderName))
                {
                    MessageBox.Show(BLLfolderName, "This folder does not exist. Click OK to create it");
                    Directory.CreateDirectory(BLLfolderName);
                }
            }

            if (chkDAL.Checked)
            {
                if (!Directory.Exists(DALfolderName))
                {
                    MessageBox.Show(DALfolderName, "This folder does not exist. Click OK to create it");
                    Directory.CreateDirectory(DALfolderName);
                }
            }

            if (chkGUI.Checked)
            {
                if (!Directory.Exists(GUIfolderName))
                {
                    MessageBox.Show(GUIfolderName, "This folder does not exist. Click OK to create it");
                    Directory.CreateDirectory(GUIfolderName);
                }
            }

            if (chkAspx.Checked)
            {
                if (!Directory.Exists(AspxfolderName))
                {
                    MessageBox.Show(AspxfolderName, "This folder does not exist. Click OK to create it");
                    Directory.CreateDirectory(AspxfolderName);
                }
            }

        }
        private void btnBuild_Click(object sender, EventArgs e)
        {
            // Disconnect from the SQL database
            string[] files;

            #region Validations
            if (isSQL)
            {
                if (String.IsNullOrEmpty(cbTables.Text))
                {
                    MessageBox.Show("Please select a SQL database table from the list.");
                    sqlServer.ConnectionContext.Disconnect();
                    return;
                }
            }
            if (chkBE.CheckState == CheckState.Checked)
            {
                if (txtBENamespace.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("The BE namespace name has not been provided. Please insert the correct name");
                    return;
                }
            }

            if (chkBLL.CheckState == CheckState.Checked)
            {
                if (txtBLLNamespace.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("The BLL namespace name has not been provided. Please insert the correct name");
                    return;
                }
            }

            if (chkDAL.CheckState == CheckState.Checked)
            {
                if (txtDALNamespace.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("The DAL namespace name has not been provided. Please insert the correct name");
                    return;
                }
            }

            if (chkGUI.CheckState == CheckState.Checked)
            {
                if (txtGUINamespace.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("The UI namespace name has not been provided. Please insert the correct name");
                    return;
                }
            }

            if (chkAspx.CheckState == CheckState.Checked)
            {
                if (txtNamespaceAspx.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("The UI namespace name has not been provided. Please insert the correct name");
                    return;
                }
            }
            #endregion

            #region Set default target folders
            if (String.IsNullOrEmpty(BEfolderName))
                BEfolderName = txtBEFile.Text.Trim();
            if (String.IsNullOrEmpty(BLLfolderName))
                BLLfolderName = txtBLLFile.Text.Trim();
            if (String.IsNullOrEmpty(DALfolderName))
                DALfolderName = txtDALFile.Text.Trim();
            if (String.IsNullOrEmpty(AspxfolderName))
                AspxfolderName = txtAspxFile.Text.Trim();
            #endregion

            if (!isSQL)
            {
                if (txtSourceFile.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Please select a source file/folder to base class creation on");
                    return;
                }

                if (txtSourceFile.Text.Trim().IndexOf(".cs") > 0)
                { // parse a single file
                    strSourcePath = txtSourceFile.Text.Trim() + className;
                    WriteCode(strSourcePath);
                }
                else
                { // parse the whole folder
                    files = Directory.GetFiles(txtSourceFile.Text.Trim(), "*.cs");
                    foreach (string source in files)
                    {
                        strSourcePath = txtSourceFile.Text.Trim() + source;
                        WriteCode(source);
                    }
                }
            }
            else
            {
                WriteCode(txtClassname.Text);
            }

            MessageBox.Show("Class creation complete!");
            //Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void WriteCode(String source)
        {
            fieldList.Clear();

            #region Singles, Plurals, etc
            className = source;
            className = className.Substring(className.LastIndexOf("\\") + 1);
            if (className.LastIndexOf(".cs") > 0)
                className = className.Substring(0, className.IndexOf(".cs"));

            if (className.Substring(className.Length - 1) == "s")
            {
                pluralClassName = className;
                className = className.Substring(0, className.Length - 1);
            }
            else
                pluralClassName = className + "s";

            lowerClass = className.ToLower();
            #endregion

            if (isSQL)
            {
                foreach (Column col in table.Columns)
                {
                    Console.WriteLine(col.Name + " - " + col.DataType + " - " + col.Default + " - " + col.Identity + ":" + col.InPrimaryKey + " - " + col.IdentitySeed + "/" + col.IdentityIncrement);

                    pMap item = new pMap();
                    item.Name = col.Name;
                    item.Type = col.DataType.ToString();
                    if (item.Name != "ID")
                        item.Caption = MakeCaption(item.Name);

                    #region Set Control type prefix (eg: "chk" for CheckEdit)
                    switch (item.Type.ToLower())
                    {
                        case "bigint":
                            item.ControlPrefix = "txt";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "TextEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "Text";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.TextBox();
                            item.AspControlType = "TextBox";
                            break;
                        case "uniqueidentifier":
                            item.ControlPrefix = "txt";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "TextEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "Text";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.TextBox();
                            item.AspControlType = "TextBox";
                            break;
                        case "bool":
                            item.ControlPrefix = "chk";
                            item.AspControlPrefix = "chk";
                            item.ControlName = "CheckEdit";
                            item.AspControlName = "CheckBox";
                            item.EditField = "Checked";
                            item.AspEditField = "Checked";
                            item.ControlType = "CheckBox";
                            item.AspControlType = "CheckBox";
                            break;
                        case "bit":
                            item.ControlPrefix = "chk";
                            item.AspControlPrefix = "chk";
                            item.ControlName = "CheckEdit";
                            item.AspControlName = "CheckBox";
                            item.EditField = "Checked";
                            item.AspEditField = "Checked";
                            item.ControlType = new System.Windows.Forms.CheckBox();
                            item.AspControlType = "CheckBox";
                            break;
                        case "int32":
                            item.ControlPrefix = "spin";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "SpinEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.NumericUpDown();
                            item.AspControlType = "TextBox";
                            break;
                        case "int":
                            item.ControlPrefix = "spin";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "SpinEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.NumericUpDown();
                            item.AspControlType = "TextBox";
                            break;
                        case "decimal":
                            item.ControlPrefix = "spin";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "SpinEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.NumericUpDown();
                            item.AspControlType = "TextBox";
                            break;
                        case "string":
                            item.ControlPrefix = "txt";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "TextEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "Text";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.TextBox();
                            item.AspControlType = "TextBox";
                            break;
                        case "nvarchar":
                            item.ControlPrefix = "txt";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "TextEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "Text";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.TextBox();
                            item.AspControlType = "TextBox";
                            break;
                        case "varchar":
                            item.ControlPrefix = "txt";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "TextEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "Text";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.TextBox();
                            item.AspControlType = "TextBox";
                            break;
                        case "double":
                            item.ControlPrefix = "spin";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "SpinEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.NumericUpDown();
                            item.AspControlType = "TextBox";
                            break;
                        case "byte":
                            item.ControlPrefix = "spin";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "SpinEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.NumericUpDown();
                            item.AspControlType = "TextBox";
                            break;
                        case "byte[]":
                            item.ControlPrefix = "pic";
                            item.AspControlPrefix = "img";
                            item.ControlName = "PictureEdit";
                            item.AspControlName = "Image";
                            item.EditField = "Image";
                            item.AspEditField = "Image";
                            item.ControlType = new System.Windows.Forms.PictureBox();
                            item.AspControlType = "Image";
                            break;
                        case "varbinary":
                            item.ControlPrefix = "pic";
                            item.AspControlPrefix = "img";
                            item.ControlName = "PictureEdit";
                            item.AspControlName = "Image";
                            item.EditField = "Image";
                            item.AspEditField = "Image";
                            item.ControlType = new System.Windows.Forms.PictureBox();
                            item.AspControlType = "Image";
                            break;
                        case "short":
                            item.ControlPrefix = "spin";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "SpinEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.NumericUpDown();
                            item.AspControlType = "TextBox";
                            break;
                        case "long":
                            item.ControlPrefix = "spin";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "SpinEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.NumericUpDown();
                            item.AspControlType = "TextBox";
                            break;
                        case "char":
                            item.ControlPrefix = "txt";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "TextEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "Text";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.TextBox();
                            item.AspControlType = "TextBox";
                            break;
                        case "datetime":
                            item.ControlPrefix = "date";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "DateEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.DateTimePicker();
                            item.AspControlType = "TextBox";
                            break;
                        case "time":
                            item.ControlPrefix = "time";
                            item.AspControlPrefix = "txt";
                            item.ControlName = "TimeEdit";
                            item.AspControlName = "TextBox";
                            item.EditField = "EditValue";
                            item.AspEditField = "Text";
                            item.ControlType = new System.Windows.Forms.DateTimePicker();
                            item.AspControlType = "TextBox";
                            break;
                        case "image":
                            item.ControlPrefix = "pic";
                            item.AspControlPrefix = "img";
                            item.ControlName = "PictureEdit";
                            item.AspControlName = "Image";
                            item.EditField = "Image";
                            item.AspEditField = "Image";
                            item.ControlType = new System.Windows.Forms.PictureBox();
                            item.AspControlType = "Image";
                            break;
                        default:
                            MessageBox.Show("Potential problem with setting initial control prefix: " + item.Name, item.Type);
                            break;
                    }
                    #endregion

                    if (col.InPrimaryKey)
                    {
                        item.Key = true;
                        key = col.Name;
                    }
                    else
                        item.Key = false;

                    if (col.Nullable)
                        item.Required = false;
                    else
                        item.Required = true;


                    fieldList.Add(item);
                    n++;
                }
            }
            else
            {
                #region Build field list from file and get name of key field
                using (StreamReader srBLL = new StreamReader(source))
                {
                    srLine = srBLL.ReadLine();
                    key = string.Empty;
                    while (key == string.Empty)
                    {
                        srLine = srBLL.ReadLine();
                        if (srLine == null)
                            break;
                        if ((srLine.IndexOf("(Session session)") > 0) | (srLine.IndexOf(": base") > 0))
                            break;
                        if ((srLine.IndexOf("ctor") > 0) | (srLine.IndexOf("Associations") > 0) | (srLine.IndexOf("using") > 0))
                            break;
                        if ((srLine.IndexOf("FieldsClass") > 0) | (srLine.IndexOf("OperandProperty") > 0))
                            break;
                        if ((srLine.IndexOf("AfterConstruction") > 0))
                            break;
                        if (srLine.IndexOf("public") > 0 && srLine.IndexOf("enum") > 0)
                            enumExists = true;
                        if ((srLine.IndexOf("public") > 0 && srLine.IndexOf("class") < 0) && (srLine.IndexOf("public") > 0 && srLine.IndexOf("enum") < 0) && (srLine.IndexOf("//") < 0))
                        {
                            int n1 = srLine.Trim().IndexOf(" ", 1);
                            int n2 = srLine.Trim().IndexOf(" ", n1 + 1);
                            pMap item = new pMap();
                            item.Name = srLine.Trim().Substring(n2 + 1);
                            item.Type = srLine.Trim().Substring(7, n2 - n1).Trim();
                            if (item.Name != "ID")
                                item.Caption = MakeCaption(item.Name);

                            #region Set Control type prefix (eg: "chk" for CheckEdit)
                            switch (item.Type.ToLower())
                            {
                                case "uniqueidentifier":
                                    item.Key = false;
                                    item.ControlPrefix = "txt";
                                    item.ControlName = "TextEdit";
                                    item.EditField = "Text";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.TextBox();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.TextEdit();
                                    break;
                                case "bool":
                                    item.Key = false;
                                    item.ControlPrefix = "chk";
                                    item.ControlName = "CheckEdit";
                                    item.EditField = "Checked";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.CheckBox();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.CheckEdit();
                                    break;
                                case "bit":
                                    item.Key = false;
                                    item.ControlPrefix = "chk";
                                    item.ControlName = "CheckEdit";
                                    item.EditField = "Checked";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.CheckBox();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.CheckEdit();
                                    break;
                                case "int32":
                                    item.Key = false;
                                    item.ControlPrefix = "spin";
                                    item.ControlName = "SpinEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.NumericUpDown();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.SpinEdit();
                                    break;
                                case "int":
                                    item.Key = false;
                                    item.ControlPrefix = "spin";
                                    item.ControlName = "SpinEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.NumericUpDown();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.SpinEdit();
                                    break;
                                case "decimal":
                                    item.Key = false;
                                    item.ControlPrefix = "spin";
                                    item.ControlName = "SpinEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.NumericUpDown();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.SpinEdit();
                                    break;
                                case "string":
                                    item.Key = false;
                                    item.ControlPrefix = "txt";
                                    item.ControlName = "TextEdit";
                                    item.EditField = "Text";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.TextBox();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.TextEdit();
                                    break;
                                case "double":
                                    item.Key = false;
                                    item.ControlPrefix = "spin";
                                    item.ControlName = "SpinEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.NumericUpDown();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.SpinEdit();
                                    break;
                                case "byte":
                                    item.Key = false;
                                    item.ControlPrefix = "spin";
                                    item.ControlName = "SpinEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.NumericUpDown();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.SpinEdit();
                                    break;
                                case "byte[]":
                                    item.Key = false;
                                    item.ControlPrefix = "pic";
                                    item.ControlName = "PictureEdit";
                                    item.EditField = "Image";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.PictureBox();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.PictureEdit();
                                    break;
                                case "short":
                                    item.Key = false;
                                    item.ControlPrefix = "spin";
                                    item.ControlName = "SpinEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.NumericUpDown();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.SpinEdit();
                                    break;
                                case "long":
                                    item.Key = false;
                                    item.ControlPrefix = "spin";
                                    item.ControlName = "SpinEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.NumericUpDown();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.SpinEdit();
                                    break;
                                case "char":
                                    item.Key = false;
                                    item.ControlPrefix = "txt";
                                    item.EditField = "Text";
                                    item.ControlName = "TextEdit";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.TextBox();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.TextEdit();
                                    break;
                                case "datetime":
                                    item.Key = false;
                                    item.ControlPrefix = "date";
                                    item.ControlName = "DateEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.DateTimePicker();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.DateEdit();
                                    break;
                                case "time":
                                    item.Key = false;
                                    item.ControlPrefix = "time";
                                    item.ControlName = "TimeEdit";
                                    item.EditField = "EditValue";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.DateTimePicker();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.TimeEdit();
                                    break;
                                case "image":
                                    item.Key = false;
                                    item.ControlPrefix = "pic";
                                    item.ControlName = "PictureEdit";
                                    item.EditField = "Image";
                                    if (chkDevExpress.Checked)
                                        item.ControlType = new System.Windows.Forms.PictureBox();
                                    //else
                                    //    item.ControlType = new DevExpress.XtraEditors.PictureEdit();


                                    break;
                                default:
                                    MessageBox.Show("Potential problem with setting control prefix: " + item.Name, item.Type);
                                    break;
                            }
                            #endregion

                            fieldList.Add(item);
                            n++;
                        }
                    }
                    if (fieldList.Count > 0)
                    {
                        key = fieldList[0].Name;
                        fieldList[0].Key = true;
                    }
                    else
                    {
                        MessageBox.Show("No fields were found in the source file: " + source);
                        return;
                    }
                    srBLL.Close();
                    srBLL.Dispose();
                }
                #endregion
            }

            #region generate code
            if (chkBE.CheckState == CheckState.Checked)
            {
                CreateBEClass();
            }

            if (chkBLL.CheckState == CheckState.Checked)
            {
                CreateBLLClass();
            }

            if (chkDAL.CheckState == CheckState.Checked)
            {
                CreateDALClass();
            }

            if (chkGUI.CheckState == CheckState.Checked)
            {
                if (chkDevExpress.Checked)
                {
                    chkFilterMgr.Checked = false;
                    chkLayout.Checked = false;
                    useLayoutControl = false;
                }

                CreateGUIClasses();
            }
            if (chkAspx.CheckState == CheckState.Checked)
            {
                CreateAspxClass();
            }
            #endregion

        }

        #region control behaviour
        #region Get SQL table structure
        private void btnGetSQLStucture_Click(object sender, EventArgs e)
        {
            getSQLServers();
        }

        private void getSQLServers()
        {
            DataTable dt = SmoApplication.EnumAvailableSqlServers(false);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    cbServers.Items.Add(dr["Name"]);
                }
            }
            cbServers.Text = cbServers.Items[0].ToString();
            lblServers.Visible = true;
            cbServers.Visible = true;
        }

        private void cbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database dbase = sqlServer.Databases[cbDatabases.Text];
            foreach (T.Table tb in dbase.Tables)
            {
                cbTables.Items.Add(tb.Name); // Add database to combobox
            }
            lblTables.Visible = true;
            cbTables.Visible = true;
        }

        private void cbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbase = sqlServer.Databases[cbDatabases.Text];
            table = dbase.Tables[cbTables.Text];
            txtClassname.Text = cbTables.Text.Trim();
        }


        private void rbSQLDirect_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSQLDirect.Checked)
            {
                grpSQL.Visible = true;
                grpTextFile.Visible = false;
                isSQL = true;
            }
            else
            {
                grpSQL.Visible = false;
                grpTextFile.Visible = true;
                isSQL = false;
            }
        }

        private void cbServers_Leave(object sender, EventArgs e)
        {
            if (cbServers.Visible)
            {
                ServerConnection conn = new ServerConnection();
                conn.LoginSecure = false;
                conn.ServerInstance = cbServers.Text;
                conn.Login = txtUsername.Text.Trim();
                conn.Password = txtPassword.Text.Trim();
                sqlServer = new Server(conn);
                Console.WriteLine(sqlServer.Name + " " + sqlServer.Information.VersionString);

                foreach (Database db in sqlServer.Databases)
                {
                    //Check if database is system database (We don't want to be adding the System databases to our list)
                    if (!db.IsSystemObject)
                    {
                        cbDatabases.Items.Add(db.Name); // Add database to combobox
                    }
                }
                lblDatabases.Visible = true;
                cbDatabases.Visible = true;
            }
        }
        #endregion
        private void txtHomeFolder_Leave(object sender, EventArgs e)
        {
            txtBEFile.Text = txtHomeFolder.Text.Trim() + txtBEFile.Text.Trim();
            txtBLLFile.Text = txtHomeFolder.Text.Trim() + txtBLLFile.Text.Trim();
            txtDALFile.Text = txtHomeFolder.Text.Trim() + txtDALFile.Text.Trim();
            txtAspxFile.Text = txtHomeFolder.Text.Trim() + txtAspxFile.Text.Trim();
        }
        private void txtCompanyNamespace_Leave(object sender, EventArgs e)
        {
            if (txtBENamespace.Text.Trim().IndexOf(".") < 1)
                txtBENamespace.Text = String.Format("{0}.{1}", txtCompanyNamespace.Text.Trim(), txtBENamespace.Text.Trim());

            if (txtBLLNamespace.Text.Trim().IndexOf(".") < 1)
                txtBLLNamespace.Text = String.Format("{0}.{1}", txtCompanyNamespace.Text.Trim(), txtBLLNamespace.Text.Trim());

            if (txtDALNamespace.Text.Trim().IndexOf(".") < 1)
                txtDALNamespace.Text = String.Format("{0}.{1}", txtCompanyNamespace.Text.Trim(), txtDALNamespace.Text.Trim());

            if (txtNamespaceAspx.Text.Trim().IndexOf(".") < 1)
                txtNamespaceAspx.Text = String.Format("{0}.{1}", txtCompanyNamespace.Text.Trim(), txtNamespaceAspx.Text.Trim());

            txtConstants.Text = txtHomeFolder.Text.Trim() + txtBENamespace.Text.Trim() + txtConstants.Text.Trim();
        }
        private void txtSourceFile_TextChanged(object sender, EventArgs e)
        {
            int nPos = ofdClasses.SafeFileName.IndexOf(".");
            txtClassname.Text = ofdClasses.SafeFileName.Substring(0, nPos);
        }
        private void btnProjectFolder_Click(object sender, EventArgs e)
        {
            fbProject.SelectedPath = targetPath;
            if (fbProject.ShowDialog() == DialogResult.OK)
            {
                if (fbProject.SelectedPath.Trim() != string.Empty)
                {
                    txtHomeFolder.Text = fbProject.SelectedPath.Trim() + "\\";
                }
            }
        }
        private void btnSourceFile_Click(object sender, EventArgs e)
        {
            fbTarget.SelectedPath = targetPath;
            if (ofdClasses.ShowDialog() == DialogResult.OK)
            {
                if (ofdClasses.FileName.Trim() != string.Empty)
                {
                    txtSourceFile.Text = ofdClasses.FileName.Trim();
                    targetPath = ofdClasses.InitialDirectory;
                    Int32 nPos = txtSourceFile.Text.LastIndexOf("\\");
                    if (nPos < txtSourceFile.Text.Length)
                        txtClassname.Text = txtSourceFile.Text.Substring(nPos + 1);
                    else
                        txtClassname.Text = String.Empty;
                }
            }
        }
        private void chkPrefix_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrefix.CheckState == CheckState.Checked)
            {
                txtPrefixBE.Visible = true;
                txtPrefixBLL.Visible = true;
                txtPrefixDAL.Visible = true;
                txtPrefixGUI.Visible = true;
                txtPrefixAspx.Visible = true;
            }
            else
            {
                txtPrefixBE.Visible = false;
                txtPrefixBLL.Visible = false;
                txtPrefixDAL.Visible = false;
                txtPrefixGUI.Visible = false;
                txtPrefixAspx.Visible = false;
                txtPrefixBE.Text = string.Empty;
                txtPrefixBLL.Text = string.Empty;
                txtPrefixDAL.Text = string.Empty;
                txtPrefixGUI.Text = string.Empty;
                txtPrefixAspx.Text = string.Empty;
            }
        }
        #endregion

        #region BE code
        private void chkBE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBE.CheckState == CheckState.Checked)
                grpBE.Visible = true;
            else
                grpBE.Visible = false;
        }
        private void btnBEFile_Click(object sender, EventArgs e)
        {
            GetBEFile();
        }
        private void GetBEFile()
        {
            #region Set write folder
            if (fbTarget.ShowDialog() == DialogResult.OK)
            {
                if (fbTarget.SelectedPath.Trim() != string.Empty)
                    BEfolderName = fbTarget.SelectedPath.Trim();
                else
                    BEfolderName = @"C:\";
            }
            else
                BLLfolderName = @"C:\";
            txtBEFile.Text = fbTarget.SelectedPath;
            #endregion
        }
        private void txtBENamespace_Leave(object sender, EventArgs e)
        {
            if (txtConstants.Text =="")
            txtConstants.Text = String.Format("{0}{1}\\Constants\\", txtHomeFolder.Text.Trim(), txtBENamespace.Text.Trim());
        }
        private void CreateBEClass()
        {
            if (txtBENamespace.Text.Trim() == string.Empty)
            {
                MessageBox.Show("The BE namespace name has not been provided. Please insert the correct name");
                return;
            }

            #region Write enums
            if (!enumExists)        // Create field enums if they don't already exist
            {
                StreamWriter sw = File.CreateText(txtConstants.Text.Trim() + className + ".cs");
                sw.WriteLine("using System;");
                sw.WriteLine("");
                sw.WriteLine("namespace " + txtBENamespace.Text.Trim() + ".Constants");
                sw.WriteLine("{");
                sw.WriteLine("  public enum " + className + "Fields");
                sw.WriteLine("  {");
                fCount = 1;
                foreach (PropertyMap field in fieldList)
                {
                    if (fCount > 1)
                        sw.Write(", " + field.Name);
                    else
                        sw.Write("    " + field.Name);
                    fCount++;
                }
                sw.WriteLine("");
                sw.WriteLine("  }");
                sw.WriteLine("}");
                sw.Close();
                sw.Dispose();
            }
            #endregion

            #region Create the AdoDotNetDataProvider class
            if (!File.Exists(txtConstants.Text.Trim() + "AdoDotNetDataProvider.cs"))
            {
                StreamWriter swAdo = File.CreateText(txtConstants.Text.Trim() + "AdoDotNetDataProvider.cs");
                swAdo.WriteLine("using System;");
                swAdo.WriteLine("");
                swAdo.WriteLine("namespace " + txtBENamespace.Text.Trim() + ".Constants");
                swAdo.WriteLine("{");
                swAdo.WriteLine("   public enum AdoDotNetDataProvider");
                swAdo.WriteLine("   {");
                swAdo.WriteLine("       SqlClient,");
                swAdo.WriteLine("       OleDb,");
                swAdo.WriteLine("       Odbc,");
                swAdo.WriteLine("   }");
                swAdo.WriteLine("}");
                swAdo.WriteLine("");
                swAdo.Close();
                swAdo.Dispose();
            }

            #endregion

            #region Write BE Class properites
            StreamWriter swBE = File.CreateText(BEfolderName + "\\" + txtPrefixBE.Text.Trim() + className + ".cs");
            swBE.WriteLine("using System;");
            swBE.WriteLine("using System.Drawing;");
            //if (!String.IsNullOrEmpty(txtBaseClass.Text.Trim()))
            //    swBE.WriteLine("using " + txtBaseClass.Text.Trim() + ";");
            swBE.WriteLine("");
            swBE.WriteLine("namespace " + txtBENamespace.Text.Trim());
            swBE.WriteLine("{");
            if (txtBaseClass.Text.Trim() == string.Empty)
                swBE.WriteLine("    public class " + className);
            //else
            //    swBE.WriteLine("    public class " + className + " : " + txtBaseClass.Text.Trim());
            swBE.WriteLine("    {");
            String defaultValue = String.Empty;
            foreach (pMap item in fieldList)
            {
                #region set data types
                switch (item.Type.ToUpper())
                {
                    case "UNIQUEIDENTIFIER":
                        item.Type = "uniqueidentifier";
                        break;
                    case "INT32":
                        item.Type = "Int32";
                        defaultValue = "0";
                        break;
                    case "INT16":
                        item.Type = "Int16";
                        defaultValue = "0";
                        break;
                    case "INT64":
                        item.Type = "Int64";
                        defaultValue = "0";
                        break;
                    case "INT":
                        item.Type = "Int32";
                        defaultValue = "0";
                        break;
                    case "BIGINT":
                        item.Type = "Int64";
                        defaultValue = "0";
                        break;
                    case "BOOLEAN":
                        item.Type = "Boolean";
                        defaultValue = "false";
                        break;
                    case "BOOL":
                        item.Type = "Boolean";
                        defaultValue = "false";
                        break;
                    case "BIT":
                        item.Type = "Boolean";
                        defaultValue = "false";
                        break;
                    case "DECIMAL":
                        item.Type = "Decimal";
                        defaultValue = "0.0M";
                        break;
                    case "STRING":
                        item.Type = "String";
                        defaultValue = "String.Empty";
                        break;
                    case "NVARCHAR":
                        item.Type = "String";
                        defaultValue = "String.Empty";
                        break;
                    case "VARCHAR":
                        item.Type = "String";
                        defaultValue = "String.Empty";
                        break;
                    case "MONEY":
                        item.Type = "Decimal";
                        defaultValue = "0.0M";
                        break;
                    case "DOUBLE":
                        item.Type = "Double";
                        defaultValue = "0.0M";
                        break;
                    case "FLOAT":
                        item.Type = "Double";
                        defaultValue = "0.0M";
                        break;
                    case "BYTE":
                        item.Type = "Byte";
                        break;
                    case "BYTE[]":
                        item.Type = "Image";
                        break;
                    case "SHORT":
                        item.Type = "Short";
                        defaultValue = "0";
                        break;
                    case "LONG":
                        item.Type = "Long";
                        defaultValue = "0";
                        break;
                    case "CHAR":
                        item.Type = "Char";
                        defaultValue = "String.Empty";
                        break;
                    case "DATETIME":
                        item.Type = "DateTime";
                        defaultValue = "DateTime.MinValue";
                        break;
                    case "DATE":
                        item.Type = "DateTime";
                        defaultValue = "DateTime.MinValue";
                        break;
                    case "TIME":
                        item.Type = "Time";
                        defaultValue = "DateTime.MinValue";
                        break;
                    case "IMAGE":
                        item.Type = "Image";
                        break;
                    case "VARBINARY":
                        item.Type = "Image";
                        break;
                    default:
                        MessageBox.Show("Potential problem with data type in BE field: " + item.Name, item.Type);
                        break;
                }
                #endregion

                swBE.WriteLine("        private " + item.Type + " _" + item.Name + " = " + defaultValue + ";");
                swBE.WriteLine("        public " + item.Type + " " + item.Name);
                swBE.WriteLine("        {");
                swBE.WriteLine("            get { return _" + item.Name + "; }");
                swBE.WriteLine("            set { _" + item.Name + " = value; }");
                swBE.WriteLine("        }");
                swBE.WriteLine("");
            }
            swBE.WriteLine("    }");
            swBE.WriteLine("}");
            swBE.Close();
            #endregion
        }
        #endregion

        #region  BLL code
        private void chkBLL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBLL.CheckState == CheckState.Checked)
                grpBL.Visible = true;
            else
                grpBL.Visible = false;
        }
        private void btnBLLFile_Click(object sender, EventArgs e)
        {
            GetBLLFile();
        }
        private void GetBLLFile()
        {
            #region Set write folder
            if (fbTarget.ShowDialog() == DialogResult.OK)
            {
                if (fbTarget.SelectedPath.Trim() != string.Empty)
                    BLLfolderName = fbTarget.SelectedPath.Trim();
                else
                    BLLfolderName = @"C:\";
            }
            else
                BLLfolderName = @"C:\";
            txtBLLFile.Text = fbTarget.SelectedPath;
            #endregion
        }
        private void CreateBLLClass()
        {
            #region Write BLL DataConnection class
            if (!File.Exists(BLLfolderName + "\\DataConnection.cs"))
            {
                using (StreamWriter swd = File.CreateText(BLLfolderName + "\\DataConnection.cs"))
                {
                    swd.WriteLine("using System;");
                    swd.WriteLine("using System.Data;");
                    swd.WriteLine("using Constants = " + txtBENamespace.Text.Trim() + ".Constants;");
                    swd.WriteLine("using DAO = " + txtDALNamespace.Text.Trim() + ".DataConnection;");
                    swd.WriteLine("");
                    swd.WriteLine("namespace " + txtBLLNamespace.Text.Trim());
                    swd.WriteLine("{");
                    swd.WriteLine("    public class DataConnection");
                    swd.WriteLine("    {");
                    swd.WriteLine("        public static AdoDotNetDataProvider DataProvider");
                    swd.WriteLine("        {");
                    swd.WriteLine("            get { return DAO.DataProvider; }");
                    swd.WriteLine("            set { DAO.DataProvider = value; }");
                    swd.WriteLine("        }");
                    swd.WriteLine("");
                    swd.WriteLine("        public static String DefaultConnectionString");
                    swd.WriteLine("        {");
                    swd.WriteLine("            get { return DAO.DefaultConnectionString; }");
                    swd.WriteLine("            set { DAO.DefaultConnectionString = value; }");
                    swd.WriteLine("        }");
                    swd.WriteLine("");
                    swd.WriteLine("        public static String ConnectionString2");
                    swd.WriteLine("        {");
                    swd.WriteLine("            get { return DAO.ConnectionString2; }");
                    swd.WriteLine("            set { DAO.ConnectionString2 = value; }");
                    swd.WriteLine("        }");
                    swd.WriteLine("    }");
                    swd.WriteLine("}");
                }
            }
            #endregion

            #region Write BLL classes
            StreamWriter sw = File.CreateText(BLLfolderName + "\\" + txtPrefixBLL.Text.Trim() + pluralClassName + ".cs");
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using Constants = " + txtBENamespace.Text.Trim() + ".Constants;");
            sw.WriteLine("using BEs        = " + txtBENamespace.Text.Trim() + "." + className + ";");
            sw.WriteLine("using DAO       = " + txtDALNamespace.Text.Trim() + "." + pluralClassName + ";");
            sw.WriteLine("");
            sw.WriteLine("namespace " + txtBLLNamespace.Text.Trim());
            sw.WriteLine("{");
            sw.WriteLine("    public class " + pluralClassName);
            sw.WriteLine("    {");

            #region Finds
            sw.WriteLine("        public static List<BEs> Find()");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectAll();");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static BEs FindByID(Int32 " + key + ")");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectById(" + key + ");");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static List<BEs> FindBy(Constants." + className + "Fields fieldName, object value)");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectBy(fieldName, value);");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static List<BEs> FindBy(Constants." + className + "Fields fieldName, String operatorType, object value)");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectBy(fieldName, operatorType, value);");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static List<BEs> FindBy(Constants." + className + "Fields fieldName1, String operatorType1, object value1, Constants." + className + "Fields fieldName2, String operatorType2, object value2)");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectBy(fieldName1, operatorType1, value1, fieldName2, operatorType2, value2);");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static List<BEs> FindBy(Constants." + className + "Fields fieldName1, String operatorType1, object value1, String joinOperand, Constants." + className + "Fields fieldName2, String operatorType2, object value2)");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectBy(fieldName1, operatorType1, value1, joinOperand, fieldName2, operatorType2, value2);");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static List<BEs> FindBy(Constants." + className + "Fields fieldName1, String operatorType1, object value1, String joinOperand1, Constants." + className + "Fields fieldName2, String operatorType2, object value2, String joinOperand2, Constants." + className + "Fields fieldName3, String operatorType3, object value3)");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectBy(fieldName1, operatorType1, value1, joinOperand1, fieldName2, operatorType2, value2, joinOperand2, fieldName3, operatorType3, value3);");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static List<BEs> FindBy(Constants." + className + "Fields fieldName, object value, Constants." + className + "Fields sortField)");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectBy(fieldName, value, sortField);");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        public static List<BEs> FindBy(Constants." + className + "Fields fieldName, String operatorType, object value, Constants." + className + "Fields sortField)");
            sw.WriteLine("        {");
            sw.WriteLine("            return DAO.SelectBy(fieldName, operatorType, value, sortField);");
            sw.WriteLine("        }");
            sw.WriteLine("");
            #endregion

            #region Save
            sw.WriteLine("        public static Boolean Save(ref BEs " + lowerClass + ")");
            sw.WriteLine("        {");
            sw.WriteLine("            Boolean result = false;");
            sw.WriteLine("            if (DAO.SelectById(" + lowerClass + "." + key + ") != null)");
            sw.WriteLine("            {");
            sw.WriteLine("                result = DAO.Update(" + lowerClass + ");");
            sw.WriteLine("            }");
            sw.WriteLine("            else");
            sw.WriteLine("            {");
            sw.WriteLine("                result = DAO.Insert(ref " + lowerClass + ");");
            sw.WriteLine("            }");
            sw.WriteLine("            return result;");
            sw.WriteLine("        }");
            #endregion
            sw.WriteLine(" ");

            #region Delete
            sw.WriteLine("        public static Boolean Delete(BEs " + lowerClass + ")");
            sw.WriteLine("        {");
            sw.WriteLine("            Boolean result = false;");
            sw.WriteLine("            return result  = DAO.Delete(" + lowerClass + ");");
            sw.WriteLine("        }");

            sw.WriteLine("        public static Boolean DeleteAll()");
            sw.WriteLine("        {");
            sw.WriteLine("            Boolean result = false;");
            sw.WriteLine("            return result  = DAO.DeleteAll();");
            sw.WriteLine("        }");
            #endregion

            sw.WriteLine("    }");
            sw.WriteLine("}");
            sw.Close();
            sw.Dispose();
            #endregion
        }
        #endregion

        #region DAL code
        private void chkDAL_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDAL.CheckState == CheckState.Checked)
            {
                grpDAL.Visible = true;
                chkFilterMgr.Visible = true;
                chkDevExpress.Visible = true;
            }
            else
            {
                grpDAL.Visible = false;
                chkFilterMgr.Visible = false;
                chkDevExpress.Visible = false;
            }

        }
        private void btnDALFile_Click(object sender, EventArgs e)
        {
            GetDALFile();
        }
        private void GetDALFile()
        {
            #region Set write folder
            if (fbTarget.ShowDialog() == DialogResult.OK)
            {
                if (fbTarget.SelectedPath.Trim() != string.Empty)
                    DALfolderName = fbTarget.SelectedPath.Trim();
                else
                    DALfolderName = @"C:\";
            }
            else
                DALfolderName = @"C:\";
            #endregion
            txtDALFile.Text = fbTarget.SelectedPath;
        }
        private void chkInheritDAO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInheritDAO.CheckState == CheckState.Checked)
                txtDAO.Visible = true;
            else
                txtDAO.Visible = false;
        }
        private void CreateDALClass()
        {
            #region Write DAL classes
            StreamWriter swDAL = File.CreateText(DALfolderName + "\\" + txtPrefixDAL.Text.Trim() + pluralClassName + ".cs");
            #region Header & private properties
            swDAL.WriteLine("using System;");
            swDAL.WriteLine("using System.Data;");
            swDAL.WriteLine("using System.Drawing;");
            swDAL.WriteLine("using System.Collections.Generic;");
            swDAL.WriteLine("using " + txtBENamespace.Text.Trim() + ";");
            swDAL.WriteLine("using Constants = " + txtBENamespace.Text.Trim() + ".Constants;");
            swDAL.WriteLine("using BE = " + txtBENamespace.Text.Trim() + "." + className + ";");
            swDAL.WriteLine("");
            swDAL.WriteLine("namespace " + txtDALNamespace.Text.Trim());
            swDAL.WriteLine("{");
            if (txtDAO.Text.Trim() == string.Empty)
                swDAL.WriteLine("    public class " + pluralClassName);
            else
                swDAL.WriteLine("    public class " + pluralClassName + " : " + txtDAO.Text.Trim());
            swDAL.WriteLine("    {");
            swDAL.WriteLine("        #region private properties");
            swDAL.WriteLine("        // These must match the SQL data table structures");
            swDAL.WriteLine("        private static String _tableName = " + '"' + className + "\"" + ";");

            #region Loop thru each line of BE class /text file and write appropriate matching line in this DAL class
            foreach (pMap item in fieldList)
            {
                swDAL.WriteLine("        private static String _" + item.Name + " = \"" + item.Name + '"' + ";");
            }
            #endregion

            swDAL.WriteLine("        #endregion");
            swDAL.WriteLine("");
            #endregion

            swDAL.WriteLine("        #region Selects");
            #region Write Select methods
            #region Select All
            swDAL.WriteLine("        public static List<BE> SelectAll()");
            swDAL.WriteLine("        {");
            swDAL.WriteLine("             List<BE> result = new List<BE>();");
            swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
            swDAL.WriteLine("             {");
            swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName;");
            swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
            swDAL.WriteLine("                 connection.Open();");
            swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
            swDAL.WriteLine("                 while (reader.Read())");
            swDAL.WriteLine("                 {");
            swDAL.WriteLine("                     result.Add(ToObject(reader));");
            swDAL.WriteLine("                 }");
            swDAL.WriteLine("             }");
            swDAL.WriteLine("             return LoadObjects(result);");
            swDAL.WriteLine("        }");
            swDAL.WriteLine("");
            #endregion

            #region SelectByID
            swDAL.WriteLine("       public static BE SelectById(Int32 id)");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("           BE result = new BE();");
            swDAL.WriteLine("           using (IDbConnection connection = DataConnection.Connection())");
            swDAL.WriteLine("           {");
            swDAL.WriteLine("               String sqlQuery = String.Format(\"SELECT * FROM {0} WHERE {1}=@" + key + "\", _tableName, _" + key + ");");
            swDAL.WriteLine("               IDbCommand command = DataConnection.Command(connection, sqlQuery);");
            swDAL.WriteLine("               AddParameter(command, \"" + key + "\", id );");
            swDAL.WriteLine("               connection.Open();");
            swDAL.WriteLine("               IDataReader reader = command.ExecuteReader();");
            swDAL.WriteLine("               while (reader.Read())");
            swDAL.WriteLine("               {");
            swDAL.WriteLine("                   result = ToObject(reader);");
            swDAL.WriteLine("               }");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("           if (result." + key + " > 0)");
            swDAL.WriteLine("           {");
            swDAL.WriteLine("               return LoadObject(result);");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("           else");
            swDAL.WriteLine("           {");
            swDAL.WriteLine("               return null;");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("       }");
            swDAL.WriteLine("       #endregion");
            swDAL.WriteLine("");
            #endregion

            #region SelectBy FieldName
            if (chkSort.CheckState == CheckState.Checked)
            {
                swDAL.WriteLine("       #region Select by FieldName");
                swDAL.WriteLine("       public static List<BE> SelectBy(Constants." + className + "Fields fieldName, object value)");
                swDAL.WriteLine("       {");
                swDAL.WriteLine("             List<BE> result = new List<BE>();");
                swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
                swDAL.WriteLine("             {");
                swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName + \" WHERE \" + fieldName.ToString() + \" = @value\";");
                swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
                swDAL.WriteLine("                 AddParameter(command, \"@value\", value);");
                swDAL.WriteLine("                 connection.Open();");
                swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
                swDAL.WriteLine("                 while (reader.Read())");
                swDAL.WriteLine("                 {");
                swDAL.WriteLine("                     result.Add(ToObject(reader));");
                swDAL.WriteLine("                 }");
                swDAL.WriteLine("             }");
                swDAL.WriteLine("             return LoadObjects(result);");
                swDAL.WriteLine("        }");
                swDAL.WriteLine("");

                swDAL.WriteLine("       public static List<BE> SelectBy(Constants." + className + "Fields fieldName, String operatorType, object value)");
                swDAL.WriteLine("       {");
                swDAL.WriteLine("             List<BE> result = new List<BE>();");
                swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
                swDAL.WriteLine("             {");
                swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName + \" WHERE \" + fieldName.ToString() + \" \" + operatorType + \" @value\";");
                swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
                swDAL.WriteLine("                 AddParameter(command, \"@value\", value);");
                swDAL.WriteLine("                 connection.Open();");
                swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
                swDAL.WriteLine("                 while (reader.Read())");
                swDAL.WriteLine("                 {");
                swDAL.WriteLine("                     result.Add(ToObject(reader));");
                swDAL.WriteLine("                 }");
                swDAL.WriteLine("             }");
                swDAL.WriteLine("             return LoadObjects(result);");
                swDAL.WriteLine("        }");
                swDAL.WriteLine("");

                swDAL.WriteLine("       public static List<BE> SelectBy(Constants." + className + "Fields fieldName1, String operatorType1, object value1, Constants." + className + "Fields fieldName2, String operatorType2, object value2)");
                swDAL.WriteLine("       {");
                swDAL.WriteLine("             List<BE> result = new List<BE>();");
                swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
                swDAL.WriteLine("             {");
                swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName + \" WHERE \" + fieldName1.ToString() + \" \" + operatorType1 + \" @value1 AND \" + fieldName2.ToString() + \" \" + operatorType2 + \" @value2\";");
                swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
                swDAL.WriteLine("                 AddParameter(command, \"@value1\", value1);");
                swDAL.WriteLine("                 AddParameter(command, \"@value2\", value2);");
                swDAL.WriteLine("                 connection.Open();");
                swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
                swDAL.WriteLine("                 while (reader.Read())");
                swDAL.WriteLine("                 {");
                swDAL.WriteLine("                     result.Add(ToObject(reader));");
                swDAL.WriteLine("                 }");
                swDAL.WriteLine("             }");
                swDAL.WriteLine("             return LoadObjects(result);");
                swDAL.WriteLine("        }");
                swDAL.WriteLine("");

                swDAL.WriteLine("       public static List<BE> SelectBy(Constants." + className + "Fields fieldName1, String operatorType1, object value1, String joinOperand, Constants." + className + "Fields fieldName2, String operatorType2, object value2)");
                swDAL.WriteLine("       {");
                swDAL.WriteLine("             List<BE> result = new List<BE>();");
                swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
                swDAL.WriteLine("             {");
                swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName + \" WHERE \" + fieldName1.ToString() + \" \" + operatorType1 + \" @value1 \"+ \" \" + joinOperand + \" \" + fieldName2.ToString() + \" \" + operatorType2 + \" @value2\";");
                swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
                swDAL.WriteLine("                 AddParameter(command, \"@value1\", value1);");
                swDAL.WriteLine("                 AddParameter(command, \"@value2\", value2);");
                swDAL.WriteLine("                 connection.Open();");
                swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
                swDAL.WriteLine("                 while (reader.Read())");
                swDAL.WriteLine("                 {");
                swDAL.WriteLine("                     result.Add(ToObject(reader));");
                swDAL.WriteLine("                 }");
                swDAL.WriteLine("             }");
                swDAL.WriteLine("             return LoadObjects(result);");
                swDAL.WriteLine("        }");
                swDAL.WriteLine("");

                swDAL.WriteLine("       public static List<BE> SelectBy(Constants." + className + "Fields fieldName1, String operatorType1, object value1, String joinOperand1, Constants." + className + "Fields fieldName2, String operatorType2, object value2, String joinOperand2, Constants." + className + "Fields fieldName3, String operatorType3, object value3)");
                swDAL.WriteLine("       {");
                swDAL.WriteLine("             List<BE> result = new List<BE>();");
                swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
                swDAL.WriteLine("             {");
                swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName + \" WHERE \" + fieldName1.ToString() + \" \" + operatorType1 + \" @value1 \"+ \" \" + joinOperand1 + \" \" + fieldName2.ToString() + \" \" + operatorType2 + \" @value2  \"+ \" \" + joinOperand2 + \" \" + fieldName3.ToString() + \" \" + operatorType3 + \" @value3\";");
                swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
                swDAL.WriteLine("                 AddParameter(command, \"@value1\", value1);");
                swDAL.WriteLine("                 AddParameter(command, \"@value2\", value2);");
                swDAL.WriteLine("                 AddParameter(command, \"@value3\", value3);");
                swDAL.WriteLine("                 connection.Open();");
                swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
                swDAL.WriteLine("                 while (reader.Read())");
                swDAL.WriteLine("                 {");
                swDAL.WriteLine("                     result.Add(ToObject(reader));");
                swDAL.WriteLine("                 }");
                swDAL.WriteLine("             }");
                swDAL.WriteLine("             return LoadObjects(result);");
                swDAL.WriteLine("        }");
                swDAL.WriteLine("");

            }
            swDAL.WriteLine("       #endregion");
            swDAL.WriteLine("");
            #endregion

            #region Select By FieldName (Sorted)
            if (chkSort.CheckState == CheckState.Checked)
            {
                swDAL.WriteLine("       #region Select by FieldName (Sorted)");
                swDAL.WriteLine("       public static List<BE> SelectBy(Constants." + className + "Fields fieldName, object value, Constants." + className + "Fields sortField)");
                swDAL.WriteLine("       {");
                swDAL.WriteLine("             List<BE> result = new List<BE>();");
                swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
                swDAL.WriteLine("             {");
                swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName + \" WHERE \" + fieldName.ToString() + \" = @value ORDER BY \" + sortField.ToString();");
                swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
                swDAL.WriteLine("                 AddParameter(command, \"@value\", value);");
                swDAL.WriteLine("                 connection.Open();");
                swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
                swDAL.WriteLine("                 while (reader.Read())");
                swDAL.WriteLine("                 {");
                swDAL.WriteLine("                     result.Add(ToObject(reader));");
                swDAL.WriteLine("                 }");
                swDAL.WriteLine("             }");
                swDAL.WriteLine("             return LoadObjects(result);");
                swDAL.WriteLine("        }");
                swDAL.WriteLine("");

                swDAL.WriteLine("       public static List<BE> SelectBy(Constants." + className + "Fields fieldName, String operatorType, object value, Constants." + className + "Fields sortField)");
                swDAL.WriteLine("       {");
                swDAL.WriteLine("             List<BE> result = new List<BE>();");
                swDAL.WriteLine("             using (IDbConnection connection = DataConnection.Connection())");
                swDAL.WriteLine("             {");
                swDAL.WriteLine("                 String sqlQuery = \"SELECT * FROM \" + _tableName + \" WHERE \" + fieldName.ToString() + \" \" + operatorType + \" \" + @value + \" ORDER BY \" + sortField.ToString();");
                swDAL.WriteLine("                 IDbCommand command = DataConnection.Command(connection, sqlQuery);");
                swDAL.WriteLine("                 AddParameter(command, \"@value\", value);");
                swDAL.WriteLine("                 connection.Open();");
                swDAL.WriteLine("                 IDataReader reader = command.ExecuteReader();");
                swDAL.WriteLine("                 while (reader.Read())");
                swDAL.WriteLine("                 {");
                swDAL.WriteLine("                     result.Add(ToObject(reader));");
                swDAL.WriteLine("                 }");
                swDAL.WriteLine("             }");
                swDAL.WriteLine("             return LoadObjects(result);");
                swDAL.WriteLine("        }");
                swDAL.WriteLine("");
            }
            swDAL.WriteLine("       #endregion");
            #endregion

            #endregion
            swDAL.WriteLine("");

            swDAL.WriteLine("       #region Insert/Update/Delete ");
            #region Write Inserts/Updates/Delete
            #region Insert
            swDAL.WriteLine("       public static Boolean Insert(ref BE" + " " + lowerClass + ")");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("           Int32 result = 0;");
            swDAL.WriteLine("           using (IDbConnection connection = DataConnection.Connection())");
            swDAL.WriteLine("           {");
            swDAL.WriteLine("               String sqlQuery = \"INSERT INTO \" + _tableName + \" (\"");
            fCount = 1;
            foreach (pMap item in fieldList)
            {
                if (fCount > 1)
                {
                    if (fCount == fieldList.Count)
                        swDAL.WriteLine("                               + _" + item.Name);
                    else
                        swDAL.WriteLine("                               + _" + item.Name + " + \", \"");
                }
                fCount++;
            }
            swDAL.Write("                               + \") VALUES (");
            fCount = 1;
            foreach (pMap item in fieldList)
            {
                if (fCount > 1)
                {
                    if (fCount == fieldList.Count)
                        swDAL.Write("@" + item.Name);
                    else
                        swDAL.Write("@" + item.Name + ", ");
                }
                fCount++;
            }
            swDAL.WriteLine(") SET @" + key + " = SCOPE_IDENTITY();\";");
            swDAL.WriteLine("");
            swDAL.WriteLine("               IDbCommand command = DataConnection.Command(connection, sqlQuery);");
            fCount = 1;
            foreach (pMap item in fieldList)
            {
                if (fCount > 1)
                    swDAL.WriteLine("               AddParameter(command, \"" + item.Name + "\", " + lowerClass + "." + item.Name + ");");

                fCount++;
            }
            swDAL.WriteLine("               AddParameter(command, \"" + key + "\", null, DbType.Int32, ParameterDirection.Output);");
            swDAL.WriteLine("               IDataParameter IdParameter = (IDataParameter)command.Parameters[\"" + key + "\"];");
            swDAL.WriteLine("               connection.Open();");
            swDAL.WriteLine("               result = command.ExecuteNonQuery();");
            swDAL.WriteLine("               " + lowerClass + "." + key + " = (int)IdParameter.Value;");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("           return result > 0;");
            swDAL.WriteLine("       }");
            swDAL.WriteLine("");
            #endregion

            #region Update
            swDAL.WriteLine("       public static Boolean Update(BE" + " " + lowerClass + ")");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("           Int32 result = 0;");
            swDAL.WriteLine("           using (IDbConnection connection = DataConnection.Connection())");
            swDAL.WriteLine("           {");
            fCount = 1;
            foreach (pMap item in fieldList)
            {
                if (fCount == 1)
                    swDAL.WriteLine("               String sqlQuery = \"UPDATE \" + " + _tableName + " + \" SET \"");
                else
                {
                    if (fCount == fieldList.Count)
                        swDAL.WriteLine("                               + _" + item.Name + " + \"=@" + item.Name + " \"" + Environment.NewLine
                                      + "                               + \"WHERE \" + _" + key + " + \"=@" + key + "\";");
                    else
                        swDAL.WriteLine("                               + _" + item.Name + " + \"=@" + item.Name + ", \"");
                }
                fCount++;
            }
            swDAL.WriteLine("");
            swDAL.WriteLine("                IDbCommand command = DataConnection.Command(connection, sqlQuery);");
            foreach (pMap item in fieldList)
            {
                swDAL.WriteLine("                AddParameter(command, \"" + item.Name + "\", " + lowerClass + "." + item.Name + ");");
            }
            swDAL.WriteLine("                connection.Open();");
            swDAL.WriteLine("                result = command.ExecuteNonQuery();");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("           return result > 0;");
            swDAL.WriteLine("       }");
            swDAL.WriteLine("");
            #endregion

            #region Delete
            swDAL.WriteLine("       public static Boolean Delete(BE" + " " + lowerClass + ")");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("           Int32 result = 0;");
            swDAL.WriteLine("           using (IDbConnection connection = DataConnection.Connection())");
            swDAL.WriteLine("           {");
            swDAL.WriteLine("               String sqlQuery    = String.Format(\"DELETE FROM {0} WHERE {1}=@" + key + "\", _tableName, _" + key + ");");
            swDAL.WriteLine("               IDbCommand command = DataConnection.Command(connection, sqlQuery);");
            swDAL.WriteLine("               AddParameter(command, \"" + key + "\", " + lowerClass + "." + key + ");");
            swDAL.WriteLine("               connection.Open();");
            swDAL.WriteLine("               result = command.ExecuteNonQuery();");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("           return result > 0;");
            swDAL.WriteLine("       }");

            swDAL.WriteLine("       public static Boolean DeleteAll()");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("           Int32 result = 0;");
            swDAL.WriteLine("           using (IDbConnection connection = DataConnection.Connection())");
            swDAL.WriteLine("           {");
            swDAL.WriteLine("               String sqlQuery    = \"DELETE FROM \" + _tableName;");
            swDAL.WriteLine("               IDbCommand command = DataConnection.Command(connection, sqlQuery);");
            swDAL.WriteLine("               connection.Open();");
            swDAL.WriteLine("               result = command.ExecuteNonQuery();");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("           return result > 0;");
            swDAL.WriteLine("       }");

            #endregion

            swDAL.WriteLine("       #endregion");
            #endregion
            swDAL.WriteLine("");

            swDAL.WriteLine("       #region Conversions");
            #region Write Conversion methods
            swDAL.WriteLine("       private static BE ToObject(IDataReader reader)");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("            BE result = new BE();");
            swDAL.WriteLine("            result." + key + " = Int32.Parse(reader[_" + key + "].ToString());");
            fCount = 1;
            foreach (pMap item in fieldList)
            {
                if (fCount > 1)
                {
                    swDAL.WriteLine("            if (reader[_" + item.Name + "] != System.DBNull.Value)");
                    switch (item.Type.ToLower())
                    {
                        case "int16":
                            swDAL.WriteLine("               result." + item.Name + " = Int16.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "int32":
                            swDAL.WriteLine("               result." + item.Name + " = Int32.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "int64":
                            swDAL.WriteLine("               result." + item.Name + " = Int64.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "uniqueidentifier":
                            swDAL.WriteLine("               result." + item.Name + " = Guid.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "string":
                            swDAL.WriteLine("               result." + item.Name + " = reader[_" + item.Name + "].ToString();");
                            break;
                        case "nvarchar":
                            swDAL.WriteLine("               result." + item.Name + " = reader[_" + item.Name + "].ToString();");
                            break;
                        case "varchar":
                            swDAL.WriteLine("               result." + item.Name + " = reader[_" + item.Name + "].ToString();");
                            break;
                        case "boolean":
                            swDAL.WriteLine("               result." + item.Name + " = Boolean.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "bit":
                            swDAL.WriteLine("               result." + item.Name + " = Boolean.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "decimal":
                            swDAL.WriteLine("               result." + item.Name + " = Decimal.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "double":
                            swDAL.WriteLine("               result." + item.Name + " = Double.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "float":
                            swDAL.WriteLine("               result." + item.Name + " = float.Parse(reader[_" + item.Name + "].ToString());\"");
                            break;
                        case "money":
                            swDAL.WriteLine("               result." + item.Name + " = Decimal.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "datetime":
                            swDAL.WriteLine("               result." + item.Name + " = DateTime.Parse(reader[_" + item.Name + "].ToString());");
                            break;
                        case "image":
                            swDAL.WriteLine("               result." + item.Name + " = byteArrayToImage((Byte[])(reader[_" + item.Name + "]));");
                            break;
                        default:
                            swDAL.WriteLine("               result." + item.Name + " = reader[_" + item.Name + "].ToString();");
                            break;
                    }
                }
                fCount++;
            }
            swDAL.WriteLine("            return result;");
            swDAL.WriteLine("       }");
            swDAL.WriteLine("");
            swDAL.WriteLine("       private static BE LoadObject(BE " + lowerClass + ")");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("           BE result = (BE)" + lowerClass + ";");
            swDAL.WriteLine("           return result;");
            swDAL.WriteLine("       }");
            swDAL.WriteLine("");
            swDAL.WriteLine("       private static List<BE> LoadObjects(List<BE> " + lowerClass + "s)");
            swDAL.WriteLine("       {");
            swDAL.WriteLine("           List<BE> result = new List<BE>();");
            swDAL.WriteLine("           foreach (BE o in " + lowerClass + "s)");
            swDAL.WriteLine("           {");
            swDAL.WriteLine("               result.Add(LoadObject(o));");
            swDAL.WriteLine("           }");
            swDAL.WriteLine("           return result;");
            swDAL.WriteLine("       }");
            swDAL.WriteLine("       #endregion");
            swDAL.WriteLine("    }");
            swDAL.WriteLine("}");
            #endregion
            swDAL.Close();
            #endregion

            #region  Write DAL DataConnection Class
            if (!File.Exists(DALfolderName + "\\DataConnection.cs"))
            {
                StreamWriter swDAC = File.CreateText(DALfolderName + "\\DataConnection.cs");
                swDAC.WriteLine("using System;");
                swDAC.WriteLine("using System.Configuration;");
                swDAC.WriteLine("using System.Data;");
                swDAC.WriteLine("using " + txtBENamespace.Text.Trim() + ".Constants;");
                swDAC.WriteLine(" ");
                swDAC.WriteLine("namespace " + txtDALNamespace.Text.Trim());
                swDAC.WriteLine("{");
                swDAC.WriteLine("    public class DataConnection");
                swDAC.WriteLine("    {");
                swDAC.WriteLine("        #region variables");
                swDAC.WriteLine("        private static AdoDotNetDataProvider _dataProvider;");
                swDAC.WriteLine("        private static String _connectionString;");
                swDAC.WriteLine("        #endregion");
                swDAC.WriteLine(" ");

                swDAC.WriteLine("       #region Properties");
                swDAC.WriteLine("       public static AdoDotNetDataProvider DataProvider");
                swDAC.WriteLine("       {");
                swDAC.WriteLine("           get { return _dataProvider; }");
                swDAC.WriteLine("           set { _dataProvider = value; }");
                swDAC.WriteLine("       }");
                swDAC.WriteLine(" ");

                swDAC.WriteLine("       public static String ConnectionString");
                swDAC.WriteLine("       {");
                swDAC.WriteLine("           get { return _connectionString; }");
                swDAC.WriteLine("           set { _connectionString = value; }");
                swDAC.WriteLine("       }");
                swDAC.WriteLine("       #endregion");
                swDAC.WriteLine(" ");
                swDAC.WriteLine("       #region Methods");
                swDAC.WriteLine("       public static IDbConnection Connection()");
                swDAC.WriteLine("       {");
                swDAC.WriteLine("           string ConnectionString = ConfigurationManager.ConnectionStrings[Aqui a tag do Web.Config].ToString();");
                swDAC.WriteLine("           switch (DataProvider)");
                swDAC.WriteLine("           {");
                swDAC.WriteLine("               case AdoDotNetDataProvider.OleDb:");
                swDAC.WriteLine("                   return new System.Data.OleDb.OleDbConnection(ConnectionString);");
                swDAC.WriteLine("               case AdoDotNetDataProvider.Odbc:");
                swDAC.WriteLine("                   return new System.Data.Odbc.OdbcConnection(ConnectionString);");
                swDAC.WriteLine("               default:");
                swDAC.WriteLine("                   return new System.Data.SqlClient.SqlConnection(ConnectionString);");
                swDAC.WriteLine("           }");
                swDAC.WriteLine("       }");
                swDAC.WriteLine(" ");

                swDAC.WriteLine("       public static IDbCommand Command()");
                swDAC.WriteLine("       {");
                swDAC.WriteLine("           return Command(null, null, CommandType.Text);");
                swDAC.WriteLine("       }");
                swDAC.WriteLine(" ");

                swDAC.WriteLine("       public static IDbCommand Command(IDbConnection connection, string commandText)");
                swDAC.WriteLine("       {");
                swDAC.WriteLine("           return Command(connection, commandText, CommandType.Text);");
                swDAC.WriteLine("       }");
                swDAC.WriteLine(" ");

                swDAC.WriteLine("       public static IDbCommand Command(IDbConnection connection, string commandText, CommandType commandType)");
                swDAC.WriteLine("       {");
                swDAC.WriteLine("           IDbCommand command;");
                swDAC.WriteLine("           switch (DataProvider)");
                swDAC.WriteLine("           {");
                swDAC.WriteLine("               case AdoDotNetDataProvider.OleDb:");
                swDAC.WriteLine("                   command = new System.Data.OleDb.OleDbCommand();");
                swDAC.WriteLine("                   break;");
                swDAC.WriteLine("               case AdoDotNetDataProvider.Odbc:");
                swDAC.WriteLine("                   command = new System.Data.Odbc.OdbcCommand();");
                swDAC.WriteLine("                   break;");
                swDAC.WriteLine("               default:");
                swDAC.WriteLine("                   command = new System.Data.SqlClient.SqlCommand();");
                swDAC.WriteLine("                   break;");
                swDAC.WriteLine("           }");
                swDAC.WriteLine("           command.Connection  = connection;");
                swDAC.WriteLine("           command.CommandType = commandType;");
                swDAC.WriteLine("           command.CommandText = commandText;");
                swDAC.WriteLine("           return command;");
                swDAC.WriteLine("       }");
                swDAC.WriteLine("       #endregion");
                swDAC.WriteLine("   }");
                swDAC.WriteLine("}");
                swDAC.Close();
                swDAC.Dispose();
            }
            #endregion

            #region Write DAL DataAccessObject Class
            if (txtDAO.Text.Trim() != string.Empty)
            {
                if (!File.Exists(DALfolderName + "\\" + txtDAO.Text.Trim() + ".cs"))
                {
                    StreamWriter swDAO = File.CreateText(DALfolderName + "\\" + txtDAO.Text.Trim() + ".cs");
                    swDAO.WriteLine("using System;");
                    swDAO.WriteLine("using System.Data;");
                    swDAO.WriteLine("using System.IO;");
                    swDAO.WriteLine("using System.Drawing;");
                    swDAO.WriteLine("using " + txtBENamespace.Text.Trim() + ";");
                    swDAO.WriteLine(" ");
                    swDAO.WriteLine("namespace " + txtDALNamespace.Text.Trim());
                    swDAO.WriteLine("{");
                    swDAO.WriteLine("   public abstract class DataAccessObject");
                    swDAO.WriteLine("   {");
                    swDAO.WriteLine("        #region Methods");
                    swDAO.WriteLine("        public static void AddParameter(IDbCommand command, String parameterName, Object value)");
                    swDAO.WriteLine("        {");
                    swDAO.WriteLine("            IDataParameter parameter = command.CreateParameter();");
                    swDAO.WriteLine("            parameter.ParameterName = parameterName;");
                    swDAO.WriteLine("            if (value != null)");
                    swDAO.WriteLine("            {");
                    swDAO.WriteLine("                if (value.GetType() == typeof(DateTime))");
                    swDAO.WriteLine("                {");
                    swDAO.WriteLine("                    if (DateTime.Parse(value.ToString()) <= DateTime.Parse(\"1/01/1900 12:00:00 AM\"))");
                    swDAO.WriteLine("                        value = null;");
                    swDAO.WriteLine("                }");
                    swDAO.WriteLine("                else if (value.GetType() == typeof(System.Drawing.Bitmap))");
                    swDAO.WriteLine("                {");
                    swDAO.WriteLine("                   value = imageToByteArray((Image)(value));                           ");
                    swDAO.WriteLine("                }");
                    swDAO.WriteLine("                if (value == null)");
                    swDAO.WriteLine("                   parameter.Value = DBNull.Value;");
                    swDAO.WriteLine("                else");
                    swDAO.WriteLine("                   parameter.Value = value;");
                    swDAO.WriteLine(" ");
                    swDAO.WriteLine("                command.Parameters.Add(parameter);");
                    swDAO.WriteLine("            }");
                    swDAO.WriteLine("        }");
                    swDAO.WriteLine(" ");

                    swDAO.WriteLine("        public static void AddParameter(IDbCommand command, String parameterName, Object value, DbType databaseType)");
                    swDAO.WriteLine("        {");
                    swDAO.WriteLine("            IDataParameter parameter = command.CreateParameter();");
                    swDAO.WriteLine("            parameter.ParameterName = parameterName;");
                    swDAO.WriteLine("            if (value != null)");
                    swDAO.WriteLine("            {");
                    swDAO.WriteLine("                if (value.GetType() == typeof(DateTime))");
                    swDAO.WriteLine("                {");
                    swDAO.WriteLine("                    if (DateTime.Parse(value.ToString()) <= DateTime.Parse(\"1/01/1900 12:00:00 AM\"))");
                    swDAO.WriteLine("                        value = null;");
                    swDAO.WriteLine("                }");
                    swDAO.WriteLine("                else if (value.GetType() == typeof(System.Drawing.Bitmap))");
                    swDAO.WriteLine("                {");
                    swDAO.WriteLine("                    value = imageToByteArray((Image)(value));");
                    swDAO.WriteLine("                }");
                    swDAO.WriteLine("            }");
                    swDAO.WriteLine("            if (value == null)");
                    swDAO.WriteLine("               parameter.Value = DBNull.Value;");
                    swDAO.WriteLine("            else");
                    swDAO.WriteLine("               parameter.Value = value;");
                    swDAO.WriteLine("            parameter.DbType = databaseType;");
                    swDAO.WriteLine("            command.Parameters.Add(parameter);");
                    swDAO.WriteLine("        }");
                    swDAO.WriteLine(" ");

                    swDAO.WriteLine("        public static void AddParameter(IDbCommand command, String parameterName, Object value, DbType databaseType, ParameterDirection parameterDirection)");
                    swDAO.WriteLine("        {");
                    swDAO.WriteLine("            IDataParameter parameter = command.CreateParameter();");
                    swDAO.WriteLine("            parameter.ParameterName = parameterName;");
                    swDAO.WriteLine("            if (value == null)");
                    swDAO.WriteLine("               parameter.Value = DBNull.Value;");
                    swDAO.WriteLine("            else");
                    swDAO.WriteLine("               parameter.Value = value;");
                    swDAO.WriteLine("            parameter.DbType = databaseType;");
                    swDAO.WriteLine("            parameter.Direction = parameterDirection; ");
                    swDAO.WriteLine("            command.Parameters.Add(parameter);");
                    swDAO.WriteLine("        }");
                    swDAO.WriteLine(" ");

                    swDAO.WriteLine("        private static object GetReaderValue(IDataReader reader, String columnName)");
                    swDAO.WriteLine("        {");
                    swDAO.WriteLine("            if (reader[columnName] == System.DBNull.Value)");
                    swDAO.WriteLine("               return null;");
                    swDAO.WriteLine("            else");
                    swDAO.WriteLine("               return reader[columnName];");
                    swDAO.WriteLine("        }");
                    swDAO.WriteLine(" ");

                    swDAO.WriteLine("        public static Byte[] imageToByteArray(Image imageIn)");
                    swDAO.WriteLine("        {");
                    swDAO.WriteLine("            if (imageIn != null)");
                    swDAO.WriteLine("            {");
                    swDAO.WriteLine("               MemoryStream ms = new MemoryStream();");
                    swDAO.WriteLine("               imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);");
                    swDAO.WriteLine("               return ms.ToArray();");
                    swDAO.WriteLine("            }");
                    swDAO.WriteLine("            else");
                    swDAO.WriteLine("            {");
                    swDAO.WriteLine("               return null;");
                    swDAO.WriteLine("            }");
                    swDAO.WriteLine("        }");
                    swDAO.WriteLine(" ");

                    swDAO.WriteLine("        public static Image byteArrayToImage(Byte[] byteArrayIn)");
                    swDAO.WriteLine("        {");
                    swDAO.WriteLine("            if (byteArrayIn != null)");
                    swDAO.WriteLine("            {");
                    swDAO.WriteLine("               MemoryStream ms   = new MemoryStream(byteArrayIn);");
                    swDAO.WriteLine("               Image returnImage = Image.FromStream(ms);");
                    swDAO.WriteLine("               return returnImage;");
                    swDAO.WriteLine("            }");
                    swDAO.WriteLine("            else");
                    swDAO.WriteLine("            {");
                    swDAO.WriteLine("               return null;");
                    swDAO.WriteLine("            }");
                    swDAO.WriteLine("        }");
                    swDAO.WriteLine("    }");
                    swDAO.WriteLine("#endregion");
                    swDAO.WriteLine("}");
                    swDAO.Close();
                }
            }
            #endregion
        }
        #endregion

        #region GUI code
        private void chkGUI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGUI.CheckState == CheckState.Checked)
            {
                grpGUI.Visible = true;
                chkDevExpress.Checked = true;
            }
            else
            {
                grpGUI.Visible = false;
                chkDevExpress.Checked = false;
            }
        }
        private void btnGUIFile_Click(object sender, EventArgs e)
        {
            GetGUIFile();
        }
        private void GetGUIFile()
        {
            #region Set write folder
            if (fbTarget.ShowDialog() == DialogResult.OK)
            {
                if (fbTarget.SelectedPath.Trim() != string.Empty)
                    GUIfolderName = fbTarget.SelectedPath.Trim();
                else
                    GUIfolderName = @"C:\";
            }
            else
                GUIfolderName = @"C:\";
            #endregion
            txtGUIFile.Text = fbTarget.SelectedPath;
        }
        private void chkInheritGUI_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInheritGUI.CheckState == CheckState.Checked)
                txtGUI.Visible = true;
            else
                txtGUI.Visible = false;
        }
        private void CreateGUIClasses()
        {
            Boolean boolCRUDRESX = false;
            Boolean boolBROWSERESX = false;

            if (chkLayout.CheckState == CheckState.Checked)
                useLayoutControl = true;
            else
                useLayoutControl = false;

            Int16 nCounter = 0;
            parentName = txtParentName.Text.Trim();

            #region Write GUI Browse class
            StreamWriter swGUI = File.CreateText(GUIfolderName + "\\" + txtPrefixGUI.Text.Trim() + "Browse" + pluralClassName + ".cs");
            #region Header & private properties
            swGUI.WriteLine("using System;");
            swGUI.WriteLine("using System.IO;");
            if (chkFilterMgr.CheckState == CheckState.Checked)
                swGUI.WriteLine("using System.Text;");
            swGUI.WriteLine("using System.Collections.Generic;");
            swGUI.WriteLine("using System.Windows.Forms;");
            if (chkDevExpress.Checked)
                swGUI.WriteLine("using DevExpress.XtraEditors;");
            swGUI.WriteLine("using AmosG.PresentationLayer.UserInterface;");
            swGUI.WriteLine("using Utils = AmosG.BusinessTier.BusinessLogicLayer.Utils;");
            swGUI.WriteLine("using BE    = " + txtBENamespace.Text.Trim() + ";");
            swGUI.WriteLine("using BL    = " + txtBLLNamespace.Text.Trim() + ";");
            swGUI.WriteLine("");
            swGUI.WriteLine("namespace " + txtGUINamespace.Text.Trim());
            swGUI.WriteLine("{");
            if (txtGUI.Text.Trim() == string.Empty)
                swGUI.WriteLine("    public partial class Browse" + pluralClassName);
            else
            {
                if (chkDevExpress.Checked)
                    swGUI.WriteLine("    public partial class Browse" + pluralClassName + " : DevExpress.XtraEditors.XtraForm");
                else
                    swGUI.WriteLine("    public partial class Browse" + pluralClassName + " : System.Windows.Forms.Form");
            }
            swGUI.WriteLine("   {");
            #endregion

            #region Browse Code
            swGUI.WriteLine("        #region private properties");
            swGUI.WriteLine("        private Int32 intID;");
            swGUI.WriteLine("        public String layoutFile;");
            swGUI.WriteLine("        private List<BE." + className + "> " + pluralClassName + ";");
            swGUI.WriteLine("        #endregion");
            swGUI.WriteLine("");

            swGUI.WriteLine("        public Browse" + pluralClassName + "()");
            swGUI.WriteLine("        {");
            swGUI.WriteLine("           InitializeComponent();");
            swGUI.WriteLine("        }");
            swGUI.WriteLine("");

            swGUI.WriteLine("        private void Browse" + pluralClassName + "_Load(object sender, EventArgs e)");
            swGUI.WriteLine("        {");
            if (chkFilterMgr.CheckState == CheckState.Checked)
            {
                swGUI.WriteLine("            if (BL.GlobalVars.gboolSaveLayouts)");
                swGUI.WriteLine("            {");
                swGUI.WriteLine("                if (BL.GlobalVars.gboolSaveLayouts)");
                swGUI.WriteLine("                {");
                swGUI.WriteLine("                    List<BE.Layout> layouts = BL.Layouts.FindBy(BE.Constants.LayoutFields.LayoutType, Name.ToUpper());");
                swGUI.WriteLine("                    bsLayouts.DataSource = layouts;");
                swGUI.WriteLine("                    barFilterMgr.DataSource = bsLayouts;");
                swGUI.WriteLine("                    foreach (BE.Layout layout in layouts)");
                swGUI.WriteLine("                    {");
                swGUI.WriteLine("                        if (layout.LayoutName.Trim() == \"DEFAULT\")");
                swGUI.WriteLine("                            barLayout.EditValue = layout.ID;");
                swGUI.WriteLine("                    }");
                swGUI.WriteLine("                }");
                swGUI.WriteLine("            }");
            }
            swGUI.WriteLine("            RefreshData();");
            swGUI.WriteLine("        }");
            swGUI.WriteLine(" ");

            swGUI.WriteLine("        private void RefreshData()");
            swGUI.WriteLine("        {");
            swGUI.WriteLine("           " + pluralClassName + " = BL." + pluralClassName + ".Find();");
            swGUI.WriteLine("           bs" + pluralClassName + ".DataSource = " + pluralClassName + ";");
            swGUI.WriteLine("        }");
            swGUI.WriteLine("");

            #region menu buttons
            if (chkDevExpress.Checked)
            {
                swGUI.WriteLine("        #region Menu buttons");

                #region Refresh button
                swGUI.WriteLine("        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
                swGUI.WriteLine("        {");
                swGUI.WriteLine("           RefreshData();");
                swGUI.WriteLine("        }");
                #endregion

                #region Add button
                swGUI.WriteLine("        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
                swGUI.WriteLine("        {");
                if (chkFilterMgr.CheckState == CheckState.Checked)
                {

                    swGUI.WriteLine("           CRUD" + className + " CRUD = new CRUD" + className + "(0);");
                    swGUI.WriteLine("           CRUD.Show();");
                }
                else
                {
                    swGUI.WriteLine("           using (CRUD" + className + " CRUD = new CRUD" + className + "(0))");
                    swGUI.WriteLine("           {");
                    swGUI.WriteLine("               CRUD.ShowDialog();");
                    swGUI.WriteLine("           }");
                }
                swGUI.WriteLine("        }");
                #endregion

                #region Delete button
                swGUI.WriteLine("        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
                swGUI.WriteLine("        {");
                swGUI.WriteLine("            if (MessageBox.Show(\"Are you sure you want to delete this " + className + "?\", \"Are you sure\", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)");
                swGUI.WriteLine("            {");
                swGUI.WriteLine("               intID = Convert.ToInt32(gvw.GetFocusedRowCellValue(\"ID\"));");
                swGUI.WriteLine("               BE." + className + " " + className + " = BL." + pluralClassName + ".FindByID(intID);");
                swGUI.WriteLine("               if (BL." + pluralClassName + ".Delete(" + className + "))");
                swGUI.WriteLine("                  BL.Utils.UpdateAudit(\"DELETE " + className + ": \" + " + className + ".Description, BL.GlobalVars.gintUserID);");
                swGUI.WriteLine("               RefreshData();");
                swGUI.WriteLine("            }");
                swGUI.WriteLine("        }");
                #endregion

                #region Print button1
                swGUI.WriteLine("        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
                swGUI.WriteLine("        {");
                swGUI.WriteLine("           gvw.OptionsPrint.AllowCancelPrintExport  = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.AutoResetPrintDocument  = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.AutoWidth               = false;");
                swGUI.WriteLine("           gvw.OptionsPrint.PrintDetails            = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.PrintFilterInfo         = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.PrintFooter             = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.PrintGroupFooter        = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.PrintHeader             = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.PrintHorzLines          = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.ShowPrintExportProgress = true;");
                swGUI.WriteLine("           gvw.OptionsPrint.UsePrintStyles          = true;");
                swGUI.WriteLine("           gvw.ShowRibbonPrintPreview();");
                swGUI.WriteLine("        }");
                #endregion

                #region Close button
                swGUI.WriteLine("        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
                swGUI.WriteLine("        {");
                swGUI.WriteLine("           Dispose(true);");
                swGUI.WriteLine("           Close();");
                swGUI.WriteLine("        }");
                #endregion

                swGUI.WriteLine("        #endregion");
            }
            swGUI.WriteLine("");
            #endregion

            swGUI.WriteLine("        private void gvw_DoubleClick(object sender, EventArgs e)");
            swGUI.WriteLine("        {");
            swGUI.WriteLine("           Crud" + className + "(sender, e);");
            swGUI.WriteLine("        }");
            swGUI.WriteLine("");

            swGUI.WriteLine("        private void Crud" + className + "(object sender, EventArgs e)");
            swGUI.WriteLine("        {");
            if (chkDevExpress.Checked)
                swGUI.WriteLine("           if (sender is DevExpress.XtraGrid.Views.Base.BaseView)");
            swGUI.WriteLine("           {");
            swGUI.WriteLine("                Int32 nID = Convert.ToInt32(gvw.GetFocusedRowCellValue(\"" + key + "\"));");
            swGUI.WriteLine("                using (CRUD" + className + " CRUD = new CRUD" + className + "(nID))");
            swGUI.WriteLine("                {");
            swGUI.WriteLine("                   CRUD.ShowDialog();");
            swGUI.WriteLine("                }");
            swGUI.WriteLine("                RefreshData();");
            swGUI.WriteLine("           }");
            swGUI.WriteLine("        }");
            swGUI.WriteLine("");

            if (chkFilterMgr.Checked)
            {
                swGUI.WriteLine("       private void barLayout_EditValueChanged(object sender, EventArgs e)");
                swGUI.WriteLine("       {");
                swGUI.WriteLine("           GetLayout();");
                swGUI.WriteLine("       }");
                swGUI.WriteLine("       private void GetLayout()");
                swGUI.WriteLine("       {");
                swGUI.WriteLine("           Int32 nID            = Convert.ToInt32(barLayout.EditValue);");
                swGUI.WriteLine("           BE.Layout layoutFile = BL.Layouts.FindByID(nID);");
                swGUI.WriteLine("           if (layoutFile      != null)");
                swGUI.WriteLine("           {");
                swGUI.WriteLine("               byte[] byteArray = Encoding.ASCII.GetBytes(layoutFile.XMLFile);");
                swGUI.WriteLine("               MemoryStream ms  = new MemoryStream(byteArray);");
                swGUI.WriteLine("               xdg.MainView.RestoreLayoutFromStream(ms, DevExpress.Utils.OptionsLayoutBase.FullLayout);");
                swGUI.WriteLine("           }");
                swGUI.WriteLine("       }");
                swGUI.WriteLine("       private void barFilterMgr_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)");
                swGUI.WriteLine("       {");
                swGUI.WriteLine("           if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)");
                swGUI.WriteLine("           {");
                swGUI.WriteLine("               String layName = (sender as LookUpEdit).Text;");
                swGUI.WriteLine("               var ms         = new MemoryStream();");
                swGUI.WriteLine("               xdg.MainView.SaveLayoutToStream(ms, DevExpress.Utils.OptionsLayoutBase.FullLayout);");
                swGUI.WriteLine("               ms.Position    = 0;");
                swGUI.WriteLine("               var sr         = new StreamReader(ms);");
                swGUI.WriteLine("               var myStr      = sr.ReadToEnd();");
                swGUI.WriteLine("               BE.Layout lay  = new BE.Layout();");
                swGUI.WriteLine("               lay.EmpID      = BL.GlobalVars.gintUserID;");
                swGUI.WriteLine("               lay.FileName   = String.Empty;");
                swGUI.WriteLine("               lay.LayoutName = layName;");
                swGUI.WriteLine("               lay.LayoutType = Name.ToUpper();");
                swGUI.WriteLine("               lay.XMLFile    = myStr;");
                swGUI.WriteLine("       ");
                swGUI.WriteLine("               BL.Layouts.Save(ref lay);");
                swGUI.WriteLine("               BL.Utils.UpdateAudit(\"Save Layout: \" + lay.LayoutName, BL.GlobalVars.gintUserID);");
                swGUI.WriteLine("           }");
                swGUI.WriteLine("       }");
            }
            swGUI.WriteLine("");
            swGUI.WriteLine("   }");
            swGUI.WriteLine("}");
            #endregion
            swGUI.Flush();
            swGUI.Close();
            swGUI.Dispose();
            #endregion

            #region Write Browse Designer code
            StreamWriter swGUI4 = File.CreateText(GUIfolderName + "\\" + txtPrefixGUI.Text.Trim() + "Browse" + className + "s.Designer.cs");

            swGUI4.WriteLine("namespace " + txtGUINamespace.Text.Trim());
            swGUI4.WriteLine("{");
            swGUI4.WriteLine("    partial class Browse" + className + "s");
            swGUI4.WriteLine("    {");
            swGUI4.WriteLine("        /// Required designer variable.");
            swGUI4.WriteLine("        /// </summary>");
            swGUI4.WriteLine("        private System.ComponentModel.IContainer components = null;");
            swGUI4.WriteLine("");
            swGUI4.WriteLine("        /// <summary>");
            swGUI4.WriteLine("        /// Clean up any resources being used.");
            swGUI4.WriteLine("        /// </summary>");
            swGUI4.WriteLine("        /// <param name=\"disposing\">true if managed resources should be disposed; otherwise, false.</param>");
            swGUI4.WriteLine("        protected override void Dispose(bool disposing)");
            swGUI4.WriteLine("        {");
            swGUI4.WriteLine("            if (disposing && (components != null))");
            swGUI4.WriteLine("            {");
            swGUI4.WriteLine("                components.Dispose();");
            swGUI4.WriteLine("            }");
            swGUI4.WriteLine("            base.Dispose(disposing);");
            swGUI4.WriteLine("        }");
            swGUI4.WriteLine(" ");

            #region Windows BROWSE Forms designer generated code
            swGUI4.WriteLine("        #region Windows Designer generated code");
            swGUI4.WriteLine(" ");
            swGUI4.WriteLine("        /// <summary>");
            swGUI4.WriteLine("        /// Required method for Designer support - do not modify");
            swGUI4.WriteLine("        /// the contents of this method with the code editor.");
            swGUI4.WriteLine("        /// </summary>");
            swGUI4.WriteLine("        private void InitializeComponent()");
            swGUI4.WriteLine("        {");
            swGUI4.WriteLine("            this.components                                          = new System.ComponentModel.Container();");
            swGUI4.WriteLine("            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Browse" + pluralClassName + "));");
            swGUI4.WriteLine("            DevExpress.Utils.SuperToolTip superToolTip1              = new DevExpress.Utils.SuperToolTip();");
            swGUI4.WriteLine("            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1      = new DevExpress.Utils.ToolTipTitleItem();");
            swGUI4.WriteLine("            DevExpress.Utils.ToolTipItem toolTipItem1                = new DevExpress.Utils.ToolTipItem();");
            swGUI4.WriteLine("            this.bs" + pluralClassName + "                           = new System.Windows.Forms.BindingSource(this.components);");
            if (chkFilterMgr.CheckState == CheckState.Checked)
                swGUI4.WriteLine("            this.bsLayouts = new System.Windows.Forms.BindingSource(this.components);");
            swGUI4.WriteLine("            this.barManager                                          = new DevExpress.XtraBars.BarManager(this.components);");
            swGUI4.WriteLine("            this.bar1                                                = new DevExpress.XtraBars.Bar();");
            swGUI4.WriteLine("            this.btnRefresh                                          = new DevExpress.XtraBars.BarButtonItem();");
            swGUI4.WriteLine("            this.btnNew                                              = new DevExpress.XtraBars.BarButtonItem();");
            swGUI4.WriteLine("            this.btnDelete                                           = new DevExpress.XtraBars.BarButtonItem();");
            swGUI4.WriteLine("            this.btnPrint                                            = new DevExpress.XtraBars.BarButtonItem();");
            swGUI4.WriteLine("            this.btnClose                                            = new DevExpress.XtraBars.BarButtonItem();");
            swGUI4.WriteLine("            this.barDockControlTop                                   = new DevExpress.XtraBars.BarDockControl();");
            swGUI4.WriteLine("            this.barDockControlBottom                                = new DevExpress.XtraBars.BarDockControl();");
            swGUI4.WriteLine("            this.barDockControlLeft                                  = new DevExpress.XtraBars.BarDockControl();");
            swGUI4.WriteLine("            this.barDockControlRight                                 = new DevExpress.XtraBars.BarDockControl();");
            swGUI4.WriteLine("            this.xdg                                                 = new DevExpress.XtraGrid.GridControl();");
            swGUI4.WriteLine("            this.gvw                                                 = new DevExpress.XtraGrid.Views.Grid.GridView();");

            foreach (pMap item in fieldList)
            {
                swGUI4.WriteLine("            this.col" + item.Name + " = new DevExpress.XtraGrid.Columns.GridColumn();");
            }
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();");
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.xdg)).BeginInit();");
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.bs" + pluralClassName + ")).BeginInit();");
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.gvw)).BeginInit();");
            swGUI4.WriteLine("            this.SuspendLayout();");

            #region Create static controls (used in every form)
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //barManager");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {");
            swGUI4.WriteLine("            this.bar1});");
            swGUI4.WriteLine("            this.barManager.DockControls.Add(this.barDockControlTop);");
            swGUI4.WriteLine("            this.barManager.DockControls.Add(this.barDockControlBottom);");
            swGUI4.WriteLine("            this.barManager.DockControls.Add(this.barDockControlLeft);");
            swGUI4.WriteLine("            this.barManager.DockControls.Add(this.barDockControlRight);");
            swGUI4.WriteLine("            this.barManager.Form = this;");
            swGUI4.WriteLine("            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {");
            swGUI4.WriteLine("            this.btnRefresh,");
            swGUI4.WriteLine("            this.btnNew,");
            swGUI4.WriteLine("            this.btnDelete,");
            swGUI4.WriteLine("            this.btnPrint,");
            swGUI4.WriteLine("            this.btnClose});");
            swGUI4.WriteLine("            this.barManager.MaxItemId = 6;");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //bar1");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.bar1.BarName = \"Tools\";");
            swGUI4.WriteLine("            this.bar1.DockCol = 0;");
            swGUI4.WriteLine("            this.bar1.DockRow = 0;");
            swGUI4.WriteLine("            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;");
            swGUI4.WriteLine("            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {");
            swGUI4.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnRefresh),");
            swGUI4.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnNew),");
            swGUI4.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnDelete),");
            swGUI4.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnPrint),");
            swGUI4.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnClose)});");
            swGUI4.WriteLine("            this.bar1.OptionsBar.UseWholeRow = true;");
            swGUI4.WriteLine("            this.bar1.Text = \"Tools\";");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //btnRefresh");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.btnRefresh.Caption = \"Refresh\";");
            swGUI4.WriteLine("            this.btnRefresh.Glyph = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Refresh;");
            swGUI4.WriteLine("            this.btnRefresh.Id = 0;");
            swGUI4.WriteLine("            this.btnRefresh.Name = \"btnRefresh\";");
            swGUI4.WriteLine("            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //btnNew");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.btnNew.Caption = \"New\";");
            swGUI4.WriteLine("            this.btnNew.Glyph = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Add;");
            swGUI4.WriteLine("            this.btnNew.Id = 0;");
            swGUI4.WriteLine("            this.btnNew.Name = \"btnNew\";");
            swGUI4.WriteLine("            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //btnDelete");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.btnDelete.Caption = \"Delete\";");
            swGUI4.WriteLine("            this.btnDelete.Glyph = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Delete;");
            swGUI4.WriteLine("            this.btnDelete.Id = 0;");
            swGUI4.WriteLine("            this.btnDelete.Name = \"btnDelete\";");
            swGUI4.WriteLine("            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //btnPrint");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.btnPrint.Caption = \"Print\";");
            swGUI4.WriteLine("            this.btnPrint.Glyph = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Print;");
            swGUI4.WriteLine("            this.btnPrint.Id = 0;");
            swGUI4.WriteLine("            this.btnPrint.Name = \"btnPrint\";");
            swGUI4.WriteLine("            this.btnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrint_ItemClick);");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //btnClose");
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.btnClose.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;");
            swGUI4.WriteLine("            this.btnClose.Caption = \"Close\";");
            swGUI4.WriteLine("            this.btnClose.Glyph = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Exit;");
            swGUI4.WriteLine("            this.btnClose.Id = 0;");
            swGUI4.WriteLine("            this.btnClose.Name = \"btnClose\";");
            swGUI4.WriteLine("            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            // barDockControlTop");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            this.barDockControlTop.CausesValidation = false;");
            swGUI4.WriteLine("            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;");
            swGUI4.WriteLine("            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);");
            swGUI4.WriteLine("            this.barDockControlTop.Size = new System.Drawing.Size(837, 39);");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            // barDockControlBottom");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            this.barDockControlBottom.CausesValidation = false;");
            swGUI4.WriteLine("            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;");
            swGUI4.WriteLine("            this.barDockControlBottom.Location = new System.Drawing.Point(0, 420);");
            swGUI4.WriteLine("            this.barDockControlBottom.Size = new System.Drawing.Size(837, 0);");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            // barDockControlLeft");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            this.barDockControlLeft.CausesValidation = false;");
            swGUI4.WriteLine("            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;");
            swGUI4.WriteLine("            this.barDockControlLeft.Location = new System.Drawing.Point(0, 39);");
            swGUI4.WriteLine("            this.barDockControlLeft.Size = new System.Drawing.Size(0, 381);");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            // barDockControlRight");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            this.barDockControlRight.CausesValidation = false;");
            swGUI4.WriteLine("            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;");
            swGUI4.WriteLine("            this.barDockControlRight.Location = new System.Drawing.Point(837, 39);");
            swGUI4.WriteLine("            this.barDockControlRight.Size = new System.Drawing.Size(0, 381);");

            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            // xdg");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            this.xdg.DataSource = this.bs" + pluralClassName + ";");
            swGUI4.WriteLine("            this.xdg.Dock = System.Windows.Forms.DockStyle.Fill;");
            swGUI4.WriteLine("            this.xdg.Location = new System.Drawing.Point(0, 34);");
            swGUI4.WriteLine("            this.xdg.MainView = this.gvw;");
            swGUI4.WriteLine("            this.xdg.MenuManager = this.barManager;");
            swGUI4.WriteLine("            this.xdg.Name = \"xdg\";");
            swGUI4.WriteLine("            this.xdg.Size = new System.Drawing.Size(373, 272);");
            swGUI4.WriteLine("            this.xdg.TabIndex = 10;");
            swGUI4.WriteLine("            this.xdg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {");
            swGUI4.WriteLine("            this.gvw});");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            // bs" + pluralClassName);
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            this.bs" + pluralClassName + ".DataSource = typeof(" + txtBENamespace.Text.Trim() + "." + className + ");");

            if (chkFilterMgr.CheckState == CheckState.Checked)
            {
                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            // barLayout");
                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            this.barLayout.Caption = \"Filter Manager:\";");
                swGUI4.WriteLine("            this.barLayout.Edit    = this.barFilterMgr;");
                swGUI4.WriteLine("            this.barLayout.Id      = 9;");
                swGUI4.WriteLine("            this.barLayout.Name    = \"barLayout\";");
                swGUI4.WriteLine("            this.barLayout.Width   = 244;");
                swGUI4.WriteLine("            this.barLayout.EditValueChanged += new System.EventHandler(this.barLayout_EditValueChanged);");
                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            // barFilterMgr");
                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            this.barFilterMgr.AutoHeight = false;");
                swGUI4.WriteLine("            this.barFilterMgr.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                swGUI4.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),");
                swGUI4.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, \"\", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject(\"barFilterMgr.Buttons\"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, \"Save current Filter/Layout\", null, null, true)});");
                swGUI4.WriteLine("            this.barFilterMgr.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {");
                swGUI4.WriteLine("            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(\"ID\", \"ID\", 34, DevExpress.Utils.FormatType.Numeric, \"\", false, DevExpress.Utils.HorzAlignment.Far),");
                swGUI4.WriteLine("            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(\"LayoutName\", \"Layout Name\", 200, DevExpress.Utils.FormatType.None, \"\", true, DevExpress.Utils.HorzAlignment.Near)});");
                swGUI4.WriteLine("            this.barFilterMgr.DataSource    = this.bsLayouts;");
                swGUI4.WriteLine("            this.barFilterMgr.DisplayMember = \"LayoutName\";");
                swGUI4.WriteLine("            this.barFilterMgr.Name          = \"barFilterMgr\";");
                swGUI4.WriteLine("            this.barFilterMgr.NullText      = \"\";");
                swGUI4.WriteLine("            this.barFilterMgr.ValueMember   = \"ID\";");
                swGUI4.WriteLine("            this.barFilterMgr.ButtonClick  += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.barFilterMgr_ButtonClick);");

                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            // bsLayouts");
                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            this.bsLayouts.DataSource = typeof(" + txtBENamespace.Text.Trim() + ".Layout);");
            }
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            // gvw");
            swGUI4.WriteLine("            // ");
            swGUI4.WriteLine("            this.gvw.Appearance.HeaderPanel.Options.UseTextOptions = true;");
            swGUI4.WriteLine("            this.gvw.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;");
            swGUI4.WriteLine("            this.gvw.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;");
            swGUI4.WriteLine("            this.gvw.ColumnPanelRowHeight = 35;");
            swGUI4.WriteLine("            this.gvw.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {");
            nCounter = 0;
            foreach (pMap item in fieldList)
            {
                nCounter++;
                if (nCounter < fieldList.Count)
                    swGUI4.WriteLine("            this.col" + item.Name + ",");
                else
                    swGUI4.WriteLine("            this.col" + item.Name + "});");
            }
            swGUI4.WriteLine("            this.gvw.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;");
            swGUI4.WriteLine("            this.gvw.GridControl = this.xdg;");
            swGUI4.WriteLine("            this.gvw.GroupFormat = \"[#image]{1} {2}\";");
            swGUI4.WriteLine("            this.gvw.Name = \"gvw\";");
            swGUI4.WriteLine("            this.gvw.OptionsBehavior.AllowIncrementalSearch = true;");
            swGUI4.WriteLine("            this.gvw.OptionsBehavior.Editable = false;");
            swGUI4.WriteLine("            this.gvw.OptionsView.ColumnAutoWidth = false;");
            swGUI4.WriteLine("            this.gvw.OptionsView.EnableAppearanceEvenRow = true;");
            swGUI4.WriteLine("            this.gvw.OptionsView.EnableAppearanceOddRow = true;");
            swGUI4.WriteLine("            this.gvw.OptionsView.ShowFooter = true;");
            swGUI4.WriteLine("            this.gvw.DoubleClick += new System.EventHandler(this.gvw_DoubleClick);");
            #endregion

            #region Create columns based on table fields
            Int32 colCount = 0;
            foreach (pMap item in fieldList)
            {
                colCount++;
                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            // col" + item.Name);
                swGUI4.WriteLine("            // ");
                swGUI4.WriteLine("            this.col" + item.Name + ".Caption = \"" + item.Caption + "\";");
                swGUI4.WriteLine("            this.col" + item.Name + ".FieldName = \"" + item.Name + "\";");
                swGUI4.WriteLine("            this.col" + item.Name + ".Name = \"col" + item.Name + "\";");
                swGUI4.WriteLine("            this.col" + item.Name + ".Visible = true;");
                swGUI4.WriteLine("            this.col" + item.Name + ".VisibleIndex = " + colCount + ";");
                swGUI4.WriteLine("            this.col" + item.Name + ".Width = 100;");
                if (item.Type.ToLower() == "decimal")
                {
                    swGUI4.WriteLine("            this.col" + item.Name + ".DisplayFormat.FormatString = \"{0:c}\";");
                    swGUI4.WriteLine("            this.col" + item.Name + ".DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;");
                }
            }
            #endregion

            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            //Browse" + pluralClassName);
            swGUI4.WriteLine("            //");
            swGUI4.WriteLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);");
            swGUI4.WriteLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;");
            swGUI4.WriteLine("            this.ClientSize = new System.Drawing.Size(800, 600);");
            swGUI4.WriteLine("            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;");
            swGUI4.WriteLine("            this.Controls.Add(this.xdg);");
            swGUI4.WriteLine("            this.Controls.Add(this.barDockControlLeft);");
            swGUI4.WriteLine("            this.Controls.Add(this.barDockControlRight);");
            swGUI4.WriteLine("            this.Controls.Add(this.barDockControlBottom);");
            swGUI4.WriteLine("            this.Controls.Add(this.barDockControlTop);");
            swGUI4.WriteLine("            this.Name = \"Browse" + pluralClassName + "\";");
            swGUI4.WriteLine("            this.Text = \"Browse " + pluralClassName + "\";");
            swGUI4.WriteLine("            this.Load += new System.EventHandler(this.Browse" + pluralClassName + "_Load);");
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.bs" + pluralClassName + ")).EndInit();");
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();");
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.xdg)).EndInit();");
            swGUI4.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.gvw)).EndInit();");
            swGUI4.WriteLine("            this.ResumeLayout(false);");
            swGUI4.WriteLine("            this.PerformLayout();");
            swGUI4.WriteLine("        }");
            swGUI4.WriteLine("        #endregion");
            #endregion

            #region private variables
            // Write out the form controls not directly linked to fields. (Menu elements, etc)
            swGUI4.WriteLine(" ");
            swGUI4.WriteLine("        private System.Windows.Forms.BindingSource bs" + pluralClassName + ";");
            if (chkFilterMgr.CheckState == CheckState.Checked)
                swGUI4.WriteLine("        private System.Windows.Forms.BindingSource bsLayouts;");
            if (chkDevExpress.Checked)
            {
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarManager barManager;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.Bar bar1;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnRefresh;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnNew;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnDelete;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnPrint;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnClose;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlTop;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlBottom;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlLeft;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlRight;");
                swGUI4.WriteLine("        private DevExpress.XtraGrid.GridControl xdg;");
                swGUI4.WriteLine("        private DevExpress.XtraGrid.Views.Grid.GridView gvw;");
                swGUI4.WriteLine("        private DevExpress.XtraBars.BarEditItem barLayout;");
                swGUI4.WriteLine("        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit barFilterMgr;");
            }

            foreach (pMap item in fieldList)
            {
                swGUI4.WriteLine("        private DevExpress.XtraGrid.Columns.GridColumn col" + item.Name + ";");
            }
            swGUI4.WriteLine("    }");
            swGUI4.WriteLine("}");
            #endregion

            swGUI4.Flush();
            swGUI4.Close();
            swGUI4.Dispose();
            #endregion

            #region Create Browse RESX file
            if (!boolBROWSERESX)
            {
                StreamWriter swBrowseRESX = File.CreateText(GUIfolderName + "\\" + txtPrefixGUI.Text.Trim() + "Browse" + pluralClassName + ".resx");
                swBrowseRESX.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                swBrowseRESX.WriteLine("<root>");
                swBrowseRESX.WriteLine(" <xsd:schema id=\"root\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\"");
                swBrowseRESX.WriteLine("     <xsd:import namespace=\"http://www.w3.org/XML/1998/namespace\" />");
                swBrowseRESX.WriteLine("         <xsd:element name=\"root\" msdata:IsDataSet=\"true\">");
                swBrowseRESX.WriteLine("           <xsd:complexType>");
                swBrowseRESX.WriteLine("             <xsd:choice maxOccurs=\"unbounded\">");
                swBrowseRESX.WriteLine("               <xsd:element name=\"metadata\">");
                swBrowseRESX.WriteLine("                 <xsd:complexType>");
                swBrowseRESX.WriteLine("                   <xsd:sequence>");
                swBrowseRESX.WriteLine("                     <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" />");
                swBrowseRESX.WriteLine("                   </xsd:sequence>");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"name\" use=\"required\" type=\"xsd:string\" />");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"type\" type=\"xsd:string\" />");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"mimetype\" type=\"xsd:string\" />");
                swBrowseRESX.WriteLine("                   <xsd:attribute ref=\"xml:space\" />");
                swBrowseRESX.WriteLine("                 </xsd:complexType>");
                swBrowseRESX.WriteLine("               </xsd:element>");
                swBrowseRESX.WriteLine("               <xsd:element name=\"assembly\">");
                swBrowseRESX.WriteLine("                 <xsd:complexType>");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"alias\" type=\"xsd:string\" />");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"name\" type=\"xsd:string\" />");
                swBrowseRESX.WriteLine("                 </xsd:complexType>");
                swBrowseRESX.WriteLine("               </xsd:element>");
                swBrowseRESX.WriteLine("               <xsd:element name=\"data\">");
                swBrowseRESX.WriteLine("                 <xsd:complexType>");
                swBrowseRESX.WriteLine("                   <xsd:sequence>");
                swBrowseRESX.WriteLine("                     <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />");
                swBrowseRESX.WriteLine("                     <xsd:element name=\"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\" />");
                swBrowseRESX.WriteLine("                   </xsd:sequence>");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" msdata:Ordinal=\"1\" />");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"type\" type=\"xsd:string\" msdata:Ordinal=\"3\" />");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\" />");
                swBrowseRESX.WriteLine("                   <xsd:attribute ref=\"xml:space\" />");
                swBrowseRESX.WriteLine("                 </xsd:complexType>");
                swBrowseRESX.WriteLine("               </xsd:element>");
                swBrowseRESX.WriteLine("               <xsd:element name=\"resheader\">");
                swBrowseRESX.WriteLine("                 <xsd:complexType>");
                swBrowseRESX.WriteLine("                   <xsd:sequence>");
                swBrowseRESX.WriteLine("                     <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />");
                swBrowseRESX.WriteLine("                   </xsd:sequence>");
                swBrowseRESX.WriteLine("                   <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" />");
                swBrowseRESX.WriteLine("                 </xsd:complexType>");
                swBrowseRESX.WriteLine("               </xsd:element>");
                swBrowseRESX.WriteLine("             </xsd:choice>");
                swBrowseRESX.WriteLine("           </xsd:complexType>");
                swBrowseRESX.WriteLine("         </xsd:element>");
                swBrowseRESX.WriteLine("       </xsd:schema>");
                swBrowseRESX.WriteLine("       <resheader name=\"resmimetype\">");
                swBrowseRESX.WriteLine("         <value>text/microsoft-resx</value>");
                swBrowseRESX.WriteLine("       </resheader>");
                swBrowseRESX.WriteLine("       <resheader name=\"version\">");
                swBrowseRESX.WriteLine("         <value>2.0</value>");
                swBrowseRESX.WriteLine("       </resheader>");
                swBrowseRESX.WriteLine("       <resheader name=\"reader\">");
                swBrowseRESX.WriteLine("         <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
                swBrowseRESX.WriteLine("       </resheader>");
                swBrowseRESX.WriteLine("       <resheader name=\"writer\">");
                swBrowseRESX.WriteLine("         <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
                swBrowseRESX.WriteLine("       </resheader>");
                swBrowseRESX.WriteLine("       <metadata name=\"barManager1.TrayLocation\" type=\"System.Drawing.Point, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\">");
                swBrowseRESX.WriteLine("           <value>17, 17</value>");
                swBrowseRESX.WriteLine("       </metadata>");
                swBrowseRESX.WriteLine("       <metadata name=\"bs" + pluralClassName + ".TrayLocation\" type=\"System.Drawing.Point, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\">");
                swBrowseRESX.WriteLine("           <value>142, 17</value>");
                swBrowseRESX.WriteLine("       </metadata>");
                swBrowseRESX.WriteLine("       </root>");
                swBrowseRESX.Flush();
                swBrowseRESX.Close();
                swBrowseRESX.Dispose();
                boolBROWSERESX = true;
            }
            #endregion

            #region Write GUI CRUD class
            StreamWriter swGUI2 = File.CreateText(GUIfolderName + "\\" + txtPrefixGUI.Text.Trim() + "CRUD" + className + ".cs");

            #region Header & private properties
            swGUI2.WriteLine("using System;");
            swGUI2.WriteLine("using System.Drawing;");
            swGUI2.WriteLine("using System.Windows.Forms;");
            swGUI2.WriteLine("using AmosG.BusinessTier.BusinessLogicLayer;");
            swGUI2.WriteLine("using AmosG.PresentationLayer.UserInterface;");
            swGUI2.WriteLine("using System.Collections.Generic;");
            swGUI2.WriteLine("using Constants = " + txtBENamespace.Text.Trim() + ".Constants;");
            swGUI2.WriteLine("using BE = " + txtBENamespace.Text.Trim() + ";");
            swGUI2.WriteLine("using BL = " + txtBLLNamespace.Text.Trim() + ";");
            swGUI2.WriteLine("using UI = " + txtGUINamespace.Text.Trim() + ";");
            swGUI2.WriteLine("");
            swGUI2.WriteLine("namespace " + txtGUINamespace.Text.Trim());
            swGUI2.WriteLine("{");
            if (txtGUI.Text.Trim() == string.Empty)
                swGUI2.WriteLine("   public partial class CRUD" + className);
            else
                swGUI2.WriteLine("   public partial class CRUD" + className + " : DevExpress.XtraEditors.XtraForm");
            swGUI2.WriteLine("   {");
            #endregion

            #region CRUD Code
            #region Header
            swGUI2.WriteLine("        #region private properties");
            swGUI2.WriteLine("        private Int32 intID;");
            swGUI2.WriteLine("        private BE." + className + " _" + className + ";");
            swGUI2.WriteLine("        public BE." + className + " " + className);
            swGUI2.WriteLine("        {");
            swGUI2.WriteLine("            get");
            swGUI2.WriteLine("            {");
            swGUI2.WriteLine("                if (_" + className + " == null)");
            swGUI2.WriteLine("                    _" + className + " = new BE." + className + "();");
            swGUI2.WriteLine("");
            swGUI2.WriteLine("                return _" + className + ";");
            swGUI2.WriteLine("            }");
            swGUI2.WriteLine("            set");
            swGUI2.WriteLine("            {");
            swGUI2.WriteLine("                _" + className + " = value;");
            swGUI2.WriteLine("            }");
            swGUI2.WriteLine("         }");
            swGUI2.WriteLine("        #endregion");
            swGUI2.WriteLine("");
            #endregion

            #region ctor
            swGUI2.WriteLine("        public CRUD" + className + "(Int32 id)");
            swGUI2.WriteLine("        {");
            swGUI2.WriteLine("           InitializeComponent();");
            swGUI2.WriteLine("           intID = id;");
            swGUI2.WriteLine("        }");
            #endregion
            swGUI2.WriteLine("");
            swGUI2.WriteLine("        private void CRUD" + className + "_Load(object sender, EventArgs e)");
            swGUI2.WriteLine("        {");
            if (useLayoutControl)
                swGUI2.WriteLine("            FormUtils.RecallFormLayout(layoutControl, Utils.GetDataPath() + Name);");
            swGUI2.WriteLine("            RefreshData();");
            swGUI2.WriteLine("            BindDataToControls();");
            swGUI2.WriteLine("            if (" + className + "." + key + " == 0)");
            swGUI2.WriteLine("               LockControls(false);");
            swGUI2.WriteLine("        }");
            swGUI2.WriteLine("");
            swGUI2.WriteLine("        private void RefreshData()");
            swGUI2.WriteLine("        {");
            swGUI2.WriteLine("           " + className + " = BL." + pluralClassName + ".FindByID(intID);");
            swGUI2.WriteLine("           bs" + className + ".DataSource = " + className + ";");
            swGUI2.WriteLine("        }");
            swGUI2.WriteLine("        #region Menu buttons");
            swGUI2.WriteLine("        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
            swGUI2.WriteLine("        {");
            swGUI2.WriteLine("            CRUD" + className + " CRUD = new CRUD" + className + "(0);");
            swGUI2.WriteLine("            CRUD.Show();");
            swGUI2.WriteLine("        }");
            swGUI2.WriteLine("");
            swGUI2.WriteLine("        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
            swGUI2.WriteLine("        {");
            swGUI2.WriteLine("            LockControls(false);");
            swGUI2.WriteLine("            BL.Utils.UpdateAudit(\"Edit " + className + ": \" + " + className + "." + key + ", UI.GlobalVars.gintUserID);");
            swGUI2.WriteLine("        }");
            swGUI2.WriteLine("        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
            swGUI2.WriteLine("        {");
            swGUI2.WriteLine("            if (MessageBox.Show(\"Are you sure you want to delete this " + className + "?\", \"Are you sure\", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)");
            swGUI2.WriteLine("            {");
            swGUI2.WriteLine("               BL." + pluralClassName + ".Delete(" + className + ");");
            swGUI2.WriteLine("               BL.Utils.UpdateAudit(\"Delete " + className + ": \" + " + className + "." + key + ", UI.GlobalVars.gintUserID);");
            swGUI2.WriteLine("               DialogResult = DialogResult.OK;");
            swGUI2.WriteLine("               Close();");
            swGUI2.WriteLine("            }");
            swGUI2.WriteLine("        }");
            //swGUI2.WriteLine("        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
            //swGUI2.WriteLine("        {");
            //swGUI2.WriteLine("           //rpt" + className + "Profile rpt = new rpt" + className + "Profile();");
            //swGUI2.WriteLine("           //rpt.Show();");
            //swGUI2.WriteLine("        }");
            swGUI2.WriteLine("");

            swGUI2.WriteLine("        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
            swGUI2.WriteLine("        {");
            swGUI2.WriteLine("            GetDataFromControls();");
            swGUI2.WriteLine("            BE." + className + " " + className.ToLower() + " = " + className + ";");
            swGUI2.WriteLine("            BL." + pluralClassName + ".Save(ref " + className.ToLower() + ");");
            swGUI2.WriteLine("            BL.Utils.UpdateAudit(\"Save " + className + ": \" + " + className + "." + key + ", UI.GlobalVars.gintUserID);");
            swGUI2.WriteLine("            LockControls(true);");
            swGUI2.WriteLine("        }");
            swGUI2.WriteLine("        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)");
            swGUI2.WriteLine("        {");
            if (useLayoutControl)
                swGUI2.WriteLine("           FormUtils.SaveFormLayout(layoutControl, Utils.GetDataPath() + Name);");
            swGUI2.WriteLine("           DialogResult = DialogResult.Cancel;");
            swGUI2.WriteLine("           Close();");
            swGUI2.WriteLine("        }");
            swGUI2.WriteLine("        #endregion");

            #endregion

            swGUI2.WriteLine("");
            swGUI2.WriteLine("        private void LockControls(bool boolLock)");
            swGUI2.WriteLine("        {");
            foreach (pMap item in fieldList)
            {
                swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + ".Properties.ReadOnly = boolLock;");
            }
            swGUI2.WriteLine("");
            swGUI2.WriteLine("            if (boolLock)");
            swGUI2.WriteLine("            {");
            swGUI2.WriteLine("                btnNew.Visibility    = DevExpress.XtraBars.BarItemVisibility.Always;");
            swGUI2.WriteLine("                btnEdit.Visibility   = DevExpress.XtraBars.BarItemVisibility.Always;");
            swGUI2.WriteLine("                btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;");
            swGUI2.WriteLine("                btnSave.Visibility   = DevExpress.XtraBars.BarItemVisibility.Never;");
            swGUI2.WriteLine("            }");
            swGUI2.WriteLine("            else");
            swGUI2.WriteLine("            {");
            swGUI2.WriteLine("                btnNew.Visibility    = DevExpress.XtraBars.BarItemVisibility.Never;");
            swGUI2.WriteLine("                btnEdit.Visibility   = DevExpress.XtraBars.BarItemVisibility.Never;");
            swGUI2.WriteLine("                btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;");
            swGUI2.WriteLine("                btnSave.Visibility   = DevExpress.XtraBars.BarItemVisibility.Always;");
            swGUI2.WriteLine("            }");
            swGUI2.WriteLine("        }");

            swGUI2.WriteLine("        private void BindDataToControls()");
            swGUI2.WriteLine("        {");
            #region Loop thru all fields and bind data
            foreach (pMap item in fieldList)
            {
                switch (item.Type.ToLower())
                {
                    case "uniqueidentifier":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "bool":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "boolean":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "int32":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "int":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "decimal":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = Convert.ToDecimal(" + className + "." + item.Name + ");");
                        break;
                    case "string":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "double":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = Convert.ToDouble(" + className + "." + item.Name + ");");
                        break;
                    case "byte":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = Convert.ToSingle(" + className + "." + item.Name + ");");
                        break;
                    case "byte[]":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "short":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = Convert.ToDouble(" + className + "." + item.Name + ");");
                        break;
                    case "long":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = Convert.ToDouble(" + className + "." + item.Name + ");");
                        break;
                    case "char":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "datetime":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "time":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    case "image":
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        break;
                    default:
                        swGUI2.WriteLine("            " + item.ControlPrefix + item.Name + "." + item.EditField + " = " + className + "." + item.Name + ";");
                        MessageBox.Show("Potential problem with setting GUI CRUD control type: " + item.Name, item.Type);
                        break;
                }
            }
            #endregion
            swGUI2.WriteLine("        }");


            swGUI2.WriteLine("        private void GetDataFromControls()");
            swGUI2.WriteLine("        {");
            #region save control data back to table
            foreach (pMap item in fieldList)
            {
                if (item.Key)
                    swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToInt32(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                else
                {
                    switch (item.Type.ToLower())
                    {
                        case "uniqueidentifier":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToInt32(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "bool":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToBoolean(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "boolean":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToBoolean(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "int32":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToInt32(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "int":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToInt32(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "decimal":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToDecimal(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "string":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = " + item.ControlPrefix + item.Name + "." + item.EditField + ";");
                            break;
                        case "double":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToDecimal(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "byte":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToDecimal(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "short":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToDecimal(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "long":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToDecimal(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "char":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = " + item.ControlPrefix + item.Name + "." + item.EditField + ".ToString();");
                            break;
                        case "datetime":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToDateTime(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "time":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = Convert.ToTime(" + item.ControlPrefix + item.Name + "." + item.EditField + ");");
                            break;
                        case "byte[]":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = " + item.ControlPrefix + item.Name + "." + item.EditField + ";");
                            break;
                        case "image":
                            swGUI2.WriteLine("            " + className + "." + item.Name + " = " + item.ControlPrefix + item.Name + "." + item.EditField + ";");
                            break;
                        default:
                            MessageBox.Show("Potential problem converting control data to table field: " + item.Name, item.Type);
                            break;
                    }
                }
            }
            #endregion
            swGUI2.WriteLine("        }");

            swGUI2.WriteLine("   }");
            swGUI2.WriteLine("}");
            swGUI2.Flush();
            swGUI2.Close();
            swGUI2.Dispose();
            #endregion

            #region Write CRUD Designer code
            StreamWriter swGUI3 = File.CreateText(GUIfolderName + "\\" + txtPrefixGUI.Text.Trim() + "CRUD" + className + ".Designer.cs");

            #region Header
            swGUI3.WriteLine("namespace " + txtGUINamespace.Text.Trim());
            swGUI3.WriteLine("{");
            swGUI3.WriteLine("    partial class CRUD" + className);
            swGUI3.WriteLine("    {");
            swGUI3.WriteLine("        /// Required designer variable.");
            swGUI3.WriteLine("        /// </summary>");
            swGUI3.WriteLine("        private System.ComponentModel.IContainer components = null;");
            swGUI3.WriteLine("");
            swGUI3.WriteLine("        /// <summary>");
            swGUI3.WriteLine("        /// Clean up any resources being used.");
            swGUI3.WriteLine("        /// </summary>");
            swGUI3.WriteLine("        /// <param name=\"disposing\">true if managed resources should be disposed; otherwise, false.</param>");
            swGUI3.WriteLine("        protected override void Dispose(bool disposing)");
            swGUI3.WriteLine("        {");
            swGUI3.WriteLine("            if (disposing && (components != null))");
            swGUI3.WriteLine("            {");
            swGUI3.WriteLine("                components.Dispose();");
            swGUI3.WriteLine("            }");
            swGUI3.WriteLine("            base.Dispose(disposing);");
            swGUI3.WriteLine("        }");
            #endregion

            #region Windows Forms designer generated code
            swGUI3.WriteLine("        private void InitializeComponent()");
            swGUI3.WriteLine("        {");
            swGUI3.WriteLine("            this.components = new System.ComponentModel.Container();");
            swGUI3.WriteLine("            this.bs" + className + " = new System.Windows.Forms.BindingSource(this.components);");
            if (chkDevExpress.Checked)
            {
                swGUI3.WriteLine("            this.barManager = new DevExpress.XtraBars.BarManager(this.components);");
                if (useLayoutControl)
                    swGUI3.WriteLine("            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();");

                swGUI3.WriteLine("            this.bar1                 = new DevExpress.XtraBars.Bar();");
                swGUI3.WriteLine("            this.btnNew               = new DevExpress.XtraBars.BarButtonItem();");
                swGUI3.WriteLine("            this.btnEdit              = new DevExpress.XtraBars.BarButtonItem();");
                swGUI3.WriteLine("            this.btnDelete            = new DevExpress.XtraBars.BarButtonItem();");
                swGUI3.WriteLine("            this.btnSave              = new DevExpress.XtraBars.BarButtonItem();");
                swGUI3.WriteLine("            this.btnPrint             = new DevExpress.XtraBars.BarButtonItem();");
                swGUI3.WriteLine("            this.btnClose             = new DevExpress.XtraBars.BarButtonItem();");
                swGUI3.WriteLine("            this.barDockControlTop    = new DevExpress.XtraBars.BarDockControl();");
                swGUI3.WriteLine("            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();");
                swGUI3.WriteLine("            this.barDockControlLeft   = new DevExpress.XtraBars.BarDockControl();");
                swGUI3.WriteLine("            this.barDockControlRight  = new DevExpress.XtraBars.BarDockControl();");
            }

            foreach (pMap item in fieldList)
            {
                if (chkDevExpress.Checked)
                    swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + " = new DevExpress.XtraEditors." + item.ControlName + "();");
                else
                    swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + " = new System.Windows.Forms." + item.ControlName + "();");
            }

            #region Layout Control or labels

            if (chkDevExpress.Checked)
            {
                if (useLayoutControl)
                {
                    swGUI3.WriteLine("            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();");

                    nCounter = 0;
                    foreach (pMap item in fieldList)
                    {
                        nCounter++;
                        swGUI3.WriteLine("            this.layoutControlItem" + nCounter + " = new DevExpress.XtraLayout.LayoutControlItem();");
                    }
                }
                else
                {
                    foreach (pMap item in fieldList)
                    {
                        swGUI3.WriteLine("            this.lbl" + item.Name + " = new DevExpress.XtraEditors.LabelControl();");
                    }
                }
            }
            else
            {
                foreach (pMap item in fieldList)
                {
                    swGUI3.WriteLine("            this.lbl" + item.Name + " = new System.Windows.Forms.Label();");
                }
            }

            #endregion

            #region Static BeginInit()s
            if (useLayoutControl)
            {
                swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();");
                swGUI3.WriteLine("            this.layoutControl.SuspendLayout();");
            }
            swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.bs" + className + ")).BeginInit();");
            swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();");

            if (useLayoutControl)
                swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();");
            #endregion

            #region LayoutControl item BeginInit()
            if (useLayoutControl)
            {
                nCounter = 0;
                foreach (pMap item in fieldList)
                {
                    nCounter++;
                    swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem" + nCounter + ")).BeginInit();");
                }
            }
            #endregion

            swGUI3.WriteLine("            this.SuspendLayout();");

            #region Layout Control code
            if (useLayoutControl)
            {
                // LayoutControl code");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            // layoutControl");
                swGUI3.WriteLine("            // ");

                #region Add Layout Control items to container
                foreach (pMap item in fieldList)
                {
                    swGUI3.WriteLine("            this.layoutControl.Controls.Add(this." + item.ControlPrefix + item.Name + ");");
                }
                #endregion

                swGUI3.WriteLine("            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;");
                swGUI3.WriteLine("            this.layoutControl.Location = new System.Drawing.Point(0, 29);");
                swGUI3.WriteLine("            this.layoutControl.Name = \"layoutControl\";");
                swGUI3.WriteLine("            this.layoutControl.Root = this.layoutControlGroup1;");
                swGUI3.WriteLine("            this.layoutControl.Size = new System.Drawing.Size(551, 197);");
                swGUI3.WriteLine("            this.layoutControl.TabIndex = 0;");
                swGUI3.WriteLine("            this.layoutControl.Text = \"layoutControl\";");
            }
            #endregion

            #region Create static controls (used in every form)
            if (chkDevExpress.Checked)
            {
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //bs" + className);
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.bs" + className + ".DataSource = typeof(" + txtBENamespace.Text.Trim() + "." + className + ");");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //barManager");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {");
                swGUI3.WriteLine("            this.bar1});");
                swGUI3.WriteLine("            this.barManager.DockControls.Add(this.barDockControlTop);");
                swGUI3.WriteLine("            this.barManager.DockControls.Add(this.barDockControlBottom);");
                swGUI3.WriteLine("            this.barManager.DockControls.Add(this.barDockControlLeft);");
                swGUI3.WriteLine("            this.barManager.DockControls.Add(this.barDockControlRight);");
                swGUI3.WriteLine("            this.barManager.Form = this;");
                swGUI3.WriteLine("            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {");
                swGUI3.WriteLine("            this.btnNew,");
                swGUI3.WriteLine("            this.btnEdit,");
                swGUI3.WriteLine("            this.btnDelete,");
                swGUI3.WriteLine("            this.btnSave,");
                swGUI3.WriteLine("            this.btnPrint,");
                swGUI3.WriteLine("            this.btnClose});");
                swGUI3.WriteLine("            this.barManager.MaxItemId = 6;");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //bar1");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.bar1.BarName   = \"Tools\";");
                swGUI3.WriteLine("            this.bar1.DockCol   = 0;");
                swGUI3.WriteLine("            this.bar1.DockRow   = 0;");
                swGUI3.WriteLine("            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;");
                swGUI3.WriteLine("            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {");
                swGUI3.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnNew),");
                swGUI3.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnEdit),");
                swGUI3.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnDelete),");
                swGUI3.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave),");
                swGUI3.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnPrint),");
                swGUI3.WriteLine("            new DevExpress.XtraBars.LinkPersistInfo(this.btnClose)});");
                swGUI3.WriteLine("            this.bar1.OptionsBar.UseWholeRow = true;");
                swGUI3.WriteLine("            this.bar1.Text = \"Tools\";");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //btnNew");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.btnNew.Caption    = \"New\";");
                swGUI3.WriteLine("            this.btnNew.Glyph      = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Add;");
                swGUI3.WriteLine("            this.btnNew.Id         = 0;");
                swGUI3.WriteLine("            this.btnNew.Name       = \"btnNew\";");
                swGUI3.WriteLine("            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAdd_ItemClick);");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //btnEdit");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.btnEdit.Caption    = \"Edit\";");
                swGUI3.WriteLine("            this.btnEdit.Glyph      = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Edit;");
                swGUI3.WriteLine("            this.btnEdit.Id         = 1;");
                swGUI3.WriteLine("            this.btnEdit.Name       = \"btnEdit\";");
                swGUI3.WriteLine("            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEdit_ItemClick);");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //btnDelete");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.btnDelete.Caption    = \"Delete\";");
                swGUI3.WriteLine("            this.btnDelete.Glyph      = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Delete;");
                swGUI3.WriteLine("            this.btnDelete.Id         = 2;");
                swGUI3.WriteLine("            this.btnDelete.Name       = \"btnDelete\";");
                swGUI3.WriteLine("            this.btnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDelete_ItemClick);");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //btnSave");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.btnSave.Caption    = \"Save\";");
                swGUI3.WriteLine("            this.btnSave.Glyph      = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Save;");
                swGUI3.WriteLine("            this.btnSave.Id         = 3;");
                swGUI3.WriteLine("            this.btnSave.Name       = \"btnSave\";");
                swGUI3.WriteLine("            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //btnPrint");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.btnPrint.Caption    = \"Print\";");
                swGUI3.WriteLine("            this.btnPrint.Glyph      = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Print;");
                swGUI3.WriteLine("            this.btnPrint.Id         = 4;");
                swGUI3.WriteLine("            this.btnPrint.Name       = \"btnPrint\";");
                swGUI3.WriteLine("            this.btnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPrint_ItemClick);");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            //btnClose");
                swGUI3.WriteLine("            //");
                swGUI3.WriteLine("            this.btnClose.Alignment  = DevExpress.XtraBars.BarItemLinkAlignment.Right;");
                swGUI3.WriteLine("            this.btnClose.Caption    = \"Close\";");
                swGUI3.WriteLine("            this.btnClose.Glyph      = global::" + txtGUINamespace.Text.Trim() + ".Properties.Resources.Exit;");
                swGUI3.WriteLine("            this.btnClose.Id         = 10;");
                swGUI3.WriteLine("            this.btnClose.Name       = \"btnClose\";");
                swGUI3.WriteLine("            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            // barDockControlTop");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            this.barDockControlTop.CausesValidation = false;");
                swGUI3.WriteLine("            this.barDockControlTop.Dock             = System.Windows.Forms.DockStyle.Top;");
                swGUI3.WriteLine("            this.barDockControlTop.Location         = new System.Drawing.Point(0, 0);");
                swGUI3.WriteLine("            this.barDockControlTop.Size             = new System.Drawing.Size(837, 39);");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            // barDockControlBottom");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            this.barDockControlBottom.CausesValidation = false;");
                swGUI3.WriteLine("            this.barDockControlBottom.Dock             = System.Windows.Forms.DockStyle.Bottom;");
                swGUI3.WriteLine("            this.barDockControlBottom.Location         = new System.Drawing.Point(0, 420);");
                swGUI3.WriteLine("            this.barDockControlBottom.Size             = new System.Drawing.Size(837, 0);");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            // barDockControlLeft");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            this.barDockControlLeft.CausesValidation = false;");
                swGUI3.WriteLine("            this.barDockControlLeft.Dock             = System.Windows.Forms.DockStyle.Left;");
                swGUI3.WriteLine("            this.barDockControlLeft.Location         = new System.Drawing.Point(0, 39);");
                swGUI3.WriteLine("            this.barDockControlLeft.Size             = new System.Drawing.Size(0, 381);");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            // barDockControlRight");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            this.barDockControlRight.CausesValidation = false;");
                swGUI3.WriteLine("            this.barDockControlRight.Dock             = System.Windows.Forms.DockStyle.Right;");
                swGUI3.WriteLine("            this.barDockControlRight.Location         = new System.Drawing.Point(837, 39);");
                swGUI3.WriteLine("            this.barDockControlRight.Size             = new System.Drawing.Size(0, 381);");
            }
            #endregion

            Int16 x = 110;
            Int16 y = 45;
            #region Create Field Controls
            nCounter = 0;
            #region Field Control Properties
            foreach (pMap item in fieldList)
            {
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            // " + item.ControlPrefix + item.Name);
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Name = \"" + item.ControlPrefix + item.Name + "\";");
                swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".MenuManager = this.barManager;");
                swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Location = new System.Drawing.Point(" + x + ", " + y + ");");
                swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Size = new System.Drawing.Size(100, 20);");
                swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".TabIndex = " + nCounter + ";");
                swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.ReadOnly = true;");

                nCounter++;
                switch (item.Type.ToLower())
                {
                    case "uniqueidentifier":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = \"" + item.Name + "\";");
                        break;
                    case "bool":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Caption = \"" + item.Name + "\";");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Text = \"" + item.Name + "\";");
                        break;
                    case "boolean":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Caption = \"" + item.Name + "\";");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Text = \"" + item.Name + "\";");
                        break;
                    case "bit":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Caption = \"" + item.Name + "\";");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Text = \"" + item.Name + "\";");
                        break;
                    case "int32":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = false;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        break;
                    case "int":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = false;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        break;
                    case "decimal":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.DisplayFormat.FormatString = \"{0:c}\";");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = true;");
                        break;
                    case "money":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.DisplayFormat.FormatString = \"{0:c}\";");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = true;");
                        break;
                    case "string":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = \"" + item.Name + "\";");
                        break;
                    case "nvarchar":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = \"" + item.Name + "\";");
                        break;
                    case "varchar":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = \"" + item.Name + "\";");
                        break;
                    case "double":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = true;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        break;
                    case "byte":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = true;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        break;
                    case "byte[]":
                        //swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;");
                        break;
                    case "short":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = true;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        break;
                    case "long":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new decimal(new int[] {");
                        swGUI3.WriteLine("            1,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0,");
                        swGUI3.WriteLine("            0});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.IsFloatValue = true;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Mask.EditMask = \"N00\";");
                        break;
                    case "char":
                        break;
                    case "datetime":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = null;");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        break;
                    case "time":
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".EditValue = new System.DateTime(2011, 2, 7, 0, 0, 0, 0);");
                        swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {");
                        swGUI3.WriteLine("            new DevExpress.XtraEditors.Controls.EditorButton()});");
                        break;
                    case "image":
                        //swGUI3.WriteLine("            this." + item.ControlPrefix + item.Name + ".SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;");
                        break;
                    default:
                        swGUI3.WriteLine("//((System.ComponentModel.ISupportInitialize)(this.xxx" + item.Name + ")).BeginInit();");
                        MessageBox.Show("Potential problem with setting GUI CRUD Designer control type: " + item.Name, item.Type);
                        break;
                }
                y += 25;
                if (y > 500)
                {
                    x = 300;
                    y = 45;
                }
            }
            #endregion

            if (useLayoutControl)
            {
                #region LayoutControl Group Properties 1
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            // layoutControlGroup1");
                swGUI3.WriteLine("            // ");
                swGUI3.WriteLine("            this.layoutControlGroup1.CustomizationFormText = \"layoutControlGroup1\";");
                swGUI3.WriteLine("            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;");
                swGUI3.WriteLine("            this.layoutControlGroup1.GroupBordersVisible = false;");
                swGUI3.WriteLine("            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {");

                #region LayoutControl item Base Layout
                nCounter = 0;
                foreach (pMap item in fieldList)
                {
                    nCounter++;
                    if (nCounter < fieldList.Count)
                        swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ",");
                    else
                        swGUI3.WriteLine("            this.layoutControlItem" + nCounter + "});");
                }
                #endregion

                swGUI3.WriteLine("            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);");
                swGUI3.WriteLine("            this.layoutControlGroup1.Name = \"Root\";");
                swGUI3.WriteLine("            this.layoutControlGroup1.Size = new System.Drawing.Size(790, 590);");
                swGUI3.WriteLine("            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);");
                swGUI3.WriteLine("            this.layoutControlGroup1.Text = \"Root\";");
                swGUI3.WriteLine("            this.layoutControlGroup1.TextVisible = false;");
                #endregion


                #region LayoutControl item Properties
                nCounter = 0;
                x = 0;
                y = 0;
                foreach (pMap item in fieldList)
                {
                    nCounter++;
                    swGUI3.WriteLine("            // ");
                    swGUI3.WriteLine("            // layoutControlItem" + nCounter);
                    swGUI3.WriteLine("            // ");
                    if (boolRightAlign)
                    {
                        swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".AppearanceItemCaption.Options.UseTextOptions = true;");
                        swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;");
                    }
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".Control = this." + item.ControlPrefix + item.Name + ";");
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".CustomizationFormText = \"" + item.Caption + ":\";");
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".Location = new System.Drawing.Point(" + x + ", " + y + ");");
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".Name = \"layoutControlItem" + nCounter + "\";");
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".Size = new System.Drawing.Size(155, 24);");
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".Text = \"" + item.Caption + ":\";");
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".TextSize = new System.Drawing.Size(71, 13);");
                    swGUI3.WriteLine("            this.layoutControlItem" + nCounter + ".TextToControlDistance = 3;");
                    swGUI3.WriteLine("            // ");
                    y += 25;
                    if (y > 500)
                    {
                        x += 300;
                        y = 0;
                    }
                }
                #endregion
            }
            else
            {
                x = 25;
                y = 45;
                foreach (pMap item in fieldList)
                {
                    swGUI3.WriteLine("            this.lbl" + item.Name + ".Location = new System.Drawing.Point(" + x + ", " + y + ");");
                    swGUI3.WriteLine("            this.lbl" + item.Name + ".Name = \"lbl" + item.Name + "\";");
                    swGUI3.WriteLine("            this.lbl" + item.Name + ".Size = new System.Drawing.Size(50, 13);");
                    swGUI3.WriteLine("            this.lbl" + item.Name + ".Text = \"" + item.Name + ": \";");
                    y += 25;
                    if (y > 500)
                    {
                        x += 400;
                        y = 45;
                    }
                }
            }
            #endregion

            swGUI3.WriteLine("            //");
            swGUI3.WriteLine("            //CRUD" + className);
            swGUI3.WriteLine("            //");
            swGUI3.WriteLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);");
            swGUI3.WriteLine("            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;");
            swGUI3.WriteLine("            this.ClientSize          = new System.Drawing.Size(800, 600);");
            if (useLayoutControl)
                swGUI3.WriteLine("            this.Controls.Add(this.layoutControl);");

            foreach (pMap item in fieldList)
            {
                swGUI3.WriteLine("            this.Controls.Add(this." + item.ControlPrefix + item.Name + ");");
                swGUI3.WriteLine("            this.Controls.Add(this.lbl" + item.Name + ");");
            }
            swGUI3.WriteLine("            this.Controls.Add(this.barDockControlLeft);");
            swGUI3.WriteLine("            this.Controls.Add(this.barDockControlRight);");
            swGUI3.WriteLine("            this.Controls.Add(this.barDockControlBottom);");
            swGUI3.WriteLine("            this.Controls.Add(this.barDockControlTop);");
            swGUI3.WriteLine("            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;");
            swGUI3.WriteLine("            this.Name          = \"CRUD" + className + "\";");
            swGUI3.WriteLine("            this.Text          = \"" + className + " Details\";");
            swGUI3.WriteLine("            this.Load         += new System.EventHandler(this.CRUD" + className + "_Load);");
            swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.bs" + className + ")).EndInit();");
            swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();");
            #endregion

            foreach (pMap item in fieldList)
            {
                swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this." + item.ControlPrefix + item.Name + ".Properties)).EndInit();");
            }

            #region Layout Control code
            if (useLayoutControl)
                swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();");
            #endregion

            nCounter = 0;
            foreach (pMap item in fieldList)
            {
                nCounter++;
                if (useLayoutControl)
                    swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem" + nCounter + ")).EndInit();");
                else
                    swGUI3.WriteLine("            ((System.ComponentModel.ISupportInitialize)(this." + item.ControlPrefix + item.Name + ".Properties)).EndInit();");
            }

            swGUI3.WriteLine("            this.ResumeLayout(false);");
            swGUI3.WriteLine("            this.PerformLayout();");
            swGUI3.WriteLine("      }");
            #endregion

            #region private variables
            // Write out the form controls not directly linked to fields. (Menu elements, etc)
            swGUI3.WriteLine(" ");
            #region Layout Control code
            if (useLayoutControl)
            {
                swGUI3.WriteLine("        private DevExpress.XtraLayout.LayoutControl layoutControl;");
                swGUI3.WriteLine("        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;");
            }
            #endregion
            swGUI3.WriteLine("        private System.Windows.Forms.BindingSource bs" + className + ";");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarManager barManager;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.Bar bar1;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnNew;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnEdit;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnDelete;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnSave;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnPrint;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarButtonItem btnClose;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlTop;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlBottom;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlLeft;");
            swGUI3.WriteLine("        private DevExpress.XtraBars.BarDockControl barDockControlRight;");

            // Loop thru all fields and create  a control based on each field type
            foreach (pMap item in fieldList)
            {
                swGUI3.WriteLine("        private DevExpress.XtraEditors." + item.ControlName + " " + item.ControlPrefix + item.Name + ";");
            }

            #region layout or Labels
            if (useLayoutControl)
            {
                nCounter = 0;
                foreach (pMap item in fieldList)
                {
                    nCounter++;
                    swGUI3.WriteLine("        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem" + nCounter + ";");
                }
            }
            else
            {
                foreach (pMap item in fieldList)
                {// write out label control
                    swGUI3.WriteLine("        private DevExpress.XtraEditors.LabelControl lbl" + item.Name + ";");
                }
            }
            #endregion
            swGUI3.WriteLine("    }");
            swGUI3.WriteLine("}");
            #endregion

            swGUI3.Flush();
            swGUI3.Close();
            swGUI3.Dispose();

            #region Create CRUD RESX file
            if (!boolCRUDRESX)
            {
                StreamWriter swCrudRESX = File.CreateText(GUIfolderName + "\\" + txtPrefixGUI.Text.Trim() + "CRUD" + className + ".resx");
                swCrudRESX.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                swCrudRESX.WriteLine("<root>");
                swCrudRESX.WriteLine(" <xsd:schema id=\"root\" xmlns=\"\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:msdata=\"urn:schemas-microsoft-com:xml-msdata\">\"");
                swCrudRESX.WriteLine("     <xsd:import namespace=\"http://www.w3.org/XML/1998/namespace\" />");
                swCrudRESX.WriteLine("         <xsd:element name=\"root\" msdata:IsDataSet=\"true\">");
                swCrudRESX.WriteLine("           <xsd:complexType>");
                swCrudRESX.WriteLine("             <xsd:choice maxOccurs=\"unbounded\">");
                swCrudRESX.WriteLine("               <xsd:element name=\"metadata\">");
                swCrudRESX.WriteLine("                 <xsd:complexType>");
                swCrudRESX.WriteLine("                   <xsd:sequence>");
                swCrudRESX.WriteLine("                     <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" />");
                swCrudRESX.WriteLine("                   </xsd:sequence>");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"name\" use=\"required\" type=\"xsd:string\" />");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"type\" type=\"xsd:string\" />");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"mimetype\" type=\"xsd:string\" />");
                swCrudRESX.WriteLine("                   <xsd:attribute ref=\"xml:space\" />");
                swCrudRESX.WriteLine("                 </xsd:complexType>");
                swCrudRESX.WriteLine("               </xsd:element>");
                swCrudRESX.WriteLine("               <xsd:element name=\"assembly\">");
                swCrudRESX.WriteLine("                 <xsd:complexType>");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"alias\" type=\"xsd:string\" />");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"name\" type=\"xsd:string\" />");
                swCrudRESX.WriteLine("                 </xsd:complexType>");
                swCrudRESX.WriteLine("               </xsd:element>");
                swCrudRESX.WriteLine("               <xsd:element name=\"data\">");
                swCrudRESX.WriteLine("                 <xsd:complexType>");
                swCrudRESX.WriteLine("                   <xsd:sequence>");
                swCrudRESX.WriteLine("                     <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />");
                swCrudRESX.WriteLine("                     <xsd:element name=\"comment\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"2\" />");
                swCrudRESX.WriteLine("                   </xsd:sequence>");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" msdata:Ordinal=\"1\" />");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"type\" type=\"xsd:string\" msdata:Ordinal=\"3\" />");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"mimetype\" type=\"xsd:string\" msdata:Ordinal=\"4\" />");
                swCrudRESX.WriteLine("                   <xsd:attribute ref=\"xml:space\" />");
                swCrudRESX.WriteLine("                 </xsd:complexType>");
                swCrudRESX.WriteLine("               </xsd:element>");
                swCrudRESX.WriteLine("               <xsd:element name=\"resheader\">");
                swCrudRESX.WriteLine("                 <xsd:complexType>");
                swCrudRESX.WriteLine("                   <xsd:sequence>");
                swCrudRESX.WriteLine("                     <xsd:element name=\"value\" type=\"xsd:string\" minOccurs=\"0\" msdata:Ordinal=\"1\" />");
                swCrudRESX.WriteLine("                   </xsd:sequence>");
                swCrudRESX.WriteLine("                   <xsd:attribute name=\"name\" type=\"xsd:string\" use=\"required\" />");
                swCrudRESX.WriteLine("                 </xsd:complexType>");
                swCrudRESX.WriteLine("               </xsd:element>");
                swCrudRESX.WriteLine("             </xsd:choice>");
                swCrudRESX.WriteLine("           </xsd:complexType>");
                swCrudRESX.WriteLine("         </xsd:element>");
                swCrudRESX.WriteLine("       </xsd:schema>");
                swCrudRESX.WriteLine("       <resheader name=\"resmimetype\">");
                swCrudRESX.WriteLine("         <value>text/microsoft-resx</value>");
                swCrudRESX.WriteLine("       </resheader>");
                swCrudRESX.WriteLine("       <resheader name=\"version\">");
                swCrudRESX.WriteLine("         <value>2.0</value>");
                swCrudRESX.WriteLine("       </resheader>");
                swCrudRESX.WriteLine("       <resheader name=\"reader\">");
                swCrudRESX.WriteLine("         <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
                swCrudRESX.WriteLine("       </resheader>");
                swCrudRESX.WriteLine("       <resheader name=\"writer\">");
                swCrudRESX.WriteLine("         <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>");
                swCrudRESX.WriteLine("       </resheader>");
                swCrudRESX.WriteLine("       <metadata name=\"barManager.TrayLocation\" type=\"System.Drawing.Point, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\">");
                swCrudRESX.WriteLine("           <value>17, 17</value>");
                swCrudRESX.WriteLine("       </metadata>");
                swCrudRESX.WriteLine("       <metadata name=\"bs" + className + ".TrayLocation\" type=\"System.Drawing.Point, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\">");
                swCrudRESX.WriteLine("           <value>142, 17</value>");
                swCrudRESX.WriteLine("       </metadata>");
                swCrudRESX.WriteLine("       </root>");
                swCrudRESX.Flush();
                swCrudRESX.Close();
                swCrudRESX.Dispose();
                boolCRUDRESX = true;
            }
            #endregion
        }
        #endregion

        // Utils
        private String MakeCaption(String str)
        {
            string converted = str;
            if ((str.Trim() != string.Empty) && (str.Trim().IndexOf(" ") > 0))
            {
                converted = str.Substring(0, 1).Trim().ToUpper();
                for (int i = 1; i < str.Length; i++)
                {
                    converted = (Char.IsUpper(str[i])) ?
                    converted += " " :
                    converted += str[i];
                }
            }
            return converted;
        }

        private void chkDevExpress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDevExpress.CheckState == CheckState.Unchecked)
            {
                chkGUI.Checked = false;
                chkFilterMgr.Checked = false;
                chkLayout.Checked = false;
            }
        }

        #region  Aspx code
        private void chkAspx_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAspx.CheckState == CheckState.Checked)
                grpAspx.Visible = true;
            else
                grpAspx.Visible = false;
        }

        private void btnAspxFile_Click(object sender, EventArgs e)
        {
            GetAspxFile();
        }

        private void GetAspxFile()
        {
            #region Set write folder
            if (fbTarget.ShowDialog() == DialogResult.OK)
            {
                if (fbTarget.SelectedPath.Trim() != string.Empty)
                    AspxfolderName = fbTarget.SelectedPath.Trim();
                else
                    AspxfolderName = @"C:\";
            }
            else
                AspxfolderName = @"C:\";
            txtAspxFile.Text = fbTarget.SelectedPath;
            #endregion
        }

        private void CreateAspxClass()
        {            
            string CssClassTitle = (txtTitle.Text == "")?"": "CssClass=\""+ txtTitle.Text+"\"";
            string CssClassTextBox = (txtTextBox.Text == "") ? "" : "CssClass=\"" + txtTextBox.Text + "\"";
            string CssClassButton = (txtButton.Text == "") ? "" : "CssClass=\"" + txtButton.Text + "\"";
            string CssClassLabel = (txtLabel.Text == "") ? "" : "CssClass=\"" + txtLabel.Text + "\"";
            string CssClassGrid = (txtGrid.Text == "") ? "" : "CssClass=\"" + txtGrid.Text + "\"";
            string CssClasslinkButton = (txtLinkButton.Text == "") ? "" : "CssClass=\"" + txtLinkButton.Text + "\"";            
            
            #region Write Aspx.cs file
            StreamWriter sw = File.CreateText(AspxfolderName + "\\" + txtPrefixAspx.Text.Trim() + pluralClassName + ".aspx.cs");
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Linq;");
            sw.WriteLine("using System.Web;");
            sw.WriteLine("using System.Web.UI;");
            sw.WriteLine("using System.Web.UI.WebControls;");
            sw.WriteLine("using " + txtBLLNamespace.Text.Trim() + ";");
            sw.WriteLine("using " + txtBENamespace.Text.Trim() + ";");
            sw.WriteLine("using " + txtBENamespace.Text.Trim() + ".Constants;");
            sw.WriteLine("");
            sw.WriteLine("namespace " + txtNamespaceAspx.Text.Trim());
            sw.WriteLine("{");
            sw.WriteLine("    public partial class " + txtPrefixAspx.Text.Trim() + pluralClassName + " : System.Web.UI.Page");
            sw.WriteLine("    {");

            sw.WriteLine("        protected " + className + " " + className.Substring(0, 4) + "; ");
            sw.WriteLine("");

            sw.WriteLine("        protected void Page_Load(object sender, EventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            if (!IsPostBack)");
            sw.WriteLine("              {");
            sw.WriteLine("                  lblSucesso.Visible = false;");
            sw.WriteLine("                  lblErro.Visible = false;");
            sw.WriteLine("                  CarregarGrid();");
            sw.WriteLine("                  BtnSave.Visible = true;");
            sw.WriteLine("                  BtnCancel.Visible = false;");
            sw.WriteLine("              }");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void BtnAdd_Click(object sender, EventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            Limpar(Page.Controls);");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void BtnSave_Click(object sender, EventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            Gravar();");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void BtnCancel_Click(object sender, EventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            Limpar(Page.Controls);");
            sw.WriteLine("            lblSucesso.Visible = false;");
            sw.WriteLine("            lblErro.Visible = false;");
            sw.WriteLine("            gdv" + className + ".EditIndex = -1;");
            sw.WriteLine("            CarregarGrid();");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void gdv" + className + "_RowCommand(object sender, GridViewCommandEventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            int index = Convert.ToInt32(e.CommandArgument);");
            sw.WriteLine("            GridViewRow row = gdv" + className + ".Rows[index];");
            sw.WriteLine("            int codigo = Int32.Parse(gdv" + className + ".DataKeys[index].Value.ToString());");

            sw.WriteLine("            if (e.CommandName == \"Edit\")");
            sw.WriteLine("            {");
            sw.WriteLine("              CarregarCampos(codigo);");
            sw.WriteLine("            }");

            sw.WriteLine("            if (e.CommandName == \"Delete\")");
            sw.WriteLine("            {");
            sw.WriteLine("              Deletar(codigo);");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void Limpar(ControlCollection controlCollection)");
            sw.WriteLine("        {");
            sw.WriteLine("            BtnCancel.Visible = false;");

            sw.WriteLine("            foreach (Control control in controlCollection)");
            sw.WriteLine("            {");
            sw.WriteLine("              if (control is TextBox)");
            sw.WriteLine("              {");
            sw.WriteLine("                  ((TextBox)control).Text = \"\";");
            sw.WriteLine("              }");

            sw.WriteLine("              if (control is DropDownList)");
            sw.WriteLine("              {");
            sw.WriteLine("                  ((DropDownList)control).SelectedIndex = -1;");
            sw.WriteLine("              }");

            sw.WriteLine("              if (control.Controls != null)");
            sw.WriteLine("              {");
            sw.WriteLine("                  Limpar(control.Controls);");
            sw.WriteLine("              }");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void CarregarGrid()");
            sw.WriteLine("        {");
            sw.WriteLine("            List<" + className + "> lst" + className.Substring(0, 4) + " = " + pluralClassName + ".Find();");

            sw.WriteLine("            gdv" + className + ".DataSource = lst" + className.Substring(0, 4) + ";");
            sw.WriteLine("            gdv" + className + ".DataBind();");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void Deletar(int Id)");
            sw.WriteLine("        {");
            sw.WriteLine("            " + className.Substring(0, 4) + " = " + pluralClassName + ".FindByID(Id);");
            sw.WriteLine("            if (" + pluralClassName + ".Delete(" + className.Substring(0, 4) + "))");
            sw.WriteLine("            {");
            sw.WriteLine("              lblSucesso.Text = \"Deletado com sucesso;\";");
            sw.WriteLine("              lblSucesso.Visible = true;");
            sw.WriteLine("            }");
            sw.WriteLine("            else");
            sw.WriteLine("            {");
            sw.WriteLine("              lblErro.Text = \"Ocorreu um problema para deletar.\";");
            sw.WriteLine("              lblErro.Visible = false;");
            sw.WriteLine("            }");

            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void gdv" + className + "_RowDeleting(object sender, GridViewDeleteEventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            gdv" + className + ".DataBind();");
            sw.WriteLine("            CarregarGrid();");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void gdv" + className + "_RowEditing(object sender, GridViewEditEventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            gdv" + className + ".DataBind();");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void gdv" + className + "_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            gdv" + className + ".EditIndex = -1;");
            sw.WriteLine("            gdv" + className + ".DataBind();");
            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void CarregarCampos(int Id)");
            sw.WriteLine("        {");
            sw.WriteLine("            BtnCancel.Visible = true;");
            sw.WriteLine("            lblSucesso.Visible = false;");
            sw.WriteLine("            lblErro.Visible = false;");
            sw.WriteLine("");
            sw.WriteLine("            " + className.Substring(0, 4) + " = " + pluralClassName + ".FindByID(Id);");

            #region save data from table to control
            foreach (pMap item in fieldList)
            {
                switch (item.Type.ToLower())
                {
                    case "bigint":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "varchar":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "uniqueidentifier":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "bool":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "boolean":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "int32":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "int":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "decimal":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = Convert.ToDecimal(" + className.Substring(0, 4) + "." + item.Name + ");");
                        break;
                    case "string":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "double":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = Convert.ToDouble(" + className.Substring(0, 4) + "." + item.Name + ");");
                        break;
                    case "byte":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = Convert.ToSingle(" + className.Substring(0, 4) + "." + item.Name + ");");
                        break;
                    case "byte[]":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "short":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = Convert.ToDouble(" + className.Substring(0, 4) + "." + item.Name + ");");
                        break;
                    case "long":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = Convert.ToDouble(" + className.Substring(0, 4) + "." + item.Name + ");");
                        break;
                    case "char":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "datetime":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "time":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "image":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    case "varbinary":
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        break;
                    default:
                        sw.WriteLine("            " + item.AspControlPrefix + item.Name + "." + item.AspEditField + " = " + className.Substring(0, 4) + "." + item.Name + ";");
                        MessageBox.Show("Potential problem with setting WebForm control type: " + item.Name, item.Type);
                        break;
                }
            }
            #endregion Loop campos

            sw.WriteLine("        }");
            sw.WriteLine("");

            sw.WriteLine("        protected void Gravar()");
            sw.WriteLine("        {");
            sw.WriteLine("            bool gravado = false;");
            sw.WriteLine("            " + className + " obj = new " + className + "();");

            #region save control data back to table
            foreach (pMap item in fieldList)
            {
                if (item.Key)
                {
                    sw.WriteLine("            if(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + "!= \"\")");
                    sw.WriteLine("              obj." + item.Name + " = Convert.ToInt32(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                }
                else
                {
                    switch (item.Type.ToLower())
                    {
                        case "bigint":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToInt32(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "varchar":
                            sw.WriteLine("            obj." + item.Name + " = " + item.AspControlPrefix + item.Name + "." + item.AspEditField + ";");
                            break;
                        case "uniqueidentifier":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToInt32(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "bool":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToBoolean(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "boolean":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToBoolean(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "int32":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToInt32(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "int":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToInt32(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "decimal":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToDecimal(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "string":
                            sw.WriteLine("            obj." + item.Name + " = " + item.AspControlPrefix + item.Name + "." + item.AspEditField + ";");
                            break;
                        case "double":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToDecimal(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "byte":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToDecimal(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "short":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToDecimal(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "long":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToDecimal(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "char":
                            sw.WriteLine("            obj." + item.Name + " = " + item.AspControlPrefix + item.Name + "." + item.AspEditField + ".ToString();");
                            break;
                        case "datetime":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToDateTime(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "time":
                            sw.WriteLine("            obj." + item.Name + " = Convert.ToTime(" + item.AspControlPrefix + item.Name + "." + item.AspEditField + ");");
                            break;
                        case "byte[]":
                            sw.WriteLine("            obj." + item.Name + " = " + item.AspControlPrefix + item.Name + "." + item.AspEditField + ";");
                            break;
                        case "image":
                            sw.WriteLine("            obj." + item.Name + " = " + item.AspControlPrefix + item.Name + "." + item.AspEditField + ";");
                            break;
                        case "varbinary":
                            sw.WriteLine("            obj." + item.Name + " = " + item.AspControlPrefix + item.Name + "." + item.AspEditField + ";");
                            break;
                        default:
                            MessageBox.Show("Potential problem converting control data to table field: " + item.Name, item.Type);
                            break;
                    }
                }
            }
            #endregion

            sw.WriteLine("            gravado = " + pluralClassName + ".Save(ref obj);");

            sw.WriteLine("            if (gravado)");
            sw.WriteLine("            {");
            sw.WriteLine("              lblSucesso.Text = \"Gravado com sucesso;\";");
            sw.WriteLine("              lblSucesso.Visible = true;");
            sw.WriteLine("              Limpar(Page.Controls);");
            sw.WriteLine("              gdv" + className + ".EditIndex = -1;");
            sw.WriteLine("              CarregarGrid();");
            sw.WriteLine("            }");
            sw.WriteLine("            else");
            sw.WriteLine("            {");
            sw.WriteLine("              lblErro.Text = \"Ocorreu um problema para gravar.\";");
            sw.WriteLine("              lblErro.Visible = false;");
            sw.WriteLine("            }");
            sw.WriteLine("        }");

            sw.WriteLine("        protected void lbtPaginaAnterior_Click(object sender, EventArgs e)");
            sw.WriteLine("        {");
            sw.WriteLine("            Response.Redirect(\"~/Default.aspx\");");
            sw.WriteLine("        }");

            sw.WriteLine("    }");
            sw.WriteLine("}");
            sw.Close();
            sw.Dispose();
            #endregion

            #region Write Aspx file
            StreamWriter swA = File.CreateText(AspxfolderName + "\\" + txtPrefixAspx.Text.Trim() + pluralClassName + ".aspx");

            swA.WriteLine("<%@ Page Title=\"\" Language=\"C#\" MasterPageFile=\"~/Site.Master\" AutoEventWireup=\"true\" CodeBehind=\"" + txtPrefixAspx.Text.Trim() + pluralClassName + ".aspx.cs\" Inherits=\"" + txtNamespaceAspx.Text.Trim() + "." + txtPrefixAspx.Text.Trim() + pluralClassName + "\" %>");

            swA.WriteLine("<%@ Register Assembly=\"AjaxControlToolkit\" Namespace=\"AjaxControlToolkit\" TagPrefix=\"ajaxToolkit\" %>");
            swA.WriteLine("<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"MainContent\" runat=\"server\">");
            swA.WriteLine("    <div id=\"errorDialog\" title=\"Error on item insertion!\"></div>");

            swA.WriteLine("    <h3 id=\"h3AddEditRecord\" class=\"ui-widget-header\">");
            swA.WriteLine("        <asp:Label ID=\"LblTitle\" " + CssClassTitle + " Text=\"" + className + "\" runat=\"server\" />");
            swA.WriteLine("    </h3>");
            swA.WriteLine("    <table id=\"Table1\">");

            #region create asp.net controls
            foreach (pMap item in fieldList)
            {
                if (item.Key)
                {
                    swA.WriteLine("        <asp:Panel ID=\"PnlPrimaryKey\" runat=\"server\">");
                    swA.WriteLine("            <tr>");
                    swA.WriteLine("                <td>ID:</td>");
                    swA.WriteLine("                <td></td>");
                    swA.WriteLine("                <td colspan=\"2\">");
                    swA.WriteLine("                    <asp:TextBox ID=\"" + item.AspControlPrefix + item.Name + "\" " + CssClassTextBox + " runat=\"server\" Enabled=\"False\" /></td>");
                    swA.WriteLine("            </tr>");
                    swA.WriteLine("        </asp:Panel>");
                }
                else
                {

                    swA.WriteLine("                <tr>");
                    swA.WriteLine("                    <td>" + ToTitleCase(item.Name.ToLower()) + ":</td>");
                    if (item.Required)
                        swA.WriteLine("                    <td>&nbsp;<span style=\"color: red;\">*</span>&nbsp;</td>");
                    else
                        swA.WriteLine("                    <td></td>");
                    swA.WriteLine("                    <td>");
                    swA.WriteLine("                        <asp:" + item.AspControlType + " ID=\"" + item.AspControlPrefix + item.Name + "\" " + CssClassTextBox + " runat=\"server\" /></td>");
                    if (item.Required)
                    {
                        swA.WriteLine("                    <td>");
                        swA.WriteLine("                        <asp:RequiredFieldValidator ID=\"Rfv" + ToTitleCase(item.Name.ToLower()) + "\" ControlToValidate=\"" + item.AspControlPrefix + item.Name + "\" ErrorMessage=\"" + ToTitleCase(item.Name.ToLower()) + " é obrigatório!\" Display=\"Dynamic\" runat=\"server\" /></td>");
                    }
                    swA.WriteLine("                </tr>");
                }
            }
            #endregion

            swA.WriteLine("        <tr>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td>&nbsp;</td>");
            swA.WriteLine("        </tr>");

            swA.WriteLine("        <tr>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td colspan=\"2\">");
            swA.WriteLine("                <asp:Button ID=\"BtnSave\" Text=\"Gravar\" " + CssClassButton + " runat=\"server\" OnClick=\"BtnSave_Click\" Height=\"26px\" Width=\"100px\" />");
            swA.WriteLine("                <asp:Button ID=\"BtnCancel\" Text=\"Cancelar\" " + CssClassButton + " runat=\"server\" Visible=\"false\" OnClick=\"BtnCancel_Click\" Height=\"26px\" Width=\"100px\" />");
            swA.WriteLine("                <asp:Label ID=\"lblSucesso\" " + CssClassLabel + " runat=\"server\" Text=\"Gravado com sucesso!\" Visible=\"False\"></asp:Label>");
            swA.WriteLine("                <asp:Label ID=\"lblErro\" " + CssClassLabel + " runat=\"server\" Text=\"Erro na gravação!\" Visible=\"False\"></asp:Label>");
            swA.WriteLine("            </td>");
            swA.WriteLine("        </tr>");
            swA.WriteLine("        <tr>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td>&nbsp;</td>");
            swA.WriteLine("        </tr>");

            swA.WriteLine("        <tr>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td colspan=\"2\">");

            string DataKeyName = "";
            foreach (pMap item in fieldList)
            {
                if (item.Key)
                {
                    DataKeyName = item.Name;
                }
            }
            swA.WriteLine("                <asp:GridView ID=\"gdv" + className + "\" " + CssClassGrid + " runat=\"server\" AutoGenerateColumns=\"False\" Width=\"501px\" DataKeyNames=\"" + DataKeyName + "\" OnRowCommand=\"gdv" + className + "_RowCommand\" OnRowCancelingEdit=\"gdv" + className + "_RowCancelingEdit\" OnRowDeleting=\"gdv" + className + "_RowDeleting\" OnRowEditing=\"gdv" + className + "_RowEditing\">");
            swA.WriteLine("                    <Columns>");

            foreach (pMap item in fieldList)
            {
                swA.WriteLine("                        <asp:BoundField DataField=\"" + item.Name + "\" HeaderText=\"" + ToTitleCase(item.Name.ToLower()) + "\" />");
            }
            swA.WriteLine("                        <asp:ButtonField Text=\"Editar\" CommandName=\"Edit\" />");
            swA.WriteLine("                        <asp:ButtonField Text=\"Deletar\" CommandName=\"Delete\" />");
            swA.WriteLine("                    </Columns>");
            swA.WriteLine("                </asp:GridView>");
            swA.WriteLine("            </td>");
            swA.WriteLine("        </tr>");

            swA.WriteLine("        <tr>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td>&nbsp;</td>");
            swA.WriteLine("        </tr>");
            swA.WriteLine("        <tr>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td></td>");
            swA.WriteLine("            <td>");
            swA.WriteLine("                <asp:LinkButton ID=\"lbtPaginaAnterior\" " + CssClasslinkButton + " runat=\"server\" OnClick=\"lbtPaginaAnterior_Click\" CausesValidation=\"False\">Voltar para a página anterior</asp:LinkButton>");
            swA.WriteLine("            </td>");
            swA.WriteLine("        </tr>");

            swA.WriteLine("    </table>");
            swA.WriteLine("</asp:Content>");


            swA.Close();
            swA.Dispose();
            #endregion

            #region Write aspx.designer.cs file
            StreamWriter swd = File.CreateText(AspxfolderName + "\\" + txtPrefixAspx.Text.Trim() + pluralClassName + ".aspx.designer.cs");

            swd.WriteLine("namespace " + txtNamespaceAspx.Text.Trim());
            swd.WriteLine("{");
            swd.WriteLine("    public partial class " + txtPrefixAspx.Text.Trim() + pluralClassName );            
            swd.WriteLine("    {");

            swd.WriteLine("        protected global::System.Web.UI.WebControls.Label LblTitle;");



            foreach (pMap item in fieldList)
            {
                if (item.Key)
                {
                    swd.WriteLine("        protected global::System.Web.UI.WebControls.Panel PnlPrimaryKey;");
                    swd.WriteLine("        protected global::System.Web.UI.WebControls." + item.AspControlType + " " + item.AspControlPrefix + item.Name + ";");
                }
                else
                {

                    swd.WriteLine("        protected global::System.Web.UI.WebControls." + item.AspControlType + " " + item.AspControlPrefix + item.Name + ";");
                    if (item.Required)
                        swd.WriteLine("        protected global::System.Web.UI.WebControls.RequiredFieldValidator Rfv" + ToTitleCase(item.Name.ToLower()) + ";");
                }
            }

            swd.WriteLine("        protected global::System.Web.UI.WebControls.Button BtnSave;");

            swd.WriteLine("        protected global::System.Web.UI.WebControls.Button BtnCancel;");

            swd.WriteLine("        protected global::System.Web.UI.WebControls.Label lblSucesso;");

            swd.WriteLine("        protected global::System.Web.UI.WebControls.Label lblErro;");

            swd.WriteLine("        protected global::System.Web.UI.WebControls.GridView gdv"+className+";");

            swd.WriteLine("        protected global::System.Web.UI.WebControls.LinkButton lbtPaginaAnterior;");
            swd.WriteLine("     }");
            swd.WriteLine("}");

            swd.Close();
            swd.Dispose();
            #endregion
        }

        #endregion

        private void grpAspx_Enter(object sender, EventArgs e)
        {

        }


    }
}
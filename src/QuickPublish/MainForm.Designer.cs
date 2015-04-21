namespace QuickPublish
{
    partial class MainForm
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
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pageNormal = new System.Windows.Forms.TabPage();
            this.chkIsCLR = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExecuteFile = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAssemblyInfoFile = new System.Windows.Forms.TextBox();
            this.txtDistributionPath = new System.Windows.Forms.TextBox();
            this.pageAssembly = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAssemblyOriginatorKeyFile = new System.Windows.Forms.TextBox();
            this.btnBrowse_AssemblyOriginatorKeyFile = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtManifestCertificateThumbprint = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtManifestKeyFile = new System.Windows.Forms.TextBox();
            this.btnBrowse_ManifestKey = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAssemblyName = new System.Windows.Forms.TextBox();
            this.pagePublish = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numApplicationRevision = new System.Windows.Forms.NumericUpDown();
            this.chkEnableApplicationRevision = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtApplicationVersionBuild = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtApplicationVersionMinor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApplicationVersionMajor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPublishOptions = new System.Windows.Forms.Button();
            this.btnApplicationUpdate = new System.Windows.Forms.Button();
            this.btnBootstrapper = new System.Windows.Forms.Button();
            this.btnApplicationFiles = new System.Windows.Forms.Button();
            this.radioInstallEnable = new System.Windows.Forms.RadioButton();
            this.radioInstallDisable = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInstallUrl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPublishUrl = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pageOthers = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportToCSProject = new System.Windows.Forms.ToolStripButton();
            this.tsbPublish = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.pageNormal.SuspendLayout();
            this.pageAssembly.SuspendLayout();
            this.pagePublish.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numApplicationRevision)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(19, 28);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(101, 12);
            label1.TabIndex = 31;
            label1.Text = "应用程序目录(&B):";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.pageNormal);
            this.tabControl1.Controls.Add(this.pageAssembly);
            this.tabControl1.Controls.Add(this.pagePublish);
            this.tabControl1.Controls.Add(this.pageOthers);
            this.tabControl1.Location = new System.Drawing.Point(12, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(745, 479);
            this.tabControl1.TabIndex = 11;
            // 
            // pageNormal
            // 
            this.pageNormal.Controls.Add(this.chkIsCLR);
            this.pageNormal.Controls.Add(this.label2);
            this.pageNormal.Controls.Add(this.txtExecuteFile);
            this.pageNormal.Controls.Add(this.label12);
            this.pageNormal.Controls.Add(this.txtAssemblyInfoFile);
            this.pageNormal.Controls.Add(this.txtDistributionPath);
            this.pageNormal.Controls.Add(label1);
            this.pageNormal.Location = new System.Drawing.Point(4, 22);
            this.pageNormal.Name = "pageNormal";
            this.pageNormal.Size = new System.Drawing.Size(737, 453);
            this.pageNormal.TabIndex = 3;
            this.pageNormal.Text = "常规";
            this.pageNormal.UseVisualStyleBackColor = true;
            // 
            // chkIsCLR
            // 
            this.chkIsCLR.AutoSize = true;
            this.chkIsCLR.Location = new System.Drawing.Point(119, 102);
            this.chkIsCLR.Name = "chkIsCLR";
            this.chkIsCLR.Size = new System.Drawing.Size(108, 16);
            this.chkIsCLR.TabIndex = 37;
            this.chkIsCLR.Text = "为托管应用程序";
            this.chkIsCLR.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "ExecuteFile 位置(&A):";
            // 
            // txtExecuteFile
            // 
            this.txtExecuteFile.Location = new System.Drawing.Point(119, 75);
            this.txtExecuteFile.Name = "txtExecuteFile";
            this.txtExecuteFile.Size = new System.Drawing.Size(455, 21);
            this.txtExecuteFile.TabIndex = 35;
            this.txtExecuteFile.Text = "ShopAsstX.exe";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(42, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 12);
            this.label12.TabIndex = 34;
            this.label12.Text = "AssemblyInfo.cs 位置(&A):";
            // 
            // txtAssemblyInfoFile
            // 
            this.txtAssemblyInfoFile.Location = new System.Drawing.Point(119, 169);
            this.txtAssemblyInfoFile.Name = "txtAssemblyInfoFile";
            this.txtAssemblyInfoFile.Size = new System.Drawing.Size(455, 21);
            this.txtAssemblyInfoFile.TabIndex = 33;
            this.txtAssemblyInfoFile.Text = "D:\\items\\TaobaoHelper\\work\\src\\ShopAsst\\Properties\\AssemblyInfo.cs";
            // 
            // txtDistributionPath
            // 
            this.txtDistributionPath.Location = new System.Drawing.Point(126, 25);
            this.txtDistributionPath.Name = "txtDistributionPath";
            this.txtDistributionPath.Size = new System.Drawing.Size(455, 21);
            this.txtDistributionPath.TabIndex = 32;
            this.txtDistributionPath.Text = "D:\\items\\TaobaoHelper\\work\\dist";
            // 
            // pageAssembly
            // 
            this.pageAssembly.Controls.Add(this.label9);
            this.pageAssembly.Controls.Add(this.txtAssemblyOriginatorKeyFile);
            this.pageAssembly.Controls.Add(this.btnBrowse_AssemblyOriginatorKeyFile);
            this.pageAssembly.Controls.Add(this.label10);
            this.pageAssembly.Controls.Add(this.txtManifestCertificateThumbprint);
            this.pageAssembly.Controls.Add(this.label11);
            this.pageAssembly.Controls.Add(this.txtManifestKeyFile);
            this.pageAssembly.Controls.Add(this.btnBrowse_ManifestKey);
            this.pageAssembly.Controls.Add(this.label13);
            this.pageAssembly.Controls.Add(this.txtAssemblyName);
            this.pageAssembly.Location = new System.Drawing.Point(4, 22);
            this.pageAssembly.Name = "pageAssembly";
            this.pageAssembly.Padding = new System.Windows.Forms.Padding(3);
            this.pageAssembly.Size = new System.Drawing.Size(737, 453);
            this.pageAssembly.TabIndex = 0;
            this.pageAssembly.Text = "程序集";
            this.pageAssembly.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "AssemblyOriginatorKeyFile:";
            // 
            // txtAssemblyOriginatorKeyFile
            // 
            this.txtAssemblyOriginatorKeyFile.Location = new System.Drawing.Point(101, 91);
            this.txtAssemblyOriginatorKeyFile.Name = "txtAssemblyOriginatorKeyFile";
            this.txtAssemblyOriginatorKeyFile.Size = new System.Drawing.Size(539, 21);
            this.txtAssemblyOriginatorKeyFile.TabIndex = 27;
            this.txtAssemblyOriginatorKeyFile.Text = "C:\\Program Files\\chuan\'gen\\ClickOnceBuilder\\keys\\loader.snk";
            // 
            // btnBrowse_AssemblyOriginatorKeyFile
            // 
            this.btnBrowse_AssemblyOriginatorKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse_AssemblyOriginatorKeyFile.Location = new System.Drawing.Point(646, 91);
            this.btnBrowse_AssemblyOriginatorKeyFile.Name = "btnBrowse_AssemblyOriginatorKeyFile";
            this.btnBrowse_AssemblyOriginatorKeyFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse_AssemblyOriginatorKeyFile.TabIndex = 26;
            this.btnBrowse_AssemblyOriginatorKeyFile.Text = "浏览(&B)...";
            this.btnBrowse_AssemblyOriginatorKeyFile.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "ManifestCertificateThumbprint:";
            // 
            // txtManifestCertificateThumbprint
            // 
            this.txtManifestCertificateThumbprint.Location = new System.Drawing.Point(101, 204);
            this.txtManifestCertificateThumbprint.Name = "txtManifestCertificateThumbprint";
            this.txtManifestCertificateThumbprint.ReadOnly = true;
            this.txtManifestCertificateThumbprint.Size = new System.Drawing.Size(539, 21);
            this.txtManifestCertificateThumbprint.TabIndex = 24;
            this.txtManifestCertificateThumbprint.Text = "3DE49C5D38A38A4A0ADCA43BB140DB49C8DF1B0E";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "ManifestKeyFile:";
            // 
            // txtManifestKeyFile
            // 
            this.txtManifestKeyFile.Location = new System.Drawing.Point(101, 156);
            this.txtManifestKeyFile.Name = "txtManifestKeyFile";
            this.txtManifestKeyFile.Size = new System.Drawing.Size(539, 21);
            this.txtManifestKeyFile.TabIndex = 22;
            this.txtManifestKeyFile.Text = "C:\\Program Files\\chuan\'gen\\ClickOnceBuilder\\keys\\ManifestKey.pfx";
            // 
            // btnBrowse_ManifestKey
            // 
            this.btnBrowse_ManifestKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse_ManifestKey.Location = new System.Drawing.Point(646, 154);
            this.btnBrowse_ManifestKey.Name = "btnBrowse_ManifestKey";
            this.btnBrowse_ManifestKey.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse_ManifestKey.TabIndex = 21;
            this.btnBrowse_ManifestKey.Text = "浏览(&B)...";
            this.btnBrowse_ManifestKey.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "Loader程序集名称(&N):";
            // 
            // txtAssemblyName
            // 
            this.txtAssemblyName.Location = new System.Drawing.Point(101, 41);
            this.txtAssemblyName.Name = "txtAssemblyName";
            this.txtAssemblyName.Size = new System.Drawing.Size(539, 21);
            this.txtAssemblyName.TabIndex = 17;
            this.txtAssemblyName.Text = "ShopAsst";
            // 
            // pagePublish
            // 
            this.pagePublish.Controls.Add(this.groupBox3);
            this.pagePublish.Controls.Add(this.groupBox2);
            this.pagePublish.Controls.Add(this.groupBox1);
            this.pagePublish.Location = new System.Drawing.Point(4, 22);
            this.pagePublish.Name = "pagePublish";
            this.pagePublish.Padding = new System.Windows.Forms.Padding(3);
            this.pagePublish.Size = new System.Drawing.Size(737, 453);
            this.pagePublish.TabIndex = 1;
            this.pagePublish.Text = "发布";
            this.pagePublish.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.numApplicationRevision);
            this.groupBox3.Controls.Add(this.chkEnableApplicationRevision);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtApplicationVersionBuild);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtApplicationVersionMinor);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtApplicationVersionMajor);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(6, 282);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(656, 85);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发布版本";
            // 
            // numApplicationRevision
            // 
            this.numApplicationRevision.Location = new System.Drawing.Point(287, 32);
            this.numApplicationRevision.Name = "numApplicationRevision";
            this.numApplicationRevision.Size = new System.Drawing.Size(82, 21);
            this.numApplicationRevision.TabIndex = 11;
            // 
            // chkEnableApplicationRevision
            // 
            this.chkEnableApplicationRevision.AutoSize = true;
            this.chkEnableApplicationRevision.Location = new System.Drawing.Point(19, 59);
            this.chkEnableApplicationRevision.Name = "chkEnableApplicationRevision";
            this.chkEnableApplicationRevision.Size = new System.Drawing.Size(186, 16);
            this.chkEnableApplicationRevision.TabIndex = 10;
            this.chkEnableApplicationRevision.Text = "随每次发布自动递增修订号(&Y)";
            this.chkEnableApplicationRevision.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "修订(&E):";
            // 
            // txtApplicationVersionBuild
            // 
            this.txtApplicationVersionBuild.Location = new System.Drawing.Point(201, 32);
            this.txtApplicationVersionBuild.Name = "txtApplicationVersionBuild";
            this.txtApplicationVersionBuild.Size = new System.Drawing.Size(80, 21);
            this.txtApplicationVersionBuild.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "内部版本(&D):";
            // 
            // txtApplicationVersionMinor
            // 
            this.txtApplicationVersionMinor.Location = new System.Drawing.Point(113, 32);
            this.txtApplicationVersionMinor.Name = "txtApplicationVersionMinor";
            this.txtApplicationVersionMinor.Size = new System.Drawing.Size(80, 21);
            this.txtApplicationVersionMinor.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "次要(&N):";
            // 
            // txtApplicationVersionMajor
            // 
            this.txtApplicationVersionMajor.Location = new System.Drawing.Point(19, 32);
            this.txtApplicationVersionMajor.Name = "txtApplicationVersionMajor";
            this.txtApplicationVersionMajor.Size = new System.Drawing.Size(80, 21);
            this.txtApplicationVersionMajor.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "主要(&J):";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnPublishOptions);
            this.groupBox2.Controls.Add(this.btnApplicationUpdate);
            this.groupBox2.Controls.Add(this.btnBootstrapper);
            this.groupBox2.Controls.Add(this.btnApplicationFiles);
            this.groupBox2.Controls.Add(this.radioInstallEnable);
            this.groupBox2.Controls.Add(this.radioInstallDisable);
            this.groupBox2.Location = new System.Drawing.Point(6, 137);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(656, 139);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "安装模式和设置";
            // 
            // btnPublishOptions
            // 
            this.btnPublishOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPublishOptions.Location = new System.Drawing.Point(520, 105);
            this.btnPublishOptions.Name = "btnPublishOptions";
            this.btnPublishOptions.Size = new System.Drawing.Size(121, 23);
            this.btnPublishOptions.TabIndex = 5;
            this.btnPublishOptions.Text = "选项(&S)...";
            this.btnPublishOptions.UseVisualStyleBackColor = true;
            this.btnPublishOptions.Click += new System.EventHandler(this.btnPublishOptions_Click);
            // 
            // btnApplicationUpdate
            // 
            this.btnApplicationUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplicationUpdate.Location = new System.Drawing.Point(520, 76);
            this.btnApplicationUpdate.Name = "btnApplicationUpdate";
            this.btnApplicationUpdate.Size = new System.Drawing.Size(121, 23);
            this.btnApplicationUpdate.TabIndex = 4;
            this.btnApplicationUpdate.Text = "更新(&U)...";
            this.btnApplicationUpdate.UseVisualStyleBackColor = true;
            this.btnApplicationUpdate.Click += new System.EventHandler(this.btnApplicationUpdate_Click);
            // 
            // btnBootstrapper
            // 
            this.btnBootstrapper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBootstrapper.Location = new System.Drawing.Point(520, 46);
            this.btnBootstrapper.Name = "btnBootstrapper";
            this.btnBootstrapper.Size = new System.Drawing.Size(121, 23);
            this.btnBootstrapper.TabIndex = 3;
            this.btnBootstrapper.Text = "系统必备(&Q)...";
            this.btnBootstrapper.UseVisualStyleBackColor = true;
            this.btnBootstrapper.Click += new System.EventHandler(this.btnBootstrapper_Click);
            // 
            // btnApplicationFiles
            // 
            this.btnApplicationFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplicationFiles.Location = new System.Drawing.Point(520, 17);
            this.btnApplicationFiles.Name = "btnApplicationFiles";
            this.btnApplicationFiles.Size = new System.Drawing.Size(121, 23);
            this.btnApplicationFiles.TabIndex = 2;
            this.btnApplicationFiles.Text = "应用程序文件(&I)...";
            this.btnApplicationFiles.UseVisualStyleBackColor = true;
            this.btnApplicationFiles.Click += new System.EventHandler(this.btnApplicationFiles_Click);
            // 
            // radioInstallEnable
            // 
            this.radioInstallEnable.AutoSize = true;
            this.radioInstallEnable.Location = new System.Drawing.Point(19, 54);
            this.radioInstallEnable.Name = "radioInstallEnable";
            this.radioInstallEnable.Size = new System.Drawing.Size(329, 16);
            this.radioInstallEnable.TabIndex = 1;
            this.radioInstallEnable.TabStop = true;
            this.radioInstallEnable.Text = "该应用程序也可以脱机使用(&可以从“开始”菜单启动)(&M)";
            this.radioInstallEnable.UseVisualStyleBackColor = true;
            // 
            // radioInstallDisable
            // 
            this.radioInstallDisable.AutoSize = true;
            this.radioInstallDisable.Location = new System.Drawing.Point(19, 20);
            this.radioInstallDisable.Name = "radioInstallDisable";
            this.radioInstallDisable.Size = new System.Drawing.Size(173, 16);
            this.radioInstallDisable.TabIndex = 0;
            this.radioInstallDisable.TabStop = true;
            this.radioInstallDisable.Text = "该应用程序只能联机使用(&O)";
            this.radioInstallDisable.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtInstallUrl);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPublishUrl);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 125);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发布位置";
            // 
            // txtInstallUrl
            // 
            this.txtInstallUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInstallUrl.Location = new System.Drawing.Point(19, 88);
            this.txtInstallUrl.Name = "txtInstallUrl";
            this.txtInstallUrl.Size = new System.Drawing.Size(621, 21);
            this.txtInstallUrl.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "安装文件夹URL(如果与以上不同)(&R):";
            // 
            // txtPublishUrl
            // 
            this.txtPublishUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPublishUrl.Location = new System.Drawing.Point(19, 40);
            this.txtPublishUrl.Name = "txtPublishUrl";
            this.txtPublishUrl.Size = new System.Drawing.Size(621, 21);
            this.txtPublishUrl.TabIndex = 1;
            this.txtPublishUrl.Text = "D:\\temp\\test\\publish";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(287, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "发布文件夹位置(网站、FTP服务器或者文件路径)(&L):";
            // 
            // pageOthers
            // 
            this.pageOthers.Location = new System.Drawing.Point(4, 22);
            this.pageOthers.Name = "pageOthers";
            this.pageOthers.Size = new System.Drawing.Size(737, 453);
            this.pageOthers.TabIndex = 2;
            this.pageOthers.Text = "其他";
            this.pageOthers.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.toolStripSeparator,
            this.tsbExportToCSProject,
            this.tsbPublish,
            this.toolStripSeparator1,
            this.tsbHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(769, 31);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(70, 28);
            this.tsbNew.Text = "新建(&N)";
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(70, 28);
            this.tsbOpen.Text = "打开(&O)";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(67, 28);
            this.tsbSave.Text = "保存(&S)";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbExportToCSProject
            // 
            this.tsbExportToCSProject.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportToCSProject.Image")));
            this.tsbExportToCSProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportToCSProject.Name = "tsbExportToCSProject";
            this.tsbExportToCSProject.Size = new System.Drawing.Size(104, 28);
            this.tsbExportToCSProject.Text = "导出为C#项目";
            // 
            // tsbPublish
            // 
            this.tsbPublish.Image = ((System.Drawing.Image)(resources.GetObject("tsbPublish.Image")));
            this.tsbPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPublish.Name = "tsbPublish";
            this.tsbPublish.Size = new System.Drawing.Size(96, 28);
            this.tsbPublish.Text = "立即发布(&W)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbHelp
            // 
            this.tsbHelp.AutoSize = false;
            this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(28, 28);
            this.tsbHelp.Text = "帮助(&L)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 534);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "ClickOnce Builder";
            this.tabControl1.ResumeLayout(false);
            this.pageNormal.ResumeLayout(false);
            this.pageNormal.PerformLayout();
            this.pageAssembly.ResumeLayout(false);
            this.pageAssembly.PerformLayout();
            this.pagePublish.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numApplicationRevision)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pageAssembly;
        private System.Windows.Forms.TabPage pagePublish;
        private System.Windows.Forms.TabPage pageOthers;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkEnableApplicationRevision;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApplicationVersionBuild;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtApplicationVersionMinor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApplicationVersionMajor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPublishOptions;
        private System.Windows.Forms.Button btnApplicationUpdate;
        private System.Windows.Forms.Button btnBootstrapper;
        private System.Windows.Forms.Button btnApplicationFiles;
        private System.Windows.Forms.RadioButton radioInstallEnable;
        private System.Windows.Forms.RadioButton radioInstallDisable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInstallUrl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPublishUrl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAssemblyOriginatorKeyFile;
        private System.Windows.Forms.Button btnBrowse_AssemblyOriginatorKeyFile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtManifestCertificateThumbprint;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtManifestKeyFile;
        private System.Windows.Forms.Button btnBrowse_ManifestKey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtAssemblyName;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripButton tsbPublish;
        private System.Windows.Forms.ToolStripButton tsbExportToCSProject;
        private System.Windows.Forms.TabPage pageNormal;
        private System.Windows.Forms.CheckBox chkIsCLR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExecuteFile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAssemblyInfoFile;
        private System.Windows.Forms.TextBox txtDistributionPath;
        private System.Windows.Forms.NumericUpDown numApplicationRevision;
    }
}
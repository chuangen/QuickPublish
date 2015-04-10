using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace QuickPublish
{
    public partial class MainForm : Form
    {
        ClickOnceConfigFile document = null;
        string initSettingsFile = "";
        readonly string FormTitle;
        public MainForm(string settingsFile)
        {
            InitializeComponent();
            this.FormTitle = this.Text;
            this.initSettingsFile = settingsFile;

            numApplicationRevision.Minimum = 0;
            numApplicationRevision.Maximum = int.MaxValue;

            tsbNew.Click += delegate (object sender, EventArgs e) { NewOptionsFile(); };
            tsbOpen.Click += delegate(object sender, EventArgs e) { LoadOptionsFile(); };
            tsbSave.Click += delegate (object sender, EventArgs e) { SaveOptionsFile(); };

            btnBrowse_ManifestKey.Click += new EventHandler(btnBrowseManifestKey_Click);
            btnBrowse_AssemblyOriginatorKeyFile.Click += new EventHandler(btnBrowse_AssemblyOriginatorKeyFile_Click);

            tsbPublish.Click += delegate(object sender, EventArgs e)
            {
                if (document == null)
                    return;
                SaveOptionsFile();

                BuildForm form = new BuildForm(document.FileName);
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                }
            };

            tsbExportToCSProject.Click += new EventHandler(tsbExportToCSProject_Click);
            txtExecuteFile.TextChanged += new EventHandler(txtExecuteFile_TextChanged);
        }

        void txtExecuteFile_TextChanged(object sender, EventArgs e)
        {//判断可执行文件的类型
            chkIsCLR.Enabled = true;
            if(document == null)
                return;
            string distPath = Path.Combine(document.BasePath, txtDistributionPath.Text);
            if(!Directory.Exists(distPath))
                return;
            if(string.IsNullOrEmpty(txtExecuteFile.Text))
                return;

            string exeFile = Path.Combine(distPath, txtExecuteFile.Text);
            if(!File.Exists(exeFile))
            {
                try
                {
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(exeFile);
                    
                    chkIsCLR.Enabled = true;
                }
                catch(BadImageFormatException ex)
                {//不能作为CLR程序集
                    chkIsCLR.Enabled = false;
                }
                catch(Exception ex)
                {//不能作为CLR程序集
                    chkIsCLR.Enabled = false;
                }
            }
        }

        void tsbExportToCSProject_Click(object sender, EventArgs e)
        {
            if (document == null)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存C#项目";
            sfd.Filter = "C# 项目文件(*.csproj)|*.csproj";
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    document.SaveAsCsProject(sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "保存失败，" + ex.Message, "提示");
                    return;
                }
            }
        }

        void btnBrowse_AssemblyOriginatorKeyFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                txtAssemblyOriginatorKeyFile.Text = ofd.FileName;
            }
        }

        void UpdateUI()
        {
            if (document == null)
            {
                this.Text = this.FormTitle;

                tsbSave.Enabled = false;
                tsbUpdateDist.Enabled = false;
                tsbPublish.Enabled = false;
                tsbPublishWizard.Enabled = false;
            }
            else
            {
                this.Text = Path.GetFileName(document.FileName) + " - " + this.FormTitle;

                tsbSave.Enabled = true;
                tsbUpdateDist.Enabled = true;
                tsbPublish.Enabled = true;
                tsbPublishWizard.Enabled = true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (string.IsNullOrEmpty(this.initSettingsFile))
            {
                this.document = null;
            }
            else if (File.Exists(this.initSettingsFile))
            {
                try
                {
                    document = ClickOnceConfigFile.NewDocument(this.initSettingsFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "加载失败，" + ex.Message, "提示");
                }
            }
            else
            {
                MessageBox.Show(this, "指定的文件不存在。", "提示");
                this.document = null;
            }


            UpdateUiFromDocument(document);

            UpdateUI();
        }

        /// <summary>
        /// 新建配置文件。
        /// </summary>
        void NewOptionsFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "新建配置文件";
            sfd.Filter = "ClickOnce 发布配置文件(*.xml)|*.xml|C# 项目文件(*.csproj)|*.csproj";
            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    document = ClickOnceConfigFile.NewDocument(sfd.FileName);
                    UpdateUiFromDocument(document);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "加载失败，" + ex.Message, "提示");
                    return;
                }
            }

            UpdateUI();
        }

        void LoadOptionsFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "打开配置文件";
            ofd.Filter = "ClickOnce 发布配置文件(*.xml)|*.xml|C# 项目文件(*.csproj)|*.csproj";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    document = new ClickOnceConfigFile(ofd.FileName);
                    UpdateUiFromDocument(document);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "加载失败，" + ex.Message, "提示");
                    return;
                }
            }

            UpdateUI();
        }
        void SaveOptionsFile()
        {
            if (document == null)
                return;

            UpdateUiToDocument(document);
            //项目文件
            document.WriteXml(document.FileName);
        }
        /// <summary>
        /// UI -> ProjectDocument。
        /// </summary>
        void UpdateUiToDocument(ClickOnceConfigFile config)
        {
            if (config == null)
                return;

            CsProject.PropertyCollection properties = config.PropertyItems;
            //常规
            config.Settings.DistributionPath = txtDistributionPath.Text;
            config.Settings.ExecuteFile = txtExecuteFile.Text;
            config.Settings.ExecuteFileType = chkIsCLR.Checked ? ExecuteFileTypes.CLR : ExecuteFileTypes.Win32;
            config.Settings.AssemblyInfoFile = txtAssemblyInfoFile.Text;
            //引导程序集
            properties.AssemblyName = txtAssemblyName.Text.Trim();
            properties.AssemblyOriginatorKeyFile = txtAssemblyOriginatorKeyFile.Text;
            properties.ManifestCertificateThumbprint = txtManifestCertificateThumbprint.Text;
            properties.ManifestKeyFile = txtManifestKeyFile.Text;

            //发布选项
            properties.PublishUrl = txtPublishUrl.Text;
            properties.InstallUrl = txtInstallUrl.Text;
            properties.Install = radioInstallEnable.Checked;

            if (chkEnableApplicationRevision.Checked)
            {//自动更新
                properties.ApplicationVersion = string.Format("{0}.{1}.{2}.%2a", // %2a = *
                    txtApplicationVersionMajor.Text,
                    txtApplicationVersionMinor.Text,
                    txtApplicationVersionBuild.Text);
            }
            else
            {
                properties.ApplicationVersion = string.Format("{0}.{1}.{2}.{3}",
                    txtApplicationVersionMajor.Text,
                    txtApplicationVersionMinor.Text,
                    txtApplicationVersionBuild.Text,
                    numApplicationRevision.Value.ToString());
            }
            properties.ApplicationRevision = (int)numApplicationRevision.Value;

            //其他

            config.RefreshPublishFiles();
        }
        /// <summary>
        /// ProjectDocument -> UI。
        /// </summary>
        /// <param name="basePath"></param>
        void UpdateUiFromDocument(ClickOnceConfigFile config)
        {
            if (config == null)
            {
                tabControl1.Enabled = false;
                //常规
                txtDistributionPath.Text = "";
                txtExecuteFile.Text = "";
                chkIsCLR.Checked = false;
                txtAssemblyInfoFile.Text = "";
                //引导程序集
                txtAssemblyName.Text = "";
                txtAssemblyOriginatorKeyFile.Text = "";
                txtManifestCertificateThumbprint.Text = "";
                txtManifestKeyFile.Text = "";

                //发布选项
                txtPublishUrl.Text = "";
                txtInstallUrl.Text = "";
                radioInstallDisable.Checked = false;
                radioInstallEnable.Checked = true;

                txtApplicationVersionMajor.Text = "";
                txtApplicationVersionMinor.Text = "";
                txtApplicationVersionBuild.Text = "";
                numApplicationRevision.Value = 0;

                chkEnableApplicationRevision.Checked = false;

                //其他
            }
            else
            {
                tabControl1.Enabled = true;
                string basePath = config.BasePath;

                CsProject.PropertyCollection properties = config.PropertyItems;
                //常规
                txtDistributionPath.Text = config.Settings.DistributionPath;
                txtExecuteFile.Text = config.Settings.ExecuteFile;
                chkIsCLR.Checked = (config.Settings.ExecuteFileType == ExecuteFileTypes.CLR);
                txtAssemblyInfoFile.Text = config.Settings.AssemblyInfoFile;

                //引导程序集
                txtAssemblyName.Text = properties.AssemblyName;
                txtAssemblyOriginatorKeyFile.Text = Common.GetAbsolutePath(basePath, properties.AssemblyOriginatorKeyFile);
                txtManifestCertificateThumbprint.Text = properties.ManifestCertificateThumbprint;
                txtManifestKeyFile.Text = Common.GetAbsolutePath(basePath, properties.ManifestKeyFile);

                //发布选项
                txtPublishUrl.Text = properties.PublishUrl;
                txtInstallUrl.Text = properties.InstallUrl;
                radioInstallDisable.Checked = !properties.Install;
                radioInstallEnable.Checked = properties.Install;

                string[] applicationVersionParts = properties.ApplicationVersion.Split('.');
                if (applicationVersionParts.Length >= 4)
                {
                    txtApplicationVersionMajor.Text = applicationVersionParts[0];
                    txtApplicationVersionMinor.Text = applicationVersionParts[1];
                    txtApplicationVersionBuild.Text = applicationVersionParts[2];
                    numApplicationRevision.Value = properties.ApplicationRevision;

                    chkEnableApplicationRevision.Checked = (applicationVersionParts[3].Trim() == "*"
                        || applicationVersionParts[3].Trim().ToLower() == "%2a");
                }

                //其他
            }
        }

        X509Certificate2 GetX509Certificate2(string path)
        {
            return GetX509Certificate2(path, X509KeyStorageFlags.DefaultKeySet);
        }
        /// <summary>
        /// 加载指定路径的证书。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        X509Certificate2 GetX509Certificate2(string path, X509KeyStorageFlags keyStorageFlags)
        {
            X509Certificate2 cert = null;
            //拷贝公钥
            if (!File.Exists(path))
            {
                MessageBox.Show(this.TopLevelControl, "密钥文件不存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            while (true)
            {//密码
                PasswordForm form = new PasswordForm();
                form.PfxName = Path.GetFileName(path);
                if (form.ShowDialog(this.TopLevelControl) != DialogResult.OK)
                {//返回
                    return null;
                }
                else
                {//尝试密码
                    try
                    {
                        //证书一定要标识为可导出
                        cert = new X509Certificate2(path, form.Password, keyStorageFlags);

                        break;//跳出循环
                    }
                    catch (CryptographicException)
                    {
                        MessageBox.Show(this.TopLevelControl, "密码不正确，请重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            return cert;
        }

        private void btnBrowseManifestKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = txtManifestKeyFile.Text;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                X509Certificate2 cert = GetX509Certificate2(ofd.FileName);
                if (cert == null)
                    return;
                txtManifestCertificateThumbprint.Text = cert.Thumbprint;
                txtManifestKeyFile.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("#WATERMARK1#");
        }

        private void btnPublishOptions_Click(object sender, EventArgs e)
        {
            if (document == null)
                return;

            PublishOptionsForm form = new PublishOptionsForm(document);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }

        private void btnApplicationUpdate_Click(object sender, EventArgs e)
        {
            if (document == null)
                return;

            ApplicationUpdateForm form = new ApplicationUpdateForm(document);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }

        private void btnApplicationFiles_Click(object sender, EventArgs e)
        {
            FilesForm form = new FilesForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }

        private void btnBootstrapper_Click(object sender, EventArgs e)
        {
            if (document == null)
                return;
            BootstrapperForm form = new BootstrapperForm(document);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);
        }
    }
}

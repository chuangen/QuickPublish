using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QuickPublish
{
    public partial class BuildForm : Form
    {
        Properties.Settings settings = Properties.Settings.Default;
        /// <summary>
        /// ClickOnce 配置文件。
        /// </summary>
        readonly string ClickOnceFile;
        readonly string FormTitle;
        public BuildForm(string clickonceFile)
        {
            this.ClickOnceFile = Path.GetFullPath(clickonceFile);
            InitializeComponent();
            this.FormTitle = this.Text;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (string.IsNullOrEmpty(settings.VisualStudioPath))
            {//配置VS的路径
                settings.VisualStudioPath = Common.GetVisualStudioPath();
                settings.Save();
            }


            this.Text = this.FormTitle + " - " + Path.GetFileName(this.ClickOnceFile);
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            PublishAsync(this.ClickOnceFile);
        }

        BackgroundWorker bw = null;
        void PublishAsync(string settingsFile)
        {
            if (bw == null)
            {
                bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            }
            if (!bw.IsBusy)
                bw.RunWorkerAsync(settingsFile);
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string settingsFile = e.Argument as string;
            Publish(bw, settingsFile);
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Visible = false;

            if (hasSuccess(rtbOutput.Text))
            {
                ClickOnceConfigFile config = new ClickOnceConfigFile(this.ClickOnceFile);
                config.PropertyItems.ApplicationRevision++;
                config.WriteXml(this.ClickOnceFile);

                this.Close();
            }
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelProcessText.Text = e.UserState as string;
        }
    }
}

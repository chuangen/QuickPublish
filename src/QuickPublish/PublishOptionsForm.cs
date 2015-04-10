using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickPublish
{
    public partial class PublishOptionsForm : Form
    {
        readonly ClickOnceConfigFile config;
        public PublishOptionsForm(ClickOnceConfigFile config)
        {
            if (config == null)
                throw new ArgumentNullException("config", "config 不能为空。");

            this.config = config;
            InitializeComponent();

            btnOK.Click += delegate(object sender, EventArgs e)
            {
                SaveSettings();
                this.DialogResult = DialogResult.OK;
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DataTable tableCultures = new DataTable("Cultures");
            tableCultures.Columns.Add("Name", typeof(string));
            tableCultures.Columns.Add("DisplayName", typeof(string));
            tableCultures.DefaultView.Sort = "DisplayName ASC";
            tableCultures.Rows.Add("", "(默认值)");
            foreach(System.Globalization.CultureInfo info in System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures))
                tableCultures.Rows.Add(info.Name, info.DisplayName);
            cmbTargetCulture.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTargetCulture.ValueMember = "Name";
            cmbTargetCulture.DisplayMember = "DisplayName";
            cmbTargetCulture.DataSource = tableCultures;

            LoadSettings();
        }
        void LoadSettings()
        {
            CsProject.PropertyCollection properties = config.PropertyItems;

            //说明
            cmbTargetCulture.SelectedValue = properties.TargetCulture;
            txtPublisherName.Text = properties.PublisherName;
            txtProductName.Text = properties.ProductName;
            cmbSupportUrl.Text = properties.SupportUrl;

            //部署
            txtWebPage.Text = properties.WebPage;
            chkCreateWebPageOnPublish.Checked = properties.CreateWebPageOnPublish;
            chkOpenBrowserOnPublish.Checked = properties.OpenBrowserOnPublish;
            chkMapFileExtensions.Checked = properties.MapFileExtensions;
            chkAutorunEnabled.Checked = properties.AutorunEnabled;

            //清单
            chkDisallowUrlActivation.Checked = properties.DisallowUrlActivation;
            chkTrustUrlParameters.Checked = properties.TrustUrlParameters;

            //文件关联
        }
        void SaveSettings()
        {
            CsProject.PropertyCollection properties = config.PropertyItems;

            //说明
            properties.TargetCulture = cmbTargetCulture.SelectedValue as string;
            properties.PublisherName = txtPublisherName.Text;
            properties.ProductName = txtProductName.Text;
            properties.SupportUrl = cmbSupportUrl.Text;

            //部署
            properties.WebPage = txtWebPage.Text;
            properties.CreateWebPageOnPublish = chkCreateWebPageOnPublish.Checked;
            properties.OpenBrowserOnPublish = chkOpenBrowserOnPublish.Checked;
            properties.MapFileExtensions = chkMapFileExtensions.Checked;
            properties.AutorunEnabled = chkAutorunEnabled.Checked;

            //清单
            properties.DisallowUrlActivation = chkDisallowUrlActivation.Checked;
            properties.TrustUrlParameters = chkTrustUrlParameters.Checked;

            //文件关联
        }
    }
}

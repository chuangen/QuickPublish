using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using QuickPublish.CsProject;

namespace QuickPublish
{
    public partial class BootstrapperForm : Form
    {
        readonly ClickOnceConfigFile config;
        public BootstrapperForm(ClickOnceConfigFile config)
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


            radioComponentsLocationDefault.Checked = false;
            radioComponentsLocationRelative.Checked = false;
            radioComponentsLocationAbsolute.Checked = true;
            radioComponentsLocationAbsolute.CheckedChanged += delegate (object sender, EventArgs e)
            {
                cmbBootstrapperComponentsUrl.Enabled = radioComponentsLocationAbsolute.Checked;
                btnBrowse.Enabled = radioComponentsLocationAbsolute.Checked;
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadSettings();
        }
        void LoadSettings()
        {
            Util.BootstrapperPackage[] packages = Util.BootstrapperPackage.GetBootstrappers();
            DataTable tablePackages = new DataTable("Packages");
            tablePackages.Columns.Add("ProductCode", typeof(string));
            tablePackages.Columns.Add("DisplayName", typeof(string));
            tablePackages.Columns.Add("Checked", typeof(bool));
            tablePackages.DefaultView.Sort = "DisplayName ASC";

            ItemGroup itemGroup = config.ItemGroupBootstrapperPackages;
            foreach (Util.BootstrapperPackage package in packages)
            {
                string ProductCode = package.ProductCode;
                string DisplayName = package.DisplayName;
                bool Checked = false;
                if (itemGroup.ContainsKey(ProductCode))
                {
                    BootstrapperPackageItem item = itemGroup[ProductCode] as BootstrapperPackageItem;
                    if (item != null && item.Install)
                        Checked = true;
                }

                tablePackages.Rows.Add(ProductCode, DisplayName, Checked);
            }

            foreach (DataRowView view in tablePackages.DefaultView)
            {
                Util.BootstrapperPackage package = new Util.BootstrapperPackage(
                    view["ProductCode"] as string,
                    view["DisplayName"] as string,
                    (bool)view["Checked"]);

                chkListBoxBootstrappers.Items.Add(package, package.Checked);
            }

            CsProject.PropertyCollection properties = config.PropertyItems;
            chkBootstrapperEnabled.Checked = properties.BootstrapperEnabled;

            switch(properties.BootstrapperComponentsLocation)
            {
                case BootstrappersLocations.Relative:
                    radioComponentsLocationRelative.Checked = true;
                    break;
                case BootstrappersLocations.Absolute:
                    radioComponentsLocationAbsolute.Checked = true;
                    break;
                default:
                    radioComponentsLocationDefault.Checked = true;
                    break;
            }

            cmbBootstrapperComponentsUrl.Text = properties.BootstrapperComponentsUrl;
        }
        void SaveSettings()
        {
            for(int i=0;i< chkListBoxBootstrappers.Items.Count;i++)
            {
                Util.BootstrapperPackage package = chkListBoxBootstrappers.Items[i] as Util.BootstrapperPackage;
                if (package == null)
                    continue;

                bool isChecked = chkListBoxBootstrappers.GetItemChecked(i);

                ItemGroup itemGroup = config.ItemGroupBootstrapperPackages;

                BootstrapperPackageItem item = null;
                if (itemGroup.ContainsKey(package.ProductCode))
                    item = itemGroup[package.ProductCode] as BootstrapperPackageItem;
                else
                {
                    item = new BootstrapperPackageItem(package.ProductCode);
                    itemGroup.Add(item);
                }

                item.Install = isChecked;
            }

            CsProject.PropertyCollection properties = config.PropertyItems;
            properties.BootstrapperEnabled = chkBootstrapperEnabled.Checked;

            if (radioComponentsLocationRelative.Checked)
                properties.BootstrapperComponentsLocation = CsProject.BootstrappersLocations.Relative;
            else if (radioComponentsLocationAbsolute.Checked)
                properties.BootstrapperComponentsLocation = CsProject.BootstrappersLocations.Absolute;
            else
                properties.BootstrapperComponentsLocation = CsProject.BootstrappersLocations.Default;
            properties.BootstrapperComponentsUrl = cmbBootstrapperComponentsUrl.Text;
        }
    }
}

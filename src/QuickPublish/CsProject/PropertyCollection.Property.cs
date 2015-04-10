using System;
using System.Collections.Generic;
using System.Text;
using QuickPublish.CsProject;
using System.Web;
using System.IO;

namespace QuickPublish.CsProject
{
    partial class PropertyCollection
    {   
        public string ProjectGuid
        {
            get { return GetValue("ProjectGuid"); }
            set { SetValue("ProjectGuid", value);; }
        }
        public string AssemblyName
        {
            get { return GetValue("AssemblyName"); }
            set { SetValue("AssemblyName", value);; }
        }
        public string AssemblyOriginatorKeyFile
        {
            get { return GetValue("AssemblyOriginatorKeyFile"); }
            set { SetValue("AssemblyOriginatorKeyFile", value);; }
        }
        public string ManifestCertificateThumbprint
        {
            get { return GetValue("ManifestCertificateThumbprint"); }
            set { SetValue("ManifestCertificateThumbprint", value);; }
        }
        public string ManifestKeyFile
        {
            get { return GetValue("ManifestKeyFile"); }
            set { SetValue("ManifestKeyFile", value);; }
        }
        public string PublishUrl
        {
            get { return GetValue("PublishUrl"); }
            set { SetValue("PublishUrl", value);; }
        }
        public string InstallUrl
        {
            get { return GetValue("InstallUrl"); }
            set { SetValue("InstallUrl", value);; }
        }
        public bool Install
        {
            get { return GetBoolean("Install", true); }
            set { SetBoolean("Install", value); }
        }
        public string ApplicationVersion
        {
            get { return GetValue("ApplicationVersion"); }
            set { SetValue("ApplicationVersion", value);; }
        }
        public int ApplicationRevision
        {
            get { return GetInteger("ApplicationRevision", 0); }
            set { SetInteger("ApplicationRevision", value); ; }
        }


        //发布选项
        
        public string TargetCulture
        {
            get { return GetValue("TargetCulture"); }
            set { SetValue("TargetCulture", value);; }
        }
        public string PublisherName
        {
            get { return GetValue("PublisherName"); }
            set { SetValue("PublisherName", value);; }
        }
        public string ProductName
        {
            get { return GetValue("ProductName"); }
            set { SetValue("ProductName", value);; }
        }
        public string SupportUrl
        {
            get { return GetValue("SupportUrl"); }
            set { SetValue("SupportUrl", value);; }
        }
        public string WebPage
        {
            get { return GetValue("WebPage"); }
            set { SetValue("WebPage", value);; }
        }
        public bool CreateWebPageOnPublish
        {
            get { return GetBoolean("CreateWebPageOnPublish", true); }
            set { SetBoolean("CreateWebPageOnPublish", value); }
        }
        public bool OpenBrowserOnPublish
        {
            get { return GetBoolean("OpenBrowserOnPublish", true); }
            set { SetBoolean("OpenBrowserOnPublish", value); }
        }
        public bool MapFileExtensions
        {
            get { return GetBoolean("MapFileExtensions", true); }
            set { SetBoolean("MapFileExtensions", value); }
        }
        public bool AutorunEnabled
        {
            get { return GetBoolean("AutorunEnabled", true); }
            set { SetBoolean("AutorunEnabled", value); }
        }
        public bool DisallowUrlActivation
        {
            get { return GetBoolean("DisallowUrlActivation", true); }
            set { SetBoolean("DisallowUrlActivation", value); }
        }
        public bool TrustUrlParameters
        {
            get { return GetBoolean("TrustUrlParameters", true); }
            set { SetBoolean("TrustUrlParameters", value); }
        }


        public bool BootstrapperEnabled
        {
            get { return GetBoolean("BootstrapperEnabled", true); }
            set { SetBoolean("BootstrapperEnabled", value); }
        }

        public BootstrappersLocations BootstrapperComponentsLocation
        {
            get
            {
                string strValue = GetValue("BootstrapperComponentsLocation");
                BootstrappersLocations result;
                try
                {
                    result = (BootstrappersLocations)EnumHelper.Parse(typeof(BootstrappersLocations), strValue, true);
                }
                catch (Exception ex)
                {
                    result = BootstrappersLocations.Default;
                }
                return result;
            }
            set
            {
                string strValue = "";
                if (value == BootstrappersLocations.Default)
                    strValue = "";
                else
                    strValue = EnumHelper.GetName(typeof(BootstrappersLocations), value);

                SetValue("BootstrapperComponentsLocation", strValue);
            }
        }
        public string BootstrapperComponentsUrl
        {
            get { return GetValue("BootstrapperComponentsUrl"); }
            set { SetValue("BootstrapperComponentsUrl", value); }
        }
    }
}

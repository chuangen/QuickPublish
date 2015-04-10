using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace QuickPublish.CsProject
{
    public partial class PropertyCollection
    {
        public static IList<string> RelPaths;
        static PropertyCollection()
        {
            List<string> paths = new List<string>();
            paths.AddRange(new string[] {
                "ManifestKeyFile",
                "AssemblyOriginatorKeyFile",
                "DistributionPath",
                "PublishUrl",

                "AssemblyInfoFile",
            });
            RelPaths = paths.AsReadOnly();
        }

        private Dictionary<string, PropertyGroup> dicGroups;
        public Dictionary<string, PropertyGroup> Groups
        {
            get { return dicGroups; }
        }
        private PropertyGroup defaultGroup;
        /// <summary>
        /// 默认组
        /// </summary>
        public PropertyGroup DefaultGroup
        {
            get { return defaultGroup; }
        }
        public PropertyCollection()
        {
            Reset();
        }
        void Reset()
        {
            this.dicGroups = new Dictionary<string, PropertyGroup>(StringComparer.OrdinalIgnoreCase);
            this.defaultGroup = new PropertyGroup("");

            dicGroups.Add("", defaultGroup);

            //基本属性
            defaultGroup.Add("Configuration", " '$(Configuration)' == '' ", "", "Debug");
            defaultGroup.Add("Platform", " '$(Platform)' == '' ", "", "AnyCPU");
            defaultGroup.Add("ProductVersion", "", "9.0.30729");
            defaultGroup.Add("SchemaVersion", "", "2.0");
            defaultGroup.Add("ProjectGuid", "", Guid.Empty.ToString("B"));
            defaultGroup.Add("OutputType", "", "WinExe");
            defaultGroup.Add("AppDesignerFolder", "", "Properties");
            defaultGroup.Add("RootNamespace", "", "Loader");
            defaultGroup.Add("AssemblyName", "", "loader");
            defaultGroup.Add("TargetFrameworkVersion", "", "v2.0");
            //defaultGroup.Add("TargetFrameworkProfile", "", "Client");
            defaultGroup.Add("FileAlignment", "", "512");

            //应用程序页
            defaultGroup.Add("ApplicationIcon", "", "");//应用程序图标
            defaultGroup.Add("StartupObject", "", "");//启动对象
            defaultGroup.Add("NoWin32Manifest", "false", "false");//创建不带清单的应用程序
            defaultGroup.Add("ApplicationManifest", "", "");//应用程序清单
            defaultGroup.Add("Win32Resource", "", "");//资源文件

            //ClickOnce 签名
            defaultGroup.Add("SignManifests", "false", "false");
            defaultGroup.Add("ManifestCertificateThumbprint", "", "");
            defaultGroup.Add("ManifestTimestampUrl", "", "");
            defaultGroup.Add("ManifestKeyFile", "", "");

            //程序集签名
            defaultGroup.Add("SignAssembly", "false", "false");
            defaultGroup.Add("AssemblyOriginatorKeyFile", "", "");
            defaultGroup.Add("DelaySign", "false", "false");//仅延迟签名

            //安全性
            defaultGroup.Add("TargetZone", "", "");//将要从中安装应用程序的区域
            defaultGroup.Add("GenerateManifests", "false", "false");//

            //发布
            defaultGroup.Add("IsWebBootstrapper", "", "true");//创建用于安装系统必备的组件的安装程序
            defaultGroup.Add("PublishUrl", "", "");//发布文件夹位置
            defaultGroup.Add("Install", "", "true");
            defaultGroup.Add("InstallFrom", "", "Web");//
            defaultGroup.Add("UpdateEnabled", "", "true");
            defaultGroup.Add("UpdateMode", "", "Background");
            defaultGroup.Add("UpdateInterval", "", "7");
            defaultGroup.Add("UpdateIntervalUnits", "", "Days");
            defaultGroup.Add("UpdatePeriodically", "", "true");
            defaultGroup.Add("UpdateRequired", "", "true");
            defaultGroup.Add("MapFileExtensions", "", "true");

            defaultGroup.Add("InstallUrl", "", "");//安装文件夹URL(如果与发布位置不同)
            defaultGroup.Add("UpdateUrl", "", "");//更新位置(如果与发布位置不同)
            defaultGroup.Add("SupportUrl", "", "");//支持URL
            defaultGroup.Add("ErrorReportUrl", "", "");//错误URL
            defaultGroup.Add("ProductName", "", "");//产品名称
            defaultGroup.Add("PublisherName", "", "");//发布者名称
            defaultGroup.Add("SuiteName", "", "");//套件名称
            defaultGroup.Add("MinimumRequiredVersion", "", "");
            defaultGroup.Add("CreateWebPageOnPublish", "false", "false");
            defaultGroup.Add("WebPage", "", "");
            defaultGroup.Add("AutorunEnabled", "false", "false");
            defaultGroup.Add("DisallowUrlActivation", "false", "false");
            defaultGroup.Add("OpenBrowserOnPublish", "", "false");
            defaultGroup.Add("TrustUrlParameters", "false", "false");
            defaultGroup.Add("ApplicationRevision", "", "0");
            defaultGroup.Add("ApplicationVersion", "", "1.0.0.%2a");
            defaultGroup.Add("UseApplicationTrust", "", "false");
            defaultGroup.Add("CreateDesktopShortcut", "false", "false");
            //defaultGroup.Add("ExcludeDeploymentUrl", "false", "false");
            defaultGroup.Add("PublishWizardCompleted", "false", "false");
            defaultGroup.Add("BootstrapperEnabled", "", "true");
            defaultGroup.Add("BootstrapperComponentsLocation", "", "");
            defaultGroup.Add("BootstrapperComponentsUrl", "", "");
        }
        public void Remove(string key)
        {
            defaultGroup.Remove(key);
        }
        public void Clear()
        {
            this.Reset();
        }

        public Property getProperty(string condition, string key)
        {
            throw new NotImplementedException("尚未实现。");
        }
        public Property getProperty(string key)
        {
            Property result = null;
            foreach (PropertyGroup group in dicGroups.Values)
            {
                if (group.ContainsKey(key))
                {
                    result = group[key];
                    break;
                }
            }

            return result;
        }
        public string GetValue(string key)
        {
            return this.GetValue(key, "");
        }
        public string GetValue(string key, string defaultValue)
        {
            Property item = getProperty(key);
            if (item == null)
                return defaultValue;

            return item.Value;
        }
        public void SetValue(string key, string value)
        {
            Property item = getProperty(key);
            if (item == null)
                this.DefaultGroup.Items.Add(key, new Property(key, "", "", value));
            else
                item.Value = value;
        }
        /// <summary>
        /// 注意：条件不支持查询，condition只使用整个表达式作为关键字
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string condition, string key, string value)
        {
            if (!dicGroups.ContainsKey(condition))
                return;

            PropertyGroup group = dicGroups[condition];
            if (group.ContainsKey(key))
                group[key].Value = value;
            else
                group.Items.Add(key, new Property(key, "", "", value));
        }
        public bool GetBoolean(string key, bool defaultValue)
        {
            string strValue = this.GetValue(key, "");
            bool value = true;
            if (!bool.TryParse(strValue, out value))
                value = defaultValue;

            return value;
        }
        public void SetBoolean(string key, bool value)
        {
            string strValue = value ? "true" : "false";
            this.SetValue(key, strValue);
        }
        public int GetInteger(string key, int defaultValue)
        {
            string strValue = this.GetValue(key, "");
            int value = 0;
            if (!int.TryParse(strValue, out value))
                value = defaultValue;

            return value;
        }
        public void SetInteger(string key, int value)
        {
            string strValue = value.ToString();
            this.SetValue(key, strValue);
        }
        public void AddGroup(XmlElement elemGroup, string basePath)
        {
            XmlDocument xmldoc = elemGroup.OwnerDocument;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("k", Consts.NamesapceURI);

            string condition = elemGroup.GetAttribute("Condition");
            PropertyGroup group;
            if(dicGroups.ContainsKey(condition))
                group = dicGroups[condition];
            else
            {
                group = new PropertyGroup(condition);
                dicGroups.Add(group.Condition, group);
            }

            foreach (XmlElement elem in elemGroup.ChildNodes)
            {
                string value = null;
                if (RelPaths.Contains(elem.Name))
                {//如果是路径，则转换为全路径
                    if (string.IsNullOrEmpty(elem.InnerText))
                        value = elem.InnerText;
                    else
                        value = Sense.Utils.IO.Path.GetAbsolutePath(basePath, elem.InnerText);
                }
                else
                {
                    value = elem.InnerText;
                }
                group.setProperty(elem.Name, elem.GetAttribute("Condition"), value);
            }
        }

        public void SaveToXml(XmlElement nodeProject, string basePath)
        {
            XmlDocument xmldoc = nodeProject.OwnerDocument;
            foreach (PropertyGroup group in dicGroups.Values)
            {
                XmlElement elemGroup = xmldoc.CreateElement("PropertyGroup", Consts.NamesapceURI);
                if (!string.IsNullOrEmpty(group.Condition))
                    elemGroup.SetAttribute("Condition", group.Condition);

                foreach (KeyValuePair<string, Property> pair in group.Items)
                {
                    if (pair.Value == null)
                        continue;
                    Property property = pair.Value;
                    if (string.IsNullOrEmpty(pair.Key))
                        continue;
                    //如果为空，则不需保存
                    if (string.IsNullOrEmpty(property.Value))
                        continue;
                    //如果是路径，则转换为相对路径
                    string value = null;
                    if (RelPaths.Contains(property.Key))
                    {
                        if (string.IsNullOrEmpty(property.Value))
                            value = property.Value;
                        else
                            value = Sense.Utils.IO.Path.GetRelativePath(basePath, property.Value);
                    }
                    else
                    {
                        value = property.Value;
                    }
                    //如果是默认值，则无需保存
                    if (property.DefaultValue == value)
                        continue;
                    XmlElement elem = xmldoc.CreateElement(property.Key, Consts.NamesapceURI);
                    if (!string.IsNullOrEmpty(property.Condition))
                        elem.SetAttribute("Condition", property.Condition);
                    elem.InnerText = value;
                    elemGroup.AppendChild(elem);
                }

                nodeProject.AppendChild(elemGroup);
            }
        }
    }
}

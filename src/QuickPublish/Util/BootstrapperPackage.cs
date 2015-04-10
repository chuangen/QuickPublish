using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Xml;
using System.Globalization;

namespace QuickPublish.Util
{
    public class BootstrapperPackage
    {
        public string ProductCode { get; private set; }
        public string DisplayName { get; private set; }
        public bool Checked { get; set; }

        public BootstrapperPackage(string ProductCode, string DisplayName, bool Checked)
        {
            this.ProductCode = ProductCode;
            this.DisplayName = DisplayName;
            this.Checked = Checked;
        }
        public override string ToString()
        {
            return this.DisplayName;
        }
        
        /// <summary>
        /// 获取 Bootstrapper 文件夹。
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// http://msdn.microsoft.com/zh-cn/library/ms165429(VS.80).aspx
        /// 
        /// 按照下面的顺序读取注册表项：
        ///     HKCU\Software\Microsoft\GenericBootstrapper\1.0\Path
        ///     HKLM\Software\Microsoft\GenericBootstrapper\1.0\Path
        /// 如果这两个项都不存在，则读取注册表项获取 SDK 的安装位置：
        ///     HKLM\Software\Microsoft\.NET Framework\sdkInstallRootv2.0
        /// </remarks>
        public static string GetBootstrappersPath()
        {
            string result = null;
            RegistryKey keyBootstrapper;
            keyBootstrapper = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\GenericBootstrapper");
            if (keyBootstrapper == null)
                keyBootstrapper = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\GenericBootstrapper");
            if (keyBootstrapper != null)
            {
                foreach (string keyName in keyBootstrapper.GetSubKeyNames())
                {//多个版本
                    RegistryKey key = keyBootstrapper.OpenSubKey(keyName);
                    string path = key.GetValue("Path") as string;
                    if (!string.IsNullOrEmpty(path))
                    {
                        result = path;
                        break;
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// 指定的版本的Bootstrapper，如：1.0、3.5、4.0等
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string GetBootstrappersPath(VisualStudioVersion version)
        {
            string strVer;
            switch (version)
            {
                case VisualStudioVersion.VisualStudio2002: strVer = "1.0"; break;
                case VisualStudioVersion.VisualStudio2003: strVer = "1.1"; break;
                case VisualStudioVersion.VisualStudio2005: strVer = "2.0"; break;
                case VisualStudioVersion.VisualStudio2008: strVer = "3.5"; break;
                case VisualStudioVersion.VisualStudio2010: strVer = "4.0"; break;
                default:
                    strVer = "3.5";
                    break;
            }
            string result = null;
            RegistryKey keyBootstrapper;
            keyBootstrapper = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\GenericBootstrapper\" + strVer);
            if (keyBootstrapper == null)
                keyBootstrapper = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\GenericBootstrapper\" + strVer);
            if (keyBootstrapper != null)
                result = keyBootstrapper.GetValue("Path") as string;

            return result;
        }
        public static VisualStudioVersion GetVsVersion(string version)
        {
            VisualStudioVersion vsVer = VisualStudioVersion.Unknown;

            double product;
            if (Double.TryParse(version, out product))
            {
                if (product == 7)
                    vsVer = VisualStudioVersion.VisualStudio2002;
                else if (product == 7.1)
                    vsVer = VisualStudioVersion.VisualStudio2003;
                else if (product == 8)
                    vsVer = VisualStudioVersion.VisualStudio2005;
                else if (product == 9)
                    vsVer = VisualStudioVersion.VisualStudio2008;
                else if (product == 10)
                    vsVer = VisualStudioVersion.VisualStudio2010;
            }

            return vsVer;
        }
        public const string NS_Bootstrapper = "http://schemas.microsoft.com/developer/2004/01/bootstrapper";
        public static BootstrapperPackage[] GetBootstrappers()
        {
            string pathBootstrappers = GetBootstrappersPath(VisualStudioVersion.VisualStudio2008);
            if(!Directory.Exists(pathBootstrappers))
                return new BootstrapperPackage[0];

            string pathPackages = Path.Combine(pathBootstrappers, "Packages");
            List<BootstrapperPackage> packages = new List<BootstrapperPackage>();
            foreach (string dir in Directory.GetDirectories(pathPackages))
            {
                string productXml = Path.Combine(dir, "product.xml");
                string packageXml = null; // zh-CHS\package.xml
                
                //寻找当前区域的资源
                CultureInfo culture = System.Windows.Forms.Application.CurrentCulture;
                while (true)
                {
                    string cultureName = culture.Name;
                    if (string.IsNullOrEmpty(cultureName))
                        break;
                    string path = Path.Combine(dir, cultureName);
                    string file = Path.Combine(path, "package.xml");
                    if (File.Exists(file))
                    {
                        packageXml = file;
                        break;
                    }
                    culture = culture.Parent;
                }
                if (string.IsNullOrEmpty(packageXml))
                    continue;
                if (!File.Exists(productXml) || !File.Exists(packageXml))
                    continue;

                string ProductCode = null;
                string DisplayName = null;
                try
                {
                    XmlDocument xmldocProduct = new XmlDocument();
                    xmldocProduct.Load(productXml);
                    ProductCode = xmldocProduct.DocumentElement.GetAttribute("ProductCode");

                    XmlDocument xmldocPackage = new XmlDocument();
                    xmldocPackage.Load(packageXml);
                    XmlElement elemProduct = xmldocPackage.DocumentElement;

                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldocPackage.NameTable);
                    nsmgr.AddNamespace("b", NS_Bootstrapper);

                    //读取本地化字符串
                    Dictionary<string, string> dicStrings = new Dictionary<string, string>();
                    XmlElement elemStrings = elemProduct.SelectSingleNode("b:Strings", nsmgr) as XmlElement;
                    if (elemStrings != null)
                    {
                        foreach (XmlElement elem in elemStrings.ChildNodes)
                        {
                            string key = elem.GetAttribute("Name");
                            string value = elem.InnerText;
                            dicStrings.Add(key, value);
                        }
                    }
                    DisplayName = elemProduct.GetAttribute("Name");
                    if (dicStrings.ContainsKey(DisplayName))
                        DisplayName = dicStrings[DisplayName];

                    string Culture = elemProduct.GetAttribute("Culture");
                    if (dicStrings.ContainsKey(Culture))
                        Culture = dicStrings[Culture];
                }
                catch (Exception ex)
                {
                    //
                }

                packages.Add(new BootstrapperPackage(ProductCode, DisplayName, false));
            }

            return packages.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish
{
    public enum ExecuteFileTypes : byte
    {
        /// <summary>
        /// 托管代码
        /// </summary>
        [StringValue("CLR")]
        CLR,
        /// <summary>
        /// Win32应用程序
        /// </summary>
        [StringValue("Win32")]
        Win32,
    }

    /// <summary>
    /// 自定义的 PublishSettings 节点
    /// </summary>
    public class PublishSettings
    {
        public PublishSettings()
        { }

        /// <summary>
        /// 发布目录，自定义属性，指定dist目录。
        /// </summary>
        public string DistributionPath { get; set; }
        public string ExecuteFile { get; set; }
        /// <summary>
        /// 可执行文件的类型。
        /// </summary>
        public ExecuteFileTypes ExecuteFileType { get; set; }
        public string AssemblyInfoFile { get; set; }

        public void FromXml(XmlElement elem, string basePath)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(elem.OwnerDocument.NameTable);
            nsmgr.AddNamespace("p", Consts.PublishBuild);

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("p:DistributionPath", nsmgr);
            this.DistributionPath = (selectedNode == null || string.IsNullOrEmpty(selectedNode.InnerText))
                ? "" : Common.GetAbsolutePath(basePath, selectedNode.InnerText);
            
            selectedNode = elem.SelectSingleNode("p:ExecuteFile", nsmgr);
            this.ExecuteFile = (selectedNode == null) ? "" : selectedNode.InnerText;
            
            selectedNode = elem.SelectSingleNode("p:ExecuteFileType", nsmgr);
            string strExecuteFileType = (selectedNode == null) ? "" : selectedNode.InnerText;
            try
            {
                this.ExecuteFileType = (ExecuteFileTypes)EnumHelper.Parse(typeof(ExecuteFileTypes), strExecuteFileType, true);
            }
            catch (Exception)
            {
                this.ExecuteFileType = ExecuteFileTypes.Win32;
            }

            selectedNode = elem.SelectSingleNode("p:AssemblyInfoFile", nsmgr);
            this.AssemblyInfoFile = (selectedNode == null || string.IsNullOrEmpty(selectedNode.InnerText))
                ? "" : Common.GetAbsolutePath(basePath, selectedNode.InnerText);
        }
        public XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("PublishSettings", Consts.PublishBuild);

            XmlElement elem = null;

            elem = xmldoc.CreateElement("DistributionPath", Consts.PublishBuild);
            elem.InnerText = Common.GetRelativePath(basePath, this.DistributionPath);
            result.AppendChild(elem);

            elem = xmldoc.CreateElement("ExecuteFile", Consts.PublishBuild);
            elem.InnerText = this.ExecuteFile;
            result.AppendChild(elem);

            elem = xmldoc.CreateElement("ExecuteFileType", Consts.PublishBuild);
            elem.InnerText = EnumHelper.GetName(typeof(ExecuteFileTypes), this.ExecuteFileType);
            result.AppendChild(elem);

            elem = xmldoc.CreateElement("AssemblyInfoFile", Consts.PublishBuild);
            elem.InnerText = Common.GetRelativePath(basePath, this.AssemblyInfoFile);
            result.AppendChild(elem);

            return result;
        }
    }
}

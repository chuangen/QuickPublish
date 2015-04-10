using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace QuickPublish.CsProject
{
    public abstract class FileItem : NodeBase
    {
        public override string Key
        {
            get { return this.FullName; }
        }
        public string FullName { get; private set; }
        public string GetInclude(string basePath)
        {
            //如果是模板文件，则使用全路径
            if (this.FullName.StartsWith(Properties.Settings.Default.TemplatePath, StringComparison.OrdinalIgnoreCase))
                return this.FullName;

            return Common.GetRelativePath(basePath, this.FullName);
        }
        public string Link { get; set; }
        /// <summary>
        /// 判断是否需要Link节点
        /// </summary>
        public bool NeedLinkNode(string basePath)
        {
            if (string.IsNullOrEmpty(this.Link))
                return false;

            string include = this.GetInclude(basePath);
            if (this.Link.Equals(include, StringComparison.OrdinalIgnoreCase))
                return false;

            return true;
        }
        internal FileItem()
        { }
        public FileItem(string basePath, string include, string link)
        {
            this.FullName = Common.GetAbsolutePath(basePath, include);
            if (string.IsNullOrEmpty(link))
                this.Link = Common.GetRelativePath(basePath, this.FullName);
            else
                this.Link = link;
        }

        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            string include = elem.GetAttribute("Include");
            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("k:Link", nsmgr);
            string link = (selectedNode == null) ? "" : selectedNode.InnerText;

            this.FullName = Common.GetAbsolutePath(basePath, include);
            if (string.IsNullOrEmpty(link))
                this.Link = Common.GetRelativePath(basePath, this.FullName);
            else
                this.Link = link;
        }
    }
    public class NoneItem : FileItem
    {
        internal NoneItem()
        { }
        public NoneItem(string basePath, string include, string link)
            : base(basePath, include, link)
        { }

        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("None", Consts.NamesapceURI);

            string include = this.GetInclude(basePath);
            result.SetAttribute("Include", include);

            if (this.NeedLinkNode(basePath))
            {//需要添加 Link 节点
                XmlElement elemLink = xmldoc.CreateElement("Link", Consts.NamesapceURI);
                elemLink.InnerText = this.Link;
                result.AppendChild(elemLink);
            }

            return result;
        }
    }

    public class CompileItem : FileItem
    {
        internal CompileItem()
        { }
        public CompileItem(string basePath, string include, string link)
            : base(basePath, include, link)
        { }

        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("Compile", Consts.NamesapceURI);

            string include = this.GetInclude(basePath);
            result.SetAttribute("Include", include);

            if (this.NeedLinkNode(basePath))
            {//需要添加 Link 节点
                XmlElement elemLink = xmldoc.CreateElement("Link", Consts.NamesapceURI);
                elemLink.InnerText = this.Link;
                result.AppendChild(elemLink);
            }

            return result;
        }
    }
    public class ContentItem : FileItem
    {
        public string CopyToOutputDirectory { get; set; }

        internal ContentItem()
        { }
        public ContentItem(string basePath, string include, string link)
            : base(basePath, include, link)
        { }
        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("Content", Consts.NamesapceURI);

            string include = this.GetInclude(basePath);
            result.SetAttribute("Include", include);

            XmlElement elemCopyToOutputDirectory = xmldoc.CreateElement("CopyToOutputDirectory", Consts.NamesapceURI);
            elemCopyToOutputDirectory.InnerText = this.CopyToOutputDirectory;
            result.AppendChild(elemCopyToOutputDirectory);

            if (this.NeedLinkNode(basePath))
            {//需要添加 Link 节点
                XmlElement elemLink = xmldoc.CreateElement("Link", Consts.NamesapceURI);
                elemLink.InnerText = this.Link;
                result.AppendChild(elemLink);
            }

            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            base.FromXmlInternal(elem, nsmgr, basePath);

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("k:CopyToOutputDirectory", nsmgr);
            this.CopyToOutputDirectory = (selectedNode == null) ? "" : selectedNode.InnerText;
        }
    }
}

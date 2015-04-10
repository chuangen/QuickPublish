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

            return Sense.Utils.IO.Path.GetRelativePath(basePath, this.FullName);
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
        protected readonly string TagName;
        protected FileItem(string tagName)
        {
            this.TagName = tagName;
        }
        public FileItem(string tagName, string basePath, string include, string link)
            : this(tagName)
        {
            this.FullName = Sense.Utils.IO.Path.GetAbsolutePath(basePath, include);
            if (string.IsNullOrEmpty(link))
                this.Link = Sense.Utils.IO.Path.GetRelativePath(basePath, this.FullName);
            else
                this.Link = link;
        }
        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement(this.TagName, Consts.NamesapceURI);

            string include = this.GetInclude(basePath);
            result.SetAttribute("Include", include);

            if (this.NeedLinkNode(basePath)) //需要添加 Link 节点
                append(result, "Link", this.Link);

            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            string include = elem.GetAttribute("Include");
            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("k:Link", nsmgr);
            string link = (selectedNode == null) ? "" : selectedNode.InnerText;

            this.FullName = Sense.Utils.IO.Path.GetAbsolutePath(basePath, include);
            if (string.IsNullOrEmpty(link))
                this.Link = Sense.Utils.IO.Path.GetRelativePath(basePath, this.FullName);
            else
                this.Link = link;
        }
    }
    public class NoneItem : FileItem
    {
        internal NoneItem()
            : base("None")
        { }
        public NoneItem(string basePath, string include, string link)
            : base("None", basePath, include, link)
        { }
    }

    public class CompileItem : FileItem
    {
        public string SubType { get; set; }

        internal CompileItem()
            : base("Compile")
        { }
        public CompileItem(string basePath, string include, string link)
            : base("Compile", basePath, include, link)
        { }
        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = base.ToXml(xmldoc, basePath);

            if (!string.IsNullOrEmpty(this.SubType))
            {
                XmlElement elemSubType = xmldoc.CreateElement("SubType", Consts.NamesapceURI);
                elemSubType.InnerText = this.SubType;
                result.AppendChild(elemSubType);
            }
            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            base.FromXmlInternal(elem, nsmgr, basePath);

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("k:SubType", nsmgr);
            this.SubType = (selectedNode == null) ? "" : selectedNode.InnerText;
        }
    }
    public class ContentItem : FileItem
    {
        public string CopyToOutputDirectory { get; set; }

        internal ContentItem()
            : base("Content")
        { }
        public ContentItem(string basePath, string include, string link)
            : base("Content", basePath, include, link)
        { }
        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = base.ToXml(xmldoc, basePath);

            XmlElement elemCopyToOutputDirectory = xmldoc.CreateElement("CopyToOutputDirectory", Consts.NamesapceURI);
            elemCopyToOutputDirectory.InnerText = this.CopyToOutputDirectory;
            result.AppendChild(elemCopyToOutputDirectory);

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
    
    public class EmbeddedResourceItem : FileItem
    {
        public string DependentUpon { get; set; }

        internal EmbeddedResourceItem()
            : base("EmbeddedResource")
        { }
        public EmbeddedResourceItem(string basePath, string include, string link)
            : base("EmbeddedResource", basePath, include, link)
        { }
        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = base.ToXml(xmldoc, basePath);

            if(!string.IsNullOrEmpty(this.DependentUpon))
                append(result, "DependentUpon", this.DependentUpon);
            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            base.FromXmlInternal(elem, nsmgr, basePath);

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("k:DependentUpon", nsmgr);
            this.DependentUpon = (selectedNode == null) ? "" : selectedNode.InnerText;
        }
    }
}

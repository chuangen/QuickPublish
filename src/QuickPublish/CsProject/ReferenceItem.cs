using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish.CsProject
{
    public class ReferenceItem : NodeBase
    {
        public override string Key
        {
            get { return this.Include; }
        }
        public string Include { get; private set; }
        public string ReferenceName { get; private set; }
        public bool SpecificVersion { get; set; }
        public string HintPath { get; set; }
        public ReferenceItem(string include, string name, bool specificVersion, string HintPath)
        {
            this.Include = include;
            this.ReferenceName = name;
            this.SpecificVersion = specificVersion;
            this.HintPath = HintPath;
        }
        internal ReferenceItem()
            : this("", "", true, "")
        { }

        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("Reference", Consts.NamesapceURI);
            result.SetAttribute("Include", this.Include);
            if (!string.IsNullOrEmpty(this.ReferenceName))
                append(result, "Name", this.ReferenceName);
            if (!this.SpecificVersion)
                append(result, "SpecificVersion", this.SpecificVersion.ToString());
            if (!string.IsNullOrEmpty(this.HintPath))
                append(result, "HintPath", this.HintPath);
            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            this.Include = elem.GetAttribute("Include");

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("k:Name", nsmgr);
            this.ReferenceName = (selectedNode == null) ? "" : selectedNode.InnerText;
            selectedNode = elem.SelectSingleNode("k:SpecificVersion", nsmgr);
            this.SpecificVersion = (selectedNode == null) ? true : bool.Parse(selectedNode.InnerText);
            selectedNode = elem.SelectSingleNode("k:HintPath", nsmgr);
            this.HintPath = (selectedNode == null) ? "" : selectedNode.InnerText;
        }

        public static ReferenceItem FromXml(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            ReferenceItem item = new ReferenceItem();
            item.FromXmlInternal(elem, nsmgr, basePath);

            return item;
        }
    }
}

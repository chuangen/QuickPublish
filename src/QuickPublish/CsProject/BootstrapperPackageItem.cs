using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish.CsProject
{
    public class BootstrapperPackageItem : NodeBase
    {
        public override string Key
        {
            get { return this.Include; }
        }
        public string Include { get; private set; }
        public bool Visible { get; set; }
        public string ProductName { get; set; }
        public bool Install { get; set; }
        public BootstrapperPackageItem(string include)
        {
            this.Include = include;

            this.Visible = false;
            this.ProductName = "";
            this.Install = false;
        }
        internal BootstrapperPackageItem()
            : this("")
        { }

        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("BootstrapperPackage", Consts.NamesapceURI);
            result.SetAttribute("Include", this.Include);

            append(result, "Visible", this.Visible.ToString());
            append(result, "ProductName", this.ProductName);
            append(result, "Install", this.Install ? "true" : "false");
            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            this.Include = elem.GetAttribute("Include");

            bool boolValue;
            XmlNode selectedNode;

            selectedNode = elem.SelectSingleNode("k:Visible", nsmgr);
            string strVisible = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (bool.TryParse(strVisible, out boolValue))
                this.Visible = boolValue;

            selectedNode = elem.SelectSingleNode("k:ProductName", nsmgr);
            this.ProductName = (selectedNode == null) ? "" : selectedNode.InnerText;

            selectedNode = elem.SelectSingleNode("k:Install", nsmgr);
            string strInstall = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (bool.TryParse(strInstall, out boolValue))
                this.Install = boolValue;
        }
    }
}

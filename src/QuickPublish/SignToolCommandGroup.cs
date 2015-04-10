using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish
{
    /// <summary>
    /// PostBuildEvent 节点
    /// </summary>
    public class SignToolCommandGroup
    {
        public SignToolCommandGroup()
        {
            this.SignToolCommand = "";
        }

        public string SignToolCommand { get; set; }

        public void FromXml(XmlElement elem, string basePath)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(elem.OwnerDocument.NameTable);
            nsmgr.AddNamespace("p", elem.NamespaceURI);

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("p:SignToolCommand", nsmgr);
            this.SignToolCommand = (selectedNode == null) ? "" : selectedNode.InnerText;
        }
        public XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("PropertyGroup", Consts.NamesapceURI);

            XmlElement elem = null;
            elem = xmldoc.CreateElement("SignToolCommand", result.NamespaceURI);
            elem.InnerText = this.SignToolCommand;
            result.AppendChild(elem);

            return result;
        }
    }
}

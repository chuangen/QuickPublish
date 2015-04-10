using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish
{
    /// <summary>
    /// PostBuildEvent 节点
    /// </summary>
    public class PostBuildEventGroup
    {
        public PostBuildEventGroup()
        {
            this.PostBuildEvent = "";
        }

        public string PostBuildEvent { get; set; }

        public void FromXml(XmlElement elem, string basePath)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(elem.OwnerDocument.NameTable);
            nsmgr.AddNamespace("p", elem.NamespaceURI);

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("p:PostBuildEvent", nsmgr);
            this.PostBuildEvent = (selectedNode == null) ? "" : selectedNode.InnerText;
        }
        public XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("PropertyGroup", Consts.NamesapceURI);

            XmlElement elem = null;
            elem = xmldoc.CreateElement("PostBuildEvent", result.NamespaceURI);
            elem.InnerText = this.PostBuildEvent;
            result.AppendChild(elem);

            return result;
        }
    }
}

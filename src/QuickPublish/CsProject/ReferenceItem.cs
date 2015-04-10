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
        public ReferenceItem(string include)
        {
            this.Include = include;
        }
        internal ReferenceItem()
            : this("")
        { }

        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = xmldoc.CreateElement("Reference", Consts.NamesapceURI);
            result.SetAttribute("Include", this.Include);
            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            this.Include = elem.GetAttribute("Include");
        }

        public static ReferenceItem FromXml(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            ReferenceItem item = new ReferenceItem();
            item.FromXmlInternal(elem, nsmgr, basePath);

            return item;
        }
    }
}

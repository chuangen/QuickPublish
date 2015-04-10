using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish.CsProject
{
    public abstract class NodeBase
    {
        public abstract string Key { get; }

        protected static void append(XmlElement parent, string name, string value)
        {
            XmlElement elem = parent.OwnerDocument.CreateElement(name, Consts.NamesapceURI);
            elem.InnerText = value;
            parent.AppendChild(elem);
        }

        internal abstract void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath);
        public abstract XmlElement ToXml(XmlDocument xmldoc, string basePath);

        public static NodeBase Create(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            if (elem == null)
                return null;

            NodeBase item = null;
            switch (elem.Name)
            {
                case "Reference": item = new ReferenceItem(); break;
                case "BootstrapperPackage": item = new BootstrapperPackageItem(); break;
                case "Compile": item = new CompileItem(); break;
                case "None": item = new NoneItem(); break;
                case "Content": item = new ContentItem(); break;
                case "PublishFile": item = new PublishFileItem(); break;
                default: item = null; break;
            }
            if (item != null)
                item.FromXmlInternal(elem, nsmgr, basePath);

            return item;
        }
    }
}

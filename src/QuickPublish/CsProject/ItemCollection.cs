using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace QuickPublish.CsProject
{
    public class ItemCollection
    {
        private Dictionary<string, ItemGroup> dicGroups;
        private List<XmlNode> itemGroupOthers = new List<XmlNode>();
        public ItemCollection()
        {
            dicGroups = new Dictionary<string, ItemGroup>(StringComparer.OrdinalIgnoreCase);
            dicGroups.Add("Reference", new ItemGroup());
            dicGroups.Add("BootstrapperPackage", new ItemGroup());
            dicGroups.Add("Compile", new ItemGroup());
            dicGroups.Add("None", new ItemGroup());
            dicGroups.Add("Content", new ItemGroup());
            dicGroups.Add("PublishFile", new ItemGroup());
        }
        public Dictionary<string, ItemGroup> Groups
        {
            get { return dicGroups; }
        }
        public void Clear()
        {
            foreach (ItemGroup group in dicGroups.Values)
                group.Clear();
        }
        public void Add(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            if (dicGroups.ContainsKey(elem.Name))
            {
                NodeBase item = NodeBase.Create(elem, nsmgr, basePath);
                if (item != null)
                {
                    dicGroups[elem.Name].Add(item);
                    return;
                }
            }

            //如果解析失败，则附加到后面
            itemGroupOthers.Add(elem);
        }

        public void AddGroup(XmlElement elemItemGroup, string basePath)
        {
            XmlDocument xmldoc = elemItemGroup.OwnerDocument;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("k", Consts.NamesapceURI);

            foreach (XmlElement elem in elemItemGroup.ChildNodes)
                this.Add(elem, nsmgr, basePath);
        }

        public void SaveToXml(XmlElement nodeProject, string basePath)
        {
            XmlDocument xmldoc = nodeProject.OwnerDocument;
            XmlElement elemOthers = xmldoc.CreateElement("ItemGroup", Consts.NamesapceURI);

            foreach (ItemGroup group in dicGroups.Values)
            {
                if (group.Count < 1)
                    continue;

                XmlElement elemGroup = xmldoc.CreateElement("ItemGroup", Consts.NamesapceURI);
                foreach (NodeBase item in group)
                {
                    if (item.GetType() == typeof(PublishFileItem))
                    {
                        PublishFileItem publishFile = (PublishFileItem)item;
                        if (publishFile.IsDefault)//是默认值，则无需发布属性
                            continue;
                    }

                    XmlElement elem = item.ToXml(xmldoc, basePath);
                    elemGroup.AppendChild(elem);
                }

                nodeProject.AppendChild(elemGroup);
            }
            foreach (XmlNode node in itemGroupOthers)
            {
                elemOthers.AppendChild(xmldoc.ImportNode(node, true));
            }
            nodeProject.AppendChild(elemOthers);
        }
    }
}

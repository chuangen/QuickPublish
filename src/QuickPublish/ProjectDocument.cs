using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using QuickPublish.CsProject;

namespace QuickPublish
{
    public partial class ProjectDocument : IDisposable
    {
        /// <summary>
        /// PropertyGroup
        /// </summary>
        protected PropertyCollection properties = new PropertyCollection();
        protected ItemCollection items = new ItemCollection();
        protected List<XmlNode> others = new List<XmlNode>();
        public string FileName { get; protected set; }
        public string BasePath
        {
            get { return Path.GetDirectoryName(this.FileName); }
        }

        public ProjectDocument(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName 不能为空。");

            this.ReadXml(fileName);
        }
        /// <summary>
        /// 将指定文件中的 XML 架构和数据读入 System.Data.DataTable 中。
        /// </summary>
        /// <param name="fileName">从中读取数据的文件的名称。</param>
        /// <returns>用于读取数据的 System.Data.XmlReadMode。</returns>
        public virtual void ReadXml(string fileName)
        {
            this.FileName = Path.GetFullPath(fileName);

            properties.Clear();
            items.Clear();
            others.Clear();

            string basePath = Path.GetDirectoryName(this.FileName);

            XmlDocument xmldoc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("k", Consts.NamesapceURI);

            xmldoc.Load(fileName);

            //Project节点
            XmlElement nodeProject = xmldoc.DocumentElement;

            foreach (XmlNode node in nodeProject.ChildNodes)
            {
                try
                {
                    switch (node.Name)
                    {
                        case "PropertyGroup":
                            properties.AddGroup((XmlElement)node, basePath);
                            break;
                        case "ItemGroup":
                            items.AddGroup((XmlElement)node, basePath);
                            break;
                        default:
                            others.Add(node.CloneNode(true));
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //
                }
            }
        }
        /// <summary>
        /// 使用指定的文件以 XML 格式写入 System.Data.DataTable 的当前内容。
        /// </summary>
        /// <param name="fileName">要向其写入 XML 数据的文件。</param>
        public virtual void WriteXml(string fileName)
        {
            this.WriteXml(fileName, "4.0");
        }
        public virtual void WriteXml(string fileName, string ToolsVersion)
        {
            string fullName = Path.GetFullPath(fileName);
            string basePath = Path.GetDirectoryName(fullName);

            XmlDocument xmldoc = new XmlDocument();

            //<?xml version="1.0" encoding="utf-8"?>
            XmlDeclaration xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);
            //<Project 节点
            XmlElement elem = xmldoc.CreateElement("Project", Consts.NamesapceURI);
            elem.SetAttribute("ToolsVersion", ToolsVersion);
            elem.SetAttribute("DefaultTargets", "Build");
            elem.SetAttribute("xmlns", Consts.NamesapceURI);
            xmldoc.AppendChild(elem);

            XmlElement nodeProject = xmldoc.DocumentElement;
            // PropertyGroup 组
            properties.SaveToXml(nodeProject, basePath);
            // ItemGroup 组
            items.SaveToXml(nodeProject, basePath);
            // 其他未知节点
            foreach (XmlNode node in others)
                nodeProject.AppendChild(xmldoc.ImportNode(node, true));

            xmldoc.Save(fileName);
        }

        #region IDisposable 成员

        public void Dispose()
        {
            //
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish.CsProject
{
    public class ProjectReferenceItem : FileItem
    {
        public string ProjectGuid { get; set; }
        public string ProjectName { get; set; }

        internal ProjectReferenceItem()
            : base("ProjectReference")
        { }
        public ProjectReferenceItem(string basePath, string include, string projectGuid, string projectName)
            : base("ProjectReference", basePath, include, "")
        {
            this.ProjectGuid = projectGuid;
            this.ProjectName = projectName;
        }
        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            XmlElement result = base.ToXml(xmldoc, basePath);

            if(!string.IsNullOrEmpty(this.ProjectGuid))
                append(result, "Project", this.ProjectGuid);
            append(result, "Name", this.ProjectName);

            return result;
        }
        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            base.FromXmlInternal(elem, nsmgr, basePath);

            XmlNode selectedNode;
            selectedNode = elem.SelectSingleNode("k:Project", nsmgr);
            this.ProjectGuid = (selectedNode == null) ? "" : selectedNode.InnerText;

            selectedNode = elem.SelectSingleNode("k:Name", nsmgr);
            this.ProjectName = (selectedNode == null) ? "" : selectedNode.InnerText;
        }
    }
}

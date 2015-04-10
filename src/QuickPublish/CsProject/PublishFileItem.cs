using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace QuickPublish.CsProject
{
    /// <summary>
    /// 发布状态
    /// </summary>
    public enum PublishState
    {
        /// <summary>
        /// 数据文件
        /// </summary>
        [StringValue("DataFile")]
        DataFile,
        /// <summary>
        /// 包括
        /// </summary>
        [StringValue("Include")]
        Include,
        /// <summary>
        /// 排除
        /// </summary>
        [StringValue("Exclude")]
        Exclude,
    }
    public class PublishFileItem : FileItem
    {
        public bool Visible {get; set;}
        /// <summary>
        /// 下载组
        /// (必需)
        /// (新建...)
        /// </summary>
        public string Group {get; set;}
        public string TargetPath {get; set;}
        public PublishState PublishState {get; set;}
        public bool IncludeHash {get; set;}
        public string FileType {get; set;}

        internal PublishFileItem()
            : base("PublishFile")
        { }
        public PublishFileItem(string basePath, string include)
            : base("PublishFile", basePath, include, "")
        {
            this.Visible = false;
            this.Group = "";
            this.TargetPath = "";
            this.PublishState = PublishState.Include;
            this.IncludeHash = true;
            this.FileType = "File";
        }
        /// <summary>
        /// 确认属性是否都是默认值。
        /// </summary>
        public bool IsDefault
        {
            get
            {
                return (this.Visible == false)
                    && string.IsNullOrEmpty(this.Group)
                    && string.IsNullOrEmpty(this.TargetPath)
                    && (this.PublishState == PublishState.Include)
                    && (this.IncludeHash == true)
                    && (this.FileType == "File");
            }
        }
        public override XmlElement ToXml(XmlDocument xmldoc, string basePath)
        {
            string include = Sense.Utils.IO.Path.GetRelativePath(basePath, this.FullName);
            XmlElement result = xmldoc.CreateElement("PublishFile", Consts.NamesapceURI);
            result.SetAttribute("Include", include);

            append(result, "Visible", this.Visible ? "true" : "false");
            append(result, "Group", this.Group);
            append(result, "TargetPath", this.TargetPath);
            append(result, "PublishState", EnumHelper.GetName(typeof(PublishState), PublishState));
            append(result, "IncludeHash", this.IncludeHash ? "true" : "false");
            append(result, "FileType", this.FileType);
            return result;
        }
        public static bool TryParsePublishState(string value, out PublishState state)
        {
            //默认为 Include
            state = PublishState.Include;
            if (string.IsNullOrEmpty(value))
                return false;
            try
            {
                state = (PublishState)EnumHelper.Parse(typeof(PublishState), value, true);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static PublishFileItem fromXml(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            PublishFileItem result = new PublishFileItem(basePath, elem.GetAttribute("Include"));

            bool boolValue;
            PublishState state;
            XmlNode selectedNode;

            selectedNode = elem.SelectSingleNode("k:Visible", nsmgr);
            string strVisible = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (bool.TryParse(strVisible, out boolValue))
                result.Visible = boolValue;

            selectedNode = elem.SelectSingleNode("k:Group", nsmgr);
            result.Group = (selectedNode == null) ? "" : selectedNode.InnerText;

            selectedNode = elem.SelectSingleNode("k:TargetPath", nsmgr);
            result.TargetPath = (selectedNode == null) ? "" : selectedNode.InnerText;

            selectedNode = elem.SelectSingleNode("k:PublishState", nsmgr);
            string strPublishState = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (TryParsePublishState(strPublishState, out state))
                result.PublishState = state;

            selectedNode = elem.SelectSingleNode("k:IncludeHash", nsmgr);
            string strIncludeHash = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (bool.TryParse(strVisible, out boolValue))
                result.IncludeHash = boolValue;

            selectedNode = elem.SelectSingleNode("k:FileType", nsmgr);
            result.FileType = (selectedNode == null) ? "" : selectedNode.InnerText;

            return result;
        }

    }
}

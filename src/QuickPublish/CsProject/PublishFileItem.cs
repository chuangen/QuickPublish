﻿using System;
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
    public class PublishFileItem : NodeBase
    {
        public override string Key
        {
            get { return this.Include; }
        }
        public string Include { get; set; }
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


        protected readonly string TagName = "PublishFile";
        public PublishFileItem()
            : this(null)
        { }

        public PublishFileItem(string include)
        {
            this.Include = include;

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
            XmlElement result = xmldoc.CreateElement("PublishFile", Consts.NamesapceURI);
            result.SetAttribute("Include", this.Include);

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

        internal override void FromXmlInternal(XmlElement elem, XmlNamespaceManager nsmgr, string basePath)
        {
            this.Include = elem.GetAttribute("Include");

            bool boolValue;
            PublishState state;
            XmlNode selectedNode;

            selectedNode = elem.SelectSingleNode("k:Visible", nsmgr);
            string strVisible = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (bool.TryParse(strVisible, out boolValue))
                this.Visible = boolValue;

            selectedNode = elem.SelectSingleNode("k:Group", nsmgr);
            this.Group = (selectedNode == null) ? "" : selectedNode.InnerText;

            selectedNode = elem.SelectSingleNode("k:TargetPath", nsmgr);
            this.TargetPath = (selectedNode == null) ? "" : selectedNode.InnerText;

            selectedNode = elem.SelectSingleNode("k:PublishState", nsmgr);
            string strPublishState = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (TryParsePublishState(strPublishState, out state))
                this.PublishState = state;

            selectedNode = elem.SelectSingleNode("k:IncludeHash", nsmgr);
            string strIncludeHash = (selectedNode == null) ? "" : selectedNode.InnerText;
            if (bool.TryParse(strVisible, out boolValue))
                this.IncludeHash = boolValue;

            selectedNode = elem.SelectSingleNode("k:FileType", nsmgr);
            this.FileType = (selectedNode == null) ? "" : selectedNode.InnerText;
        }
    }
}

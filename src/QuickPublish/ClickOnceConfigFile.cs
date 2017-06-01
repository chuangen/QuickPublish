using System;
using System.Collections.Generic;
using System.Text;
using QuickPublish.CsProject;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace QuickPublish
{
    /// <summary>
    /// ClickOnce 发布配置文件。
    /// </summary>
    public class ClickOnceConfigFile : ProjectDocument
    {
        protected readonly PublishSettings pubSettings = new PublishSettings();
        protected List<XmlNode> importNodes = new List<XmlNode>();
        protected readonly PostBuildEventGroup groupPostBuildEvent = new PostBuildEventGroup();
        protected readonly SignToolCommandGroup groupSignToolCommand = new SignToolCommandGroup();
        public PublishSettings Settings
        {
            get { return pubSettings; }
        }
        public ClickOnceConfigFile(string fileName)
            : base(fileName)
        {
        }

        enum PropertyGroupType
        {
            Normal,
            PublishSettings,
            PostBuildEvent,
            SignToolCommand,
        }
        public override void ReadXml(string fileName)
        {
            base.FileName = Path.GetFullPath(fileName);

            properties.Clear();
            items.Clear();
            importNodes.Clear();
            others.Clear();

            string basePath = Path.GetDirectoryName(this.FileName);

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(fileName);

            //Project节点
            XmlElement nodeProject = xmldoc.DocumentElement;

            foreach (XmlNode node in nodeProject.ChildNodes)
            {
                try
                {
                    switch (node.Name)
                    {
                        case "PublishSettings":
                            pubSettings.FromXml((XmlElement)node, basePath);
                            break;
                        case "PropertyGroup":
                            PropertyGroupType type = PropertyGroupType.Normal;
                            foreach(XmlElement elem in node.ChildNodes)
                            {
                                if(elem.Name == "DistributionPath")
                                {
                                    type = PropertyGroupType.PublishSettings;
                                    break;
                                }
                                if (elem.Name == "PostBuildEvent")
                                {
                                    type = PropertyGroupType.PostBuildEvent;
                                    break;
                                }
                                if (elem.Name == "SignToolCommand")
                                {
                                    type = PropertyGroupType.SignToolCommand;
                                    break;
                                }
                            }
                            switch(type)
                            {
                                case PropertyGroupType.Normal:
                                    properties.AddGroup((XmlElement)node, basePath);
                                    break;
                                case PropertyGroupType.PublishSettings:
                                    pubSettings.FromXml((XmlElement)node, basePath);
                                    break;
                                case PropertyGroupType.PostBuildEvent:
                                    groupPostBuildEvent.FromXml((XmlElement)node, basePath);
                                    break;
                                case PropertyGroupType.SignToolCommand:
                                    groupSignToolCommand.FromXml((XmlElement)node, basePath);
                                    break;
                            }
                            break;
                        case "ItemGroup":
                            items.AddGroup((XmlElement)node, basePath);
                            break;
                        case "Import":
                            importNodes.Add(node.CloneNode(true));
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
        public override void WriteXml(string fileName)
        {
            string basePath = Path.GetDirectoryName(fileName);

            XmlDocument xmldoc = new XmlDocument();

            //<?xml version="1.0" encoding="utf-8"?>
            XmlDeclaration xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(xmldecl);
            //<Project 节点
            XmlElement elem = xmldoc.CreateElement("Project", Consts.NamesapceURI);
            elem.SetAttribute("ToolsVersion", "4.0");
            elem.SetAttribute("DefaultTargets", "Build");
            elem.SetAttribute("xmlns", Consts.NamesapceURI);
            xmldoc.AppendChild(elem);

            XmlElement nodeProject = xmldoc.DocumentElement;
            // PropertyGroup 组
            properties.SaveToXml(nodeProject, basePath);
            // PublishSettings
            nodeProject.AppendChild(pubSettings.ToXml(xmldoc, basePath));
            // ItemGroup 组
            items.SaveToXml(nodeProject, basePath);
            // Import 节点
            foreach (XmlNode node in importNodes)
                nodeProject.AppendChild(xmldoc.ImportNode(node, true));
            // PostBuildEvent
            nodeProject.AppendChild(groupPostBuildEvent.ToXml(xmldoc, basePath));
            // SignToolCommand
            nodeProject.AppendChild(groupSignToolCommand.ToXml(xmldoc, basePath));
            // 其他未知节点
            foreach (XmlNode node in others)
                nodeProject.AppendChild(xmldoc.ImportNode(node, true));

            xmldoc.Save(fileName);
        }

        public static ClickOnceConfigFile NewDocument(string fileName)
        {
            try
            {
                string templatePath = Properties.Settings.Default.TemplatePath;
                ClickOnceConfigFile document = new ClickOnceConfigFile(Path.Combine(templatePath, "loader.csproj"));
                CsProject.PropertyCollection properties = document.PropertyItems;

                properties.ProjectGuid = Guid.NewGuid().ToString("B").ToUpper();
                document.WriteXml(fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new ClickOnceConfigFile(fileName);
        }

        static void getFileNames(List<string> fileNames, string parent)
        {
            if (!Directory.Exists(parent))
                return;
            fileNames.AddRange(Directory.GetFiles(parent));
            foreach (string folder in Directory.GetDirectories(parent))
            {
                if (Path.GetFileName(folder).StartsWith("."))
                    continue;
                getFileNames(fileNames, folder);
            }
        }
        public void RefreshPublishFiles()
        {
            string basePath = Path.GetDirectoryName(this.FileName);
            string distPath = pubSettings.DistributionPath;
            if (!Directory.Exists(distPath))
                return;

            List<string> fileNames = new List<string>();
            getFileNames(fileNames, distPath);

            ItemGroup itemGroupContents = items.Groups["Content"];
            ItemGroup itemGroupPublishFiles = items.Groups["PublishFile"];
            foreach (string file in fileNames)
            {
                string fullName = Path.GetFullPath(file);
                string include = Sense.Utils.IO.Path.GetRelativePath(basePath, fullName);
                string link = Sense.Utils.IO.Path.GetRelativePath(distPath, fullName);
                if (!itemGroupContents.ContainsKey(fullName))
                {
                    ContentItem item = new ContentItem(basePath, include, link);
                    item.CopyToOutputDirectory = "PreserveNewest";
                    itemGroupContents.Add(item);
                }
                if (!itemGroupPublishFiles.ContainsKey(link))
                {
                    PublishFileItem item = new PublishFileItem(link);
                    item.Visible = false;
                    item.Group = "";
                    item.TargetPath = "";
                    item.PublishState = PublishState.Include;
                    item.IncludeHash = true;
                    item.FileType = "File";
                    itemGroupPublishFiles.Add(item);
                }
            }
            List<string> removes = new List<string>();
            foreach (string fullName in itemGroupContents.GetKeys())
            {
                if (!fileNames.Contains(fullName))
                    removes.Add(fullName);
            }
            foreach (string fullName in removes)
            {
                if (itemGroupContents.ContainsKey(fullName))
                    itemGroupContents.Remove(fullName);
            }
            removes.Clear();
            foreach (string item in itemGroupPublishFiles.GetKeys())
            {
                bool found = false;
                foreach(ContentItem contentItem in itemGroupContents)
                {
                    string include = null;
                    if (contentItem.NeedLinkNode(basePath))
                        include = contentItem.Link;
                    else
                        include = contentItem.GetInclude(basePath);

                    if(item == include)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    continue;
                removes.Add(item);
            }
            foreach (string item in removes)
            {
                if (itemGroupPublishFiles.ContainsKey(item))
                    itemGroupPublishFiles.Remove(item);
            }
        }

        /// <summary>
        /// 按配置生成保存为C#项目。
        /// </summary>
        /// <param name="filename"></param>
        public static void SaveAsCsProject(string configFile, string projectFile)
        {
            ClickOnceConfigFile config = new ClickOnceConfigFile(configFile);
            config.SaveAsCsProject(projectFile);
        }
        public void SaveAsCsProject(string projectFile)
        {
            string projectPath = Path.GetDirectoryName(projectFile);
            if (!Directory.Exists(projectPath))
                Directory.CreateDirectory(projectPath);

            //使用 ProjectDocument 保存，则不会保存PublishSettings节点
            base.WriteXml(projectFile);

            ProjectDocument document = new ProjectDocument(projectFile);

            SetAssemblyInfoCs(document, pubSettings.AssemblyInfoFile);
            SetProgramCs(document, pubSettings.ExecuteFile, pubSettings.ExecuteFileType);

            document.WriteXml(projectFile);
        }
        /// <summary>
        /// 根据指定的AssemblyInfo.cs文件的内容，生成需要的AssemblyInfo.cs文件。
        /// </summary>
        /// <param name="document"></param>
        /// <param name="AssemblyInfoFile"></param>
        static void SetAssemblyInfoCs(ProjectDocument document, string AssemblyInfoFile)
        {
            string link = @"Properties\AssemblyInfo.cs";

            string templatePath = Properties.Settings.Default.TemplatePath;
            string basePath = document.BasePath;

            //模板文档
            string templateFile = Path.Combine(templatePath, link);
            string destFile = Path.Combine(basePath, link);

            string destPath = Path.GetDirectoryName(destFile);
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            //读取模板
            string[] templateLines = File.ReadAllLines(Path.Combine(templatePath, link));

            //存储最终生成的内容
            string[] destLines = templateLines;
            if (!string.IsNullOrEmpty(AssemblyInfoFile) && File.Exists(AssemblyInfoFile))
            {//如果指定 AssemblyInfo.cs 文件，则根据源内容，替换指定的值
                //存储源文件的赋值键对
                Dictionary<string, string> dicValues = new Dictionary<string, string>();
                string srcContent = File.ReadAllText(AssemblyInfoFile);
                string pattern = @"^(?<tab>\s*)\[assembly:\s*(?<key>\w+)\s*\((?<value>.+?)\)\s*\]\s*$";
                Regex regex = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
                MatchCollection matches = regex.Matches(srcContent);
                foreach (Match m in matches)
                {
                    if (m.Success)
                    {
                        string key = m.Groups["key"].Value;
                        string value = m.Groups["value"].Value;
                        if (!dicValues.ContainsKey(key))
                            dicValues.Add(key, value);
                    }
                }
                regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
                for (int i = 0; i < destLines.Length; i++)
                {
                    Match match = regex.Match(destLines[i]);
                    if (match.Success)
                    {//如果匹配，则替换值
                        string key = match.Groups["key"].Value;
                        if (dicValues.ContainsKey(key))
                        {
                            destLines[i] = string.Format("{0}[assembly: {1}({2})]",
                                match.Groups["tab"].Value,
                                key,
                                dicValues[key]);
                        }
                    }
                }
            }
            File.WriteAllLines(destFile, destLines, Encoding.UTF8);

            CompileItem compileItem = null;
            foreach (CompileItem item in document.ItemGroupCompiles)
            {
                if (item.Link == link || item.FullName.EndsWith(link, StringComparison.OrdinalIgnoreCase))
                {
                    compileItem = item;
                    break;
                }
            }
            if (compileItem != null)
                document.ItemGroupCompiles.Remove(compileItem.FullName);

            string fullName = Path.GetFullPath(destFile);
            document.ItemGroupCompiles.Add(new CompileItem(basePath, fullName, link));
        }
        static void SetProgramCs(ProjectDocument document, string ExecuteFile, ExecuteFileTypes ExecuteFileType)
        {
            string link = @"Properties\Program.cs";

            string templatePath = Properties.Settings.Default.TemplatePath;
            string basePath = document.BasePath;

            string srcFile = Path.Combine(templatePath, link);
            string destFile = Path.Combine(basePath, link);

            string destPath = Path.GetDirectoryName(destFile);
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);
            File.Copy(srcFile, destFile, true);

            string text = File.ReadAllText(destFile, Encoding.UTF8);
            text = System.Text.RegularExpressions.Regex.Replace(text,
                "^        public const string ExecuteFile = @\"ShopAsstX.exe\";$",
                string.Format("        public const string ExecuteFile = @\"{0}\";", ExecuteFile)
                );
            text = System.Text.RegularExpressions.Regex.Replace(text,
                "^        public const bool IsCLR = true;$",
                string.Format("        public const bool IsCLR = {0};", (ExecuteFileType == ExecuteFileTypes.CLR) ? "true" : "false")
                );
            File.WriteAllText(destFile, text, Encoding.UTF8);

            //指定 Program.cs 文件
            CompileItem compileItem = null;
            foreach (CompileItem item in document.ItemGroupCompiles)
            {
                if (item.Link == link || item.FullName.EndsWith(link, StringComparison.OrdinalIgnoreCase))
                {
                    compileItem = item;
                    break;
                }
            }
            if (compileItem != null)
                document.ItemGroupCompiles.Remove(compileItem.FullName);

            string fullName = Path.GetFullPath(destFile);
            document.ItemGroupCompiles.Add(new CompileItem(basePath, fullName, link));
        }
    }
}

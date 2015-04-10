using System;
using System.Collections.Generic;
using System.Text;
using QuickPublish.CsProject;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;

namespace QuickPublish
{
    partial class BuildForm
    {
        void Publish(BackgroundWorker bw, string settingsFile)
        {
            ClickOnceConfigFile config = new ClickOnceConfigFile(settingsFile);
            string publishPath = config.PropertyItems.PublishUrl;

            bw.ReportProgress(5, "初始化临时目录...");
            string templatePath = Properties.Settings.Default.TemplatePath;
            string projectPath = Path.Combine(publishPath, Guid.NewGuid().ToString("N").ToLower());
            if (!Directory.Exists(projectPath))
                Directory.CreateDirectory(projectPath);

            bw.ReportProgress(5, "生成C#项目...");
            string projectFile = Path.Combine(projectPath, "loader.csproj");
            config.SaveAsCsProject(projectFile);

            bw.ReportProgress(5, string.Format("正在编译{0}...", projectFile));
            CmdMSBuild(projectFile);

            //复制
            bw.ReportProgress(5, string.Format("复制到{0}...", publishPath));
            string appPublish = Path.Combine(projectPath, @"bin\Release\app.publish");
            if (Directory.Exists(appPublish))
            {
                Common.MoveDirectory(appPublish, publishPath, null);
            }

            bw.ReportProgress(5, string.Format("清理临时目录{0}...", publishPath));
            Directory.Delete(projectPath, true);

            bw.ReportProgress(100, "操作完成。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectFullName"></param>
        /// <remarks>
        /// VS2008命令行：%comspec% /k ""C:\Program Files\Microsoft Visual Studio 9.0\VC\vcvarsall.bat"" x86
        /// 
        /// 批处理中加载环境变量 @call "C:\Program Files\Microsoft Visual Studio 9.0\VC\vcvarsall.bat" x86
        /// 生成 MSBuild loader.csproj /t:Rebuild /p:Configuration=Release
        /// 发布 MSBuild loader.csproj /t:Publish /p:Configuration=Release /nologo
        /// 
        /// </remarks>
        void CmdMSBuild(string projectFullName)
        {
            string preCurrentDirectory = Environment.CurrentDirectory;
            //切换到当前目录
            Environment.CurrentDirectory = Path.GetDirectoryName(projectFullName);
            
            string vcvarsallFile = Path.Combine(settings.VisualStudioPath, @"VC\vcvarsall.bat");
            //处理器类型 x86 x64
            string processorArchitecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");

            string[] commands = new string[] {
                string.Format("\"\"{0}\"\" {1}", vcvarsallFile, processorArchitecture),//vcvarsall.bat x86
                "echo off",//echo off
                "MSBuild loader.csproj /t:Publish /p:Configuration=Release /nologo",
            };

            lineNumber = 0;
            Process process = new Process();
            process.StartInfo.FileName = Environment.GetEnvironmentVariable("ComSpec");
            process.StartInfo.Arguments = "/C " + string.Join(" & ", commands);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;
            process.ErrorDataReceived += new DataReceivedEventHandler(process_ErrorDataReceived);
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            process.Close();

            //恢复当前目录
            Environment.CurrentDirectory = preCurrentDirectory;
        }

        /// <summary>
        /// 判断生成是否成功。
        /// </summary>
        bool hasSuccess(string output)
        {
            Regex regex = new Regex(@"(?:"
                + @"(?<warning>\d+) 个警告\s+(?<error>\d+) 个错误"
                + @")|(?:"
                + @"(?<warn>\d+) Warning\(s\)\s+(?<error>\d+) Error\(s\)"
                + @")", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match match = regex.Match(output);
            if (!match.Success)
                return false;

            int warnCount, errorCount;
            int.TryParse(match.Groups["warn"].Value, out warnCount);
            int.TryParse(match.Groups["error"].Value, out errorCount);
            if (errorCount > 0)
                return false;

            return true;
        }

        static int lineNumber = 0;
        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DataReceivedEventHandler(process_OutputDataReceived), sender, e);
                return;
            }

            if (String.IsNullOrEmpty(e.Data))
                return;

            lineNumber++;
            rtbOutput.Select(rtbOutput.TextLength, 0);
            rtbOutput.SelectionColor = Color.Red;
            string output = "ERROR: " + e.Data;
            Debug.WriteLine(output);
            rtbOutput.AppendText(output + Environment.NewLine);
            rtbOutput.ScrollToCaret();
        }
        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DataReceivedEventHandler(process_OutputDataReceived), sender, e);
                return;
            }

            if (String.IsNullOrEmpty(e.Data))
                return;

            lineNumber++;

            //CSC : error CS2001
            if (e.Data.StartsWith("CSC : error CS"))
            {
                rtbOutput.Select(rtbOutput.TextLength, 0);
                rtbOutput.SelectionColor = Color.Red;
            }
            else
            {
                rtbOutput.Select(rtbOutput.TextLength, 0);
                rtbOutput.SelectionColor = Color.Black;
            }

            string output = e.Data;
            Debug.WriteLine(output);
            rtbOutput.AppendText(output + Environment.NewLine);
            rtbOutput.ScrollToCaret();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace QuickPublish
{
    public class Common
    {
        /// <summary>
        /// 获取VS的安装路径
        /// </summary>
        /// <returns></returns>
        public static string GetVisualStudioPath()
        {
            string result = "";
            // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0\
            // ShellFolder  = C:\Program Files\Microsoft Visual Studio 10.0\
            // InstallDir   = C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\

            RegistryKey keyVisualStudio = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\");
            foreach(string keyName in keyVisualStudio.GetSubKeyNames())
            {
                RegistryKey key = keyVisualStudio.OpenSubKey(keyName);
                string ShellFolder = key.GetValue("ShellFolder") as string;
                if(ShellFolder == null)
                {
                    string InstallDir = key.GetValue("InstallDir") as string;
                    if(InstallDir != null)
                    {
                        ShellFolder = Path.GetFullPath( Path.Combine(InstallDir, "../..") );
                    }
                }

                if(!string.IsNullOrEmpty(ShellFolder))
                {
                    result = ShellFolder;
                    break;
                }
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <param name="excludes">排除的文件夹和文件名称</param>
        public static void MoveDirectory(string srcPath, string destPath, string[] excludes)
        {
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            foreach (string srcFile in Directory.GetFiles(srcPath))
            {
                string destFile = Path.Combine(destPath, Path.GetFileName(srcFile));
                string parentFolder = Path.GetDirectoryName(destFile);
                if (!Directory.Exists(parentFolder))
                    Directory.CreateDirectory(parentFolder);

                if (File.Exists(destFile))
                    File.Delete(destFile);
                File.Move(srcFile, destFile);
            }
            foreach (string folder in Directory.GetDirectories(srcPath))
            {
                bool isInclude = true;
                if( excludes != null)
                {//检查是否排除
                    foreach (string exName in excludes)
                    {
                        if (string.Equals(folder, exName, StringComparison.OrdinalIgnoreCase))
                        {
                            isInclude = false;
                            break;
                        }
                    }
                }
                if(!isInclude)
                    continue;
                
                string destFolder = Path.Combine(destPath, Path.GetFileName(folder));
                MoveDirectory(folder, destFolder, excludes);
            }
            //如果目录为空，则删掉
            if (Directory.GetFileSystemEntries(srcPath).Length < 1)
                Directory.Delete(srcPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        /// <param name="excludes">排除的文件夹和文件名称</param>
        public static void CopyDirectory(string srcPath, string destPath, string[] excludes)
        {
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);

            foreach (string srcFile in Directory.GetFiles(srcPath))
            {
                string destFile = Path.Combine(destPath, Path.GetFileName(srcFile));
                string parentFolder = Path.GetDirectoryName(destFile);
                if (!Directory.Exists(parentFolder))
                    Directory.CreateDirectory(parentFolder);

                File.Copy(srcFile, destFile, true);
            }
            foreach (string folder in Directory.GetDirectories(srcPath))
            {
                bool isInclude = true;
                if (excludes != null)
                {//检查是否排除
                    foreach (string exName in excludes)
                    {
                        if (string.Equals(folder, exName, StringComparison.OrdinalIgnoreCase))
                        {
                            isInclude = false;
                            break;
                        }
                    }
                }
                if (!isInclude)
                    continue;

                string destFolder = Path.Combine(destPath, Path.GetFileName(folder));
                CopyDirectory(folder, destFolder, excludes);
            }
        }
    }
}

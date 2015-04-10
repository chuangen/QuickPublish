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
        /// 获取绝对路径。
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(string path1, string path2)
        {
            return Path.GetFullPath(Path.Combine(path1, path2));
        }
        /// <summary>
        /// 获取路径2相对于路径1的相对路径
        /// </summary>
        /// <param name="strPath1">路径1</param>
        /// <param name="strPath2">路径2</param>
        /// <returns>返回路径2相对于路径1的路径</returns>
        /// <example>
        /// string strPath = GetRelativePath(@"C:\WINDOWS\system32", @"C:\WINDOWS\system\*.*" );
        /// //strPath == @"..\system\*.*"
        /// </example>
        /// <remarks>一个获取相对路径的方法（C#）
        /// From:http://blog.csdn.net/lynnlin1122/archive/2007/03/01/1518648.aspx</remarks>
        public static string GetRelativePath(string strPath1, string strPath2)
        {
            if (!strPath1.EndsWith("\\")) strPath1 += "\\";    //如果不是以"\"结尾的加上"\"
            int intIndex = -1, intPos = strPath1.IndexOf('\\');
            //以"\"为分界比较从开始处到第一个"\"处对两个地址进行比较,如果相同则扩展到
            //下一个"\"处;直到比较出不同或第一个地址的结尾.
            while (intPos >= 0)
            {
                intPos++;
                if (string.Compare(strPath1, 0, strPath2, 0, intPos, true) != 0) break;
                intIndex = intPos;
                intPos = strPath1.IndexOf('\\', intPos);
            }

            //如果从不是第一个"\"处开始有不同,则从最后一个发现有不同的"\"处开始将strPath2
            //的后面部分付值给自己,在strPath1的同一个位置开始望后计算每有一个"\"则在strPath2
            //的前面加上一个"..\"(经过转义后就是"..\\").
            if (intIndex >= 0)
            {
                strPath2 = strPath2.Substring(intIndex);
                intPos = strPath1.IndexOf("\\", intIndex);
                while (intPos >= 0)
                {
                    strPath2 = "..\\" + strPath2;
                    intPos = strPath1.IndexOf("\\", intPos + 1);
                }
            }
            //否则直接返回strPath2
            return strPath2;
        }


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
    }
}

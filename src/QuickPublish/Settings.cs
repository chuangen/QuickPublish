using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QuickPublish.Properties
{
    partial class Settings
    {
        /// <summary>
        /// 模板路径。
        /// </summary>
        public string TemplatePath
        {
            get { return Path.Combine(Application.StartupPath, @"template\"); }
        }
    }
}

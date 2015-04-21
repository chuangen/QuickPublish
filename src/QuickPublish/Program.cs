using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuickPublish
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string settingsFile = "";
            string options = "";
            if (args.Length > 0)
                settingsFile = args[0];
            if (args.Length > 1)
                options = args[1];

            Form form = null;
            switch (options.Trim().ToLower())
            {
                case "/update":
                    string fileName = System.IO.Path.Combine(Application.StartupPath, settingsFile);
                    ClickOnceConfigFile document = new ClickOnceConfigFile(fileName);
                    document.RefreshPublishFiles();
                    //项目文件
                    document.WriteXml(document.FileName);
                    Console.WriteLine("/update 成功更新。");
                    return;
                case "/build":
                    form = new BuildForm(settingsFile);
                    break;
                default:
                    form = new MainForm(settingsFile);
                    break;
            }
            Application.Run(form);
        }
    }
}

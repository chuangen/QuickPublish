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

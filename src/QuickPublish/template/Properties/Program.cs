using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.IO;

namespace Loader
{
    static class Consts
    {
        public const string ExecuteFile = @"ShopAsstX.exe";
        public const bool IsCLR = true;
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
#if DEBUG
            Debugger.Break();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] newArgs = args;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                List<string> list = new List<string>();
                try
                {
                    string queryString = ApplicationDeployment.CurrentDeployment.ActivationUri.Query;
                    NameValueCollection parameters = System.Web.HttpUtility.ParseQueryString(queryString);
                    foreach (string key in parameters.AllKeys)
                    {
                        list.Add(key + "=" + parameters[key]);
                    }
                }
                catch (Exception)
                { }

                newArgs = list.ToArray();
            }

            string exePath = Path.Combine(Application.StartupPath, Consts.ExecuteFile);
            if (Consts.IsCLR)
            {
                AppDomain domain = AppDomain.CurrentDomain;
                domain.ExecuteAssembly(exePath, domain.Evidence, newArgs);
            }
            else
            {
                ProcessStartInfo info = new ProcessStartInfo(exePath);
                info.UseShellExecute = false;
                info.Arguments = string.Join(" ", newArgs);
                Process.Start(info);
            }

        }
    }
}

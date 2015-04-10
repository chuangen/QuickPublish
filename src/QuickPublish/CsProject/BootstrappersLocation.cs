using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickPublish.CsProject
{
    /// <summary>
    /// 系统必备下载位置。
    /// </summary>
    public enum BootstrappersLocations
    {
        /// <summary>
        /// 默认，从组件供应商网站下载
        /// </summary>
        [StringValue("Default")]
        Default,
        /// <summary>
        /// 与我应用程序相同位置下载
        /// </summary>
        [StringValue("Relative")]
        Relative,
        /// <summary>
        /// 指定下载位置。
        /// </summary>
        [StringValue("Absolute")]
        Absolute,
    }
}

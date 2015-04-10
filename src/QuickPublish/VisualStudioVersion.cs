using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPublish
{
    /// <summary>
    /// Visual Studio 所有版本。
    /// </summary>
    public enum VisualStudioVersion
    {
        /// <summary>
        /// 未知。
        /// </summary>
        [StringValue("")]
        Unknown = 0,
        /// <summary>
        /// Visual Studio 2002 = 7.0
        /// </summary>
        [StringValue("Visual Studio 2002")]
        VisualStudio2002 = 2002,
        /// <summary>
        /// Visual Studio 2003 = 7.1
        /// </summary>
        [StringValue("Visual Studio 2003")]
        VisualStudio2003 = 2003,
        /// <summary>
        /// Visual Studio 2005 = 8.0
        /// </summary>
        [StringValue("Visual Studio 2005")]
        VisualStudio2005 = 2005,
        /// <summary>
        /// Visual Studio 2008 = 9.0
        /// </summary>
        [StringValue("Visual Studio 2008")]
        VisualStudio2008 = 2008,
        /// <summary>
        /// Visual Studio 2010 = 10.0
        /// </summary>
        [StringValue("Visual Studio 2010")]
        VisualStudio2010 = 2010,
    }
}

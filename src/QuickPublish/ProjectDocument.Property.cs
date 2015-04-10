using System;
using System.Collections.Generic;
using System.Text;
using QuickPublish.CsProject;
using System.Web;
using System.IO;

namespace QuickPublish
{
    partial class ProjectDocument
    {
        public PropertyCollection PropertyItems
        {
            get { return properties; }
        }

        public ItemGroup ItemGroupCompiles
        {
            get { return items.Groups["Compile"]; }
        }
        public ItemGroup ItemGroupBootstrapperPackages
        {
            get { return items.Groups["BootstrapperPackage"]; }
        }
    }
}

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

        public ItemGroup ItemGroupReferences
        {
            get { return items.Groups["Reference"]; }
        }
        public ItemGroup ItemGroupBootstrapperPackages
        {
            get { return items.Groups["BootstrapperPackage"]; }
        }
        public ItemGroup ItemGroupCompiles
        {
            get { return items.Groups["Compile"]; }
        }
        public ItemGroup ItemGroupEmbeddedResources
        {
            get { return items.Groups["EmbeddedResource"]; }
        }
        public ItemGroup ItemGroupProjectReferences
        {
            get { return items.Groups["ProjectReference"]; }
        }
        public ItemGroup ItemGroupNones
        {
            get { return items.Groups["None"]; }
        }
        public ItemGroup ItemGroupContents
        {
            get { return items.Groups["Content"]; }
        }
        public ItemGroup ItemGroupPublishFiles
        {
            get { return items.Groups["PublishFile"]; }
        }
    }
}

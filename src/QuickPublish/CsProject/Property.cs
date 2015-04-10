using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPublish.CsProject
{
    public class Property
    {
        public string Key { get; private set; }
        public string Condition { get; set; }
        public readonly string DefaultValue = "";
        public string Value { get; set; }
        public Property(string key, string condition, string defaultValue, string value)
        {
            this.Key = key;
            this.Condition = condition;
            this.DefaultValue = defaultValue;
            this.Value = value;
        }
    }
}

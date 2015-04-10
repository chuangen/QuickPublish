using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish.CsProject
{
    public class PropertyGroup
    {
        private Dictionary<string, Property> items;
        public Dictionary<string, Property> Items
        {
            get { return items; }
        }
        public PropertyGroup()
            : this("")
        { }
        public readonly string Condition;
        public PropertyGroup(string condition)
        {
            this.Condition = condition;

            this.items = new Dictionary<string, Property>(StringComparer.OrdinalIgnoreCase);
        }
        public void Add(string key, string defaultValue)
        {
            this.Add(key, "", defaultValue, defaultValue);
        }
        public void Add(string key, string defaultValue, string value)
        {
            this.Add(key, "", defaultValue, value);
        }
        public void Add(string key, string condition, string defaultValue, string value)
        {
            Property property = new Property(key, condition, defaultValue, value);
            if (items.ContainsKey(key))
                items[key] = property;
            else
                items.Add(key, property);
        }

        public Property this[string key]
        {
            get
            {
                if (items.ContainsKey(key))
                    return items[key];

                return null;
            }
            set
            {
                if (items.ContainsKey(key))
                    items[key] = value;
                else
                    items.Add(key, value);
            }
        }
        public bool ContainsKey(string key)
        {
            return items.ContainsKey(key);
        }
        public string getProperty(string key)
        {
            if (items.ContainsKey(key))
                return items[key].Value;

            return null;
        }
        public void setProperty(string key, string value)
        {
            if (items.ContainsKey(key))
                items[key].Value = value;
            else
                items.Add(key, new Property(key, "", "", value));
        }
        public void setProperty(string key, string condition, string value)
        {
            if (items.ContainsKey(key))
                items[key].Value = value;
            else
                items.Add(key, new Property(key, condition, "", value));
        }

        public void Remove(string key)
        {
            items.Remove(key);
        }

        public string PublishUrl
        {
            get { return getProperty("PublishUrl"); }
            set { setProperty("PublishUrl", value); }
        }
    }
}

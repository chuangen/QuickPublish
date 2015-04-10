using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickPublish.CsProject
{
    public class ItemGroup<T> : IEnumerable<T> where T : NodeBase
    {
        private SortedDictionary<string, T> dic;
        /// <summary>
        /// 节点的限定名。
        /// </summary>
        public string Name { get; private set; }
        public ItemGroup()
            : this("")
        { }
        public ItemGroup(string name)
        {
            this.Name = name;
            dic = new SortedDictionary<string, T>(StringComparer.OrdinalIgnoreCase);
        }
        public T this[string key]
        {
            get { return dic[key]; }
            set { dic[key] = value; }
        }
        public string[] GetKeys()
        {
            string[] array = new string[dic.Count];
            dic.Keys.CopyTo(array, 0);

            return array;
        }
        public int Count
        {
            get { return dic.Count; }
        }
        public void Clear()
        {
            dic.Clear();
        }
        public bool Remove(string key)
        {
            return dic.Remove(key);
        }
        public bool ContainsKey(string key)
        {
            return dic.ContainsKey(key);
        }
        public void Add(T item)
        {
            if (dic.ContainsKey(item.Key))
                dic[item.Key] = item;
            else
                dic.Add(item.Key, item);
        }

        #region IEnumerable<T> 成员

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return dic.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return dic.Values.GetEnumerator();
        }

        #endregion
    }
}

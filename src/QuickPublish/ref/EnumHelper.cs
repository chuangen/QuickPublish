using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace QuickPublish
{
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the names.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns></returns>
        public static string[] GetNames(Type enumType)
        {
            if(enumType == null)
                throw new ArgumentNullException("enumType", "enumType 为 null。");
            if(!enumType.IsEnum)
                throw new ArgumentException("enumType", "enumType 参数不是 System.Enum。");

            List<string> names = new List<string>();
            foreach(FieldInfo field in enumType.GetFields())
            {
                if(field.FieldType != enumType)
                    continue;

                // Get the stringvalue attributes
                StringValueAttribute[] attribs = field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                string name = attribs.Length > 0 ? attribs[0].StringValue : field.Name;//返回StringValue 或 字段名称。
                if(!string.IsNullOrEmpty(name))
                    names.Add(name);
            }
            return names.ToArray();
        }
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetName(Type enumType, object value)
        {
            // Get fieldinfo for this type
            FieldInfo field = enumType.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : field.Name;
        }
        /// <summary>
        /// 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。
        /// </summary>
        /// <param name="enumType">枚举的 System.Type。</param>
        /// <param name="value">包含要转换的值或名称的字符串。</param>
        /// <returns>enumType 类型的对象，其值由 value 表示。</returns>
        public static object Parse(Type enumType, string value)
        {
            return Parse(enumType, value, true);
        }
        /// <summary>
        /// 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。一个参数指定该操作是否区分大小写。
        /// </summary>
        /// <param name="enumType">枚举的 System.Type。</param>
        /// <param name="value">包含要转换的值或名称的字符串。</param>
        /// <param name="ignoreCase">如果为 true，则忽略大小写；否则考虑大小写。</param>
        /// <returns>enumType 类型的对象，其值由 value 表示。</returns>
        public static object Parse(Type enumType, string value, bool ignoreCase)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType", "enumType 为 null。");
            if (!enumType.IsEnum)
                throw new ArgumentException("enumType", "enumType 参数不是 System.Enum。");

            object result = null;
            foreach (FieldInfo field in enumType.GetFields())
            {
                // Get the stringvalue attributes
                StringValueAttribute[] attribs = field.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

                string name = attribs.Length > 0 ? attribs[0].StringValue : field.Name;//返回StringValue 或 字段名称。
                if (string.Equals(name, value, ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture))
                    result = field.GetRawConstantValue();
            }
            return result;
        }
    }
}

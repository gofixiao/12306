using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockUtility
{

    /// <summary>
    /// 枚举成员描述特性类
    /// </summary>
    public class EnumDescriptionAttribute : Attribute
    {
        public string Description { get; private set; }
        public EnumDescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }

    public static class EnumExtension
    {
        /// <summary>
        /// 从枚举成员的EnumDescriptionAttribute特性性获取成员说明
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum source)
        {
            var query = (EnumDescriptionAttribute[])source.GetType().GetField(source.ToString()).GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (query.Length > 0)
                return query[0].Description;
            return "";
        }


        
    }
}

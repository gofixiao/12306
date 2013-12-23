using System;
using System.Text.RegularExpressions;
using System.Text;

namespace RockUtility
{
    public static class StringExtension
    {
        #region 检查字符串是否可转换为数字
        public static bool IsNum(this string source)
        {
            if (source.IsNullOrEmpty())
                return false;
            return new Regex("^[0-9]+$").IsMatch(source);
        }
        #endregion

        #region 检查字符串是否为空
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
        #endregion

        #region 将字符串转换成数字
        public static int ToInt(this string source)
        {
            return int.Parse(source);
        }
        #endregion

        #region 字符串截取
        public static string CutStr(this string source, int Length)
        {
            return CutStr(source, Length, true);
        }

        public static string CutStr(this string source, int Length, bool withellipsis)
        {
            if (source.IsNullOrEmpty())
                return "";
            if (source.Length > Length)
            {
                if (withellipsis)
                    return source.Substring(0, Length) + "...";
                else
                    return source.Substring(0, Length);
            }
            return source;
        }
        #endregion

        #region 替换单引号
        /// <summary>
        /// 用于在拼接SQL语句时，将单引号替换为两个单引号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FilterSpecial(this string source)
        {
            return source.Trim().Replace("'", "''");
        }
        #endregion

        #region 返回红色字体HTML以"<font color=\"red\"></font>"标签包裹
        /// <summary>
        /// 返回红色字体HTML以"<font color=\"red\"></font>"标签包裹
        /// </summary>
        ///<param name="text">要显示的文本</param>
        /// <returns></returns>
        public static string RedFont(this string source)
        {
            return "<font color=\"red\">" + source + "</font>";
        }
        #endregion


        /// <summary>
        /// 根据字符串返回对应枚举类型
        /// </summary>
        /// <typeparam name="T">对应枚举类型</typeparam>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static T GetEnumByValue<T>(this string source)
        {
            if (typeof(T).BaseType == typeof(Enum))
            {
                foreach (T value in Enum.GetValues(typeof(T)))
                {
                    if (source == value.ToString())
                    {
                        return value;
                    }
                }
            }
            else
            {
                throw new ArgumentException("T必须为枚举类型");
            }

            return default(T);
        }

    }
}

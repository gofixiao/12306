using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace RockUtility
{
    public static class PageExtension
    {
        #region 检查指定键名的QueryString是否能被转换成数字
        /// <summary>
        /// 检查指定键名的QueryString是否能被转换成数字
        /// </summary>
        /// <param name="key">QueryString键名</param>
        /// <returns></returns>
        public static bool IsQueryStringInt(this Page page, string key)
        {
            return page.Request.QueryString[key].IsNum();
        }
        #endregion

        #region 将指定键名的QueryString转换成数字并返回
        /// <summary>
        /// 将指定键名的QueryString转换成数字并返回
        /// </summary>
        /// <param name="key">QueryString键名</param>
        /// <returns></returns>
        public static int GetQueryStringAsInt(this Page page, string key)
        {
            return page.Request.QueryString[key].ToInt();
        }
        #endregion

       

    }
}

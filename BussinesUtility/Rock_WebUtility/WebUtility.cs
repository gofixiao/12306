using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Data;
using System.Linq;

namespace RockUtility
{
    public class WebUtility
    {

        //默认构造函数
        public WebUtility()
        {

        }

        #region 将给定Request.Form集合赋值到给定Model
        /// <summary>
        /// 将给定Request.Form集合赋值到给定Model的对应属性上
        /// 应用于将页面中控件的值赋值到Model的对应属性上
        /// 规则，Form元素键名必须以 "*_*"规则进行命名，也就是控件的ID名称
        /// 如"Txt_News","DDL_Title"
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Model"></param>
        public static void SetModelByCollection(NameValueCollection Form, object Model)
        {
            PropertyInfo[] Arr = Model.GetType().GetProperties();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (string item in Form.Keys)
            {
                if (item.IndexOf('_') > 0 && !Form[item].IsNullOrEmpty())
                {
                    dic.Add(item.Substring(item.IndexOf('_') + 1), Form[item]);
                }
            }
            foreach (PropertyInfo item in Arr)
            {
                if (dic.ContainsKey(item.Name))
                {
                    item.SetValue(Model, Convert.ChangeType(dic[item.Name], Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType), null);
                }
            }
        }
        #endregion

        #region 将给定Model的属性赋值到页面对应控件上
        /// <summary>
        /// 将给定Model的属性赋值到页面对应控件上
        /// 规则，控件ID必须以 "****_****"规则进行命名
        /// 暂只支持TextBox，Label,Literal,DropDownList的赋值
        /// 如"Txt_News","DDL_Title"  
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Model"></param>
        public static void SetControlByModel(Page page, object Model)
        {
            PropertyInfo[] Arr = Model.GetType().GetProperties();
            Dictionary<string, object> keyValueDic = new Dictionary<string, object>();
            foreach (PropertyInfo item in Arr)
            {
                keyValueDic.Add(item.Name, item.GetValue(Model, null));
            }
            foreach (Control item in GetControlList(page))
            {
                if (item.ID == null || item.ID.IndexOf('_') < 0)
                {
                    continue;
                }
                string KeyName = item.ID.Substring(item.ID.IndexOf('_') + 1);
                if (keyValueDic.ContainsKey(KeyName) && keyValueDic[KeyName] != null)
                {
                    switch (item.GetType().FullName)
                    {
                        case "System.Web.UI.WebControls.Literal":
                            ((Literal)item).Text = keyValueDic[KeyName].ToString();
                            break;
                        case "System.Web.UI.WebControls.Label":
                            ((Label)item).Text = keyValueDic[KeyName].ToString();
                            break;
                        case "System.Web.UI.WebControls.TextBox":
                            ((TextBox)item).Text = keyValueDic[KeyName].ToString();
                            break;
                        case "System.Web.UI.WebControls.DropDownList":
                            ((DropDownList)item).SelectedValue = keyValueDic[KeyName].ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取Page对象控件集合，有模板页时仍可获取
        /// </summary>
        public static IEnumerable<Control> GetControlList(Page page)
        {
            List<Control> list = new List<Control>();
            //有模板页的情况
            if (page.Master != null)
            {
                foreach (Control control in page.Controls[0].Controls)
                {
                    if (control is HtmlForm)
                    {
                        foreach (Control con in control.Controls)
                        {
                            if (con is ContentPlaceHolder)
                            {
                                list.AddRange(con.Controls.Cast<Control>());
                            }
                        }
                    }
                }
                return list;
            }
            return page.Form.Controls.Cast<Control>();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Linq;
using System.Web;

namespace RockUtility
{
    /// <summary>
    /// AJAXHandlerBase 的摘要说明
    /// </summary>
    public abstract class AJAXHandlerBase : IHttpHandler
    {

        public bool IsReusable { get { return false; } }

        #region 属性
        protected string UnKonwError = "网络超时，请刷新再试！";

        #endregion

        /// <summary>
        /// HttpResponse对象
        /// </summary>
        protected HttpResponse Response;
        /// <summary>
        /// HttpRequest对象
        /// </summary>
        protected HttpRequest Request;
        /// <summary>
        /// 请求需调用方法的对应标识
        /// </summary>
        protected string action = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.Cache.SetLastModified(DateTime.Now);
            context.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));


            Response = context.Response;
            Request = context.Request;

            context.Response.Clear();

            AJAXHandler();
        }

        /// <summary>
        /// 处理AJAX请求方法
        /// </summary>
        /// <param name="action">请求动作标识</param>
        public abstract void AJAXHandler();

        #region 调用Response.Write输出结果
        /// <summary>
        /// 调用Response.Write输出结果
        /// </summary>
        /// <param name="result"></param>
        public void Write(object result)
        {
            Response.Write(result);
        }
        #endregion

        #region 输出成功提示，格式为JSON形式
        /// <summary>
        /// 输出成功提示，格式为JSON形式
        /// (例：{msg:"提示语",flag:1})
        /// </summary>
        /// <param name="Msg"></param>
        public void WriteSuccess(object Msg)
        {
            WriteJsonResult(Msg, 0);
        }

        /// <summary>
        /// 输出成功提示，格式为JSON形式
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="flag">输出结果标识</param>
        private void WriteJsonResult(object Msg, int flag)
        {
            string json = "{\"msg\":\"" + Msg.ToString() + "\",\"flag\":\"" + flag.ToString() + "\"}";
            Response.Write(json);
        }
        #endregion

        #region 输出错误提示，格式为JSON形式
        /// <summary>
        /// 输出成功提示，格式为JSON形式
        /// (例：{msg:"提示语",flag:0})
        /// </summary>
        /// <param name="Msg"></param>
        public void WriteError(object Msg)
        {
            WriteJsonResult(Msg, 1);
        }
        #endregion

        #region 输出Jquery跨域形式数据结果
        public void WriteAsCrossDomain(string callBack, string data)
        {
            Write(callBack + "(" + data + ");");
        }
        #endregion


        #region 将给定Request集合赋值到给定Model
        /// <summary>
        /// 将给定Request.Form集合赋值到给定Model的对应属性上
        /// </summary>
        /// <param name="Form"></param>
        /// <param name="Model"></param>
        public static void SetModelByCollection(NameValueCollection Form, object Model)
        {
            var propertyArr = Model.GetType().GetProperties();
            foreach (PropertyInfo item in propertyArr)
            {
                if (Form[item.Name] != null)
                {
                    item.SetValue(Model, Convert.ChangeType(Form[item.Name], Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType), null);
                }
            }
        }
        #endregion


    }
}
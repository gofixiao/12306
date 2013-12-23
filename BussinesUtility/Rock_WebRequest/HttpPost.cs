using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace RockUtility
{
    public class HttpPost : ZDRequestBase
    {
        #region 属性
        /// <summary>
        /// POST请求数据
        /// </summary>
        public virtual string PostData
        {
            get;
            set;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">请求URL</param>
        /// <param name="postdata">post请求参数</param>
        public HttpPost(string uri, string postdata)
            : base(uri)
        {
            this.PostData = postdata;
            base.Method = HttpMethod.Post;
            base.ContentType = "application/x-www-form-urlencoded";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">请求URL</param>
        /// <param name="postdata">post请求参数</param>
        public HttpPost(string uri, Dictionary<string, string> postdatadic)
            : base(uri)
        {

            string postdata = string.Empty;
            List<string> strs = new List<string>();
            foreach (string item in postdatadic.Keys)
            {
                strs.Add(string.Format("{0}={1}", item, postdatadic[item]));
            }
            postdata = string.Join("&", strs);

            this.PostData = postdata;
            base.Method = HttpMethod.Post;
            base.ContentType = "application/x-www-form-urlencoded";
        }
        #endregion

        /// <summary>
        /// 为POST请求添加Body参数信息
        /// </summary>
        /// <param name="reqStream"></param>
        protected override void WriteBody()
        {
            this.req.Headers.Add(HttpRequestHeader.ContentEncoding, "utf-8");
            if (!string.IsNullOrEmpty(PostData))
            {
                var dataBytes = Encoding.UTF8.GetBytes(PostData);
                using (var reqStream = base.req.GetRequestStream())
                {
                    reqStream.Write(dataBytes, 0, dataBytes.Length);
                }
            }
        }

    }
}

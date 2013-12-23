using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace RockUtility
{
    public class ZDRequestBase : IDisposable
    {
        #region 属性
        private string method = HttpMethod.Get;
        private string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:24.0) Gecko/20100101 Firefox/24.0";

        /// <summary>
        /// 请求Uri
        /// </summary>
        public virtual string Uri { get; set; }

        /// <summary>
        /// 请求方法类型
        /// </summary>
        public string Method { get { return method; } set { method = value; } }

        /// <summary>
        /// 请求的Content-typeHttp头
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 请求的UserAgent
        /// </summary>
        public string UserAgent
        {
            get
            {
                return DefaultUserAgent;
            }
            set
            {
                DefaultUserAgent = value;
            }
        }

        /// <summary>
        /// 当前HttpRequest请求对象
        /// </summary>
        public HttpWebRequest req { get; set; }


        /// <summary>
        /// 当前HttpRequest请求返回的WebResponse
        /// </summary>
        public WebResponse response { get; set; }

        #endregion

        #region 构造函数
        public ZDRequestBase(string uri)
        {
            this.Uri = uri;
            req = System.Net.WebRequest.Create(Uri) as HttpWebRequest;
            req.CookieContainer = new CookieContainer();
        }
        #endregion

        /// <summary>
        /// 发送请求 返回此次请求的相应 即WebResponse对象
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse SendRequest()
        {
            return this.SendRequest(null, 0, null, null);
        }

        /// <summary>
        /// 发送请求 返回此次请求的相应 即WebResponse对象
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse SendRequest(string proxyHost, int proxyPort, string proxyUserID, string proxyPassword)
        {
            if (string.IsNullOrEmpty(this.Uri))
            {
                throw new Exception("无效的Uri，请检查！");
            }

            if (string.IsNullOrEmpty(req.UserAgent))
            {
                req.UserAgent = this.UserAgent;
            }            


            //设置https请求时  SSL权限
            if (new Uri(this.Uri).Scheme == "https")
            {
                ServicePointManager.CertificatePolicy = new CustomCertificatePolicy();
            }


            //设置代理
            if (!string.IsNullOrEmpty(proxyHost))
            {
                WebProxy proxy = new WebProxy(proxyHost, proxyPort)
                {
                    BypassProxyOnLocal = true
                };

                NetworkCredential cred = null;
                if (!string.IsNullOrEmpty(proxyUserID) && !string.IsNullOrEmpty(proxyPassword))
                {
                    cred = new NetworkCredential(proxyUserID, proxyPassword);
                    proxy.Credentials = cred;
                    req.Credentials = cred;
                }
                else
                {
                    proxy.Credentials = CredentialCache.DefaultCredentials;
                    req.Credentials = CredentialCache.DefaultCredentials;
                }

                req.Proxy = proxy;
            }
            else
            {
                req.Proxy = WebRequest.DefaultWebProxy;
                req.Credentials = System.Net.CredentialCache.DefaultCredentials; ;
                req.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            }

            if (!string.IsNullOrEmpty(this.ContentType))
            {
                req.ContentType = this.ContentType;
            }

            if (!string.IsNullOrEmpty(this.Method))
            {
                req.Method = this.Method;
            }

            if (req.Method == HttpMethod.Post)
            {
                WriteBody();
            }

            try
            {
                response = req.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response as HttpWebResponse;
        }

        /// <summary>
        /// 发送请求 返回当前请求输出流的字符串形式
        /// </summary>
        /// <returns>当前请求输出流的字符串形式</returns>
        public virtual string SendReqReturnResponseString()
        {
            response = SendRequest();
            if (response == null)
                return "";
            return ReadResponse(response);
        }

        /// <summary>
        /// 发送请求 返回当前请求的输出流
        /// </summary>
        /// <returns>当前请求的输出流</returns>
        public virtual Stream SendReqReturnResponseStream()
        {
            response = SendRequest();
            return response.GetResponseStream();
        }

        /// <summary>
        /// 为POST请求添加Body参数信息
        /// </summary>
        /// <param name="reqStream"></param>
        protected virtual void WriteBody()
        {
            // Nothing to do. 
        }

        /// <summary>
        /// 为请求添加HttpHeader
        /// </summary>
        /// <param name="headcoll"></param>
        protected virtual void AddRequestHeaders(WebHeaderCollection headcoll)
        {
            req.Headers.Add(headcoll);
        }

        /// <summary>
        /// 获取请求的返回流
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        protected string ReadResponse(WebResponse res)
        {
            string response = string.Empty;
            var stream = res.GetResponseStream();
            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
            {
                response = reader.ReadToEnd();
            }
            res.Close();
            return response;
        }

        #region IDisposeable接口实现
        public void Dispose()
        {
            this.response.Close();
        }
        #endregion
    }
}

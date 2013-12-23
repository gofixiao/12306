using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RockUtility
{
    public class OAuth_AuthorizationCode : IOAuth
    {
        #region AuthorizationCode授权方式必须参数
        public virtual string AppId { get; internal set; }
        public virtual string APPSecret { get; internal set; }
        public virtual string AuthorizeBaseUrl { get; internal set; }
        public virtual string AccessTokenRequestUrl { get; internal set; }
        public virtual string CallBackUrl { get; internal set; }
        #endregion


        #region 构造函数
        public OAuth_AuthorizationCode(string appid, string appsecret, string callbackurl, string authorzeurl, string accesstokenurl)
        {
            this.AppId = appid;
            this.APPSecret = appsecret;
            this.CallBackUrl = callbackurl;
            this.AuthorizeBaseUrl = authorzeurl;
            this.AccessTokenRequestUrl = accesstokenurl;
        }
        #endregion


        /// <summary>
        /// 获取AuthorizationCode授权地址
        /// </summary>
        /// <param name="dic">QueryString键值对</param>
        /// <returns></returns>
        public virtual string GetAuthorizeUrl(Dictionary<string, string> dic)
        {
            UriBuilder uri = new UriBuilder(AuthorizeBaseUrl);
            uri.Query = Common.BuildQueryString(dic);
            return uri.ToString();
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns>返回请求结果JSON字符串，AccessToken需自行根据不同应用进行提取</returns>
        public virtual string GetAccessToken(Dictionary<string, string> dic, HttpMethod.RequestMethod method)
        {
            UriBuilder uri = new UriBuilder(AccessTokenRequestUrl);
            string queryString = Common.BuildQueryString(dic);
            uri.Query = queryString;
            string result = string.Empty;

            if (method == HttpMethod.RequestMethod.GET) //GET请求
            {
                HttpGet get = new HttpGet(uri.ToString());
                result = get.SendReqReturnResponseString();
            }
            else if (method == HttpMethod.RequestMethod.POST) //POST请求
            {
                HttpPost post = new HttpPost(AccessTokenRequestUrl, queryString);
                result = post.SendReqReturnResponseString();
            }
            return result;
        }

    }
}

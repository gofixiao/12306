
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockUtility
{
    public class HttpGet : ZDRequestBase
    {
        /// <summary>
        /// GET请求QueryString参数集合
        /// </summary>
        private ParamList _paralist = new ParamList();

        private string _Uri = string.Empty;

        public ParamList paralist
        {
            get { return _paralist; }
            set { _paralist = value; }
        }

        public HttpGet(string uri)
            : base(uri)
        {
            base.Method = HttpMethod.Get;
        }

        /// <summary>
        /// 发送请求 返回当前请求输出流的字符串形式
        /// </summary>
        /// <returns>当前请求输出流的字符串形式</returns>
        public override string SendReqReturnResponseString()
        {
            if (this.paralist.Count > 0)
            {
                base.Uri = RebuildUri(base.Uri);
            }
            return base.SendReqReturnResponseString();
        }

        /// <summary>
        /// 为URL添加QueryString参数
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        private string RebuildUri(string URL)
        {
            Uri uri = new Uri(URL);
            StringBuilder query = new StringBuilder();
            if (string.IsNullOrEmpty(uri.Query) && paralist.Count > 0)
            {
                query.Append("?");
                paralist.ForEach(data => query.AppendFormat("{0}={1}&", data.key, data.value));
            }
            else if (paralist.Count > 0)
                paralist.ForEach(data => query.AppendFormat("&{0}={1}", data.key, data.value));

            if (query.Length > 0)
                query.Length = query.Length - 1;

            return URL + query.ToString();
        }
    }

    #region  GET请求QueryString参数相关类
    public class ParamList : List<RequestPara>
    {
        public ParamList()
        { }

        public ParamList(IEnumerable<RequestPara> items)
        {
            base.AddRange(items);
        }

        public void Add(string name, string value)
        {
            base.Add(new RequestPara(name, value));
        }
    }

    public class RequestPara
    {
        public RequestPara(string name, string value)
        {
            this.key = name;
            this.value = value;
        }

        public string key { get; set; }
        public string value { get; set; }
    }
    #endregion


}

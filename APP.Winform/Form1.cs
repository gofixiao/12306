using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.Web;
using RockUtility;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace APP.Winform
{



    public partial class Form1 : Form
    {
        CookieCollection reqCookies = null;
        public Form1()
        {
            InitializeComponent();
            this.webBrowser1.Navigate("E:\\MyTestDemo\\Carl_Template\\APP.Winform\\EncryptJS.htm");

            GenerateValidateCode();
        }


        /// <summary>
        /// 刷新验证码
        /// </summary>
        public void GenerateValidateCode()
        {
            //获取验证码和cookie
            HttpGet get = new HttpGet("https://dynamic.12306.cn/otsweb/passCodeNewAction.do?module=login&rand=sjrand");
            var imgName = Guid.NewGuid();
            var response = get.SendRequest();
            reqCookies = response.Cookies;
            string imgPath = Common.GetImageFromStream(response, Common.GetVirtualPath("\\ValidateCode\\" + imgName));

            this.pictureBox1.ImageLocation = imgPath;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            string validateCode = this.textBox1.Text;
            string urlPrefix = "https://dynamic.12306.cn";

            //获取JS文件
            HttpGet get1 = new HttpGet("https://dynamic.12306.cn/otsweb/loginAction.do?method=init");
            get1.req.CookieContainer.Add(reqCookies);
            string html = get1.SendReqReturnResponseString();
            string jsURL = html.Substring(html.IndexOf("/otsweb/dynamicJsAction.do?jsversion="));
            jsURL = urlPrefix + jsURL.Substring(0, jsURL.IndexOf("method=loginJs") + "method=loginJs".Length);

            //获取JS文件中的 Key
            HttpGet get2 = new HttpGet(jsURL);
            get2.req.CookieContainer.Add(reqCookies);
            string jsContent = get2.SendReqReturnResponseString();
            string dynamicKey = jsContent.Substring(jsContent.IndexOf("function gc()"));
            int beginIndex = dynamicKey.IndexOf("key='") + "key='".Length;
            dynamicKey = dynamicKey.Substring(beginIndex, dynamicKey.IndexOf("';") - beginIndex);

            //调用加密JS方法  得到加密的value
            string encryptValue = this.webBrowser1.Document.InvokeScript("finalEncrypt", new object[] { dynamicKey, "1111" }).ToString();


            //得到loginRand，refundfLag，
            HttpGet get3 = new HttpGet("https://dynamic.12306.cn/otsweb/loginAction.do?method=loginAysnSuggest");
            get3.req.CookieContainer.Add(reqCookies);
            string loginParam = get3.SendReqReturnResponseString();
            string loginRand = loginParam.Substring(loginParam.IndexOf(":\"") + 2);
            loginRand = loginRand.Substring(0, loginRand.IndexOf("\","));
            string tempStr = "randError\":\"";
            string randError = loginParam.Substring(loginParam.IndexOf(tempStr) + tempStr.Length, 1);


            Dictionary<string, string> loginDic = new Dictionary<string, string>();
            loginDic.Add("loginRand", loginRand);
            loginDic.Add("refundLogin", "N");
            loginDic.Add("refundFlag", randError);
            loginDic.Add("isClick", "");
            loginDic.Add("form_tk", "null");
            loginDic.Add("loginUser.user_name", this.textBox2.Text);
            loginDic.Add("nameErrorFocus", "");
            loginDic.Add("user.password", this.textBox3.Text);
            loginDic.Add("passwordErrorFocus", "");
            loginDic.Add("randCode", validateCode);
            loginDic.Add("randErrorFocus", "");
            loginDic.Add(HttpUtility.UrlEncode(dynamicKey), HttpUtility.UrlEncode(encryptValue));
            loginDic.Add("myversion", "undefined");


            string postdata = string.Empty;
            List<string> strs = new List<string>();
            foreach (string item in loginDic.Keys)
            {
                strs.Add(string.Format("{0}={1}", item, loginDic[item]));
            }
            postdata = string.Join("&", strs);
            HttpPost post = new HttpPost("https://dynamic.12306.cn/otsweb/loginAction.do?method=login", postdata);
            post.req.Referer = "https://dynamic.12306.cn/otsweb/loginAction.do?method=init";
            post.req.CookieContainer.Add(reqCookies);

            string loginResult = post.SendReqReturnResponseString();

            string[] error = new string[] { "密码输入错误,您还有3次机会!", "请输入正确的验证码！", "登录名不存在!" };

            if (!loginResult.Contains("欢迎您登录中国铁路客户服务中心网站"))
            {
                for (int i = 0; i < error.Length; i++)
                {
                    if (loginResult.Contains(error[i]))
                    {
                        GenerateValidateCode();
                        MessageBox.Show(error[i]);
                    }
                }
            }
            else
            {
                order order = new Winform.order();
                order.reqCookies = reqCookies;
                order.dynamicKey = dynamicKey;
                order.dynamicValue = encryptValue;
                order.Show();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            GenerateValidateCode();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            GenerateValidateCode();
        }

    }
}

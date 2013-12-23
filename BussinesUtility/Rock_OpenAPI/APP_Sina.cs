using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RockUtility
{
    public class APP_Sina : OAuth_AuthorizationCode
    {


        public APP_Sina(string appid, string appsecret, string callbackurl, string authorzeurl, string accesstokenurl)
            : base(appid, appsecret, callbackurl, authorzeurl, accesstokenurl)
        {
        }

        public override string GetAccessToken(Dictionary<string, string> dic, HttpMethod.RequestMethod method)
        {
            string json = base.GetAccessToken(dic, method);
            return "";
            //JObject obj = JObject.Parse(json);
            //return obj["access_token"].ToString();
        }
    }
}

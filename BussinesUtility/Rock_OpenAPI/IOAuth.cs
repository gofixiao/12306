using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RockUtility
{
    public interface IOAuth
    {
        #region OAuth必须属性
        string AppId { get; }
        string APPSecret { get; }
        #endregion

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        string GetAccessToken(Dictionary<string, string> dic, HttpMethod.RequestMethod method);
    }
}

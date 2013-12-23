using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RockUtility
{
    public class JsonOperator
    {
        /// <summary>
        /// 将给定对象转换为json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertToJson(object obj)
        {
            string json = string.Empty;
            if (obj == null)
                return json;
            return JsonConvert.SerializeObject(obj);
        }


        /// <summary>
        /// 将给定字符串转换为指定类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ConvertToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}

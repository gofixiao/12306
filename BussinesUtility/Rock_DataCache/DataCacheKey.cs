using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockUtility
{
    public class DataCacheKey
    {
        //默认缓存过期时间，单位为分钟
        public static readonly int CacheExpiredTime = 15;
        //实体类属性缓存键名
        public static readonly string ModelPropertyInfo = "ModelPropertyInfo";
        //实体类属性键名集合缓存键名
        public static readonly string ModelPropertyNames = "ModelPropertyNames";
        //实体类属性类型缓存键名
        public static readonly string ModelPropertyType = "ModelPropertyType";
        //实体类映射数据库表相关信息TabAttribute
        public static readonly string ModelTabAttribute = "ModelTabAttribute";
    }
}

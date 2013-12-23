using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Caching;
using System.Collections;
namespace RockUtility
{
    /// <summary>
    /// 缓存相关的操作类
    /// </summary>
    public class RockUtility
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        private static readonly System.Web.Caching.Cache objCache = HttpRuntime.Cache;

        /// <summary>/// 设置从不过期以缓存依赖的方式缓存数据/// </summary>
        /// <param name="CacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        public static void InsertCache(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep)
        {
            if (objObject != null)
            {
                objCache.Insert(CacheKey,
                    objObject,
                    dep,
                    System.Web.Caching.Cache.NoAbsoluteExpiration,//从不过期  
                    System.Web.Caching.Cache.NoSlidingExpiration,//禁用可调过期   
                    System.Web.Caching.CacheItemPriority.Default,
                    null);
            }
        }
        /// <summary>/// 按秒钟缓存数据// </summary>
        /// <param name="CacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        /// <param name="Minutes">缓存时间(秒)</param>
        public static void InsertCacheAddSeconds(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep, int Minutes)
        {
            if (objObject != null)
            {
                objCache.Insert(CacheKey, objObject, dep, DateTime.Now.AddSeconds(Minutes), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }
        /// <summary>/// 按分钟缓存数据// </summary>
        /// <param name="CacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        /// <param name="Minutes">缓存时间(分钟)</param>
        public static void InsertCacheMinutes(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep, int Minutes)
        {
            if (objObject != null)
            {
                objCache.Insert(CacheKey, objObject, dep, DateTime.Now.AddMinutes(Minutes), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }

        /// <summary>/// 按小时缓存数据// </summary>
        /// <param name="CacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        /// <param name="Hours">缓存小时)</param>
        public static void InsertCacheHours(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep, int Hours)
        {
            if (objObject != null)
            {
                objCache.Insert(CacheKey, objObject, dep, DateTime.Now.AddHours(Hours), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }
        /// <summary>/// 按天缓存数据// </summary>
        /// <param name="CacheKey">索引键值</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="cacheDepen">依赖对象</param>
        /// <param name="Days">缓存天数</param>
        public static void InsertCacheDays(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep, int Days)
        {
            if (objObject != null)
            {
                objCache.Insert(CacheKey, objObject, dep, DateTime.Now.AddDays(Days), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }

        /// <summary>
        /// 设定滑动过期缓存
        /// (当缓存最后一次使用后经过所指定的时间后则清除缓存)   /// 
        /// </summary>
        /// <param name="CacheKey">缓存键名</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="dep">缓存依赖项</param>
        /// <param name="dep">滑动过期时间，单位为分钟</param>
        public static void InsertCacheExpired(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep, int Minutes)
        {
            objCache.Insert(CacheKey, objObject, dep, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(Minutes));
        }

        /// <summary>
        /// 设定滑动过期缓存(过期时间为15分钟)
        /// (当缓存最后一次使用后经过所指定的时间后则清除缓存)
        /// </summary>
        /// <param name="CacheKey">缓存键名</param>
        /// <param name="objObject">缓存对象</param>
        /// <param name="dep">缓存依赖项</param>
        public static void InsertCacheExpired(string CacheKey, object objObject, System.Web.Caching.CacheDependency dep)
        {
            objCache.Insert(CacheKey, objObject, dep, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(DataCacheKey.CacheExpiredTime));
        }


        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = objCache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                objCache.Remove(enumerator.Key.ToString());

            }
        }
        /// <summary>
        /// 获取指定的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {

            return objCache[key];

        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {

            objCache.Remove(key);

        }

        /// <summary>
        /// 按指定模式移除
        /// </summary>
        /// <param name="pattern"></param>
        public static void RemoveByPattern(string pattern)
        {

            IDictionaryEnumerator enumerator = objCache.GetEnumerator();

            Regex regex1 = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

            while (enumerator.MoveNext())
            {

                if (regex1.IsMatch(enumerator.Key.ToString()))
                {

                    objCache.Remove(enumerator.Key.ToString());

                }

            }

        }

    }
}

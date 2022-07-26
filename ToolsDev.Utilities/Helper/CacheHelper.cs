using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsDev.Utilities.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class CacheHelper
    {
        private const string CacheSetting = "Cache.Duration";
        private static System.Web.Caching.Cache _cache { get { return System.Web.HttpContext.Current.Cache; } }
        private static int _cacheDurationMinutes { get; set; }

        static CacheHelper()
        {
            _cacheDurationMinutes = GetCacheDuration();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            _cache.Insert(key, value,
                          null,
                          System.Web.Caching.Cache.NoAbsoluteExpiration,
                          GetSlidingExpiration(_cacheDurationMinutes),
                          System.Web.Caching.CacheItemPriority.Default,
                          null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            T value = default(T);

            if (Exists(key))
                value = (T)_cache.Get(key);

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return _cache.Get(key) != null;
        }

        private static TimeSpan GetSlidingExpiration(int duration)
        {
            return new TimeSpan(0, duration, 0);
        }

        private static int GetCacheDuration()
        {
            //default
            int duration = 10;
            var setting = ConfigurationManager.AppSettings[CacheSetting];

            if (!string.IsNullOrEmpty(setting))
                int.TryParse(setting, out duration);

            return duration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveKeysLike(string key)
        {
            List<string> cacheKeys = new List<string>();
            IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                if (cacheEnum.Key.ToString().Contains(key))
                    cacheKeys.Add(cacheEnum.Key.ToString());
            }
            foreach (var cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RemoveAll()
        {
            List<string> cacheKeys = new List<string>();
            IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cacheKeys.Add(cacheEnum.Key.ToString());
            }
            foreach (var cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey);
            }
        }
    }
}

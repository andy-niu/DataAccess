using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Memory.Cache
{
    /// <summary>
    /// 管理Web中的缓存
    /// </summary>
    /// <remarks>
    /// <c>WebCache</c>主要针对System.Web.Caching.Cache进行封装并提供更友好的方法来实现缓存的操作
    /// </remarks>
    public class CacheManage
    {

        /// <summary>
        /// WebCache私有构造函数
        /// </summary>
        private CacheManage() { }

        /// <summary>
        /// WebCache静态构造函数
        /// </summary>
        static CacheManage()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
                _cache = context.Cache;
            else
                _cache = HttpRuntime.Cache;
        }

        //>> Based on Factor = 5 default value

        /// <summary>
        /// 天因数
        /// </summary>
        public static readonly int DayFactor = 17280;

        /// <summary>
        /// 小时因数
        /// </summary>
        public static readonly int HourFactor = 720;

        /// <summary>
        /// 分钟因数
        /// </summary>
        public static readonly int MinuteFactor = 12;

        /// <summary>
        /// 秒因数
        /// </summary>
        /// <note type="implementnotes">不用直接使用SecondFactor，建议使用SecondFactorCalculate(int)</note>
        private static readonly double SecondFactor = 0.2;

        private static int Factor = 5;
        private static readonly System.Web.Caching.Cache _cache;

        /// <summary>
        /// 重设基础因数(整体调整缓存时间)
        /// </summary>
        /// <param name="cacheFactor">新设的基础因数</param>
        public static void ReSetFactor(int cacheFactor)
        {
            Factor = cacheFactor;
        }

        ///// <summary>
        ///// 是否存在cacheKey的缓存项
        ///// </summary>
        //public static bool Contains(string cacheKey)
        //{
        //    IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
        //    while (cacheEnum.MoveNext())
        //    {
        //        if (cacheEnum.Key.ToString().Equals(cacheKey, StringComparison.CurrentCultureIgnoreCase))
        //            return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// 从缓存中清除所有缓存项
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
            List<string> keys = new List<string>();
            while (cacheEnum.MoveNext())
            {
                keys.Add(cacheEnum.Key.ToString());
            }

            foreach (string key in keys)
            {
                _cache.Remove(key);
            }
        }

        /// <summary>
        /// 移除指定的缓存项
        /// </summary>
        /// <param name="key">要移除的缓存项标识</param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        #region Add

        /// <summary>
        /// 加入缓存项
        /// </summary>
        /// <param name="key">缓存项标识</param>
        /// <param name="obj">缓存项</param>
        /// <param name="secondsBase">缓存秒基数(最终秒数的计算为Factor * secondsBase) </param>
        public static void Add(string key, object obj, int secondsBase)
        {
            Add(key, obj, null, secondsBase);
        }

        /// <summary>
        /// 加入缓存项
        /// </summary>
        /// <param name="key">缓存项标识</param>
        /// <param name="obj">缓存项</param>
        /// <param name="secondsBase">缓存秒基数(最终秒数的计算为Factor * secondsBase) </param>
        /// <param name="priority">缓存优先级<see cref="System.Web.Caching.CacheItemPriority"/></param>
        public static void Add(string key, object obj, int secondsBase, CacheItemPriority priority)
        {
            Add(key, obj, null, secondsBase, priority);
        }

        /// <summary>
        /// 加入缓存项
        /// </summary>
        /// <param name="key">缓存项标识</param>
        /// <param name="obj">缓存项</param>
        /// <param name="dep">缓存依赖<see cref="System.Web.Caching.CacheDependency"/></param>
        /// <param name="seconds">缓存的秒数</param>
        public static void Add(string key, object obj, CacheDependency dep, int seconds)
        {
            Add(key, obj, dep, seconds, CacheItemPriority.Normal);
        }

        /// <summary>
        /// 加入缓存项
        /// </summary>
        /// <param name="key">缓存项标识</param>
        /// <param name="obj">缓存项</param>
        /// <param name="dep">缓存依赖<see cref="System.Web.Caching.CacheDependency"/></param>
        /// <param name="secondsBase">缓存秒基数(最终秒数的计算为Factor * secondsBase) </param>
        /// <param name="priority">缓存优先级<see cref="System.Web.Caching.CacheItemPriority"/></param>
        public static void Add(string key, object obj, CacheDependency dep, int secondsBase, CacheItemPriority priority)
        {
            if (obj != null)
                _cache.Add(key, obj, dep, DateTime.Now.AddSeconds(Factor * secondsBase), TimeSpan.Zero, priority, null);
        }

        /// <overloads>永久加入缓存项（缓存的失效依赖时间以外的因素）</overloads>
        /// <summary>
        /// 永久加入缓存项（缓存的失效依赖时间以外的因素）
        /// </summary>
        /// <param name="key">缓存项标识</param>
        /// <param name="obj">缓存项</param>
        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        /// <summary>
        /// 永久加入缓存项（缓存的失效依赖时间以外的因素）
        /// </summary>
        /// <param name="key">缓存项标识</param>
        /// <param name="obj">缓存项</param>
        /// <param name="dep">缓存依赖</param>
        public static void Max(string key, object obj, System.Web.Caching.CacheDependency dep)
        {
            if (obj != null)
                _cache.Add(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
        }

        #endregion

        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="key">缓存项标识</param>
        /// <returns>缓存项</returns>
        public static object Get(string key)
        {
            return _cache[key];
        }


        /// <summary>
        /// Return int of seconds * SecondFactor
        /// </summary>
        /// <example>
        /// 用于设置秒基数，以下示例用于设置缓存时间为10秒
        /// <code>
        /// WebCache.Add(cacheKey, obj, WebCache.SecondFactorCalculate(10));
        /// </code>
        /// </example>
        public static int SecondFactorCalculate(int seconds)
        {
            // Add method below takes integer seconds, so we have to round any fractional values
            return Convert.ToInt32(Math.Round((double)seconds * SecondFactor));
        }

        #region 属性

        /// <summary>
        /// 缓存对象
        /// </summary>
        public static System.Web.Caching.Cache Cache
        {
            get { return _cache; }
        }

        #endregion
    }
}

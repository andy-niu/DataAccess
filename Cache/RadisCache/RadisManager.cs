using System;
using ServiceStack.Redis;
using System.Collections.Generic;

namespace Cache.RadisCache
{
    public class RadisManager
    {
        private static PooledRedisClientManager _prcm;

        #region  字段

        /// <summary>
        ///读写服务器地址集合
        /// </summary>
        private static string[] _writeServerList;
        /// <summary>
        ///只读服务器地址集合
        /// </summary>
        private static string[] _readServerList;
        /// <summary>
        /// 读写服务器名称
        /// </summary>
        private static string _redisWriteServerlName = String.Empty;
        /// <summary>
        /// 读写服务器名称
        /// </summary>
        private static string _redisReadServerlName = String.Empty;
        /// <summary>
        /// Redis“写”链接池链接数
        /// </summary>
        private static int _redisPoolMaxWrite;
        /// <summary>
        /// Redis“读”链接池链接数
        /// </summary>
        private static int _redisPoolMaxRead;
        #endregion

        #region 属性

        public static long DefaultDb = 1;

        /// <summary>
        /// 读写服务器地址集合
        /// </summary>
        public static string[] RedisWriteServers
        {
            get {
                return _writeServerList ?? (_writeServerList = System.Configuration.ConfigurationManager.AppSettings["RedisWriteServers"].Split(','));
            }
            set { _writeServerList = value; }
        }

        /// <summary>
        /// 读写服务器名称
        /// </summary>
        public static string RedisWriteSentinelName
        {
            get
            {
                if (String.IsNullOrEmpty(_redisWriteServerlName))
                {
                    //哨兵服务器名称:van1master
                    _redisWriteServerlName = System.Configuration.ConfigurationManager.AppSettings["RedisWriteSentinelName"];
                }
                return _redisWriteServerlName;
            }
            set { _redisWriteServerlName = value; }
        }
        /// <summary>
        /// 只读服务器地址集合
        /// </summary>
        public static string[] RedisReadServers
        {
            get
            {
                return _readServerList ?? (_readServerList = System.Configuration.ConfigurationManager.AppSettings["RedisReadServers"].Split(','));
            }
            set { _readServerList = value; }
        }

        /// <summary>
        /// 读写服务器名称
        /// </summary>
        public static string RedisReadSentinelName
        {
            get
            {
                if (String.IsNullOrEmpty(_redisReadServerlName))
                {
                    //哨兵服务器名称:van1master
                    _redisReadServerlName = System.Configuration.ConfigurationManager.AppSettings["RedisReadSentinelName"];
                }
                return _redisReadServerlName;
            }
            set { _redisReadServerlName = value; }
        }

        /// <summary>
        /// Redis“写”链接池链接数
        /// </summary>
        public static int RedisPoolMaxWrite
        {
            get
            {
                if (_redisPoolMaxWrite <= 0)
                {
                    //-Redis连接池读写最大连接数
                    _redisPoolMaxWrite = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RedisPoolMaxWrite"]);
                }
                return _redisPoolMaxWrite;
            }
            set { _redisPoolMaxWrite = value; }
        }
       
        /// <summary>
        /// Redis“读”链接池链接数
        /// </summary>
        public static int RedisPoolMaxRead
        {
            get
            {
                if (_redisPoolMaxRead <= 0)
                {
                    //Redis连接池只读最大连接数
                    _redisPoolMaxRead = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RedisPoolMaxRead"]);
                }
                return _redisPoolMaxRead;
            }
            set { _redisPoolMaxRead = value; }
        }

        #endregion

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象  
        /// </summary>
         static RadisManager()
         {
             CreateManager();
         }
        /// <summary>
         /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            _prcm = new PooledRedisClientManager(RedisWriteServers, RedisReadServers, new RedisClientManagerConfig
            {
                AutoStart = true,
                MaxReadPoolSize = RedisPoolMaxRead,
                MaxWritePoolSize = RedisPoolMaxWrite,
                DefaultDb = DefaultDb,
            });
        }

        /// <summary>
        /// 重新初始化Redis池
        /// </summary>
        public static void ReInitializeRedisPool()
        {
            CreateManager();

        }


        /// <summary>
        /// 客户端缓存操作对象  
        /// </summary>
        /// <returns></returns>
        public static IRedisClient GetClient()
        {
            if(_prcm==null)
                CreateManager();
            return _prcm.GetClient();
        }
        /// <summary>
        /// 客户端缓存只读操作对象  
        /// </summary>
        /// <returns></returns>
        public static IRedisClient GetReadOnlyClient()
        {
            if (_prcm == null)
                CreateManager();
            return _prcm.GetReadOnlyClient();
        }
    }
}

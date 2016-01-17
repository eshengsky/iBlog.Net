using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iBlog.Utility.Redis
{
    public class RedisManager
    {
        /// <summary>  
        /// redis配置文件信息  
        /// </summary>  
        private static readonly RedisConfigInfo RedisConfigInfo = RedisConfigInfo.GetConfig();

        private static PooledRedisClientManager _prcm;

        /// <summary>  
        /// 静态构造方法，初始化链接池管理对象  
        /// </summary>  
        static RedisManager()
        {
            CreateManager();
        }

        /// <summary>  
        /// 创建链接池管理对象  
        /// </summary>  
        private static void CreateManager()
        {
            IEnumerable<string> writeServerList = SplitString(RedisConfigInfo.WriteServerList, ",");
            IEnumerable<string> readServerList = SplitString(RedisConfigInfo.ReadServerList, ",");

            _prcm = new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = RedisConfigInfo.MaxWritePoolSize,
                                 MaxReadPoolSize = RedisConfigInfo.MaxReadPoolSize,
                                 AutoStart = RedisConfigInfo.AutoStart,
                             });
        }

        private static IEnumerable<string> SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>  
        /// 客户端缓存操作对象  
        /// </summary>  
        public static IRedisClient GetClient()
        {
            if (_prcm == null)
                CreateManager();

            return _prcm.GetClient();
        }

        #region 操作Item

        /// <summary> 
        /// 添加单个对象
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="key"></param> 
        /// <param name="t"></param>
        /// <param name="ts"></param>
        /// <returns></returns> 
        public static bool SetItem<T>(string key, T t, TimeSpan ts = new TimeSpan())
        {
            try
            {
                using (IRedisClient redis = GetClient())
                {
                    if (ts != new TimeSpan())
                    {
                        return redis.Set(key, t, ts);
                    }
                    return redis.Set(key, t);
                }
            }
            catch
            {
            }
            return false;
        }

        /// <summary> 
        /// 获取单个对象
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        public static T GetItem<T>(string key) where T : class
        {
            using (IRedisClient redis = GetClient())
            {
                return redis.Get<T>(key);
            }
        }

        /// <summary> 
        /// 移除单个对象
        /// </summary> 
        /// <param name="key"></param> 
        public static bool RemoveItem(string key)
        {
            using (IRedisClient redis = GetClient())
            {
                return redis.Remove(key);
            }
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Memcached.ClientLibrary;
using System.Web;

namespace Valeo.Common
{
    /// <summary>
    /// Memcache通用类
    /// </summary>
    public abstract class MemberHelper
    {

        private static readonly MemcachedClient mc = new MemcachedClient();

        //static MemberHelper() //静态构造函数只会执行一次
        //{
        //    string[] serverlist = { "127.0.0.1:11211" };//Memcache服务器IP地址和端口号，这里用本地机子进行测试

        //    //初始化池
        //    SockIOPool pool = SockIOPool.GetInstance();
        //    pool.SetServers(serverlist);

        //    pool.InitConnections = 3;
        //    pool.MinConnections = 3;
        //    pool.MaxConnections = 5;

        //    pool.SocketConnectTimeout = 1000;
        //    pool.SocketTimeout = 3000;

        //    pool.MaintenanceSleep = 30;
        //    pool.Failover = true;

        //    pool.Nagle = false;
        //    pool.Initialize();

        //    //获得客户端实例（可以认为是telnet或socket客户端）
        //    MemcachedClient mc = new MemcachedClient();
        //    mc.EnableCompression = false;
        //}
         
        #region 创建Memcache服务
        /// <summary>
        /// 创建Memcache服务
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">Socket连接池名称</param>
        /// <returns>Memcache客户端代理类</returns>
        private static MemcachedClient CreateServer(ArrayList serverlist, string poolName)
        {
            //初始化memcache服务器池
            SockIOPool pool = SockIOPool.GetInstance(poolName);
            //设置Memcache池连接点服务器端。
            pool.SetServers(serverlist);
            pool.Initialize();
            //其他参数根据需要进行配置 

            //创建了一个Memcache客户端的代理类。
            MemcachedClient mc = new MemcachedClient();
            mc.PoolName = poolName;
            mc.EnableCompression = false;//是否压缩 

            return mc;
        }
        #endregion

        #region 缓存是否存在
        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool CacheIsExists(ArrayList serverlist, string poolName, string key)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);

            if (mc.KeyExists(key))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 添加缓存

        #region 添加缓存(键不存在则添加,键存在则不能添加)
        /// <summary>
        /// 添加缓存(键不存在则添加,键存在则不能添加)
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">过期分钟数</param>
        /// <returns></returns>
        public static bool AddCache(ArrayList serverlist, string poolName, string key, string value, int minutes)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            return mc.Add(key, value, DateTime.Now.AddMinutes(minutes));
        }
        #endregion

        #region 添加缓存(键不存在则添加,键存在则覆盖)
         
        /// <summary>
        /// 向Memcache存储数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            mc.Set(key, value);
        }

        public static void Set(string key, object value, DateTime time)
        {
            mc.Set(key, value, time);//绝对过期时间
        }

        /// <summary>
        /// 添加缓存(键不存在则添加,键存在则覆盖)
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">过期分钟数</param>
        /// <returns></returns>
        public static bool SetCache(ArrayList serverlist, string poolName, string key, string value, int minutes)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            return mc.Set(key, value, DateTime.Now.AddMinutes(minutes));
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        public static void SetCache(string cacheKey, object objObject, TimeSpan timeout)
        {
            var objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, System.Web.Caching.Cache.NoAbsoluteExpiration, timeout, System.Web.Caching.CacheItemPriority.High, null);
        }

        #endregion

        #endregion

        #region 替换缓存

        #region 替换缓存(键存在的才能替换,不存在则不替换)
        /// <summary>
        /// 替换缓存(键存在的才能替换,不存在则不替换)
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">过期分钟数</param>
        /// <returns></returns>
        public static bool ReplaceCache(ArrayList serverlist, string poolName, string key, string value, int minutes)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            return mc.Replace(key, value, DateTime.Now.AddMinutes(minutes));
        }
        #endregion

        #endregion

        #region 获取缓存

        #region 获取单个键对应的缓存

        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static object GetCache(string cacheKey)
        {
            var objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 获取单个键对应的缓存
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <param name="key">键</param> 
        /// <returns></returns>
        public static object GetCache(ArrayList serverlist, string poolName, string key)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            if (mc.KeyExists(key))
            {
                return mc.Get(key);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取Memcache中的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        { 
            return mc.Get(key);
        }

        #endregion

        #region 获取键数组对应的值
        /// <summary>
        /// 获取键数组对应的值
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <param name="keys">键列表</param>
        /// <returns>Hashtable键值对</returns>
        public static Hashtable GetCacheHt(ArrayList serverlist, string poolName, string[] keys)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            return mc.GetMultiple(keys);
        }
        #endregion

        #region 获取键数组对应的值
        /// <summary>
        /// 获取键数组对应的值
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <param name="keys">键列表</param>
        /// <returns>值的数组(不包含键)</returns>
        public static object[] GetCacheList(ArrayList serverlist, string poolName, string[] keys)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            object[] list = mc.GetMultipleArray(keys);
            ArrayList returnList = new ArrayList();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != null)
                {
                    returnList.Add(list[i]);
                }
            }
            return returnList.ToArray();
        }
        #endregion

        #endregion

        #region 删除缓存
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool DelCache(ArrayList serverlist, string poolName, string key)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            return mc.Delete(key);
        }
        #endregion

        #region 清空所有缓存
        /// <summary>
        /// 清空所有缓存
        /// </summary>
        /// <param name="serverlist">IP端口列表</param>
        /// <param name="poolName">连接池名称</param>
        /// <returns></returns>
        public static bool FlushAll(ArrayList serverlist, string poolName)
        {
            MemcachedClient mc = CreateServer(serverlist, poolName);
            return mc.FlushAll();
        }
        #endregion
    }
}
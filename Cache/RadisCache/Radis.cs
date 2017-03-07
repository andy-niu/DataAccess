using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cache.RadisCache;
using ServiceStack.Caching;

namespace Cache.RadisCache
{
    public class Radis : ICacheClient
    {
        //传入数据库索引构造
        public Radis(long DefaultDb)
        {
            RadisManager.DefaultDb = DefaultDb;
        }
        //默认构造
        public Radis()
        {
        }

        /// <summary>
        /// 添加Radis
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool Add<T>(string key, T value)
        {
            return RadisManager.GetClient().Add<T>(key, value);
        }
        /// <summary>
        /// 添加Radis（过期时间）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns></returns>
        public bool Add<T>(string key, T value, DateTime expiresAt)
        {
            return RadisManager.GetClient().Add<T>(key, value,expiresAt);
        }
        /// <summary>
        /// 添加Radis（时间间隔）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">时间间隔</param>
        /// <returns></returns>
        public bool Add<T>(string key, T value, TimeSpan expiresIn)
        {
            return RadisManager.GetClient().Add<T>(key, value, expiresIn);
        }
        /// <summary>
        /// 数字递减存储键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="amount">整数</param>
        /// <returns></returns>
        public long Decrement(string key, uint amount)
        {
            return RadisManager.GetClient().Decrement(key, amount);
        }
        /// <summary>
        /// 清空radis所有数据库中的所有键
        /// </summary>
        public void FlushAll()
        {
            RadisManager.GetClient().FlushAll();
        }
        /// <summary>
        ///  清空当前radis数据库中的所有键
        /// </summary>
        public void FlushDb()
        {
            RadisManager.GetClient().FlushDb();
        }
        /// <summary>
        /// 获取radis值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return RadisManager.GetClient().Get<T>(key);
        }
        /// <summary>
        /// 根据传入的多个key获取多条记录的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="keys">多个键</param>
        /// <returns></returns>
        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            return RadisManager.GetClient().GetAll<T>(keys);
        }
        public long Increment(string key, uint amount)
        {
            return RadisManager.GetClient().Increment(key, amount);
        }
        /// <summary>
        /// 更具键删除键值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return RadisManager.GetClient().Remove(key);
        }
        /// <summary>
        /// 删除多个键值
        /// </summary>
        /// <param name="keys">键</param>
        public void RemoveAll(IEnumerable<string> keys)
        {
            RadisManager.GetClient().RemoveAll(keys);
        }
        /// <summary>
        /// 根据传入的key覆盖一条记录的值，当key不存在不会添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool Replace<T>(string key, T value)
        {
            return RadisManager.GetClient().Replace<T>(key, value);
        }
        /// <summary>
        /// 根据传入的key覆盖一条记录的值，当key不存在不会添加 到达该时间点销毁
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间</param>
        /// <returns></returns>
        public bool Replace<T>(string key, T value, DateTime expiresAt)
        {
            return RadisManager.GetClient().Replace<T>(key, value, expiresAt);
        }
        /// <summary>
        /// 根据传入的key覆盖一条记录的值，当key不存在不会添加 经过该时间段销毁
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">时间间隔</param>
        /// <returns></returns>
        public bool Replace<T>(string key, T value, TimeSpan expiresIn)
        {
            return RadisManager.GetClient().Replace<T>(key, value, expiresIn);
        }
        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value)
        {
            return RadisManager.GetClient().Set<T>(key, value);
        }
        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresAt">过期时间点</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, DateTime expiresAt)
        {
            return RadisManager.GetClient().Set<T>(key, value, expiresAt);
        }
        /// <summary>
        /// 根据传入的key修改一条记录的值，当key不存在则添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresIn">时间间隔</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan expiresIn)
        {
            return RadisManager.GetClient().Set<T>(key, value, expiresIn);
        }
        /// <summary>
        /// 根据传入的多个key覆盖多条记录
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="values">值</param>
        public void SetAll<T>(IDictionary<string, T> values)
        {
            RadisManager.GetClient().SetAll<T>(values);
        }

        public void Dispose()
        {
            RadisManager.GetClient().Dispose();
        }
    }
}

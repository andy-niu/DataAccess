using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.RadisCache
{
    public  interface IRadis
    {
        bool Add<T>(string key, T value);
        bool Add<T>(string key, T value, DateTime expiresAt);
        bool Add<T>(string key, T value, TimeSpan expiresIn);
        long Decrement(string key, uint amount);
        void FlushAll();
        T Get<T>(string key);
        IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);
        long Increment(string key, uint amount);
        bool Remove(string key);
        void RemoveAll(IEnumerable<string> keys);
        bool Replace<T>(string key, T value);
        bool Replace<T>(string key, T value, DateTime expiresAt);
        bool Replace<T>(string key, T value, TimeSpan expiresIn);
        bool Set<T>(string key, T value);
        bool Set<T>(string key, T value, DateTime expiresAt);
        bool Set<T>(string key, T value, TimeSpan expiresIn);
        void SetAll<T>(IDictionary<string, T> values);
    }
}

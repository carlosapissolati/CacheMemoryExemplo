using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheMemory.Api.Helpers.Interfaces
{
    public interface ICacheManager
    {
        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        void Set(string key, dynamic data, TimeSpan? cacheTime = null);

        bool IsSet(string key);

        void Remove(string key);

        Task RemoveAsync(string key);
    }
}

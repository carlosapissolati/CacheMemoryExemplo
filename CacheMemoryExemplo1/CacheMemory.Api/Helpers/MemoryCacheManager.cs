using CacheMemory.Api.Helpers.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheMemory.Api.Helpers
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public T Get<T>(string key)
        {
            return (T)_memoryCache.Get(key);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        public void Set(string key, object data, TimeSpan? cacheTime = null)
        {

            if (data == null)
                return;

            if (IsSet(key))
                return;

            if (!cacheTime.HasValue || cacheTime.Value.Ticks == 0)
            {
                cacheTime = TimeSpan.FromMinutes(int.MaxValue);
            }

            var memoryCacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(cacheTime.Value);

            _memoryCache.Set(key, data, memoryCacheEntryOptions);
        }

        public bool IsSet(string key)
        {
            return _memoryCache.Get(key) != null;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.CompletedTask;
        }

    }
}

using CacheMemoryExemplo.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheMemoryExemplo.API.CacheManager
{
    public static class CacheManagerExtensions
    {
        public static async Task<T> ProcessarCacheAsync<T>(this ICacheManager cacheManager, Func<Task<T>> func, string key, string[] keys, TimeSpan? cacheTime)
        {
            return await GetAsync(cacheManager, string.Format(key, keys), cacheTime, func);
        }

        public static async Task<T> ProcessarCacheAsync<T>(this ICacheManager cacheManager, Func<Task<T>> func, string key, TimeSpan? cacheTime)
        {
            return await GetAsync(cacheManager, key, cacheTime, func);
        }

        private static async Task<T> GetAsync<T>(this ICacheManager cacheManager, string key, TimeSpan? cacheTime, Func<Task<T>> func)
        {
            var data = await cacheManager.GetAsync<T>(key);

            if (data != null)
                return data;

            var result = await func();

            if (result != null)
                cacheManager.Set(key, result, cacheTime);

            return result;
        }
    }
}

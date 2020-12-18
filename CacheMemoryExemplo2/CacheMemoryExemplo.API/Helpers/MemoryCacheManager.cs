using CacheMemoryExemplo.API.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheMemoryExemplo.API.Helpers
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly Settings _settings;

        public MemoryCacheManager(IOptions<Settings> options, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _settings = options.Value;
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
            if (!_settings.CacheHabilitado)
                return;

            if (data == null)
                return;

            if (IsSet(key))
                return;

            if (!cacheTime.HasValue || cacheTime.Value.Ticks == 0)
            {
                if (_settings.CacheTempoEmMinutos > 0)
                    cacheTime = TimeSpan.FromMinutes(_settings.CacheTempoEmMinutos.Value);
                else
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

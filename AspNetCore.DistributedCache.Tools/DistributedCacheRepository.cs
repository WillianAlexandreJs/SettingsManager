using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.DistributedCache.Tools
{
    public class DistributedCacheRepository : IDistributedCacheRepository
    {
        private readonly IDistributedCache _cache;
        private readonly List<TimeCache> _timesCache;

        public DistributedCacheRepository([FromServices] IDistributedCache cache, IOptions<List<TimeCache>> timesCache)
        {
            _cache = cache;
            _timesCache = timesCache.Value;
        }

        public T GetCache<T>(string cacheKey)
        {
            string dataCache = _cache.GetString(cacheKey);

            if (dataCache is null)
                return default;

            return JsonConvert.DeserializeObject<T>(dataCache);
        }


        public bool InsertCache<T>(string cacheKey, T dados)
        {
            DistributedCacheEntryOptions optionsCache = new DistributedCacheEntryOptions();
            optionsCache.SetAbsoluteExpiration(GetCacheExpiration(cacheKey));
            _cache.SetString(cacheKey, JsonConvert.SerializeObject(dados), optionsCache);

            return true;
        }

        public void UpdateStateCache(string cacheKey, ECacheStatus cacheStatus)
        {
            _cache.SetString($"{cacheKey}_Status", JsonConvert.SerializeObject(new CacheStatus { Date = DateTime.Now, Status = cacheStatus }));
        }

        public CacheStatus GetStateCache(string cacheKey)
        {
            string dataCache = _cache.GetString($"{cacheKey}_Status");

            if (dataCache is null)
                return default;

            return JsonConvert.DeserializeObject<CacheStatus>(dataCache);
        }

        private TimeSpan GetCacheExpiration(string cacheKey)
        {
            TimeCache timeCache = _timesCache.FirstOrDefault(x => x.CacheKey == cacheKey);

            if (timeCache is null)
                return TimeSpan.FromHours(1);

            return (Enum.Parse<Frequency>(timeCache.Frequency)) switch
            {
                Frequency.Second => TimeSpan.FromSeconds(timeCache.Interval),
                Frequency.Minute => TimeSpan.FromMinutes(timeCache.Interval),
                Frequency.Hour => TimeSpan.FromHours(timeCache.Interval),
                Frequency.Day => TimeSpan.FromDays(timeCache.Interval),
                _ => TimeSpan.FromHours(1),
            };
        }
    }
}

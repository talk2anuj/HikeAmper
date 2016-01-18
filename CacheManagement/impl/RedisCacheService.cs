using Common.Models.Hike;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Configuration;
using System.Linq;

namespace CacheManagement.impl
{
    public class RedisCacheService<T> : ICacheService
    {
        public string CacheName;
        public IDatabase Cache;

        public RedisCacheService(string cacheName)
        {
            CacheName = cacheName;
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings[cacheName].ConnectionString);
            Cache = connection.GetDatabase();
        }

        public bool PopulateDetails(string url, HikeSummary summary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            var detail = Cache.StringGet(hike);
            if (detail != RedisValue.Null)
            {
                summary.SetDetail(JsonConvert.DeserializeObject<T>(detail));
                return true;
            }
            return false;
        }

        public void AddDetails(HikeSummary summary)
        {
            string hike = CacheUtility.GetCacheKey(summary.Url);
            Cache.StringSet(hike, JsonConvert.SerializeObject(summary.MapDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }

        public void UpdateDetails(HikeSummary summary)
        {
            AddDetails(summary);
        }

        public void ClearCache()
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings[CacheName].ConnectionString);
            var endpoints = connection.GetEndPoints();
            var server = connection.GetServer(endpoints.First());
            var keys = server.Keys();
            foreach (var key in keys)
            {
                Cache.KeyDelete(key);
            }
        }
    }
}
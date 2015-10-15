using System;
using System.Configuration;
using System.Linq;
using CommonModels.Hike;
using HikeService.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheManagement.Services.impl
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

        public void AddDetails(string url, HikeSummary summary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            Cache.StringSet(hike, JsonConvert.SerializeObject(summary.MapDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
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
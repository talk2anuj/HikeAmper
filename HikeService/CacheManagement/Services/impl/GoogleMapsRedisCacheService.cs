using System;
using CommonModels.Hike;
using CommonModels.Map;
using HikeService.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheManagement.Services.impl
{
    public class GoogleMapsRedisCacheService: RedisCacheService
    {
        public override bool PopulateDetails(string url, HikeSummary hikeSummary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            var mapDetails = Cache.StringGet(hike);
            if (mapDetails != RedisValue.Null)
            {
                hikeSummary.MapDetails = JsonConvert.DeserializeObject<MapDetails>(mapDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string url, HikeSummary hikeSummary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.MapDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }

        public GoogleMapsRedisCacheService(string cacheName)
            : base(cacheName)
        {
        }
    }
}
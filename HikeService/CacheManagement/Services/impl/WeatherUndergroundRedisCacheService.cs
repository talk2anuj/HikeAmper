using System;
using CommonModels.Hike;
using CommonModels.Weather;
using HikeService.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheManagement.Services.impl
{
    public class WeatherUndergroundRedisCacheService: RedisCacheService
    {
        public override bool PopulateDetails(string url, HikeSummary hikeSummary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            var weatherDetails = Cache.StringGet(hike);
            if (weatherDetails != RedisValue.Null)
            {
                hikeSummary.WeatherDetails = JsonConvert.DeserializeObject<WeatherDetails[]>(weatherDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string url, HikeSummary hikeSummary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.WeatherDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }

        public WeatherUndergroundRedisCacheService(string cacheName)
            : base(cacheName)
        {
        }
    }
}
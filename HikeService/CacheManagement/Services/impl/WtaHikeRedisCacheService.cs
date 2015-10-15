using System;
using CommonModels.Hike;
using HikeService.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheManagement.Services.impl
{
    public class WtaHikeRedisCacheService: RedisCacheService
    {
        public override bool PopulateDetails(string url, HikeSummary hikeSummary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            var hikeDetails = Cache.StringGet(hike);
            if (hikeDetails != RedisValue.Null)
            {
                hikeSummary.HikeAndTripDetails.HikeDetails = JsonConvert.DeserializeObject<HikeDetails>(hikeDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string url, HikeSummary hikeSummary)
        {
            //-1 is to avoid overflow
            string hike = CacheUtility.GetCacheKey(url);
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.HikeAndTripDetails.HikeDetails), TimeSpan.FromMinutes(TimeSpan.MaxValue.TotalMinutes-1));
        }

        public WtaHikeRedisCacheService(string cacheName)
            : base(cacheName)
        {
        }
    }
}
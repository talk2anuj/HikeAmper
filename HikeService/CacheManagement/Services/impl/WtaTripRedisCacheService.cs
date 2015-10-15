using System;
using CommonModels.Hike;
using HikeService.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheManagement.Services.impl
{
    public class WtaTripRedisCacheService: RedisCacheService
    {
        public WtaTripRedisCacheService(string cacheName)
            : base(cacheName)
        {
        }

        public override bool PopulateDetails(string url, HikeSummary hikeSummary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            var tripDetails = Cache.StringGet(hike);
            if (tripDetails != RedisValue.Null)
            {
                hikeSummary.HikeAndTripDetails.TripDetails = JsonConvert.DeserializeObject<TripDetails>(tripDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string url, HikeSummary hikeSummary)
        {
            string hike = CacheUtility.GetCacheKey(url);
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.HikeAndTripDetails.TripDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }
    }
}
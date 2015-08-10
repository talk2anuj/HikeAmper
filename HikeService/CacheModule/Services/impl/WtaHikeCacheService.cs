using System;
using HikeService.HikesModule.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheModule.Services.impl
{
    public class WtaHikeCacheService: CacheService
    {
        public override bool PopulateDetails(string hike, HikeSummary hikeSummary)
        {
            var hikeDetails = Cache.StringGet(hike);
            if (hikeDetails != RedisValue.Null)
            {
                hikeSummary.HikeAndTripDetails.HikeDetails = JsonConvert.DeserializeObject<HikeDetails>(hikeDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string hike, HikeSummary hikeSummary)
        {
            //-1 is to avoid overflow
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.HikeAndTripDetails.HikeDetails), TimeSpan.FromMinutes(TimeSpan.MaxValue.TotalMinutes-1));
        }

        public WtaHikeCacheService(string cacheName)
            : base(cacheName)
        {
        }
    }
}
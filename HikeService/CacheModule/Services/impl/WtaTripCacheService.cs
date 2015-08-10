using System;
using HikeService.HikesModule.Models;
using HikeService.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheModule.Services.impl
{
    public class WtaTripCacheService: CacheService
    {
        public WtaTripCacheService(string cacheName)
            : base(cacheName)
        {
        }

        public override bool PopulateDetails(string hike, HikeSummary hikeSummary)
        {
            var tripDetails = Cache.StringGet(hike);
            if (tripDetails != RedisValue.Null)
            {
                hikeSummary.HikeAndTripDetails.TripDetails = JsonConvert.DeserializeObject<TripDetails>(tripDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string hike, HikeSummary hikeSummary)
        {
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.HikeAndTripDetails.TripDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }
    }
}
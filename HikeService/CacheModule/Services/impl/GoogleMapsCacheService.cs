using System;
using HikeService.HikesModule.Models;
using HikeService.MapsModule.Models;
using HikeService.Utilities;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheModule.Services.impl
{
    public class GoogleMapsCacheService: CacheService
    {
        public override bool PopulateDetails(string hike, HikeSummary hikeSummary)
        {
            var mapDetails = Cache.StringGet(hike);
            if (mapDetails != RedisValue.Null)
            {
                hikeSummary.MapDetails = JsonConvert.DeserializeObject<MapDetails>(mapDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string hike, HikeSummary hikeSummary)
        {
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.MapDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }

        public GoogleMapsCacheService(string cacheName)
            : base(cacheName)
        {
        }
    }
}
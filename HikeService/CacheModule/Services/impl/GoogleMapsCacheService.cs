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
            var mapDetails = cache.StringGet(hike);
            if (mapDetails != RedisValue.Null)
            {
                hikeSummary.MapDetails = JsonConvert.DeserializeObject<MapDetails>(mapDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string hike, HikeSummary hikeSummary)
        {
            cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.MapDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }

        public GoogleMapsCacheService(IDatabase cache) : base(cache)
        {
        }
    }
}
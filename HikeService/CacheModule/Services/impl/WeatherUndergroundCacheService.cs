﻿using System;
using HikeService.HikesModule.Models;
using HikeService.Utilities;
using HikeService.WeatherModule.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HikeService.CacheModule.Services.impl
{
    public class WeatherUndergroundCacheService: CacheService
    {
        public override bool PopulateDetails(string hike, HikeSummary hikeSummary)
        {
            var weatherDetails = Cache.StringGet(hike);
            if (weatherDetails != RedisValue.Null)
            {
                hikeSummary.WeatherDetails = JsonConvert.DeserializeObject<WeatherDetails[]>(weatherDetails);
                return true;
            }
            return false;
        }

        public override void AddDetails(string hike, HikeSummary hikeSummary)
        {
            Cache.StringSet(hike, JsonConvert.SerializeObject(hikeSummary.WeatherDetails), TimeSpan.FromMinutes(CacheUtility.GetMinutesToExpiry()));
        }

        public WeatherUndergroundCacheService(string cacheName)
            : base(cacheName)
        {
        }
    }
}
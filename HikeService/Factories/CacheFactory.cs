using HikeService.CacheManagement.Services;
using HikeService.CacheManagement.Services.impl;

namespace HikeService.Factories
{
    public class CacheFactory
    {
        private static WtaHikeCacheService _wtaHikeCacheService;
        private static GoogleMapsCacheService _googleMapsCacheService;
        private static WeatherUndergroundCacheService _weatherUndergroundCacheService;
        private static WtaTripCacheService _tripCacheService;

        static CacheFactory()
        {
            _weatherUndergroundCacheService = new WeatherUndergroundCacheService("weathercache");
            _wtaHikeCacheService = new WtaHikeCacheService("hikecache");
            _googleMapsCacheService = new GoogleMapsCacheService("mapcache");
            _tripCacheService = new WtaTripCacheService("tripcache");
        }

        public static CacheService GetHikeCacheService()
        {
            return _wtaHikeCacheService;
        }

        public static CacheService GetWeatherCacheService()
        {
            return _weatherUndergroundCacheService;
        }

        public static CacheService GetMapCacheService()
        {
            return _googleMapsCacheService;
        }

        public static CacheService GetTripCacheService()
        {
            return _tripCacheService;
        }
    }
}
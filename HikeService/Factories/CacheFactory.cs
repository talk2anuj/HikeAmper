using HikeService.CacheManagement;
using HikeService.CacheManagement.Services;
using HikeService.CacheManagement.Services.impl;

namespace HikeService.Factories
{
    public class CacheFactory
    {
        private static WtaHikeRedisCacheService _wtaHikeRedisCacheService;
        private static GoogleMapsRedisCacheService _googleMapsRedisCacheService;
        private static WeatherUndergroundRedisCacheService _weatherUndergroundRedisCacheService;
        private static WtaTripRedisCacheService _tripRedisCacheService;
        private static CacheStubService _cacheStubService;

        static CacheFactory()
        {
            _weatherUndergroundRedisCacheService = new WeatherUndergroundRedisCacheService("weathercache");
            _wtaHikeRedisCacheService = new WtaHikeRedisCacheService("hikecache");
            _googleMapsRedisCacheService = new GoogleMapsRedisCacheService("mapcache");
            _tripRedisCacheService = new WtaTripRedisCacheService("tripcache");
            _cacheStubService = new CacheStubService();
        }

        public static ICacheService GetHikeCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _wtaHikeRedisCacheService;
                case CacheType.AzureStorage:
                    return _cacheStubService;
                default:
                    return _cacheStubService;
            }
        }

        public static ICacheService GetWeatherCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _weatherUndergroundRedisCacheService;
                case CacheType.AzureStorage:
                    return _cacheStubService;
                default:
                    return _cacheStubService;
            }
        }

        public static ICacheService GetMapCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _googleMapsRedisCacheService;
                case CacheType.AzureStorage:
                    return _cacheStubService;
                default:
                    return _cacheStubService;
            }
        }

        public static ICacheService GetTripCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _tripRedisCacheService;
                case CacheType.AzureStorage:
                    return _cacheStubService;
                default:
                    return _cacheStubService;
            }
        }
    }
}
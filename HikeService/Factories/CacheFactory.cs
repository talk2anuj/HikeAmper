using CommonModels.Hike;
using CommonModels.Map;
using CommonModels.Weather;
using HikeService.CacheManagement;
using HikeService.CacheManagement.Services;
using HikeService.CacheManagement.Services.impl;

namespace HikeService.Factories
{
    public class CacheFactory
    {
        private static RedisCacheService<HikeDetails> _wtaHikeRedisRedisCacheService;
        private static RedisCacheService<MapDetails> _googleMapsRedisRedisCacheService;
        private static RedisCacheService<WeatherDetails[]> _weatherUndergroundRedisRedisCacheService;
        private static RedisCacheService<TripDetails> _tripRedisRedisCacheService;
        private static StubCacheService _stubCacheService;

        static CacheFactory()
        {
            _weatherUndergroundRedisRedisCacheService = new RedisCacheService<WeatherDetails[]>("weathercache");
            _wtaHikeRedisRedisCacheService = new RedisCacheService<HikeDetails>("hikecache");
            _googleMapsRedisRedisCacheService = new RedisCacheService<MapDetails>("mapcache");
            _tripRedisRedisCacheService = new RedisCacheService<TripDetails>("tripcache");
            _stubCacheService = new StubCacheService();
        }

        public static ICacheService GetHikeCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _wtaHikeRedisRedisCacheService;
                case CacheType.AzureStorage:
                    return _stubCacheService;
                default:
                    return _stubCacheService;
            }
        }

        public static ICacheService GetWeatherCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _weatherUndergroundRedisRedisCacheService;
                case CacheType.AzureStorage:
                    return _stubCacheService;
                default:
                    return _stubCacheService;
            }
        }

        public static ICacheService GetMapCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _googleMapsRedisRedisCacheService;
                case CacheType.AzureStorage:
                    return _stubCacheService;
                default:
                    return _stubCacheService;
            }
        }

        public static ICacheService GetTripCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _tripRedisRedisCacheService;
                case CacheType.AzureStorage:
                    return _stubCacheService;
                default:
                    return _stubCacheService;
            }
        }
    }
}
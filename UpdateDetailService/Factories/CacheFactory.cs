using CacheManagement;
using CacheManagement.impl;
using CommonModels;
using CommonModels.Hike;
using CommonModels.Map;
using CommonModels.Weather;

namespace UpdateDetailService.Factories
{
    public class CacheFactory
    {
        private static RedisCacheService<HikeDetails> _hikeRedisCacheService;
        private static RedisCacheService<MapDetails> _mapsRedisCacheService;
        private static RedisCacheService<WeatherDetails[]> _weatherRedisCacheService;
        private static RedisCacheService<TripDetails> _tripRedisCacheService;
        private static AzureStorageCacheService<HikeDetails> _hikeAzureStorageCacheService;
        private static AzureStorageCacheService<TripDetails> _tripAzureStorageCacheService;
        private static AzureStorageCacheService<WeatherDetails[]> _weatherAzureStorageCacheService;
        private static AzureStorageCacheService<MapDetails> _mapAzureStorageCacheService;
        private static StubCacheService _stubCacheService;

        static CacheFactory()
        {
            _weatherRedisCacheService = new RedisCacheService<WeatherDetails[]>("redisweathercache");
            _hikeRedisCacheService = new RedisCacheService<HikeDetails>("redishikecache");
            _mapsRedisCacheService = new RedisCacheService<MapDetails>("redismapcache");
            _tripRedisCacheService = new RedisCacheService<TripDetails>("redistripcache");
            _hikeAzureStorageCacheService = new AzureStorageCacheService<HikeDetails>(Constants.HikeServiceStorage, Constants.HikeDetailsTableName);
            _tripAzureStorageCacheService = new AzureStorageCacheService<TripDetails>(Constants.HikeServiceStorage, Constants.TripDetailsTableName);
            _weatherAzureStorageCacheService = new AzureStorageCacheService<WeatherDetails[]>(Constants.HikeServiceStorage, Constants.WeatherDetailsTableName);
            _mapAzureStorageCacheService = new AzureStorageCacheService<MapDetails>(Constants.HikeServiceStorage, Constants.MapDetailsTableName);
            _stubCacheService = new StubCacheService();
        }

        public static ICacheService GetHikeCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _hikeRedisCacheService;
                case CacheType.AzureStorage:
                    return _hikeAzureStorageCacheService;
                default:
                    return _stubCacheService;
            }
        }

        public static ICacheService GetTripCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _tripRedisCacheService;
                case CacheType.AzureStorage:
                    return _tripAzureStorageCacheService;
                default:
                    return _stubCacheService;
            }
        }

        public static ICacheService GetWeatherCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _weatherRedisCacheService;
                case CacheType.AzureStorage:
                    return _weatherAzureStorageCacheService;
                default:
                    return _stubCacheService;
            }
        }

        public static ICacheService GetMapCacheService(CacheType type)
        {
            switch (type)
            {
                case CacheType.Redis:
                    return _mapsRedisCacheService;
                case CacheType.AzureStorage:
                    return _mapAzureStorageCacheService;
                default:
                    return _stubCacheService;
            }
        }
    }
}
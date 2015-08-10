using System.Configuration;
using System.Net;
using HikeService.CacheModule.Services;
using HikeService.CacheModule.Services.impl;
using HikeService.HikesModule.Services;
using HikeService.HikesModule.Services.impl;
using HikeService.MapsModule.Services;
using HikeService.MapsModule.Services.impl;
using HikeService.StorageModule.Services;
using HikeService.StorageModule.Services.impl;
using HikeService.WeatherModule;
using HikeService.WeatherModule.impl;
using StackExchange.Redis;

namespace HikeService.Factories
{
    static class ServiceFactory
    {
        private static AzureDataStorageService _azureStorageService;
        private static WtaHikeDetailsService _wtaHikeDetailsService;
        private static WeatherUndergroundService _nationalWeatherDetailsService;
        private static GoogleMapsService _googleMapsService;
        private static WtaHikeCacheService _wtaHikeCacheService;
        private static GoogleMapsCacheService _googleMapsCacheService;
        private static WeatherUndergroundCacheService _weatherUndergroundCacheService;
        private static WtaTripCacheService _tripCacheService;

        static ServiceFactory()
         {
             _azureStorageService = new AzureDataStorageService();
             _wtaHikeDetailsService = new WtaHikeDetailsService();
             _nationalWeatherDetailsService = new WeatherUndergroundService();
            _googleMapsService = new GoogleMapsService();
            _weatherUndergroundCacheService = new WeatherUndergroundCacheService("weathercache");
            _wtaHikeCacheService = new WtaHikeCacheService("hikecache");
            _googleMapsCacheService = new GoogleMapsCacheService("mapcache");
            _tripCacheService = new WtaTripCacheService("tripcache");

         }

        public static DataStorageService GetStorageService()
        {
            return _azureStorageService;
        }

         public static HikeDetailsService GetHikeDetailsService()
         {
             return _wtaHikeDetailsService;
         }

        public static WeatherDetailsService GetWeatherDetailsService()
        {
            return _nationalWeatherDetailsService;
        }

        public static MapsService GetMapsService()
        {
            return _googleMapsService;
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
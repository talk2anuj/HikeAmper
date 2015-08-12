using HikeService.HikesModule.Services;
using HikeService.HikesModule.Services.impl;
using HikeService.MapsModule.Services;
using HikeService.MapsModule.Services.impl;
using HikeService.Storage.Services;
using HikeService.Storage.Services.impl;
using HikeService.WeatherModule;
using HikeService.WeatherModule.impl;

namespace HikeService.Factories
{
    static class ServiceFactory
    {
        private static AzureDataStorageService _azureStorageService;
        private static WtaHikeDetailsService _wtaHikeDetailsService;
        private static WeatherUndergroundService _nationalWeatherDetailsService;
        private static GoogleMapsService _googleMapsService;

        static ServiceFactory()
         {
             _azureStorageService = new AzureDataStorageService();
             _wtaHikeDetailsService = new WtaHikeDetailsService();
             _nationalWeatherDetailsService = new WeatherUndergroundService();
            _googleMapsService = new GoogleMapsService();
         }

        public static IDataStorageService GetStorageService()
        {
            return _azureStorageService;
        }

         public static IHikeDetailsService GetHikeDetailsService()
         {
             return _wtaHikeDetailsService;
         }

        public static IWeatherDetailsService GetWeatherDetailsService()
        {
            return _nationalWeatherDetailsService;
        }

        public static IMapsService GetMapsService()
        {
            return _googleMapsService;
        }
    }
}
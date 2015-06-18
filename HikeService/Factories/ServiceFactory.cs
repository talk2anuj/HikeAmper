using HikeService.HikesModule.Services.impl;
using HikeService.MapsModule.Services.impl;
using HikeService.StorageModule.Services;
using HikeService.StorageModule.Services.impl;
using HikeService.WeatherModule.impl;

namespace HikeService.Factories
{
    static class ServiceFactory
    {
//        private static FileDataStorageService _fileStorageService;
        private static AzureDataStorageService _azureStorageService;
        private static WtaHikeDetailsService _wtaHikeDetailsService;
        private static WeatherUndergroundService _nationalWeatherDetailsService;
        private static GoogleMapsService _googleMapsService;

        static ServiceFactory()
         {
//             _fileStorageService = new FileDataStorageService();
             _azureStorageService = new AzureDataStorageService();
             _wtaHikeDetailsService = new WtaHikeDetailsService();
             _nationalWeatherDetailsService = new WeatherUndergroundService();
            _googleMapsService = new GoogleMapsService();
         }
        
        public static DataStorageService GetStorageService()
        {
            return _azureStorageService;
        }

         public static WtaHikeDetailsService GetHikeDetailsService()
         {
             return _wtaHikeDetailsService;
         }

        public static WeatherUndergroundService GetWeatherDetailsService()
        {
            return _nationalWeatherDetailsService;
        }

        public static GoogleMapsService GetMapsService()
        {
            return _googleMapsService;
        }
    }
}
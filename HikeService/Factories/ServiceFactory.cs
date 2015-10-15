using CommonModels.Hike;
using CommonModels.Map;
using CommonModels.Weather;
using HikeService.Builders;
using HikeService.HikesModule.Services.impl;
using HikeService.MapsModule.Services.impl;
using HikeService.Storage.Services;
using HikeService.Storage.Services.impl;
using HikeService.WeatherModule.impl;

namespace HikeService.Factories
{
    static class ServiceFactory
    {
        private static AzureDataStorageService _azureStorageService;
        private static WtaHikeDetailService _wtaHikeDetailService;
        private static WtaTripDetailService _wtaTripDetailService;
        private static WeatherUndergroundService _nationalWeatherDetailService;
        private static GoogleMapsService _googleMapsService;

        static ServiceFactory()
         {
             _azureStorageService = new AzureDataStorageService();
             _wtaHikeDetailService = new WtaHikeDetailService();
            _wtaTripDetailService = new WtaTripDetailService();
             _nationalWeatherDetailService = new WeatherUndergroundService();
            _googleMapsService = new GoogleMapsService();
         }

        public static IDataStorageService GetStorageService()
        {
            return _azureStorageService;
        }

         public static IDetailService<HikeDetails> GetHikeDetailService()
         {
             return _wtaHikeDetailService;
         }

        public static IDetailService<TripDetails> GetTripDetailService()
        {
            return _wtaTripDetailService;
        }

        public static IDetailService<WeatherDetails[]> GetWeatherDetailService()
        {
            return _nationalWeatherDetailService;
        }

        public static IDetailService<MapDetails> GetMapService()
        {
            return _googleMapsService;
        }
    }
}
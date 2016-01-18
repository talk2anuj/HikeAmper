using Common.Models.Hike;
using Common.Models.Map;
using Common.Models.Weather;
using DetailServices;
using DetailServices.impl;

namespace HikeService.Factories
{
    internal static class ServiceFactory
    {
        private static WtaHikeDetailService _wtaHikeDetailService;
        private static WtaTripDetailService _wtaTripDetailService;
        private static WeatherUndergroundService _nationalWeatherDetailService;
        private static GoogleMapsService _googleMapsService;

        static ServiceFactory()
        {
            _wtaHikeDetailService = new WtaHikeDetailService();
            _wtaTripDetailService = new WtaTripDetailService();
            _nationalWeatherDetailService = new WeatherUndergroundService();
            _googleMapsService = new GoogleMapsService();
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
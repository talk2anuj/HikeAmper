using CacheManagement;
using Common.Models.Hike;
using Common.Models.Map;
using Common.Models.Weather;
using DetailServices.Builders;

namespace DetailServices.Factories
{
    public static class BuilderFactory
    {
        private static readonly SummaryBuilder SummaryBuilder;

        static BuilderFactory()
        {
            SummaryBuilder = new SummaryBuilder();
        }

        public static SummaryBuilder GetHikeSummaryBuilder()
        {
            //Extend later if required - Set the builders based on url as input and return appropriate HikeSummaryBuilder
            SummaryBuilder.HikeDetailBuilder = new DetailBuilder<HikeDetails>(ServiceFactory.GetHikeDetailService(), CacheFactory.GetHikeCacheService(CacheType.AzureStorage));
            SummaryBuilder.TripDetailBuilder = new DetailBuilder<TripDetails>(ServiceFactory.GetTripDetailService(), CacheFactory.GetTripCacheService(CacheType.AzureStorage));
            SummaryBuilder.WeatherDetailBuilder = new DetailBuilder<WeatherDetails[]>(ServiceFactory.GetWeatherDetailService(), CacheFactory.GetWeatherCacheService(CacheType.AzureStorage));
            SummaryBuilder.MapDetailBuilder = new DetailBuilder<MapDetails>(ServiceFactory.GetMapService(), CacheFactory.GetMapCacheService(CacheType.Stub));
            return SummaryBuilder;
        }
    }
}
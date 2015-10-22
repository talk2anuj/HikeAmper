using CacheManagement;
using CommonModels.Hike;
using CommonModels.Map;
using CommonModels.Weather;
using DetailServices.Builders;

namespace UpdateDetailService.Factories
{
	static class BuilderFactory
	{
	    private static readonly SummaryBuilder SummaryBuilder;

	    static BuilderFactory()
	    {
	        SummaryBuilder = new SummaryBuilder();
	    }
        public static SummaryBuilder GetHikeSummaryBuilder()
        {
            //Extend later if required - Set the builders based on url as input and return appropriate HikeSummaryBuilder
            SummaryBuilder.HikeDetailBuilder = new DetailBuilder<HikeDetails>(ServiceFactory.GetHikeDetailService(), CacheFactory.GetHikeCacheService(CacheType.Stub));
            SummaryBuilder.TripDetailBuilder = new DetailBuilder<TripDetails>(ServiceFactory.GetTripDetailService(), CacheFactory.GetTripCacheService(CacheType.Stub));
            SummaryBuilder.WeatherDetailBuilder = new DetailBuilder<WeatherDetails[]>(ServiceFactory.GetWeatherDetailService(), CacheFactory.GetWeatherCacheService(CacheType.Stub));
            SummaryBuilder.MapDetailBuilder = new DetailBuilder<MapDetails>(ServiceFactory.GetMapService(), CacheFactory.GetMapCacheService(CacheType.Stub));
            return SummaryBuilder;
        }
	}
}
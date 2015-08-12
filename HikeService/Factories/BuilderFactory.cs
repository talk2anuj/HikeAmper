using HikeService.Builders;

namespace HikeService.Factories
{
	static class BuilderFactory
	{
	    private static readonly HikeSummaryBuilder HikeSummaryBuilder;

	    static BuilderFactory()
	    {
	        HikeSummaryBuilder = new HikeSummaryBuilder();
	    }
        public static HikeSummaryBuilder GetHikeSummaryBuilder()
        {
            //Extend later if required - Set the builders based on url as input and return appropriate HikeSummaryBuilder
            HikeSummaryBuilder.HikeDetailsBuilder = new HikeDetailsBuilder(ServiceFactory.GetHikeDetailsService(), CacheFactory.GetHikeCacheService());
            HikeSummaryBuilder.WeatherDetailsBuilder = new WeatherDetailsBuilder(ServiceFactory.GetWeatherDetailsService(), CacheFactory.GetWeatherCacheService());
            HikeSummaryBuilder.TripDetailsBuilder = new TripDetailsBuilder(ServiceFactory.GetHikeDetailsService(),
                CacheFactory.GetTripCacheService());
            HikeSummaryBuilder.MapDetailsBuilder = new MapDetailsBuilder(ServiceFactory.GetMapsService(),
                CacheFactory.GetMapCacheService());
            return HikeSummaryBuilder;
        }
	}
}
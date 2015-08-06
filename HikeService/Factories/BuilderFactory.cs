using System.Configuration;
using HikeService.HikesModule.Builders;
using StackExchange.Redis;

namespace HikeService.Factories
{
	static class BuilderFactory
	{
	    private static readonly HikeSummaryBuilder _hikeSummaryBuilder;

	    static BuilderFactory()
	    {
	        _hikeSummaryBuilder = new HikeSummaryBuilder();
	    }
        public static HikeSummaryBuilder GetHikeSummaryBuilder()
        {
            _hikeSummaryBuilder.HikeDetailsService = ServiceFactory.GetHikeDetailsService();
            _hikeSummaryBuilder.WeatherDetailsService = ServiceFactory.GetWeatherDetailsService();
            _hikeSummaryBuilder.MapsService = ServiceFactory.GetMapsService();
            _hikeSummaryBuilder.WeatherCacheService = ServiceFactory.GetWeatherCacheService();
            _hikeSummaryBuilder.HikeCacheService = ServiceFactory.GetHikeCacheService();
            _hikeSummaryBuilder.MapCacheService = ServiceFactory.GetMapCacheService();
            _hikeSummaryBuilder.TripCacheService = ServiceFactory.GetTripCacheService();
            return _hikeSummaryBuilder;
        }
	}
}
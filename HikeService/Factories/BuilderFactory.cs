using HikeService.HikesModule.Builders;

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
//            _hikeSummaryBuilder.MapsService = ServiceFactory.GetMapsService();
            return _hikeSummaryBuilder;
        }
	}
}
using CommonModels.Hike;
using HikeService.CacheManagement;
using HikeService.CacheManagement.Services;
using HikeService.WeatherModule;
using CacheUtility = HikeService.Utilities.CacheUtility;

namespace HikeService.Builders
{
    public class WeatherDetailsBuilder: DetailsBuilder
    {
        public IWeatherDetailsService DetailsService { get; set; }
        public WeatherDetailsBuilder(IWeatherDetailsService detailsService, CacheService cache): base(cache)
        {
            DetailsService = detailsService;
        }
        public override void Build(string url, HikeSummary hikeSummary)
        {
            string cacheKey = CacheUtility.GetCacheKey(url);
            if (!Cache.PopulateDetails(cacheKey, hikeSummary))
		        {
		            hikeSummary.WeatherDetails =
		                DetailsService.GetWeatherForecastDetails(hikeSummary.HikeAndTripDetails.HikeDetails.Location);
		            Cache.AddDetails(cacheKey, hikeSummary);
		        }
        }
    }
}
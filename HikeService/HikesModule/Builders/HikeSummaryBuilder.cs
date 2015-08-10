using HikeService.CacheModule.Services;
using HikeService.HikesModule.Models;
using HikeService.HikesModule.Services;
using HikeService.MapsModule.Services;
using HikeService.WeatherModule;

namespace HikeService.HikesModule.Builders
{
	public class HikeSummaryBuilder
	{
		public HikeDetailsService HikeDetailsService { get; set; }
		public WeatherDetailsService WeatherDetailsService { get; set; }
		public MapsService MapsService { get; set; }
        public CacheService WeatherCacheService { get; set; }
        public CacheService HikeCacheService { get; set; }
        public CacheService MapCacheService { get; set; }
        public CacheService TripCacheService { get; set; }

		public HikeSummary Build(string url)
		{
			HikeSummary hikeSummary = new HikeSummary();
		    hikeSummary.HikeAndTripDetails = new HikeAndTripDetails();
		    var hike = GetHikeName(url);
		    //HikeCacheService.ClearCache();
		    //TripCacheService.ClearCache();
		    //MapCacheService.ClearCache();
		    //WeatherCacheService.ClearCache();
            if (!HikeCacheService.PopulateDetails(hike, hikeSummary))
		    {
		        hikeSummary.HikeAndTripDetails.HikeDetails = HikeDetailsService.GetHikeInformation(url);
                HikeCacheService.AddDetails(hike, hikeSummary);
		    }
            if (!TripCacheService.PopulateDetails(hike, hikeSummary))
            {
                hikeSummary.HikeAndTripDetails.TripDetails = HikeDetailsService.GetTripInformation(url);
                TripCacheService.AddDetails(hike, hikeSummary);
            }
		    if (hikeSummary.HikeAndTripDetails.HikeDetails != null &&
		        hikeSummary.HikeAndTripDetails.HikeDetails.Location != null)
		    {
		        if (!WeatherCacheService.PopulateDetails(hike, hikeSummary))
		        {
		            hikeSummary.WeatherDetails =
		                WeatherDetailsService.GetWeatherForecastDetails(hikeSummary.HikeAndTripDetails.HikeDetails.Location);
		            WeatherCacheService.AddDetails(hike, hikeSummary);
		        }
		        if (!MapCacheService.PopulateDetails(hike, hikeSummary))
		        {
		            hikeSummary.MapDetails = MapsService.GetMapDetails(hikeSummary.HikeAndTripDetails.HikeDetails.Location);
		            MapCacheService.AddDetails(hike, hikeSummary);
		        }
		    }
		    return hikeSummary;
		}

	    private string GetHikeName(string url)
	    {
	        var parts = url.Split('/');
	        return parts[parts.Length - 1];
	    }
	}
}
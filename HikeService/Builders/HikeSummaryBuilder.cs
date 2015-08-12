using CommonModels.Hike;

namespace HikeService.Builders
{
	public class HikeSummaryBuilder
	{
        public HikeDetailsBuilder HikeDetailsBuilder { get; set; }
        public TripDetailsBuilder TripDetailsBuilder { get; set; }
        public WeatherDetailsBuilder WeatherDetailsBuilder { get; set; }
        public MapDetailsBuilder MapDetailsBuilder { get; set; }

		public HikeSummary Build(string url)
		{
			HikeSummary hikeSummary = new HikeSummary();
		    hikeSummary.HikeAndTripDetails = new HikeAndTripDetails();
		    var hike = GetHikeName(url);
		    //HikeCacheService.ClearCache();
		    //TripCacheService.ClearCache();
		    //MapCacheService.ClearCache();
		    //WeatherCacheService.ClearCache();
		    HikeDetailsBuilder.Build(url, hikeSummary);
		    TripDetailsBuilder.Build(url, hikeSummary);
		    if (hikeSummary.HikeAndTripDetails.HikeDetails != null &&
		        hikeSummary.HikeAndTripDetails.HikeDetails.Location != null)
		    {
		        WeatherDetailsBuilder.Build(url, hikeSummary);
		        MapDetailsBuilder.Build(url, hikeSummary);
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
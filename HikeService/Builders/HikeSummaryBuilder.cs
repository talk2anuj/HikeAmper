using CommonModels.Hike;

namespace HikeService.Builders
{
	public class HikeSummaryBuilder
	{
        public HikeDetailsBuilder HikeDetailsBuilder { get; set; }
        public TripDetailsBuilder TripDetailsBuilder { get; set; }
        public WeatherDetailsBuilder WeatherDetailsBuilder { get; set; }
        public MapDetailsBuilder MapDetailsBuilder { get; set; }

		public HikeSummary Build(string url, bool partial)
		{
			HikeSummary hikeSummary = new HikeSummary();
		    hikeSummary.HikeAndTripDetails = new HikeAndTripDetails();
		    HikeDetailsBuilder.Build(url, hikeSummary, partial);
		    TripDetailsBuilder.Build(url, hikeSummary, partial);
		    if (hikeSummary.HikeAndTripDetails.HikeDetails != null &&
		        hikeSummary.HikeAndTripDetails.HikeDetails.Location != null)
		    {
		        WeatherDetailsBuilder.Build(url, hikeSummary, partial);
		        MapDetailsBuilder.Build(url, hikeSummary, partial);
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
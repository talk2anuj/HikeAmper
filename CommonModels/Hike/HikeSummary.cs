using CommonModels.Map;
using CommonModels.Weather;

namespace CommonModels.Hike
{
	public class HikeSummary
	{
		public HikeAndTripDetails HikeAndTripDetails { get; set; }
        public WeatherDetails[] WeatherDetails { get; set; }
        public MapDetails MapDetails { get; set; }

	    public void SetDetail<T>(T details)
	    {
            if (details is HikeAndTripDetails)
            {
                HikeAndTripDetails = details as HikeAndTripDetails;
            } else if (details is WeatherDetails[])
            {
                WeatherDetails = details as WeatherDetails[];
            } else if (details is MapDetails)
            {
                MapDetails = details as MapDetails;
            }
        }
	}
}
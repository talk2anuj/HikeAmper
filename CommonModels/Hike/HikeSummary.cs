using CommonModels.Map;
using CommonModels.Weather;

namespace CommonModels.Hike
{
	public class HikeSummary
	{
		public HikeAndTripDetails HikeAndTripDetails { get; set; }
        public WeatherDetails[] WeatherDetails { get; set; }
        public MapDetails MapDetails { get; set; }
	}
}
using HikeService.MapsModule.Models;
using HikeService.WeatherModule.Models;

namespace HikeService.HikesModule.Models
{
	public class HikeSummary
	{
		public HikeAndTripDetails HikeAndTripDetails { get; set; }
        public WeatherDetails[] WeatherDetails { get; set; }
        public MapDetails MapDetails { get; set; }
	}
}
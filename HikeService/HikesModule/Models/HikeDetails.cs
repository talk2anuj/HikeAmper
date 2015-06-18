

using HikeService.MapsModule.Models;

namespace HikeService.HikesModule.Models
{
	public class HikeDetails
	{
		public string Url { get; set; }
		public string Name { get; set; }
		public double RoundTripLength { get; set; }
		public double Elevation { get; set; }
        public GeographicalLocation Location { get; set; }
	}
}
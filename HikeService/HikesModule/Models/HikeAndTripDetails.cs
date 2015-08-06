using HikeService.MapsModule.Models;

namespace HikeService.HikesModule.Models
{
	public class HikeAndTripDetails
	{
        public HikeDetails HikeDetails { get; set; }
        public TripDetails TripDetails { get; set; }

	}

    public class HikeDetails
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public double RoundTripLength { get; set; }
        public double Elevation { get; set; }
        public GeographicalLocation Location { get; set; }
        public string TripReportsUrl { get; set; }
    }

    public class TripDetails
    {
        public string Date { get; set; }
        public string TypeOfHike { get; set; }
        public string TrailCondition { get; set; }
        public string SnowCondition { get; set; }
        public string RoadCondition { get; set; }
        public string BugsCondition { get; set; }
        public string Url { get; set; }
    }
}
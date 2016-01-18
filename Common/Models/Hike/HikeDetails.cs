using Common.Models.Map;

namespace Common.Models.Hike
{
    public class HikeDetails
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public double RoundTripLength { get; set; }
        public ElevationDetails ElevationDetails { get; set; }
        public GeographicalLocation Location { get; set; }
        public string TripReportsUrl { get; set; }
        public string PassRequired { get; set; }
        public string LocationName { get; set; }
    }

    public class ElevationDetails
    {
        public string Gain { get; set; }
        public string HighestPoint { get; set; }
    }
}
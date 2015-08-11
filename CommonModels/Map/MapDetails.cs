
namespace CommonModels.Map
{
    public class MapDetails
    {
        public string Distance { get; set; }
        public string Duration { get; set; }

        public MapDetails(string distance, string duration)
        {
            Distance = distance;
            Duration = duration;
        }
    }

    public class DistanceAndDuration
    {
        public Routes[] routes { get; set; }
    }

    public class Routes
    {
        public Legs[] legs { get; set; }
    }

    public class Legs
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
    }

    public class Distance
    {
        public string text { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
    }
}
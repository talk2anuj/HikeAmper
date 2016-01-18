namespace Common.Models.Map
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
        public Routes[] Routes { get; set; }
    }

    public class Routes
    {
        public Legs[] Legs { get; set; }
    }

    public class Legs
    {
        public Distance Distance { get; set; }
        public Duration Duration { get; set; }
    }

    public class Distance
    {
        public string Text { get; set; }
    }

    public class Duration
    {
        public string Text { get; set; }
    }
}
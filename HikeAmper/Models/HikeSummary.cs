namespace HikeAmper.Models
{
    public class HikeSummary
    {
        public HikeAndTripDetails HikeAndTripDetails { get; set; }
        public WeatherDetails[] WeatherDetails { get; set; }
        public MapDetails MapDetails { get; set; }
    }

    public class HikeAndTripDetails
    {
        public HikeDetails HikeDetails { get; set; }
        public TripDetails TripDetails { get; set; }

    }

    public class HikeDetails
    {
        public string url { get; set; }
        public string name { get; set; }
        public double roundTripLength { get; set; }
        public double elevation { get; set; }
        public LocationDetails location { get; set; }
        public string TripReportsUrl { get; set; }
    }

    public class LocationDetails
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class WeatherDetails
    {
        public Date date { get; set; }
        public Temperature high { get; set; }
        public Temperature low { get; set; }
        public string conditions { get; set; }
        public int pop { get; set; }
        public Wind maxwind { get; set; }
        public Wind avewind { get; set; }
        public int avehumidity { get; set; }
        public int maxhumidity { get; set; }
        public int minhumidity { get; set; }
    }

    public class Date
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public string monthname_short { get; set; }
        public string weekday_short { get; set; }
    }

    public class Temperature
    {
        public string fahrenheit { get; set; }
        public string celsius { get; set; }
    }

    public class Wind
    {
        public int mph { get; set; }
        public int kph { get; set; }
        public string dir { get; set; }
        public int degrees { get; set; }
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

    public class MapDetails
    {
        public string Distance { get; set; }
        public string Duration { get; set; }
    }
}
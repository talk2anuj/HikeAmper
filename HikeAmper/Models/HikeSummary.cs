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

    public class GeographicalLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
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
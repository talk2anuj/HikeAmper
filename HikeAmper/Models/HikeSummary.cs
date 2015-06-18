using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HikeAmper.Models
{
    public class HikeSummary
    {
        public HikeDetails hikeDetails { get; set; }
        public WeatherDetails[] weatherDetails { get; set; }
    }

    public class HikeDetails
    {
        public string url { get; set; }
        public string name { get; set; }
        public double roundTripLength { get; set; }
        public double elevation { get; set; }
        public LocationDetails location { get; set; }
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
}
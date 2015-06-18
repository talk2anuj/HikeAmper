namespace HikeService.WeatherModule.Models
{
    public class WeatherData
    {
        public WeatherForecast Forecast { get; set; }
    }

    public class WeatherForecast
    {
        public Simpleforecast Simpleforecast { get; set; }
    }

    public class Simpleforecast
    {
        public WeatherDetails[] Forecastday { get; set; }
    }
    public class WeatherDetails
    {
        public Date Date { get; set; }
        public Temperature High { get; set; }
        public Temperature Low { get; set; }
        public string Conditions { get; set; }
        public int Pop { get; set; }
        public Wind Maxwind { get; set; }
        public Wind Avewind { get; set; }
        public int Avehumidity { get; set; }
        public int Maxhumidity { get; set; }
        public int Minhumidity { get; set; }

    }

    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string monthname_short { get; set; }
        public string weekday_short { get; set; }
    }

    public class Temperature
    {
        public string Fahrenheit { get; set; }
        public string Celsius { get; set; }
    }

    public class Wind
    {
        public int Mph { get; set; }
        public int Kph { get; set; }
        public string Dir { get; set; }
        public int Degrees { get; set; }
    }
}
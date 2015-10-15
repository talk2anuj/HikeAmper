using System;
using CommonModels.Hike;
using CommonModels.Map;
using CommonModels.Weather;
using HikeService.Utilities;
using Newtonsoft.Json;

namespace HikeService.DetailServices.impl
{
    public class WeatherUndergroundService : IDetailService<WeatherDetails[]>
    {
        private const string MyKey = "e4938cdd8fd24af0";

        public void PopulateDetails(string url, HikeSummary summary)
        {
            GeographicalLocation locationDetails = summary.HikeAndTripDetails.HikeDetails.Location;
            var locationUrl = GetUrl(locationDetails);
            //scrape data from WeatherData Underground Service
            string json = WebClientUtility.GetJsonString(locationUrl);
            summary.WeatherDetails = Get4DaysForecast(json);
        }
        private WeatherDetails[] Get4DaysForecast(string json)
        {
            try
            {
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
                return weatherData.Forecast.Simpleforecast.Forecastday;
            }
            catch (Exception e)
            {
                //Log error
            }
            return null;
        }

        private string GetUrl(GeographicalLocation locationDetails)
        {
            return "http://api.wunderground.com/api/" + MyKey + "/forecast/q/" + locationDetails.Latitude + "," +
                   locationDetails.Longitude + ",json";
        }
    }
}
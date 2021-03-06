﻿using Common.Models.Hike;
using Common.Models.Map;
using Common.Models.Weather;
using DetailServices.Utilities;
using Newtonsoft.Json;
using System;

namespace DetailServices.impl
{
    public class WeatherUndergroundService : IDetailService<WeatherDetails[]>
    {
        private const string MyKey = "e4938cdd8fd24af0";

        public void PopulateDetails(string url, HikeSummary summary, string userName)
        {
            GeographicalLocation locationDetails = summary.HikeDetails.Location;
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
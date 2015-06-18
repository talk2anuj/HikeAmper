using HikeService.HikesModule.Models;
using HikeService.MapsModule.Models;
using HikeService.WeatherModule.Models;

namespace HikeService.WeatherModule
{
    public interface WeatherDetailsService
    {
        WeatherDetails[] GetWeatherForecastDetails(GeographicalLocation locationDetails);
    }
}
using CommonModels.Map;
using CommonModels.Weather;

namespace HikeService.WeatherModule
{
    public interface WeatherDetailsService
    {
        WeatherDetails[] GetWeatherForecastDetails(GeographicalLocation locationDetails);
    }
}
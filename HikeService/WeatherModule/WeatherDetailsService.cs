using CommonModels.Map;
using CommonModels.Weather;

namespace HikeService.WeatherModule
{
    public interface IWeatherDetailsService
    {
        WeatherDetails[] GetWeatherForecastDetails(GeographicalLocation locationDetails);
    }
}
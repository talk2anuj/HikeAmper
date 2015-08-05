using HikeService.HikesModule.Models;
using HikeService.HikesModule.Services;
using HikeService.MapsModule.Services;
using HikeService.WeatherModule;

namespace HikeService.HikesModule.Builders
{
	public class HikeSummaryBuilder
	{
		public HikeDetailsService HikeDetailsService { get; set; }
		public WeatherDetailsService WeatherDetailsService { get; set; }
		public MapsService MapsService { get; set; }

		public HikeSummary Build(string url)
		{
			HikeSummary hikeSummary = new HikeSummary();
			hikeSummary.HikeDetails = HikeDetailsService.GetInformation(url);
		    hikeSummary.WeatherDetails = WeatherDetailsService.GetWeatherForecastDetails(hikeSummary.HikeDetails.Location);
            hikeSummary.MapDetails = MapsService.GetMapDetails(hikeSummary.HikeDetails.Location);
            return hikeSummary;
		}
	}
}
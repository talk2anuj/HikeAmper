using CommonModels.Hike;
using CommonModels.Map;
using CommonModels.Weather;

namespace HikeService.Builders
{
	public class SummaryBuilder
	{
        public DetailBuilder<HikeDetails> HikeDetailBuilder { get; set; }
        public DetailBuilder<TripDetails> TripDetailBuilder { get; set; }
        public DetailBuilder<WeatherDetails[]> WeatherDetailBuilder { get; set; }
        public DetailBuilder<MapDetails> MapDetailBuilder { get; set; }

		public HikeSummary Build(string url, bool partial)
		{
		    HikeSummary hikeSummary = new HikeSummary {HikeAndTripDetails = new HikeAndTripDetails()};
		    HikeDetailBuilder.Build(url, hikeSummary);
		    TripDetailBuilder.Build(url, hikeSummary);
		    WeatherDetailBuilder.Build(url, hikeSummary);
		    MapDetailBuilder.Build(url, hikeSummary);
		    return hikeSummary;
		}
	}
}
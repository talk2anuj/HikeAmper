using Common.Models.Hike;
using Common.Models.Map;
using Common.Models.Weather;

namespace DetailServices.Builders
{
    public class SummaryBuilder
    {
        public DetailBuilder<HikeDetails> HikeDetailBuilder { get; set; }
        public DetailBuilder<TripDetails> TripDetailBuilder { get; set; }
        public DetailBuilder<WeatherDetails[]> WeatherDetailBuilder { get; set; }
        public DetailBuilder<MapDetails> MapDetailBuilder { get; set; }

        public HikeSummary Build(string url, string user = null)
        {
            HikeSummary hikeSummary = new HikeSummary { Url = url };
            HikeDetailBuilder.Build(user, url, hikeSummary);
            TripDetailBuilder.Build(user, url, hikeSummary);
            WeatherDetailBuilder.Build(user, url, hikeSummary);
            MapDetailBuilder.Build(user, url, hikeSummary);
            return hikeSummary;
        }
    }
}
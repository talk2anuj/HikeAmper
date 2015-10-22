using CommonModels.Map;
using CommonModels.Weather;

namespace CommonModels.Hike
{
	public class HikeSummary
	{
        public HikeDetails HikeDetails { get; set; }
        public TripDetails TripDetails { get; set; }
        public WeatherDetails[] WeatherDetails { get; set; }
        public MapDetails MapDetails { get; set; }
        public string Url { get; set; }

	    public void SetDetail<T>(T details)
	    {
	        if (details is HikeDetails)
	        {
	            HikeDetails = details as HikeDetails;
	        }
            else if (details is TripDetails)
            {
                TripDetails = details as TripDetails;
            }
	        else if (details is WeatherDetails[])
	        {
	            WeatherDetails = details as WeatherDetails[];
	        }
	        else if (details is MapDetails)
	        {
	            MapDetails = details as MapDetails;
	        }
	    }


	    public T GetDetail<T>()
        {
            if (typeof(T) == typeof(HikeDetails))
            {
                return (T)(object)HikeDetails;
            }
	        if (typeof(T) == typeof(TripDetails))
	        {
	            return (T)(object)TripDetails;
	        }
	        if (typeof(T) == typeof(WeatherDetails[]))
	        {
	            return (T)(object)WeatherDetails;
	        }
	        if (typeof (T) == typeof (MapDetails))
	        {
	            return (T)(object)MapDetails;
	        }
	        return default(T);
        }
    }
}
using Common.Models.Hike;
using Common.Models.Map;
using DetailServices.Utilities;
using Newtonsoft.Json;
using System;

namespace DetailServices.impl
{
    public class GoogleMapsService : IDetailService<MapDetails>
    {
        public const string Api = " https://maps.googleapis.com/maps/api/directions/json?";

        public void PopulateDetails(string url, HikeSummary summary)
        {
            GeographicalLocation destination = summary.HikeDetails.Location;
            MapDetails mapDetails = new MapDetails("Distance NA", "Duration NA");
            try
            {
                PhysicalAddress source = new PhysicalAddress();
                source.Zip = "98052";
                string locationUrl = GetUrl(source, destination);
                string json = WebClientUtility.GetJsonString(locationUrl);
                DistanceAndDuration value = JsonConvert.DeserializeObject<DistanceAndDuration>(json);
                mapDetails.Distance = value.Routes[0].Legs[0].Distance.Text;
                mapDetails.Duration = value.Routes[0].Legs[0].Duration.Text;
            }
            catch (Exception e)
            {
                //Log error
            }
            summary.MapDetails = mapDetails;
        }

        private string GetUrl(PhysicalAddress source, GeographicalLocation destination)
        {
            return Api + "origin=" + source.Zip + "&destination=" + destination.Latitude + "," + destination.Longitude;
        }
    }
}
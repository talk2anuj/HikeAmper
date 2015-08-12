using System;
using CommonModels.Map;
using HikeService.Utilities;
using Newtonsoft.Json;

namespace HikeService.MapsModule.Services.impl
{
    public class GoogleMapsService: IMapsService
    {
        public const string Api = " https://maps.googleapis.com/maps/api/directions/json?";

        public MapDetails GetMapDetails(GeographicalLocation destination)
        {
            MapDetails mapDetails = new MapDetails("Distance NA", "Duration NA");
            try
            {
                PhysicalAddress source = new PhysicalAddress();
                source.Zip = "98052";
                string url = GetUrl(source, destination);
                string json = WebClientUtility.GetJsonString(url);
                DistanceAndDuration value = JsonConvert.DeserializeObject<DistanceAndDuration>(json);
                mapDetails.Distance = value.Routes[0].Legs[0].Distance.Text;
                mapDetails.Duration = value.Routes[0].Legs[0].Duration.Text;
            }
            catch (Exception e)
            {
                //Log error
            }
            return mapDetails;
        }

        private string GetUrl(PhysicalAddress source, GeographicalLocation destination)
        {
            return Api + "origin=" + source.Zip + "&destination=" + destination.Latitude + "," + destination.Longitude;
        }
    }
}
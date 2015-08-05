using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HikeService.MapsModule.Models;
using HikeService.Utilities;
using Newtonsoft.Json;

namespace HikeService.MapsModule.Services.impl
{
    public class GoogleMapsService: MapsService
    {
        public const string api = " https://maps.googleapis.com/maps/api/directions/json?";

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
                mapDetails.Distance = value.routes[0].legs[0].distance.text;
                mapDetails.Duration = value.routes[0].legs[0].duration.text;
            }
            catch (Exception e)
            {
                //Log error
            }
            return mapDetails;
        }

        private string GetUrl(PhysicalAddress source, GeographicalLocation destination)
        {
            return api + "origin=" + source.Zip + "&destination=" + destination.Latitude + "," + destination.Longitude;
        }
    }
}
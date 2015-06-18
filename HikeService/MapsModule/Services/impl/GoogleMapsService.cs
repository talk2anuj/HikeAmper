using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HikeService.MapsModule.Models;
using HikeService.Utilities;

namespace HikeService.MapsModule.Services.impl
{
    public class GoogleMapsService: MapsService
    {
        public const string api = " https://maps.googleapis.com/maps/api/directions/json?";
       
        public MapDetails GetMapDetails(PhysicalAddress source, GeographicalLocation destination)
        {
            string url = GetUrl(source, destination);
            string json = WebClientUtility.GetJsonString(url);
            return new MapDetails();
        }

        public MapDetails GetMapDetails()
        {
            throw new NotImplementedException();
        }

        private string GetUrl(PhysicalAddress source, GeographicalLocation destination)
        {
            return api + "origin=" + source.Zip + "&destination=" + destination.Latitude + "," + destination.Longitude;
        }
    }
}
using Common.Models.Hike;
using Common.Models.Map;
using Common.Models.Storage;
using DetailServices.Factories;
using DetailServices.Utilities;
using Newtonsoft.Json;
using System;

namespace DetailServices.impl
{
    public class GoogleMapsService : IDetailService<MapDetails>
    {
        public const string Api = " https://maps.googleapis.com/maps/api/directions/json?";

        public void PopulateDetails(string url, HikeSummary summary, string userName)
        {
            GeographicalLocation destination = summary.HikeDetails.Location;
            MapDetails mapDetails = new MapDetails("Distance NA", "Duration NA");
            try
            {
                var zipCode = GetZipCode(userName);
                if (zipCode != null)
                {
                    string locationUrl = GetUrl(zipCode, destination);
                    string json = WebClientUtility.GetJsonString(locationUrl);
                    DistanceAndDuration value = JsonConvert.DeserializeObject<DistanceAndDuration>(json);
                    mapDetails.Distance = value.Routes[0].Legs[0].Distance.Text;
                    mapDetails.Duration = value.Routes[0].Legs[0].Duration.Text;
                }
            }
            catch (Exception e)
            {
                //Log error
            }
            summary.MapDetails = mapDetails;
        }

        private string GetUrl(string zipCode, GeographicalLocation destination)
        {
            return Api + "origin=" + zipCode + "&destination=" + destination.Latitude + "," + destination.Longitude;
        }

        private string GetZipCode(string userName)
        {
            var storageService = StorageFactory.GetStorageService<UserDataEntity>(StorageType.AzureStorage);
            UserDataEntity entity = new UserDataEntity(userName);
            var result = storageService.GetEntity(entity);
            return ((UserDataEntity)result.Result)?.ZipCode;
        }
    }
}
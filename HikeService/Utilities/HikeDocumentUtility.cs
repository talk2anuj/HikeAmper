using System;
using System.Linq;
using CommonModels.Hike;
using CommonModels.Map;
using HtmlAgilityPack;

namespace HikeService.Utilities
{
    public static class HikeDocumentUtility
    {
        private static string _distanceElement = "distance";
        private static string _latitude = "latitude";
        private static string _longitude = "longitude";
        private static string _hikeStats = "hike-stats";
        private static string _typeOfHike = "Type of Hike";
        private static string _trail = "Trail";
        private static string _road = "Road";
        private static string _bugs = "Bugs";
        private static string _snow = "Snow";
        private static string _date = "date-and-author";
        private static string _notAvailable = "Not Available";
        private static string _passRequiredInfo = "pass-required-info";


        public static T GetInfoFromDocument<T>(Func<HtmlDocument, T> func, HtmlDocument doc)
        {
            try
            {
                T result = func(doc);
            }
            catch (Exception e)
            {
                //Log error
            }
            return default(T);
        }
        public static string GetName(HtmlDocument doc)
        {
            var name = _notAvailable;
            try
            {
                var node = doc.DocumentNode.SelectSingleNode("//h1[@class='documentFirstHeading']");
                name = node.InnerText;
            }
            catch (Exception e)
            {
                //Log error
            }
            return name;
        }

        public static ElevationDetails GetElevationDetails(HtmlDocument doc)
        {
            var elevationDetails = new ElevationDetails();
            try
            {
                var element = doc.GetElementbyId(_hikeStats);
                var nodes = element.Descendants("div").Where(d => d.Attributes.Contains("class"));
                foreach (var node in nodes)
                {
                    if (node.InnerText.Contains("Elevation"))
                    {
                        var innerNodes = node.SelectNodes("div");
                        foreach(var innerNode in innerNodes)
                        {
                            if (innerNode.InnerText.Contains("Gain"))
                            {
                                elevationDetails.Gain = innerNode.SelectSingleNode("span").InnerText;
                            }
                            if (innerNode.InnerText.Contains("Highest"))
                            {
                                elevationDetails.HighestPoint = innerNode.SelectSingleNode("span").InnerText;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Log error
            }
            return elevationDetails;
        }

        public static double GetRoundTripLength(HtmlDocument doc)
        {
            var rountTripLength = 0.0;
            try
            {
                var element = doc.GetElementbyId(_distanceElement);
                var node = element.SelectSingleNode("span");
                rountTripLength = double.Parse(node.InnerText);
            }
            catch (Exception e)
            {
                //Log error
            }
            return rountTripLength;
        }

        public static GeographicalLocation GetLocation(HtmlDocument doc)
        {
            GeographicalLocation location = new GeographicalLocation();
            try
            {
                var latitude = doc.DocumentNode.SelectSingleNode("//span[@class='" + _latitude + "']");
                var longitude = doc.DocumentNode.SelectSingleNode("//span[@class='" + _longitude + "']");
                location.Latitude = double.Parse(latitude.InnerText);
                location.Longitude = double.Parse(longitude.InnerText);
            }
            catch (Exception e)
            {
                //Log error
            }
            return location;
        }

        public static HikeDetails GetHikeDetails(string url, HtmlDocument doc)
        {
            var hikeDetails = new HikeDetails();
            hikeDetails.Url = url;
            hikeDetails.Name = GetName(doc);
            hikeDetails.ElevationDetails = GetElevationDetails(doc);
            hikeDetails.RoundTripLength = GetRoundTripLength(doc);
            hikeDetails.Location = GetLocation(doc);
            hikeDetails.LocationName = GetLocationName(doc);
            hikeDetails.PassRequired = GetPassRequiredInfo(doc);
            hikeDetails.TripReportsUrl = url + "/@@related_tripreport_listing";
            return hikeDetails;
        }

        private static string GetPassRequiredInfo(HtmlDocument doc)
        {
            var passRequiredInfo = "NA";
            try
            {
                var element = doc.GetElementbyId(_passRequiredInfo);
                passRequiredInfo = element.SelectSingleNode("a").InnerText;
            }
            catch (Exception e)
            {
                //Log error
            }
            return passRequiredInfo;
        }

        private static string GetLocationName(HtmlDocument doc)
        {
            var locationName = "NA";
            try
            {
                var element = doc.GetElementbyId(_hikeStats);
                var nodes = element.Descendants("div").Where(d => d.Attributes.Contains("class"));
                foreach (var node in nodes)
                {
                    if (node.InnerText.Contains("Location"))
                    {
                        locationName = node.SelectSingleNode("div").InnerText;
                    }
                }
            }
            catch (Exception e)
            {
                //Log error
            }
            return locationName;
        }

        public static TripDetails GetTripReportDetails(HtmlDocument doc)
        {
            TripDetails tripDetails = new TripDetails();
            try
            {
                var node = doc.DocumentNode.SelectSingleNode("//a[contains(@class,'full-report-link')]");
                var latestTripReportUrl = node.Attributes["href"].Value;
                PopulateTripReportDetails(tripDetails, latestTripReportUrl);
            }
            catch (Exception e)
            {
                //Log error
            }
            return tripDetails;
        }

        private static void PopulateTripReportDetails(TripDetails tripDetails, string url)
        {
            HtmlDocument doc = WebClientUtility.GetHtmlDocument(url);
            tripDetails.Url = url;
            tripDetails.Date = GetTripReportDate(tripDetails, doc);
            var node = doc.GetElementbyId("trip-conditions");
            var conditions = node.Descendants("div");
            foreach (var condition in conditions)
            {
                try
                {
                    var nodeTypeText = condition.SelectSingleNode("h4").InnerText;
                    var nodeValueText = condition.SelectSingleNode("span").InnerText;
                    if (_typeOfHike.Equals(nodeTypeText))
                    {
                        tripDetails.TypeOfHike = nodeValueText;
                    }
                    if (_trail.Equals(nodeTypeText))
                    {
                        tripDetails.TrailCondition = nodeValueText;
                    }
                    if (_road.Equals(nodeTypeText))
                    {
                        tripDetails.RoadCondition = nodeValueText;
                    }
                    if (_bugs.Equals(nodeTypeText))
                    {
                        tripDetails.BugsCondition = nodeValueText;
                    }
                    if (_snow.Equals(nodeTypeText))
                    {
                        tripDetails.SnowCondition = nodeValueText;
                    }
                }
                catch (Exception e)
                {
                    //Log error
                }
            }
        }

        private static string GetTripReportDate(TripDetails tripDetails, HtmlDocument doc)
        {
            var date = "";
            try
            {
                var dateText = doc.DocumentNode.SelectSingleNode("//span[contains(@class, '" + _date + "')]").InnerText;
                dateText = dateText.Substring(dateText.IndexOf(", on", StringComparison.OrdinalIgnoreCase) + ", on".Length);
                date = dateText.Split('(')[0].Trim();
            }
            catch (Exception e)
            {
                //Log errors
            }
            return date;
        }
    }
}
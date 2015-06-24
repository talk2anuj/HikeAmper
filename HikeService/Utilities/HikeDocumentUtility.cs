using System;
using System.Linq;
using HikeService.HikesModule.Models;
using HikeService.MapsModule.Models;
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

		public static string GetName(HtmlDocument doc)
		{
			var node = doc.DocumentNode.SelectSingleNode("//h1[@class='documentFirstHeading']");
			return node.InnerText;
		}

		public static double GetElevation(HtmlDocument doc)
		{
			var element = doc.GetElementbyId(_hikeStats);
			var nodes = element.Descendants("div").Where(d => d.Attributes.Contains("class"));			
			foreach (var node in nodes)
			{
				var text = node.InnerText;					
				if (node.InnerText.Contains("Elevation"))
				{
					string elevation = node.SelectSingleNode("div").SelectSingleNode("span").InnerText;
					return double.Parse(elevation);
				}
			}
			return 0;
		}

		public static double GetRoundTripLength(HtmlDocument doc)
		{
			var element = doc.GetElementbyId(_distanceElement);
			var node = element.SelectSingleNode("span");			
            return double.Parse(node.InnerText);
		}

		public static GeographicalLocation GetLocation(HtmlDocument doc)
		{
			var latitude = doc.DocumentNode.SelectSingleNode("//span[@class='"+_latitude+"']");
			var longitude = doc.DocumentNode.SelectSingleNode("//span[@class='"+_longitude+"']");			
			GeographicalLocation location = new GeographicalLocation();
			location.Latitude = double.Parse(latitude.InnerText);			
			location.Longitude = double.Parse(longitude.InnerText);			
			return location;
		}

        public static TripReportDetails GetTripReportDetails(HtmlDocument doc)
        {
            TripReportDetails tripReportDetails = new TripReportDetails();
            var node = doc.DocumentNode.SelectSingleNode("//a[contains(@class,'full-report-link')]");
            var latestTripReportUrl = node.Attributes["href"].Value;
            PopulateTripReportDetails(tripReportDetails, latestTripReportUrl);
            return tripReportDetails;
        }

	    private static void PopulateTripReportDetails(TripReportDetails tripReportDetails, string url)
	    {
            HtmlDocument doc = WebClientUtility.GetHtmlDocument(url);            
	        var dateText = doc.DocumentNode.SelectSingleNode("//span[@class='" + _date + "']").InnerText;
            tripReportDetails.Date = dateText.Substring(dateText.IndexOf("on", StringComparison.OrdinalIgnoreCase) + "on".Length);            
            var node = doc.GetElementbyId("trip-conditions");
            var conditions = node.Descendants("div");
            foreach (var condition in conditions)
            {
                var nodeTypeText = condition.SelectSingleNode("h4").InnerText;
                var nodeValueText = condition.SelectSingleNode("span").InnerText;
                if (_typeOfHike.Equals(nodeTypeText))
                {
                    tripReportDetails.TypeOfHike = nodeValueText;
                }
                if (_trail.Equals(nodeTypeText))
                {
                    tripReportDetails.TrailCondition = nodeValueText;
                }
                if (_road.Equals(nodeTypeText))
                {
                    tripReportDetails.RoadCondition = nodeValueText;
                }
                if (_bugs.Equals(nodeTypeText))
                {
                    tripReportDetails.BugsCondition = nodeValueText;
                }
                if (_snow.Equals(nodeTypeText))
                {
                    tripReportDetails.SnowCondition = nodeValueText;
                }
            }            
	    }
	}
}
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
    }
}
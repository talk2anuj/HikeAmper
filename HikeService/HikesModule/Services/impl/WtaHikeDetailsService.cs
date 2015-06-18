using HikeService.HikesModule.Models;
using HikeService.Utilities;
using HtmlAgilityPack;

namespace HikeService.HikesModule.Services.impl
{
	public class WtaHikeDetailsService : HikeDetailsService
	{
		public HikeDetails GetInformation(string url)
		{
			//scrape data from WTA
			HtmlDocument doc = WebClientUtility.GetHtmlDocument(url);
			HikeDetails hikeDetails = PopulateHikeDetails(url, doc);           
			return hikeDetails;
		}

		private HikeDetails PopulateHikeDetails(string url, HtmlDocument doc)
		{
			HikeDetails hikeDetails = new HikeDetails();
			hikeDetails.Url = url;
			hikeDetails.Name = HikeDocumentUtility.GetName(doc);			
			hikeDetails.Elevation = HikeDocumentUtility.GetElevation(doc);
			hikeDetails.RoundTripLength = HikeDocumentUtility.GetRoundTripLength(doc);
			hikeDetails.Location = HikeDocumentUtility.GetLocation(doc);
			return hikeDetails;
		}
	}
}
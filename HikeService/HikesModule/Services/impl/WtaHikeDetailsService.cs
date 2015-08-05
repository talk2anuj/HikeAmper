using HikeService.HikesModule.Models;
using HikeService.Utilities;
using HtmlAgilityPack;

namespace HikeService.HikesModule.Services.impl
{
	public class WtaHikeDetailsService : HikeDetailsService
	{
        private static string TRIP_REPORT_URL_EXTENSION = "/@@related_tripreport_listing";

		public HikeDetails GetInformation(string url)
		{
			//scrape data from WTA
			HtmlDocument hikeDoc = WebClientUtility.GetHtmlDocument(url);
            HtmlDocument tripReportDoc = WebClientUtility.GetHtmlDocument(url + TRIP_REPORT_URL_EXTENSION);
            HikeDetails hikeDetails = PopulateHikeDetails(url, hikeDoc, tripReportDoc);
			return hikeDetails;
		}

        private HikeDetails PopulateHikeDetails(string url, HtmlDocument hikeDoc, HtmlDocument tripReportDoc)
		{
			HikeDetails hikeDetails = new HikeDetails();
			hikeDetails.Url = url;
            hikeDetails.Name = HikeDocumentUtility.GetName(hikeDoc);
            hikeDetails.Elevation = HikeDocumentUtility.GetElevation(hikeDoc);
            hikeDetails.RoundTripLength = HikeDocumentUtility.GetRoundTripLength(hikeDoc);
            hikeDetails.Location = HikeDocumentUtility.GetLocation(hikeDoc);
            hikeDetails.TripReport = HikeDocumentUtility.GetTripReportDetails(tripReportDoc);
            hikeDetails.TripReportsUrl = url + "/@@related_tripreport_listing";
			return hikeDetails;
		}
	}
}
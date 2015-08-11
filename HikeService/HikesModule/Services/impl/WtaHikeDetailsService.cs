using CommonModels.Hike;
using HikeService.Utilities;
using HtmlAgilityPack;

namespace HikeService.HikesModule.Services.impl
{
	public class WtaHikeDetailsService : HikeDetailsService
	{
        private static string TRIP_REPORT_URL_EXTENSION = "/@@related_tripreport_listing";

		public HikeDetails GetHikeInformation(string url)
		{
			//scrape data from WTA
			HtmlDocument hikeDoc = WebClientUtility.GetHtmlDocument(url);
            var hikeDetails = HikeDocumentUtility.GetHikeDetails(url, hikeDoc);
			return hikeDetails;
		}

        public TripDetails GetTripInformation(string url)
        {
            //scrape data from WTA
            HtmlDocument tripReportDoc = WebClientUtility.GetHtmlDocument(url + TRIP_REPORT_URL_EXTENSION);
            var tripDetails = HikeDocumentUtility.GetTripReportDetails(tripReportDoc);
            return tripDetails;
        }
	}
}
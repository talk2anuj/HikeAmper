using Common.Models.Hike;
using DetailServices.Utilities;
using HtmlAgilityPack;

namespace DetailServices.impl
{
    public class WtaTripDetailService : IDetailService<TripDetails>
    {
        private static string _tripReportUrlExtension = "/@@related_tripreport_listing";

        public void PopulateDetails(string url, HikeSummary summary, string userName)
        {
            //scrape data from WTA
            HtmlDocument tripReportDoc = WebClientUtility.GetHtmlDocument(url + _tripReportUrlExtension);
            summary.TripDetails = HikeDocumentUtility.GetTripReportDetails(tripReportDoc);
        }
    }
}
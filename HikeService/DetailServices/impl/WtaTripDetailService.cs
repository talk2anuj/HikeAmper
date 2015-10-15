using CommonModels.Hike;
using HikeService.Utilities;
using HtmlAgilityPack;

namespace HikeService.DetailServices.impl
{
    public class WtaTripDetailService : IDetailService<TripDetails>
    {
        private static string _tripReportUrlExtension = "/@@related_tripreport_listing";
        public void PopulateDetails(string url, HikeSummary summary)
        {
            //scrape data from WTA
            HtmlDocument tripReportDoc = WebClientUtility.GetHtmlDocument(url + _tripReportUrlExtension);
            summary.HikeAndTripDetails.TripDetails = HikeDocumentUtility.GetTripReportDetails(tripReportDoc);
        }
    }
}
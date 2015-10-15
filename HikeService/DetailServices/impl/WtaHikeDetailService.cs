using CommonModels.Hike;
using HikeService.Utilities;
using HtmlAgilityPack;

namespace HikeService.DetailServices.impl
{
	public class WtaHikeDetailService : IDetailService<HikeDetails>
	{
	    public void PopulateDetails(string url, HikeSummary summary)
	    {
            //scrape data from WTA
            HtmlDocument hikeDoc = WebClientUtility.GetHtmlDocument(url);
            summary.HikeAndTripDetails.HikeDetails = HikeDocumentUtility.GetHikeDetails(url, hikeDoc);
        }
	}
}
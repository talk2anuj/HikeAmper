using CommonModels.Common;
using CommonModels.Hike;
using HikeService.Builders;
using HikeService.Utilities;
using HtmlAgilityPack;

namespace HikeService.HikesModule.Services.impl
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
using CommonModels.Common;
using CommonModels.Hike;
using DetailServices.Utilities;
using HtmlAgilityPack;

namespace DetailServices.impl
{
	public class WtaHikeDetailService : IDetailService<HikeDetails>
	{
	    public void PopulateDetails(string url, HikeSummary summary)
	    {
            //scrape data from WTA
            HtmlDocument hikeDoc = WebClientUtility.GetHtmlDocument(url);
            summary.HikeDetails = HikeDocumentUtility.GetHikeDetails(url, hikeDoc);
        }
	}
}
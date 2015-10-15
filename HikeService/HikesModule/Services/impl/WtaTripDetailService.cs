using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonModels.Hike;
using HikeService.Builders;
using HikeService.Utilities;
using HtmlAgilityPack;

namespace HikeService.HikesModule.Services.impl
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
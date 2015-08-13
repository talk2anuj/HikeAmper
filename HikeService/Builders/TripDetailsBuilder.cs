using CommonModels.Hike;
using HikeService.CacheManagement;
using HikeService.CacheManagement.Services;
using HikeService.HikesModule.Services;
using CacheUtility = HikeService.Utilities.CacheUtility;

namespace HikeService.Builders
{
    public class TripDetailsBuilder: DetailsBuilder
    {
         public IHikeDetailsService DetailsService { get; set; }
        public TripDetailsBuilder(IHikeDetailsService detailsService, CacheService cache): base(cache)
        {
            DetailsService = detailsService;
        }
        public override void Build(string url, HikeSummary hikeSummary, bool partial)
        {
            string cacheKey = CacheUtility.GetCacheKey(url);
            if (!Cache.PopulateDetails(cacheKey, hikeSummary))
            {
                hikeSummary.HikeAndTripDetails.TripDetails = DetailsService.GetTripInformation(url);
                Cache.AddDetails(cacheKey, hikeSummary);
            }
        }
    }
}
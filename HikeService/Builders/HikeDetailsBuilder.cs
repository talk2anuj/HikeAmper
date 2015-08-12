using CommonModels.Hike;
using HikeService.CacheManagement;
using HikeService.CacheManagement.Services;
using HikeService.HikesModule.Services;
using CacheUtility = HikeService.Utilities.CacheUtility;

namespace HikeService.Builders
{
    public class HikeDetailsBuilder: DetailsBuilder
    {
        public IHikeDetailsService DetailsService { get; set; }
        public HikeDetailsBuilder(IHikeDetailsService detailsService, CacheService cache): base(cache)
        {
            DetailsService = detailsService;
        }
        public override void Build(string url, HikeSummary hikeSummary)
        {
            string cacheKey = CacheUtility.GetCacheKey(url);
            if (!Cache.PopulateDetails(cacheKey, hikeSummary))
            {
                hikeSummary.HikeAndTripDetails.HikeDetails = DetailsService.GetHikeInformation(url);
                Cache.AddDetails(cacheKey, hikeSummary);
            }
        }
    }
}
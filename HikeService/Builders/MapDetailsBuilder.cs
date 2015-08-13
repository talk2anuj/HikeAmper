using CommonModels.Hike;
using HikeService.CacheManagement;
using HikeService.CacheManagement.Services;
using HikeService.MapsModule.Services;
using CacheUtility = HikeService.Utilities.CacheUtility;

namespace HikeService.Builders
{
    public class MapDetailsBuilder: DetailsBuilder
    {
        public IMapsService DetailsService { get; set; }
        public MapDetailsBuilder(IMapsService detailsService, CacheService cache): base(cache)
        {
            DetailsService = detailsService;
        }
        public override void Build(string url, HikeSummary hikeSummary, bool partial)
        {
            string cacheKey = CacheUtility.GetCacheKey(url);
            if (!Cache.PopulateDetails(cacheKey, hikeSummary))
		    {
		        hikeSummary.MapDetails = DetailsService.GetMapDetails(hikeSummary.HikeAndTripDetails.HikeDetails.Location);
		        Cache.AddDetails(cacheKey, hikeSummary);
		    }
        }
    }
}
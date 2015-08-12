using CommonModels.Hike;
using HikeService.CacheManagement.Services;

namespace HikeService.Builders
{
    public abstract class DetailsBuilder
    {
        public CacheService Cache { get; set; }
        protected DetailsBuilder(CacheService cache)
        {
            Cache = cache;
        }
        public abstract void Build(string url, HikeSummary hikeSummary);
    }
}
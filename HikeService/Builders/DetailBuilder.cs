using CommonModels.Hike;
using HikeService.CacheManagement.Services;
using HikeService.DetailServices;

namespace HikeService.Builders
{
    public class DetailBuilder<T>
    {
        public IDetailService<T> DetailService { get; set; }
        public ICacheService CacheService { get; set; }
        public DetailBuilder(IDetailService<T> detailService, ICacheService cacheService)
        {
            DetailService = detailService;
            CacheService = cacheService;
        }

        public void Build(string url, HikeSummary hikeSummary)
        {
            if (!CacheService.PopulateDetails(url, hikeSummary))
            {
                DetailService.PopulateDetails(url, hikeSummary);
                CacheService.AddDetails(url, hikeSummary);
            }
        }
    }
}
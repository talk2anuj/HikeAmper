using CacheManagement;
using Common.Models.Hike;

namespace DetailServices.Builders
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

        public void Build(string user, string url, HikeSummary hikeSummary)
        {
            if (!CacheService.PopulateDetails(url, hikeSummary))
            {
                DetailService.PopulateDetails(url, hikeSummary);
                CacheService.AddDetails(hikeSummary);
            }
        }
    }
}
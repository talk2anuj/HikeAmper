using CacheManagement;
using Common.Models.Hike;

namespace DetailServices.Builders
{
    public class DetailBuilder<T>
    {
        private IDetailService<T> DetailService { get; set; }
        private ICacheService CacheService { get; set; }

        public DetailBuilder(IDetailService<T> detailService, ICacheService cacheService)
        {
            DetailService = detailService;
            CacheService = cacheService;
        }

        public void Build(string userName, string url, HikeSummary hikeSummary)
        {
            if (!CacheService.PopulateDetails(url, hikeSummary))
            {
                DetailService.PopulateDetails(url, hikeSummary, userName);
                CacheService.AddDetails(hikeSummary);
            }
        }
    }
}
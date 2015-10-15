using CommonModels.Hike;

namespace HikeService.CacheManagement.Services
{
    public interface ICacheService
    {
        bool PopulateDetails(string hike, HikeSummary summary);

        void AddDetails(string hike, HikeSummary summary);

        void ClearCache();
    }
}
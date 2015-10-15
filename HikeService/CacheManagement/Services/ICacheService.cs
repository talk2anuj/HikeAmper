using CommonModels.Hike;

namespace HikeService.CacheManagement.Services
{
    public interface ICacheService
    {
        bool PopulateDetails(string hike, HikeSummary hikeSummary);

        void AddDetails(string hike, HikeSummary hikeSummary);

        void ClearCache();
    }
}
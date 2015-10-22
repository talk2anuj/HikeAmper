using CommonModels.Hike;

namespace CacheManagement
{
    public interface ICacheService
    {
        bool PopulateDetails(string hike, HikeSummary summary);

        void AddDetails(HikeSummary summary);

        void UpdateDetails(HikeSummary summary);

        void ClearCache();
    }
}
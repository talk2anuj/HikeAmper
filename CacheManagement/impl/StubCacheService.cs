using CommonModels.Hike;

namespace CacheManagement.impl
{
    public class StubCacheService : ICacheService
    {
        public bool PopulateDetails(string hike, HikeSummary summary)
        {
            return false;
        }

        public void AddDetails(HikeSummary summary)
        {
            //do nothing
        }

        public void UpdateDetails(HikeSummary summary)
        {
            //do nothing
        }

        public void ClearCache()
        {
            //do nothing
        }
    }
}
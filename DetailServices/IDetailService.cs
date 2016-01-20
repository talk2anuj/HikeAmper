using Common.Models.Hike;

namespace DetailServices
{
    public interface IDetailService<T>
    {
        void PopulateDetails(string url, HikeSummary summary, string userName);
    }
}
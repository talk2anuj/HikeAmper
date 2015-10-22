using CommonModels.Common;
using CommonModels.Hike;

namespace DetailServices
{
    public interface IDetailService<T>
    {
        void PopulateDetails(string url, HikeSummary summary);
    }
}
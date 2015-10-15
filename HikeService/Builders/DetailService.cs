using CommonModels.Hike;

namespace HikeService.Builders
{
    public interface IDetailService<T>
    {
        void PopulateDetails(string url, HikeSummary summary);
    }
}
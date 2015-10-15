using CommonModels.Hike;

namespace HikeService.DetailServices
{
    public interface IDetailService<T>
    {
        void PopulateDetails(string url, HikeSummary summary);
    }
}
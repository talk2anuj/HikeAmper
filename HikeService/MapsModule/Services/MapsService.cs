using CommonModels.Map;

namespace HikeService.MapsModule.Services
{
    public interface IMapsService
    {
        MapDetails GetMapDetails(GeographicalLocation destination);
    }
}
using Common.Models.Storage;
using HikeService.Factories;
using Storage;
using System.Web;
using System.Web.Http;

namespace HikeService.Controllers
{
    public class ResourceController<T> : ApiController
    {
        public IDataStorageService _storageService;

        public ResourceController()
        {
            _storageService = StorageFactory.GetStorageService<T>(StorageType.AzureStorage);
        }

        public void Options()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "POST, GET, DELETE, OPTIONS");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
        }
    }

    public class InputData
    {
        public string Value;
    }
}
using Common.Models.Storage;
using HikeService.Factories;
using System.Web;
using System.Web.Http;

namespace HikeService.Controllers
{
    public class UserController : ResourceController
    {
        public void Post(string userName, [FromBody] UserData data)
        {
            var dataStorageService = StorageFactory.GetStorageService<UserDataEntity>(StorageType.AzureStorage);

            UserDataEntity entity = new UserDataEntity(userName, data.Value);
            dataStorageService.InsertEntity(entity);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        }
    }
}
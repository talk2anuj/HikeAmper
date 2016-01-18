using Common.Models.Hike;
using Common.Models.Storage;
using DetailServices.Builders;
using HikeService.Factories;
using Storage;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HikeService.Controllers
{
    public class HikesController : ApiController
    {
        public List<HikeSummary> Get(string user)
        {
            //Extend if required later: Get the HikeSummaryBuilder based on the URL
            SummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();
            IDataStorageService dataStorageService = StorageFactory.GetStorageService<HikeDataEntity>(StorageType.AzureStorage);

            List<HikeDataEntity> entities = dataStorageService.GetEntities<HikeDataEntity>(user);
            List<string> urls = new List<string>();
            urls.AddRange(entities.OrderBy(entity => entity.Timestamp).Select(entity => entity.Url));
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return urls.Select(url => summaryBuilder.Build(url, user)).ToList();
        }

        public List<HikeSummary> Post(string user, [FromBody] UserData data)
        {
            SummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();
            var dataStorageService = StorageFactory.GetStorageService<HikeDataEntity>(StorageType.AzureStorage);
            List<string> urls = new List<string>();
            if ((!string.IsNullOrEmpty(data.Value)) && data.Value.StartsWith("http://www.wta.org/") && data.Value.Contains("/go-hiking/hikes/"))
            {
                HikeDataEntity entity = new HikeDataEntity(user, data.Value);
                if (dataStorageService.InsertEntity(entity))
                {
                    urls.Add(data.Value);
                }
            }
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return urls.Select(url => summaryBuilder.Build(url, user)).ToList();
        }

        public bool Delete(string user, [FromBody] UserData data)
        {
            var dataStorageService = StorageFactory.GetStorageService<HikeDataEntity>(StorageType.AzureStorage);
            HikeDataEntity entity = new HikeDataEntity(user, data.Value);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return dataStorageService.DeleteEntity(entity);
        }

        public void Options()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "POST, GET, DELETE, OPTIONS");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
        }
    }

    public class UserData
    {
        public string Value;
    }
}
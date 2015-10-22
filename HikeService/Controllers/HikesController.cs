using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using CommonModels.Common;
using CommonModels.Hike;
using CommonModels.Storage;
using DetailServices.Builders;
using HikeService.Factories;
using Storage;

namespace HikeService.Controllers
{
	public class HikesController : ApiController
	{
        public List<HikeSummary> Get(string type, string user, string continuationToken)
		{
            //Extend if required later: Get the HikeSummaryBuilder based on the URL
            SummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();
		    IDataStorageService dataStorageService = StorageFactory.GetStorageService<HikeDataEntity>(StorageType.AzureStorage);

		    List<HikeDataEntity> entities = dataStorageService.GetEntities<HikeDataEntity>(user);
            List<string> urls = new List<string>();
            urls.AddRange(entities.OrderBy(entity => entity.Timestamp).Select(entity => entity.Url));

            return urls.Select(url => summaryBuilder.Build(url, user)).ToList();
		}

		public bool Post (string type, string user, [FromBody] UserData data)
		{
		    var dataStorageService = StorageFactory.GetStorageService<HikeDataEntity>(StorageType.AzureStorage);
		    if ((!string.IsNullOrEmpty(data.Value)) && data.Value.StartsWith("http://www.wta.org/") && data.Value.Contains("/go-hiking/hikes/"))
		    {
                HikeDataEntity entity = new HikeDataEntity(user, data.Value);
                return dataStorageService.InsertEntity(entity);
		    }
		    return false;
		}

	    public bool Delete(string type, string user, [FromBody] UserData data)
	    {
            var dataStorageService = StorageFactory.GetStorageService<HikeDataEntity>(StorageType.AzureStorage);
            HikeDataEntity entity = new HikeDataEntity(user, data.Value);
            return dataStorageService.DeleteEntity(entity);
	    }
	}

	public class UserData
	{
		public string Value;
	}
}
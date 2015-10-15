using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using CommonModels.Hike;
using HikeService.Builders;
using HikeService.Factories;
using HikeService.Storage.Services;

namespace HikeService.HikesModule.Controllers
{
	public class HikesController : ApiController
	{
        public List<HikeSummary> Get(string type, string user, string continuationToken)
		{
            //Extend if required later: Get the HikeSummaryBuilder based on the url
            SummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();
		    IDataStorageService dataStorageService = StorageFactory.GetStorageService();

		    List<string> urls = dataStorageService.GetUrls(type, user);
            //Minimal build all urls

            //Based on continuation token, full build just few and return those only
            int index = int.Parse(continuationToken) * 8;
            if (index > 0)
            {
                int count = index + 8 <= urls.Count ? 8 : urls.Count - index;
                urls = urls.GetRange(index, count);
                Thread.Sleep(60000);
                return urls.Select(url => summaryBuilder.Build(url, false)).ToList();
            }
            return urls.Select(url => summaryBuilder.Build(url, true)).ToList();
		}

		public bool Post (string type, string user, [FromBody] UserData data)
		{
		    IDataStorageService dataStorageService = StorageFactory.GetStorageService();
		    if ((!string.IsNullOrEmpty(data.Value)) && data.Value.StartsWith("http://www.wta.org/") && data.Value.Contains("/go-hiking/hikes/"))
		    {
		        return dataStorageService.WriteUrl(user, type, data.Value);
		    } else {
                return false;
		    }
		}

	    public bool Delete(string type, string user, [FromBody] UserData data)
	    {
            IDataStorageService dataStorageService = StorageFactory.GetStorageService();
	        return dataStorageService.DeleteUrl(user, type, data.Value);
	    }
	}

	public class UserData
	{
		public string Value;
	}
}
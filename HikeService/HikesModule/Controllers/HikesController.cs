using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CommonModels.Hike;
using HikeService.Builders;
using HikeService.Factories;
using HikeService.Storage.Services;

namespace HikeService.HikesModule.Controllers
{
	public class HikesController : ApiController
	{
		public List<HikeSummary> Get (string type, string user)
		{
            //Extend if required later: Get the HikeSummaryBuilder based on the url
            HikeSummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();
		    IDataStorageService dataStorageService = ServiceFactory.GetStorageService();

		    List<string> urls = dataStorageService.GetUrls(type, user);

		    return urls.Select(url => summaryBuilder.Build(url)).ToList();
		}

		public bool Post (string type, string user, [FromBody] UserData data)
		{
		    IDataStorageService dataStorageService = ServiceFactory.GetStorageService();
		    if ((!string.IsNullOrEmpty(data.Value)) && data.Value.StartsWith("http://www.wta.org/") && data.Value.Contains("/go-hiking/hikes/"))
		    {
		        return dataStorageService.WriteUrl(user, type, data.Value);
		    } else {
                return false;
		    }
		}

	    public bool Delete(string type, string user, [FromBody] UserData data)
	    {
            IDataStorageService dataStorageService = ServiceFactory.GetStorageService();
	        return dataStorageService.DeleteUrl(user, type, data.Value);
	    }
	}

	public class UserData
	{
		public string Value;
	}
}
using Common.Models.Hike;
using Common.Models.Storage;
using DetailServices.Builders;
using HikeService.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HikeService.Controllers
{
    public class HikesController : ResourceController<HikeDataEntity>
    {
        public List<HikeSummary> Get(string user)
        {
            //Extend if required later: Get the HikeSummaryBuilder based on the URL
            SummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();

            List<HikeDataEntity> entities = _storageService.GetEntities<HikeDataEntity>(user);
            List<string> urls = new List<string>();
            urls.AddRange(entities.OrderBy(entity => entity.Timestamp).Select(entity => entity.Url));
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return urls.Select(url => summaryBuilder.Build(url, user)).ToList();
        }

        public List<HikeSummary> Post(string user, [FromBody] InputData data)
        {
            SummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();
            List<string> urls = new List<string>();
            if ((!string.IsNullOrEmpty(data.Value)) && data.Value.StartsWith("http://www.wta.org/") && data.Value.Contains("/go-hiking/hikes/"))
            {
                HikeDataEntity entity = new HikeDataEntity(user, data.Value);
                if (_storageService.InsertEntity(entity))
                {
                    urls.Add(data.Value);
                }
            }
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return urls.Select(url => summaryBuilder.Build(url, user)).ToList();
        }

        public bool Delete(string user, [FromBody] InputData data)
        {
            HikeDataEntity entity = new HikeDataEntity(user, data.Value);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            return _storageService.DeleteEntity(entity);
        }
    }
}
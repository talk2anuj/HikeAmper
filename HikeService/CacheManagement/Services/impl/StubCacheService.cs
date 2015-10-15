using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonModels.Hike;

namespace HikeService.CacheManagement.Services.impl
{
    public class StubCacheService : ICacheService
    {
        public bool PopulateDetails(string hike, HikeSummary summary)
        {
            return false;
        }

        public void AddDetails(string hike, HikeSummary summary)
        {
            //do nothing
        }

        public void ClearCache()
        {
            //do nothing
        }
    }
}
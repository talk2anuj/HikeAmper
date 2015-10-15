using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonModels.Hike;

namespace HikeService.CacheManagement.Services.impl
{
    public class CacheStubService : ICacheService
    {
        public bool PopulateDetails(string hike, HikeSummary hikeSummary)
        {
            return false;
        }

        public void AddDetails(string hike, HikeSummary hikeSummary)
        {
            //do nothing
        }

        public void ClearCache()
        {
            //do nothing
        }
    }
}
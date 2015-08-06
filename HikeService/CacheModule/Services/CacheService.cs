using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HikeService.HikesModule.Models;
using StackExchange.Redis;

namespace HikeService.CacheModule.Services
{
    public abstract class CacheService
    {
        public IDatabase cache;

        public CacheService(IDatabase cache)
        {
            this.cache = cache;
        }
        public abstract bool PopulateDetails(string hike, HikeSummary hikeSummary);

        public abstract void AddDetails(string hike, HikeSummary hikeSummary);
    }
}
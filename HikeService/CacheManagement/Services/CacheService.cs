using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using CommonModels.Hike;
using StackExchange.Redis;

namespace HikeService.CacheManagement.Services
{
    public abstract class CacheService
    {
        public string CacheName;
        public IDatabase Cache;

        public CacheService(string cacheName)
        {
            CacheName = cacheName;
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings[cacheName].ConnectionString);
            Cache = connection.GetDatabase();
        }
        public abstract bool PopulateDetails(string hike, HikeSummary hikeSummary);

        public abstract void AddDetails(string hike, HikeSummary hikeSummary);

        public void ClearCache()
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings[CacheName].ConnectionString);
            var endpoints = connection.GetEndPoints();
            var server = connection.GetServer(endpoints.First());
            var keys = server.Keys();
            foreach (var key in keys)
            {
                Cache.KeyDelete(key);
            }
        }
    }
}
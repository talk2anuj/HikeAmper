using CacheManagement;
using Common.Models.Hike;
using Common.Models.Storage;
using DetailServices.Builders;
using Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UpdateDetailService.Factories;

namespace UpdateDetailService
{
    internal class Program
    {
        private static void Main()
        {
            var hikeDataEntities = GetHikeDataEntities();
            var hikeSummaryList = GetHikeSummaryList(hikeDataEntities);
            var count = 0;
            foreach (var hikeSummary in hikeSummaryList)
            {
                if (count == 9)
                {
                    count = 0;
                    Thread.Sleep(60000);
                }
                UpdateDetails(hikeSummary);
                count = count + 1;
            }
        }

        private static List<HikeSummary> GetHikeSummaryList(List<HikeDataEntity> entities)
        {
            SummaryBuilder summaryBuilder = BuilderFactory.GetHikeSummaryBuilder();
            List<string> urls = new List<string>();
            urls.AddRange(entities.Select(entity => entity.Url));
            List<HikeSummary> summaryList = urls.Select(url => summaryBuilder.Build(url)).ToList();
            return summaryList;
        }

        private static List<HikeDataEntity> GetHikeDataEntities()
        {
            IDataStorageService dataStorageService = StorageFactory.GetStorageService<HikeDataEntity>(StorageType.AzureStorage);
            List<HikeDataEntity> entities = dataStorageService.GetAllTableEntities<HikeDataEntity>();
            return entities;
        }

        public static void UpdateDetails(HikeSummary hikeSummary)
        {
            CacheFactory.GetHikeCacheService(CacheType.AzureStorage).UpdateDetails(hikeSummary);
            CacheFactory.GetTripCacheService(CacheType.AzureStorage).UpdateDetails(hikeSummary);
            CacheFactory.GetWeatherCacheService(CacheType.AzureStorage).UpdateDetails(hikeSummary);
            CacheFactory.GetMapCacheService(CacheType.AzureStorage).UpdateDetails(hikeSummary);
        }
    }
}
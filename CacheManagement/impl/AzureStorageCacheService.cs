using Common.Models.Hike;
using Common.Models.Storage;
using Newtonsoft.Json;
using Storage.impl;

namespace CacheManagement.impl
{
    public class AzureStorageCacheService<T> : ICacheService
    {
        private AzureDataStorageService Storage { get; }

        public AzureStorageCacheService(string storageName, string tableName)
        {
            Storage = new AzureDataStorageService(storageName, tableName);
        }

        public bool PopulateDetails(string hike, HikeSummary summary)
        {
            var entity = new DetailEntity<T>(hike);
            var tableEntity = Storage.GetEntity(entity);
            if (tableEntity.Result != null)
            {
                var result = ((DetailEntity<T>)tableEntity.Result);
                var detail = result.Detail;
                summary.SetDetail((JsonConvert.DeserializeObject<T>(detail)));
                return true;
            }
            return false;
        }

        public void AddDetails(HikeSummary summary)
        {
            var detail = summary.GetDetail<T>();
            var entity = new DetailEntity<T>(summary.Url, detail);
            Storage.InsertEntity(entity);
        }

        public void UpdateDetails(HikeSummary summary)
        {
            var detail = summary.GetDetail<T>();
            var entity = new DetailEntity<T>(summary.Url, detail);
            Storage.UpdateEntity(entity);
        }

        public void ClearCache()
        {
            Storage.DeleteTable();
        }
    }
}
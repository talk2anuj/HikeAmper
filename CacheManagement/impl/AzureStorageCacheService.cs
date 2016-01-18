using Common.Models.Hike;
using Common.Models.Storage;
using Newtonsoft.Json;
using Storage.impl;
using System;

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
            //TODO: Add a helper function to check if entity exists in table
            var entity = new DetailEntity<T>(hike);
            var tableEntity = Storage.GetEntity(entity);
            if (tableEntity.Result != null)
            {
                var result = ((DetailEntity<T>)tableEntity.Result);
                if (IsValid(result))
                {
                    var detail = result.Detail;
                    summary.SetDetail((JsonConvert.DeserializeObject<T>(detail)));
                    return true;
                }
            }
            return false;
        }

        public void AddDetails(HikeSummary summary)
        {
            var detail = summary.GetDetail<T>();

            if (HasDetails(summary.Url))
            {
                UpdateDetails(summary);
            }
            else
            {
                var entity = new DetailEntity<T>(summary.Url, detail);
                Storage.InsertEntity(entity);
            }
        }

        private bool HasDetails(string url)
        {
            var entity = new DetailEntity<T>(url);
            var tableEntity = Storage.GetEntity(entity);
            return tableEntity.Result != null;
        }

        public void UpdateDetails(HikeSummary summary)
        {
            var detail = summary.GetDetail<T>();
            var entity = new DetailEntity<T>(summary.Url, detail);
            Storage.UpdateEntity(entity);
        }

        private bool IsValid(DetailEntity<T> result)
        {
            return result.Timestamp.Date == DateTime.Now.Date;
        }

        public void ClearCache()
        {
            Storage.DeleteTable();
        }
    }
}
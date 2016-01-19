using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace Common.Models.Storage
{
    public class DetailEntity<T> : TableEntity
    {
        public string Detail { get; set; }

        public DetailEntity()
        {
        }

        public DetailEntity(string partitionKey)
        {
            PartitionKey = StorageUtility.GetHikeName(partitionKey);
            RowKey = StorageUtility.GetHikeName(partitionKey);
        }

        public DetailEntity(string partitionKey, T detail)
        {
            PartitionKey = StorageUtility.GetHikeName(partitionKey);
            RowKey = StorageUtility.GetHikeName(partitionKey);
            Detail = JsonConvert.SerializeObject(detail);
        }
    }
}
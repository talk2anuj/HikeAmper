using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace CommonModels.Storage
{
    public class DetailEntity<T> : TableEntity
    {
        public string Detail { get; set; }
        public DetailEntity()
        {
        }

        public DetailEntity(string partitionKey)
        {
            PartitionKey = StorageUtility.ParseRowKey(partitionKey);
            RowKey = StorageUtility.ParseRowKey(partitionKey);
        }

        public DetailEntity(string partitionKey, T detail)
        {
            PartitionKey = StorageUtility.ParseRowKey(partitionKey);
            RowKey = StorageUtility.ParseRowKey(partitionKey);
            Detail = JsonConvert.SerializeObject(detail);
        }
    }
}
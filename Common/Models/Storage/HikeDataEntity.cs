using Microsoft.WindowsAzure.Storage.Table;

namespace Common.Models.Storage
{
    public class HikeDataEntity : TableEntity
    {
        public string Url { get; set; }

        public HikeDataEntity(string user, string fullUrl)
        {
            Url = fullUrl;
            PartitionKey = user;
            RowKey = StorageUtility.ParseRowKey(fullUrl);
        }

        public HikeDataEntity()
        {
        }
    }
}
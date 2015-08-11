using HikeService.Storage.Utilities;
using Microsoft.WindowsAzure.Storage.Table;

namespace HikeService.Storage.Models
{
    public class HikeDataEntity: TableEntity
    {
        public string User { get; set; }    
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
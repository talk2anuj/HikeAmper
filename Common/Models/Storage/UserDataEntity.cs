using Common.Models.Map;
using Microsoft.WindowsAzure.Storage.Table;

namespace Common.Models.Storage
{
    public class UserDataEntity : TableEntity
    {
        public string User { get; set; }
        public string Zip { get; set; }

        public UserDataEntity(string user, PhysicalAddress address)
        {
            Zip = address.Zip;
            PartitionKey = user;
            RowKey = address.Zip;
        }

        public UserDataEntity()
        {
        }
    }
}
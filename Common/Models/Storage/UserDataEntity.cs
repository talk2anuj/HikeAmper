using Microsoft.WindowsAzure.Storage.Table;

namespace Common.Models.Storage
{
    public class UserDataEntity : TableEntity
    {
        public UserDataEntity(string user, string zipCode)
        {
            PartitionKey = user;
            RowKey = zipCode;
        }

        public UserDataEntity()
        {
        }
    }
}
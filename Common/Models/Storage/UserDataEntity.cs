using Microsoft.WindowsAzure.Storage.Table;

namespace Common.Models.Storage
{
    public class UserDataEntity : TableEntity
    {
        public string ZipCode { get; set; }

        public UserDataEntity(string userName, string zipCode)
        {
            PartitionKey = userName;
            RowKey = userName;
            ZipCode = zipCode;
        }

        public UserDataEntity(string userName)
        {
            PartitionKey = userName;
            RowKey = userName;
        }

        public UserDataEntity()
        {
        }
    }
}
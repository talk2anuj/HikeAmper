using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using HikeService.MapsModule.Models;
using HikeService.StorageModule.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace HikeService.StorageModule.Services.impl
{
    public class AzureDataStorageService: DataStorageService
    {
        private const string HikesTableName = "HikesTable";
        private const string UserDataTableName = "UserDataTable";
        public CloudTableClient Client { get; set; }
        public CloudTable HikesTable { get; set; }
        public CloudTable UserDataTable { get; set; }
        public AzureDataStorageService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["hikeservicestorage"].ConnectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            Client = storageAccount.CreateCloudTableClient();
            InitializeTables();
        }

        private void InitializeTables()
        {
            CloudTable htable = Client.GetTableReference(HikesTableName);
            htable.CreateIfNotExists();
            HikesTable = htable;

            CloudTable utable = Client.GetTableReference(UserDataTableName);
            utable.CreateIfNotExists();
            UserDataTable = utable;
        }

        public bool WriteUserData(string user, PhysicalAddress address)
        {
            UserDataEntity e = new UserDataEntity(user, address);
            bool result = false;
            if (!HasEntity(UserDataTable, e))
            {
                result = InsertEntity(UserDataTable, e);
            }

            return result;
        }

        public bool WriteUrl(string user, string type, string url)
        {   
            HikeDataEntity e = new HikeDataEntity(user, url);
            bool result = false;
            if (!HasEntity(HikesTable, e))
            {
                result = InsertEntity(HikesTable, e);
            }

            return result;      
        }

        public bool DeleteUrl(string user, string type, string url)
        {
            HikeDataEntity e = new HikeDataEntity(user, url);
            bool result = false;
            if (HasEntity(HikesTable, e))
            {
                result = DeleteEntity(HikesTable, e);
            }

            return result;  
        }

        public List<string> GetUrls(string type, string user)
        {
            List<string> urls = new List<string>();
            if (HikesTable.Exists())
            {
                var query = new TableQuery<HikeDataEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user));
                var entities = HikesTable.ExecuteQuery(query).ToList();
                urls.AddRange(entities.Select(entity => entity.Url));
            }
            
            return urls;
        }

        public UserDataEntity GetUserData(string user)
        {
            if (UserDataTable.Exists())
            {
                var query = new TableQuery<UserDataEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user));
                var entity = UserDataTable.ExecuteQuery(query).ToList();
                return entity.First();
            }

            return null;
        }

        private bool HasEntity(CloudTable table, TableEntity e)
        {
            TableOperation op = TableOperation.Retrieve<TableEntity>(e.PartitionKey, e.RowKey);
            TableResult entity = table.Execute(op);
            return (entity.Result != null);
        }

        private bool InsertEntity(CloudTable table, TableEntity e)
        {
            TableOperation op = TableOperation.Insert(e);
            TableResult entity = table.Execute(op);
            return (entity.Result != null);
        }

        private bool DeleteEntity(CloudTable table, TableEntity e)
        {
            e.ETag = "*";
            TableOperation op = TableOperation.Delete(e);
            TableResult entity = table.Execute(op);
            return (entity.Result != null);
        }
    }    
}
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Storage.impl
{
    public class AzureDataStorageService : IDataStorageService
    {
        public CloudTableClient Client { get; set; }
        public CloudTable Table { get; set; }

        public AzureDataStorageService(string storageAccountName, string tableName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[storageAccountName].ConnectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            Client = storageAccount.CreateCloudTableClient();
            InitializeTable(tableName);
        }

        private void InitializeTable(string tableName)
        {
            CloudTable table = Client.GetTableReference(tableName);
            table.CreateIfNotExists();
            Table = table;
        }

        //        public bool WriteUserData(string user, PhysicalAddress address)
        //        {
        //            UserDataEntity e = new UserDataEntity(user, address);
        //            bool result = false;
        //            if (!HasEntity(UserDataTable, e))
        //            {
        //                result = InsertEntity(UserDataTable, e);
        //            }
        //
        //            return result;
        //        }

        //        public UserDataEntity GetUserData(string user)
        //        {
        //            if (UserDataTable.Exists())
        //            {
        //                var query = new TableQuery<UserDataEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, user));
        //                var entity = UserDataTable.ExecuteQuery(query).ToList();
        //                return entity.First();
        //            }
        //
        //            return null;
        //        }

        public bool InsertEntity<T>(T entity)
        {
            bool result = false;
            var tableEntity = entity as TableEntity;
            if (!HasEntity(tableEntity))
            {
                result = InsertEntity(tableEntity);
            }

            return result;
        }

        public bool UpdateEntity<T>(T entity)
        {
            var tableEntity = entity as TableEntity;
            return UpdateEntity(tableEntity);
        }

        public bool DeleteEntity<T>(T entity)
        {
            bool result = false;
            var tableEntity = entity as TableEntity;
            if (HasEntity(tableEntity))
            {
                result = DeleteEntity(tableEntity);
            }

            return result;
        }

        public List<T> GetEntities<T>(string partitionKey) where T : TableEntity, new()
        {
            if (Table.Exists())
            {
                var query = new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
                return Table.ExecuteQuery(query).ToList();
            }
            return null;
        }

        public List<T> GetAllTableEntities<T>() where T : TableEntity, new()
        {
            if (Table.Exists())
            {
                return Table.ExecuteQuery(new TableQuery<T>()).ToList();
            }
            return null;
        }

        public TableResult GetEntity<T>(T entity) where T : TableEntity
        {
            if (Table.Exists())
            {
                TableOperation op = TableOperation.Retrieve<T>(entity.PartitionKey, entity.RowKey);
                return Table.Execute(op);
            }
            return null;
        }

        private bool HasEntity(TableEntity tableEntity)
        {
            var entity = GetEntity(tableEntity);
            return (entity.Result != null);
        }

        private bool InsertEntity(TableEntity e)
        {
            TableOperation op = TableOperation.Insert(e);
            var entity = Table.Execute(op);
            return (entity.Result != null);
        }

        private bool UpdateEntity(TableEntity e)
        {
            TableOperation op = TableOperation.InsertOrReplace(e);
            var entity = Table.Execute(op);
            return (entity.Result != null);
        }

        private bool DeleteEntity(TableEntity e)
        {
            e.ETag = "*";
            TableOperation op = TableOperation.Delete(e);
            TableResult entity = Table.Execute(op);
            return (entity.Result != null);
        }

        public void DeleteTable()
        {
            Table.DeleteIfExists();
        }
    }
}
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace Storage
{
	public interface IDataStorageService
	{
		bool InsertEntity<T>(T entity);
        bool DeleteEntity<T>(T entity);
	    TableResult GetEntity<T>(T entity) where T : TableEntity;
        List<T> GetEntities<T>(string partitionKey) where T : TableEntity, new();
	    List<T> GetAllTableEntities<T>() where T : TableEntity, new();
	    void UpdateEntity<T>(T entity);
        void DeleteTable();
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage.Table;

namespace Storage.impl
{
	public class FileDataStorageService : IDataStorageService
	{
		private const string FileExtension = ".txt";
		public bool WriteUrl(string user, string url)
		{
            // result tells if URL already exits in the file or not
            var result = false;
			StreamWriter file;
			string fileName = GetFileName(user);
			bool fileExists = File.Exists(fileName);
			if (!fileExists)
			{
				file = File.CreateText(fileName);
				file.Write(url + Environment.NewLine);
                result = true;
			} else {
				file = File.AppendText(fileName);
				file.Write(url + Environment.NewLine);
			}
			file.Close();
            return result;
		}

	    public bool DeleteUrl(string user, string url)
	    {
	        throw new NotImplementedException();
	    }

	    private string GetFileName(string user)
		{
			return @"E:\MyProjects\Files\" + user + FileExtension;
		}
		public List<string> GetUrls(string user)
		{
            string fileName = GetFileName(user);
            return GetUrlsFromFile(fileName);
        }

        private List<string> GetUrlsFromFile(string fileName)
        {
            List<string> urls = new List<string>();
            if (File.Exists(fileName))
            {
                urls = new List<string>(File.ReadAllLines(fileName));
            }
            return urls;
        }

	    public bool InsertEntity<T>(T entity)
	    {
	        throw new NotImplementedException();
	    }

	    public bool DeleteEntity<T>(T entity)
	    {
	        throw new NotImplementedException();
	    }

	    public TableResult GetEntity<T>(T entity) where T : TableEntity
        {
	        throw new NotImplementedException();
	    }

	    public List<T> GetEntities<T>(string partitionKey) where T : TableEntity, new()
	    {
	        throw new NotImplementedException();
	    }

	    public List<T> GetAllTableEntities<T>() where T : TableEntity, new()
	    {
	        throw new NotImplementedException();
	    }

	    public void UpdateEntity<T>(T entity)
	    {
	        throw new NotImplementedException();
	    }

	    public void DeleteTable()
	    {
	        throw new NotImplementedException();
	    }
	}
}
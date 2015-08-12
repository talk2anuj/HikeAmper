using System;
using System.Collections.Generic;
using System.IO;

namespace HikeService.Storage.Services.impl
{
	public class FileDataStorageService : IDataStorageService
	{
		private const string FileExtension = ".txt";
		public bool WriteUrl(string user, string type, string url)
		{
            // result tells if URL already exits in the file or not
            var result = false;
			StreamWriter file = null;
			string fileName = GetFileName(user);
			bool fileExists = File.Exists(fileName);
			if (!fileExists)
			{
				file = File.CreateText(fileName);
				file.Write(url + Environment.NewLine);
                result = true; ;
			} else {
				file = File.AppendText(fileName);
				file.Write(url + Environment.NewLine);
			}	
			file.Close();
            return result;
		}

	    public bool DeleteUrl(string user, string type, string url)
	    {
	        throw new NotImplementedException();
	    }

	    private string GetFileName(string user)
		{
			return @"E:\MyProjects\Files\" + user + FileExtension;
		}
		public List<string> GetUrls(string type, string user)
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
    }
}
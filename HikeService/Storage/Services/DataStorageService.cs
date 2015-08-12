using System.Collections.Generic;

namespace HikeService.Storage.Services
{
	public interface IDataStorageService
	{		
		bool WriteUrl(string user, string type, string url);
        bool DeleteUrl(string user, string type, string url);
		List<string> GetUrls(string type, string user);		
	}
}
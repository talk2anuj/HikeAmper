using HikeService.Storage.Services;
using HikeService.Storage.Services.impl;

namespace HikeService.Factories
{
    public class StorageFactory
    {
        private static AzureDataStorageService _azureStorageService;

        static StorageFactory()
        {
            _azureStorageService = new AzureDataStorageService();
        }

        public static IDataStorageService GetStorageService()
        {
            return _azureStorageService;
        }
    }
}
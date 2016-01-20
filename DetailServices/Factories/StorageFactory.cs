using Common;
using Common.Models.Storage;
using Storage;
using Storage.impl;

namespace DetailServices.Factories
{
    public class StorageFactory
    {
        private static AzureDataStorageService _azureHikeStorageService;
        private static AzureDataStorageService _azureUserStorageService;

        private static FileDataStorageService _fileHikeStorageService;

        static StorageFactory()
        {
            _azureHikeStorageService = new AzureDataStorageService(Constants.StorageAccountName, Constants.HikesTableName);
            _azureUserStorageService = new AzureDataStorageService(Constants.StorageAccountName, Constants.UserDataTableName);
            _fileHikeStorageService = new FileDataStorageService();
        }

        public static IDataStorageService GetStorageService<T>(StorageType type)
        {
            switch (type)
            {
                case StorageType.AzureStorage:
                    return GetAzureStorageService<T>();

                case StorageType.FileStorage:
                    return _fileHikeStorageService;

                default:
                    return _azureHikeStorageService;
            }
        }

        private static IDataStorageService GetAzureStorageService<T>()
        {
            if (typeof(T) == typeof(HikeDataEntity))
            {
                return _azureHikeStorageService;
            }

            if (typeof(T) == typeof(UserDataEntity))
            {
                return _azureUserStorageService;
            }

            return null;
        }
    }
}
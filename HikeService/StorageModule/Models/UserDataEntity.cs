using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HikeService.MapsModule.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace HikeService.StorageModule.Models
{
    public class UserDataEntity: TableEntity
    {
        public string User { get; set; }    
        public string Zip { get; set; }

        public UserDataEntity(string user, PhysicalAddress address)
        {
            Zip = address.Zip;
            PartitionKey = user;
            RowKey = address.Zip;
        }

        public UserDataEntity()
        {
        }
    }
}
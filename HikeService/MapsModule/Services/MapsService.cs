using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HikeService.MapsModule.Models;

namespace HikeService.MapsModule.Services
{
    public interface MapsService
    {
        MapDetails GetMapDetails(GeographicalLocation destination);
    }
}
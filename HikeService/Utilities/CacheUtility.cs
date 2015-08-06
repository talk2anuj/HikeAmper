using System;

namespace HikeService.Utilities
{
    public static class CacheUtility
    {
        public static double GetMinutesToExpiry()
        {
            DateTime Now = DateTime.Now;
            DateTime Eod = new DateTime(Now.Year, Now.Month, Now.Day, 23, 59, 59);
            return (Eod - Now).TotalMinutes;
        }

    }
}
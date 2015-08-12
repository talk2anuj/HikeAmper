using System;

namespace HikeService.Utilities
{
    public static class CacheUtility
    {
        public static double GetMinutesToExpiry()
        {
            DateTime now = DateTime.Now;
            DateTime eod = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            return (eod - now).TotalMinutes;
        }

        public static string GetCacheKey(string url)
        {
            var parts = url.Split('/');
            return parts[parts.Length - 1];
        }
    }
}
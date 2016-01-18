namespace Common
{
    public static class StorageUtility
    {
        public static string ParseRowKey(string fullUrl)
        {
            string name = null;
            string[] tokens = fullUrl.Split('/');
            if (tokens.Length > 0)
            {
                name = tokens[tokens.Length - 1];
            }
            return name;
        }
    }
}
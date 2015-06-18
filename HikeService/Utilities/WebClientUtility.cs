using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace HikeService.Utilities
{
    public static class WebClientUtility
    {
        public static HtmlDocument GetHtmlDocument(string url)
        {
            HtmlDocument doc = new HtmlDocument();
            WebClient client = new WebClient();
            string htmlData = client.DownloadString(url);
            doc.LoadHtml(htmlData);
            return doc;
        }

        public static string GetJsonString(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            string responseFromServer = null;
            if (dataStream != null)
            {
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
            }
            return responseFromServer;
        }
    }
}
using System;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace DetailServices.Utilities
{
    public static class WebClientUtility
    {
        public static HtmlDocument GetHtmlDocument(string url)
        {
            try
            {
                HtmlDocument doc = new HtmlDocument();
                WebClient client = new WebClient();
                string htmlData = client.DownloadString(url);
                doc.LoadHtml(htmlData);
                return doc;
            }
            catch (Exception e)
            {
                //Log errors using something
                return null;
            }
        }

        public static string GetJsonString(string url)
        {
            try
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
            catch (Exception e)
            {
                //Log error
            }
            return null;
        }
    }
}
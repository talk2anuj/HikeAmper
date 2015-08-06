﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using HikeService.HikesModule.Models;
using HikeService.MapsModule.Models;
using HtmlAgilityPack;

namespace HikeService.Utilities
{
    public static class HikeDocumentUtility
    {
        private static string _distanceElement = "distance";
        private static string _latitude = "latitude";
        private static string _longitude = "longitude";
        private static string _hikeStats = "hike-stats";
        private static string _typeOfHike = "Type of Hike";
        private static string _trail = "Trail";
        private static string _road = "Road";
        private static string _bugs = "Bugs";
        private static string _snow = "Snow";
        private static string _date = "date-and-author";
        private static string _notAvailable = "Not Available";


        public static T GetInfoFromDocument<T>(Func<HtmlDocument, T> func, HtmlDocument doc)
        {
            try
            {
                T result = func(doc);
            }
            catch (Exception e)
            {
                //Log error
            }
            return default(T);
        }
        public static string GetName(HtmlDocument doc)
        {
            var name = _notAvailable;
            try
            {
                var node = doc.DocumentNode.SelectSingleNode("//h1[@class='documentFirstHeading']");
                name = node.InnerText;
            }
            catch (Exception e)
            {
                //Log error
            }
            return name;
        }

        public static double GetElevation(HtmlDocument doc)
        {
            var elevation = 0.0;
            try
            {
                var element = doc.GetElementbyId(_hikeStats);
                var nodes = element.Descendants("div").Where(d => d.Attributes.Contains("class"));
                foreach (var node in nodes)
                {
                    var text = node.InnerText;
                    if (node.InnerText.Contains("Elevation"))
                    {
                        string elevationStr = node.SelectSingleNode("div").SelectSingleNode("span").InnerText;
                        elevation = double.Parse(elevationStr);
                    }
                }
            }
            catch (Exception e)
            {
                //Log error
            }
            return elevation;
        }

        public static double GetRoundTripLength(HtmlDocument doc)
        {
            var rountTripLength = 0.0;
            try
            {
                var element = doc.GetElementbyId(_distanceElement);
                var node = element.SelectSingleNode("span");
                rountTripLength = double.Parse(node.InnerText);
            }
            catch (Exception e)
            {
                //Log error
            }
            return rountTripLength;
        }

        public static GeographicalLocation GetLocation(HtmlDocument doc)
        {
            GeographicalLocation location = new GeographicalLocation();
            try
            {
                var latitude = doc.DocumentNode.SelectSingleNode("//span[@class='" + _latitude + "']");
                var longitude = doc.DocumentNode.SelectSingleNode("//span[@class='" + _longitude + "']");
                location.Latitude = double.Parse(latitude.InnerText);
                location.Longitude = double.Parse(longitude.InnerText);
            }
            catch (Exception e)
            {
                //Log error
            }
            return location;
        }

        public static HikeDetails GetHikeDetails(string url, HtmlDocument doc)
        {
            var hikeDetails = new HikeDetails();
            hikeDetails.Url = url;
            hikeDetails.Name = HikeDocumentUtility.GetName(doc);
            hikeDetails.Elevation = HikeDocumentUtility.GetElevation(doc);
            hikeDetails.RoundTripLength = HikeDocumentUtility.GetRoundTripLength(doc);
            hikeDetails.Location = HikeDocumentUtility.GetLocation(doc);
            hikeDetails.TripReportsUrl = url + "/@@related_tripreport_listing";
            return hikeDetails;
        }

        public static TripDetails GetTripReportDetails(HtmlDocument doc)
        {
            TripDetails tripDetails = new TripDetails();
            try
            {
                var node = doc.DocumentNode.SelectSingleNode("//a[contains(@class,'full-report-link')]");
                var latestTripReportUrl = node.Attributes["href"].Value;
                PopulateTripReportDetails(tripDetails, latestTripReportUrl);
            }
            catch (Exception e)
            {
                //Log error
            }
            return tripDetails;
        }

        private static void PopulateTripReportDetails(TripDetails tripDetails, string url)
        {
            HtmlDocument doc = WebClientUtility.GetHtmlDocument(url);
            tripDetails.Url = url;
            tripDetails.Date = GetTripReportDate(tripDetails, doc);
            var node = doc.GetElementbyId("trip-conditions");
            var conditions = node.Descendants("div");
            foreach (var condition in conditions)
            {
                try
                {
                    var nodeTypeText = condition.SelectSingleNode("h4").InnerText;
                    var nodeValueText = condition.SelectSingleNode("span").InnerText;
                    if (_typeOfHike.Equals(nodeTypeText))
                    {
                        tripDetails.TypeOfHike = nodeValueText;
                    }
                    if (_trail.Equals(nodeTypeText))
                    {
                        tripDetails.TrailCondition = nodeValueText;
                    }
                    if (_road.Equals(nodeTypeText))
                    {
                        tripDetails.RoadCondition = nodeValueText;
                    }
                    if (_bugs.Equals(nodeTypeText))
                    {
                        tripDetails.BugsCondition = nodeValueText;
                    }
                    if (_snow.Equals(nodeTypeText))
                    {
                        tripDetails.SnowCondition = nodeValueText;
                    }
                }
                catch (Exception e)
                {
                    //Log error
                }
            }
        }

        private static string GetTripReportDate(TripDetails tripDetails, HtmlDocument doc)
        {
            var date = "";
            try
            {
                var dateText = doc.DocumentNode.SelectSingleNode("//span[contains(@class, '" + _date + "')]").InnerText;
                dateText = dateText.Substring(dateText.IndexOf(", on", StringComparison.OrdinalIgnoreCase) + ", on".Length);
                date = dateText.Split('(')[0].Trim();
            }
            catch (Exception e)
            {
                //Log errors
            }
            return date;
        }
    }
}
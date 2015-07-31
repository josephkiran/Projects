using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FusedLocationProvider.Xml2CSharp;

namespace FusedLocationProvider.Lib
{
    public class KMLGenerator
    {
        public static List<Segment> completeSegments = new List<Segment>();

        public static string GenerateCSV(List<Segment> segments)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < segments.Count; i++)
            {
                Segment item = segments[i];
                sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}{10}", 
                    item.StartLat, item.StartLog,
                    item.EndLat,item.EndLog, 
                    item.AvgDevYaw.ToString("#.####"), item.AvgDevRoll.ToString("#.####"),
                    item.AvgDevPitch.ToString("#.####"),
                    item.Speed.ToString(), 
                    item.RoadCondition.ToString(),item.Time.ToLongTimeString(),
                    System.Environment.NewLine));
            }
            return sb.ToString();
        }


        public static Document GenerateKML(List<Segment> segments)
        {
            Document d = new Document();
            d.Style = new List<Style>();
            Style st = new Style();
            st.Id = "yellowLine";
            st.LineStyle = new LineStyle();
            st.LineStyle.Color = "7f00ffff";
            st.LineStyle.Width = "4";
            d.Style.Add(st);
            st = new Style();
            st.Id = "greenLine";
            st.LineStyle = new LineStyle();
            st.LineStyle.Color = "7d00ff00";
            st.LineStyle.Width = "4";
            d.Style.Add(st);
            st = new Style();
            st.Id = "redLine";
            st.LineStyle = new LineStyle();
            st.LineStyle.Color = "ff0000ff";
            st.LineStyle.Width = "4";
            d.Style.Add(st);
            st = new Style();
            st.Id = "blueLine";
            st.LineStyle = new LineStyle();
            st.LineStyle.Color = "ffff0000";
            st.LineStyle.Width = "4";
            d.Style.Add(st);
            st = new Style();
            st.Id = "blackLine";
            st.LineStyle = new LineStyle();
            st.LineStyle.Color = "87000000";
            st.LineStyle.Width = "4";
            d.Style.Add(st);

            completeSegments.AddRange(segments);

            d.Placemarks = new List<Placemark>();

            for (int i = 0; i < segments.Count; i++)
            {
                Segment item = segments[i];
                Placemark p = new Placemark();
                p.Name = string.Format("{0},{1},{2},{3},{4}",item.AvgDevYaw.ToString("#.###"),item.AvgDevRoll.ToString("#.###"),item.AvgDevPitch.ToString("#.###"),item.Speed.ToString(),item.RoadCondition.ToString());
                p.Description = "JK";
                p.LineString = new LineString();
                p.LineString.AltitudeMode = "absolute";
                p.LineString.Coordinates = string.Format("{0},{1},0,{2},{3},0", item.StartLog,item.StartLat , item.EndLog, item.EndLat );
                p.StyleUrl = GetRoadConditionStyleID(item.RoadCondition);
                d.Placemarks.Add(p);
            }

            return d;
        }

        public static string GetRoadConditionStyleID(RoadType rdtype)
        {
            string styleID = "#greenLine";

            switch (rdtype)
            {
                case RoadType.Good:
                case RoadType.Idle:
                    styleID = "#greenLine";
                    break;
                   
                case RoadType.SlightyBumpy:
                    styleID = "#yellowLine";
                    break;
                case RoadType.Bumpy:
                    styleID = "#yellowLine";
                    break;
                case RoadType.Worst:
                    styleID = "#redLine";
                    break;
                case RoadType.RandomAction:
                    styleID = "#blueLine";
                    break;
                default:
                    styleID = "#blackLine";
                    break;
            }
            return styleID;
        }

        

    }
}
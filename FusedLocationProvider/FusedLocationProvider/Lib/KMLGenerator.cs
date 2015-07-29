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

        public static Document GenerateKML(List<Segment> segments)
        {
            Document d = new Document();
            d.Placemarks = new List<Placemark>();

            for (int i = 0; i < segments.Count; i++)
            {
                Segment item = segments[i];
                Placemark p = new Placemark();
                p.LineString = new LineString();
                p.LineString.AltitudeMode = "absolute";
                p.LineString.Coordinates = string.Format("{0},{1},0,{2},{3},0", item.StartLat, item.StartLog, item.EndLat, item.EndLog);
                d.Placemarks.Add(p);
            }

            return d;
        }

        

    }
}
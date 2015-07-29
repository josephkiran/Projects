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
            
            foreach (var item in segments)
            {
                d.Placemark = new Placemark();
                d.Placemark.LineString = new LineString();
                d.Placemark.LineString.AltitudeMode = "absolute";
                d.Placemark.LineString.Coordinates = string.Format("{0},{1},0,{2},{3},0", item.StartLat, item.StartLog, item.EndLat, item.EndLog);
            }

            return d;
        }

        

    }
}
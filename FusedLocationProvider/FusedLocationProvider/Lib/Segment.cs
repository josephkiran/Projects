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

namespace FusedLocationProvider.Lib
{
    public class Segment
    {
        public double StartLat { get; set; }
        public double StartLog { get; set; }

        public double EndLat { get; set; }
        public double EndLog { get; set; }

        public static int TotalSegments { get; set; }
        public double AvgDevYaw { get; set; }
        public double AvgDevRoll { get; set; }
        public double AvgDevPitch { get; set; }

        public double Speed { get; set; }

        public RoadType RoadCondition { get; set; }
    }
}
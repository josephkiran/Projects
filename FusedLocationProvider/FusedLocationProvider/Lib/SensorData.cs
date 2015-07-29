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
    public class SensorData
    {
        public float Yaw { get; set; }
        public float Roll { get; set; }
        public float Pitch { get; set; }
        public double Lat { get; set; }
        public double Log { get; set; }

        public double Speed { get; set; }

        public DateTime Time { get; set; }

     }
}
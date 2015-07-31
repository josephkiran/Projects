using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FusedLocationProvider.Lib
{
    public class GPXData
    {
        public double StartLat { get; set; }
        public double StartLog { get; set; }

        public double EndLat { get; set; }
        public double EndLog { get; set; }

        public double StdDevYaw { get; set; }
        public double StdDevRoll { get; set; }
        public double StdDevPitch { get; set; }

        public RoadType RoadCondition { get; set; }
        public double Speed { get;  set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return string.Format("YAW:{0}\nPITCH:{1}\nROLL:{2}\nTYPE:{3}", StdDevYaw.ToString(), StdDevPitch.ToString(), StdDevRoll.ToString(), RoadCondition.ToString());
            //return string.Format("TYPE: {0}", RoadCondition.ToString());
        }

    }
}
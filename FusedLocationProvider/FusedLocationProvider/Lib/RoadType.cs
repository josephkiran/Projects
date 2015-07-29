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
    public enum RoadType
    {
        Good=0,
        SlightyBumpy=1,
        Bumpy=2,
        Worst=3,
        RandomAction=4,
        Idle=99
    }
}
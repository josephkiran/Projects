using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps
{
    public class CalibrationSettings
    {
        public SensorMode Mode { get; set; }
    }


    public enum SensorMode
    {
        Calibration,
        Normal
    }
}

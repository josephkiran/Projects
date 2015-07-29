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
    public class DataProcessor
    {
        List<SensorData> _sensorDataSet = null;
        double _sdDevYaw = 0;
        double _sdDevPitch = 0;
        double _sdDevRoll = 0;
        RoadType _rdType = RoadType.Idle;

        public float Range1 { get; set; }
        public float Range2 { get; set; }
        public float Range3 { get; set; }
        public float Range4 { get; set; }


        public DataProcessor(List<SensorData> sensorDataSet)
        {
            _sensorDataSet = sensorDataSet;
        }

        private double getStandardDeviation(List<float> doubleList)
        {
            if(doubleList.Count > 0)
            {
                float average = doubleList.Average();
                float sumOfDerivation = 0;
                foreach (float value in doubleList)
                {
                    sumOfDerivation += (value) * (value);
                }
                float sumOfDerivationAverage = sumOfDerivation / doubleList.Count;
                return Math.Sqrt(sumOfDerivationAverage - (average * average));

            }else
            {
                return 0;
            }

            
        }

        public GPXData ProcessDataForGPX()
        {
            if (_sensorDataSet.Count == 0) return new GPXData();
            List<float> yawList = new List<float>();
            List<float> rollList = new List<float>();
            List<float> pitchList = new List<float>();
            
            foreach (var sensData in _sensorDataSet)
            {
                yawList.Add(sensData.Yaw);
                rollList.Add(sensData.Roll);
                pitchList.Add(sensData.Pitch);
            }
            _sdDevPitch = getStandardDeviation(pitchList);
            _sdDevRoll = getStandardDeviation(rollList);
            _sdDevYaw = getStandardDeviation(yawList);
            if (double.IsNaN(_sdDevRoll))
            {
                _sdDevRoll = 0;
            }

            if (double.IsNaN(_sdDevYaw))
            {
                _sdDevYaw = 0;
            }

            if (double.IsNaN(_sdDevPitch))
            {
                _sdDevPitch = 0;
            }

            _rdType = GetRoadType();
            
            return new GPXData() {
                StartLat =_sensorDataSet[0].Lat,
                StartLog =_sensorDataSet[0].Log,
                EndLat = _sensorDataSet[_sensorDataSet.Count-1].Lat,
                EndLog = _sensorDataSet[_sensorDataSet.Count - 1].Log,
                RoadCondition = _rdType,
                 StdDevPitch= _sdDevPitch,
                 StdDevRoll = _sdDevRoll,
                 StdDevYaw = _sdDevYaw
            };
        }

       
        private RoadType GetRoadTypeV1()
        {
            RoadType rdType;
            if (_sdDevPitch < Range1 && _sdDevYaw < Range1 && _sdDevRoll < Range1)
            {
                rdType = RoadType.Good;

            }else if (_sdDevPitch < Range2 && _sdDevYaw < Range2 && _sdDevRoll < Range2)
            {
                rdType = RoadType.SlightyBumpy;
            }
            else if (_sdDevPitch < Range3 && _sdDevYaw < Range3 && _sdDevRoll < Range3)
            {
                rdType = RoadType.Bumpy;
            }
            else if (_sdDevPitch < Range4 && _sdDevYaw < Range4 && _sdDevRoll < Range4)
            {
                rdType = RoadType.Bumpy;
            }
            else
            {
                rdType = RoadType.RandomAction;
            }

            return rdType;
        }

        private RoadType GetRoadType()
        {
            RoadType rdType;
            if (_sdDevPitch < Range1 && _sdDevRoll < Range1 )
            {
                rdType = RoadType.Good;

            }
            else if (_sdDevPitch < Range2 && _sdDevRoll < Range2 )
            {
                rdType = RoadType.SlightyBumpy;
            }
            else if (_sdDevPitch < Range3 && _sdDevRoll < Range3 )
            {
                rdType = RoadType.Bumpy;
            }
            else if (_sdDevPitch < Range4 && _sdDevRoll < Range4 )
            {
                rdType = RoadType.Worst;
            }
            else
            {
                rdType = RoadType.RandomAction;
            }

            return rdType;
        }

    }
}
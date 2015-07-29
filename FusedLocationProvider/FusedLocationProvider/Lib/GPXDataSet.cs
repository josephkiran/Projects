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
    public class GPXDataSet
    {
        public List<Segment> Segments { get; set; }

        List<GPXData> _gpxdataList = new List<GPXData>();
        public void AddGPXData(GPXData gpxDt)
        {
            _gpxdataList.Add(gpxDt);
        }

        public double StartLat { get; set; }
        public double StartLog { get; set; }

        public double EndLat { get; set; }
        public double EndLog { get; set; }

        public static int TotalSegments { get; set; }
        public double AvgDevYaw { get; set; }
        public double AvgDevRoll { get; set; }
        public double AvgDevPitch { get; set; }

        public RoadType RoadCondition { get; set; }

        private float _avgWeight = 0;


        public List<GPXData> GPXDataList { get { return _gpxdataList; } }

        public GPXDataSet()
        {
            Segments = new List<Segment>();
        }
        public void CreateKMLInit(string coordinates)
        {

        }


        public void OnOverallAvgTimedEvent(object source, ElapsedEventArgs e)
        {
            if (_gpxdataList.Count > 0)
            {
                TotalSegments++;
                if(TotalSegments> int.MaxValue)
                {
                    TotalSegments = 0;
                }
                int count = 0;
                float totalWeightage = 0;
                foreach (var item in _gpxdataList)
                {
                    if (item.RoadCondition == RoadType.RandomAction)
                    {
                        continue;
                    }
                    
                    count++;
                    totalWeightage = getWeightage(item.RoadCondition);
                }

                this.StartLat = _gpxdataList[0].StartLat;
                this.StartLog = _gpxdataList[0].StartLog;
                this.EndLat = _gpxdataList[_gpxdataList.Count-1].EndLat;
                this.EndLog = _gpxdataList[_gpxdataList.Count-1].EndLog;


                _avgWeight = totalWeightage / count;

                RoadCondition = RoadType.Good;
                if (_avgWeight > 0.2)
                {
                    RoadCondition = RoadType.Worst;
                }
                else if (_avgWeight > 0.1)
                {
                    RoadCondition = RoadType.Bumpy;
                }else if(_avgWeight > 0.03)
                {
                    RoadCondition = RoadType.SlightyBumpy;
                }

                this.Segments.Add(GetCurrentSegment());

                _gpxdataList.Clear();
            }
            else
            {
                AvgDevPitch = 0;
                AvgDevRoll = 0;
                AvgDevYaw = 0;
                _avgWeight = 0;
                RoadCondition = RoadType.Good;

            }
        }

        private Segment GetCurrentSegment()
        {
            return new Segment()
            {
                AvgDevPitch = this.AvgDevPitch,
                AvgDevRoll = this.AvgDevRoll,
                AvgDevYaw = this.AvgDevYaw,
                StartLat = this.StartLat,
                StartLog = this.StartLog,
                EndLat = this.EndLat,
                EndLog = this.EndLog,
                RoadCondition=this.RoadCondition
            };
        }


     

        private float getWeightage(RoadType rd)
        {
            float weight = 0;
            switch (rd)
            {
                case RoadType.Good:
                    weight = 0;
                    break;
                case RoadType.SlightyBumpy:
                    weight = 0.4f;
                    break;
                case RoadType.Bumpy:
                    weight = 1.0f;
                    break;
                case RoadType.Worst:
                    weight = 1.5f;
                    break;
                case RoadType.RandomAction:
                    weight = 0;
                    break;
                case RoadType.Idle:
                    weight = 0;
                    break;
                default:
                    weight = 0;
                    break;
            }
            return weight;
        }

        public override string ToString()
        {
            return string.Format("OVERALL: {0} : {1}\n{2}", RoadCondition.ToString(), _avgWeight.ToString(),TotalSegments.ToString());
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FusedLocationProvider.Lib
{
    public class GPXDataSet
    {
        
        public List<Segment> Segments { get; set; }
       
        public static Action<RoadType> PlayRoadStatus;

        List<GPXData> _gpxdataList = new List<GPXData>();
        public void AddGPXData(GPXData gpxDt)
        {
            _gpxdataList.Add(gpxDt);
        }

        public double TotalWeight { get; set; }
        public double StartLat { get; set; }
        public double StartLog { get; set; }

        public double EndLat { get; set; }
        public double EndLog { get; set; }

        public static int TotalSegments { get; set; }

        //public static List<Segment> OverallSegments { get; set; }
        public double AvgDevYaw { get; set; }
        public double AvgDevRoll { get; set; }
        public double AvgDevPitch { get; set; }

        public RoadType RoadCondition { get; set; }

        public string IterationRoadConditions { get; set; }

        public DateTime Time { get; set; }

        private float _avgWeight = 0;


        public List<GPXData> GPXDataList { get { return _gpxdataList; } }

        public double Speed { get; private set; }

        public GPXDataSet()
        {
            Segments = new List<Segment>();
            
        }

         static GPXDataSet()
        {
           
           
            //OverallSegments = new List<Segment>();
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
                List<double> yawList = new List<double>();
                List<double> rollList = new List<double>();
                List<double> pitchList = new List<double>();
                IterationRoadConditions = "";
                foreach (var item in _gpxdataList)
                {
                    if (item.RoadCondition == RoadType.RandomAction)
                    {
                        continue;
                    }

                    yawList.Add(item.StdDevYaw);
                    rollList.Add(item.StdDevRoll);
                    pitchList.Add(item.StdDevPitch);
                    IterationRoadConditions += ((int)item.RoadCondition).ToString() + ",";
                    count++;
                    totalWeightage += getWeightage(item.RoadCondition);
                }

               
               

                this.AvgDevPitch = getStandardDeviation(pitchList);
                this.AvgDevRoll = getStandardDeviation(rollList);
                this.AvgDevYaw = getStandardDeviation(yawList);

                this.StartLat = _gpxdataList[0].StartLat;
                this.StartLog = _gpxdataList[0].StartLog;
                this.EndLat = _gpxdataList[_gpxdataList.Count-1].EndLat;
                this.EndLog = _gpxdataList[_gpxdataList.Count-1].EndLog;
                this.Time = _gpxdataList[_gpxdataList.Count - 1].Time;

                TotalWeight = totalWeightage;
                _avgWeight = totalWeightage / count;

                RoadCondition = RoadType.Good;
                if (_avgWeight > 0.6)
                {
                     RoadCondition = RoadType.Worst;
                }
                else if (_avgWeight > 0.4)
                {
                    RoadCondition = RoadType.Bumpy;
                }else if(_avgWeight > 0.30)
                {
                    RoadCondition = RoadType.SlightyBumpy;
                }

                this.Speed = _gpxdataList[_gpxdataList.Count - 1].Speed;
                this.Segments.Add(GetCurrentSegment());
                try
                {
                    PlayRoadStatus(RoadCondition);
                }
                catch (Exception ex)
                {

                    throw;
                }
                

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

        private double getStandardDeviation(List<double> doubleList)
        {
            if (doubleList.Count > 0)
            {
                double average = doubleList.Average();
                double sumOfDerivation = 0;
                foreach (double value in doubleList)
                {
                    sumOfDerivation += (value) * (value);
                }
                double sumOfDerivationAverage = sumOfDerivation / doubleList.Count;
                return Math.Sqrt(sumOfDerivationAverage - (average * average));

            }
            else
            {
                return 0;
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
                Speed = this.Speed,
                RoadCondition=this.RoadCondition,
                IterationRoadConditions=this.IterationRoadConditions,
                Time = this.Time

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
                    weight = 1.0f;
                    break;
                case RoadType.Bumpy:
                    weight = 1.2f;
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
            return string.Format("JK: {0} : {1}\nAVG:{2}\n{3}", RoadCondition.ToString(), TotalWeight.ToString("#.###"), _avgWeight.ToString("#.###"),TotalSegments.ToString());
        }

    }
}
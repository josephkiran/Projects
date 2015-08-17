using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps
{
    public class Calibrator
    {
        CarSensor _sensor = null;
        CalibrationSettings _settings = null;

        public CalibrationSettings Settings
        {
            get { return _settings; }
        }

        public Calibrator()
        {
            _settings = new CalibrationSettings();
            _settings.Mode = SensorMode.Calibration;
            _sensor = new CarSensor(_settings);

        }

        public async void StartCalibration()
        {
            
        }


        public async void StopCalibrating()
        {

        }
    }
}

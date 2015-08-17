using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps
{
    public class CarSensor
    {


        public CarSensor(CalibrationSettings calSettings)
        {

        }

        private CarSensor()
        {

        }

        public static CarSensor CreateSensor()
        {
            return new CarSensor();
        }



        public void StartSensing()
        {

        }
    }
}

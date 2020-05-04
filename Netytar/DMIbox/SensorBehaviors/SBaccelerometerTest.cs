using NeeqDMIs.ATmega;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Netytar.DMIbox.SensorBehaviors
{
    public class SBaccelerometerTest : ISensorReaderBehavior
    {
        string[] para = new string[3];

        public SBaccelerometerTest()
        {

        }

        public void ReceiveSensorRead(string val)
        {
            para = val.Split('/');

            if(para.Length == 6)
            {
                Rack.DMIBox.GyroX = int.Parse(para[0]);
                Rack.DMIBox.GyroY = int.Parse(para[1]);
                Rack.DMIBox.GyroZ = int.Parse(para[2]);
                Rack.DMIBox.AccX = int.Parse(para[3]);
                Rack.DMIBox.AccY = int.Parse(para[4]);
                Rack.DMIBox.AccZ = int.Parse(para[5]);

                PrintIndicators();

                Rack.DMIBox.MidiModule.SetPitchBend((Rack.DMIBox.GyroCalibX / 2 + 8192));
            }
            else
            {
                // missing values
            }
            
        }

        private void PrintIndicators()
        {
            Rack.DMIBox.TestString = "X: " + Rack.DMIBox.AccCalibX + "\nY: " + Rack.DMIBox.AccCalibY + "\nZ: " + Rack.DMIBox.AccCalibZ;
        }

        private int ReadValue(string val)
        {
            return int.Parse(val, CultureInfo.InvariantCulture.NumberFormat);
        }

        /*
         * Gyro max values: 32767, -32768
         */
        
    }
}

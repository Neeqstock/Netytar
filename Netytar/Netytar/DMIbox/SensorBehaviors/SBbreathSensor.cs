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
    public class SBbreathSensor : ISensorReaderBehavior
    {
        private int v = 1;
        private int offThresh;
        private int onThresh;
        private float sensitivity;

        public SBbreathSensor(int offThresh, int onThresh, float sensitivity)
        {
            this.offThresh = offThresh;
            this.onThresh = onThresh;
            this.sensitivity = sensitivity;
        }

        public void ReceiveSensorRead(string val)
        {
            if(NetytarRack.DMIBox.NetytarControlMode == NetytarControlModes.BreathSensor)
            {
                float b = 0;

                try
                {
                    b = float.Parse(val, CultureInfo.InvariantCulture.NumberFormat);
                }
                catch
                {

                }

                v = (int)(b);

                NetytarRack.DMIBox.NetytarMainWindow.BreathSensorValue = v;
                NetytarRack.DMIBox.Pressure = (int)(v * 2 * sensitivity);
                NetytarRack.DMIBox.Modulation = (int)(v / 8 * sensitivity);

                if (v > onThresh && NetytarRack.DMIBox.Blow == false)
                {
                    NetytarRack.DMIBox.Blow = true;
                    //NetytarRack.DMIBox.Pressure = 110;
                }

                if (v < offThresh)
                {
                    NetytarRack.DMIBox.Blow = false;
                }
            }
            
        }
    }
}

using EyeXFramework;
using NeeqDMIs.Eyetracking.EyeX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Netytar.DMIbox.EyeXBehaviors
{
    public class EPBdynamicsBehavior : IEyeXEyePositionBehavior
    {
        private int v = 1;
        private int offThresh;
        private int onThresh;
        private float maxSwing;
        private float sensitivity;

        private float b;
        public float B
        {
            get { return b; }
            set
            {
                if(value < 0)
                {
                    b = 0;
                }
                else if(value > maxSwing)
                {
                    b = 50;
                }
                else if(value == 0) { }
                else if (value < -50) { }
                else
                {
                    b = value;
                }

            }
        }

        public EPBdynamicsBehavior(int offThresh, int onThresh, float sensitivity, float maxSwing)
        {
            this.offThresh = offThresh;
            this.onThresh = onThresh;
            this.sensitivity = sensitivity;
            this.maxSwing = maxSwing;
        }



        public void ReceiveEyePosition(EyePositionEventArgs e)
        {
            if (NetytarRack.DMIBox.NetytarControlMode == NetytarControlModes.EyePos)
            {
                if (NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.IsValid && NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y > 1)
                {
                    B = (float)(NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y - NetytarRack.DMIBox.EyePosBaseY);
                    int Bnorm = (int)((B * 127f) / maxSwing);

                    NetytarRack.DMIBox.NetytarMainWindow.BreathSensorValue = Bnorm;
                    NetytarRack.DMIBox.Pressure = (int)(Bnorm * 2 * sensitivity);
                    //NetytarRack.DMIBox.Modulation = (int)(Bnorm / 8 * sensitivity);

                    if (Bnorm > onThresh && NetytarRack.DMIBox.Blow == false)
                    {
                        NetytarRack.DMIBox.Blow = true;
                        //NetytarRack.DMIBox.Pressure = 110;
                    }

                    if (Bnorm < offThresh)
                    {
                        NetytarRack.DMIBox.Blow = false;
                    }
                }
            }

        }
    }
}

using EyeXFramework;
using NeeqDMIs.Eyetracking.EyeX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netytar.DMIbox.EyeXBehaviors
{
    public class EPBdynamicsBehavior : IEyeXEyePositionBehavior
    {
        private int v = 1;
        private int offThresh;
        private int onThresh;
        private float sensitivity;

        public EPBdynamicsBehavior(int offThresh, int onThresh, float sensitivity)
        {
            this.offThresh = offThresh;
            this.onThresh = onThresh;
            this.sensitivity = sensitivity;
        }

        public void ReceiveEyePosition(EyePositionEventArgs e)
        {
            if (NetytarRack.DMIBox.NetytarControlMode == NetytarControlModes.EyePos)
            {
                float b = (float)(NetytarRack.DMIBox.EyeXModule.LastEyePosition.LeftEye.Y - NetytarRack.DMIBox.EyePosBaseY);

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

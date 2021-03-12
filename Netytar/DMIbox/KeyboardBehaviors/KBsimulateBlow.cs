﻿using NeeqDMIs.Keyboard;
using RawInputProcessor;

namespace Netytar
{
    public class KBsimulateBlow : IKeyboardBehavior
    {
        private VKeyCodes keyBlow = VKeyCodes.Space;

        private bool blowing = false;
        int returnVal = 0;

        public int ReceiveEvent(RawInputEventArgs e)
        {
            returnVal = 0;

            if(Rack.DMIBox.NetytarControlMode == NetytarControlModes.Keyboard)
            {
                if (e.VirtualKey == (ushort)keyBlow && e.KeyPressState == KeyPressState.Down)
                {
                    blowing = true;
                    returnVal = 1;
                    Rack.DMIBox.NetytarMainWindow.BreathSensorValue = 127;
                }
                else if (e.VirtualKey == (ushort)keyBlow && e.KeyPressState == KeyPressState.Up)
                {
                    blowing = false;
                    returnVal = 1;
                    Rack.DMIBox.NetytarMainWindow.BreathSensorValue = 0;
                }
                Rack.DMIBox.Blow = blowing;
            }

            return returnVal;
        }
    }
}

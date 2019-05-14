using NeeqDMIs.Keyboard;
using RawInput_dll;

namespace Netytar
{
    public class KBsimulateBlow : AKeyboardBehavior
    {
        private string keyBlow = LVKeyNames.SPACE;

        private bool blowing = false;
        int returnVal = 0;

        public override int ReceiveEvent(RawInputEventArg e)
        {
            returnVal = 0;

            if(NetytarRack.DMIBox.NetytarControlMode == NetytarControlModes.Keyboard)
            {
                if (e.KeyPressEvent.VKeyName == keyBlow && e.KeyPressEvent.KeyPressState == LKeyPressStates.MAKE)
                {
                    blowing = true;
                    returnVal = 1;
                    NetytarRack.DMIBox.NetytarMainWindow.BreathSensorValue = 127;
                }
                else if (e.KeyPressEvent.VKeyName == keyBlow && e.KeyPressEvent.KeyPressState == LKeyPressStates.BREAK)
                {
                    blowing = false;
                    returnVal = 1;
                    NetytarRack.DMIBox.NetytarMainWindow.BreathSensorValue = 0;
                }
                NetytarRack.DMIBox.Blow = blowing;
            }

            return returnVal;
        }
    }
}

using NeeqDMIs.Keyboard;
using RawInput_dll;

namespace Netytar
{
    class KBemulateMouse : AKeyboardBehavior
    {
        private string keyEmulate = LVKeyNames.LCONTROL;

        private bool eyeTrackerToMouse = false;
        private bool cursorVisible = true;
        private bool autoScrollerEnabled = false;

        public override int ReceiveEvent(RawInputEventArg e)
        {
            if(e.KeyPressEvent.VKeyName == keyEmulate && e.KeyPressEvent.KeyPressState == LKeyPressStates.MAKE)
            {
                eyeTrackerToMouse = !eyeTrackerToMouse;
                cursorVisible = !cursorVisible;
                autoScrollerEnabled = !autoScrollerEnabled;

                NetytarRack.DMIBox.EyeXModule.MouseEmulator.EyetrackerToMouse = eyeTrackerToMouse;
                NetytarRack.DMIBox.EyeXModule.MouseEmulator.CursorVisible = cursorVisible;
                NetytarRack.DMIBox.AutoScroller.Enabled = autoScrollerEnabled;

                return 0;
            }
            return 1;
        }
    }
}

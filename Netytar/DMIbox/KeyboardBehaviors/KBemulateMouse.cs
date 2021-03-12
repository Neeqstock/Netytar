﻿using NeeqDMIs.Keyboard;
using RawInputProcessor;
using System.Windows;

namespace Netytar
{
    class KBemulateMouse : IKeyboardBehavior
    {
        private VKeyCodes keyStartEmulate = VKeyCodes.Control;
        private VKeyCodes keyStopEmulate = VKeyCodes.Shift;

        private bool eyeTrackerToMouse = false;
        private bool cursorVisible = true;
        private bool autoScrollerEnabled = false;

        public int ReceiveEvent(RawInputEventArgs e)
        {
            if (e.VirtualKey == (ushort)keyStartEmulate)
            {
                eyeTrackerToMouse = true;
                cursorVisible = false;
                autoScrollerEnabled = true;

                SetStuff();

                return 0;
            }
            else if (e.VirtualKey == (ushort)keyStopEmulate)
            {
                eyeTrackerToMouse = false;
                cursorVisible = true;
                autoScrollerEnabled = false;

                SetStuff();

                return 0;
            }
            return 1;
        }

        private void SetStuff()
        {
            switch (Rack.DMIBox.Eyetracker)
            {

                case Eyetracker.Tobii:
                    Rack.DMIBox.TobiiModule.MouseEmulator.EyetrackerToMouse = eyeTrackerToMouse;
                    Rack.DMIBox.TobiiModule.MouseEmulator.CursorVisible = cursorVisible;
                    Rack.DMIBox.AutoScroller.Enabled = autoScrollerEnabled;
                    break;
                case Eyetracker.Eyetribe:
                    Rack.DMIBox.EyeTribeModule.MouseEmulator.EyetrackerToMouse = eyeTrackerToMouse;
                    Rack.DMIBox.EyeTribeModule.MouseEmulator.CursorVisible = cursorVisible;
                    Rack.DMIBox.AutoScroller.Enabled = autoScrollerEnabled;
                    break;
                default:
                    break;
            }
        }
    }
}

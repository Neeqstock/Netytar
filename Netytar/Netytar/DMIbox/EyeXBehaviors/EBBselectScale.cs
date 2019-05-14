using NeeqDMIs.Eyetracking.EyeX;
using NeeqDMIs.Music;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Netytar.DMIbox.EyeXBehaviors
{
    public class EBBselectScale : AEyeXBlinkBehavior
    {
        private MainWindow mainWindow;

        public EBBselectScale(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            LCThresh = 40;
            RCThresh = 40;
        }

        public override void Event_doubleClose() { }

        public override void Event_doubleOpen() { }

        public override void Event_leftClose()
        {
            NetytarRack.DMIBox.NetytarMainWindow.SelectedScale = new Scale(NetytarRack.DMIBox.NetytarSurface.CheckedButton.Note.ToAbsNote(), ScaleCodes.maj);
        }

        public override void Event_leftOpen() { }

        public override void Event_rightClose()
        {
            NetytarRack.DMIBox.NetytarMainWindow.SelectedScale = new Scale(NetytarRack.DMIBox.NetytarSurface.CheckedButton.Note.ToAbsNote(), ScaleCodes.min);
        }

        public override void Event_rightOpen() { }
    }
}

using NeeqDMIs.Eyetracking.EyeX;
using NeeqDMIs.Music;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Netytar.DMIbox.EyeXBehaviors
{
    public class EBBactivateButton : AEyeXBlinkBehavior
    {
        public EBBactivateButton()
        {
            DCThresh = 4;
        }

        public override void Event_doubleClose()
        {
            if(NetytarRack.DMIBox.NetytarControlMode == NetytarControlModes.EyePos)
            {
                if (NetytarRack.DMIBox.HasAButtonGaze)
                {
                    NetytarRack.DMIBox.LastGazedButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
            }
        }

        public override void Event_doubleOpen() { }

        public override void Event_leftClose() { }

        public override void Event_leftOpen() { }

        public override void Event_rightClose() { }

        public override void Event_rightOpen() { }
    }
}

using NeeqDMIs.Eyetracking.Tobii;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Netytar.DMIbox.TobiiBehaviors
{
    public class EBBactivateButton : ATobiiBlinkBehavior
    {
        public EBBactivateButton()
        {
            DCThresh = 4;
        }

        public override void Event_doubleClose()
        {
            if(Rack.DMIBox.NetytarControlMode == NetytarControlModes.EyePos)
            {
                if (Rack.DMIBox.HasAButtonGaze)
                {
                    Rack.DMIBox.LastGazedButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
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

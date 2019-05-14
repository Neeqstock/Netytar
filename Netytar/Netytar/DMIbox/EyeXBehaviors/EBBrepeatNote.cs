using NeeqDMIs.Eyetracking.EyeX;
using NeeqDMIs.Music;

namespace Netytar.DMIbox.EyeXBehaviors
{
    public class EBBrepeatNote : AEyeXBlinkBehavior
    {
        public EBBrepeatNote()
        {
            DCThresh = 4;
        }

        public override void Event_doubleClose()
        {
            if (NetytarRack.DMIBox.Blow)
            {
                NetytarRack.DMIBox.Blow = false;
                NetytarRack.DMIBox.Blow = true;
                NetytarRack.DMIBox.NetytarSurface.FlashSpark();
            }
        }

        public override void Event_doubleOpen() { }

        public override void Event_leftClose() { }

        public override void Event_leftOpen() { }

        public override void Event_rightClose() { }

        public override void Event_rightOpen() { }
    }
}

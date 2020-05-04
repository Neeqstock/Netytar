﻿using NeeqDMIs.Eyetracking.Tobii;
using NeeqDMIs.Music;

namespace Netytar.DMIbox.TobiiBehaviors
{
    public class EBBrepeatNote : ATobiiBlinkBehavior
    {
        public EBBrepeatNote()
        {
            DCThresh = 4;
        }

        public override void Event_doubleClose()
        {
            if (Rack.DMIBox.Blow)
            {
                Rack.DMIBox.Blow = false;
                Rack.DMIBox.Blow = true;
                //NetytarRack.DMIBox.NetytarSurface.FlashSpark();
            }
        }

        public override void Event_doubleOpen() { }

        public override void Event_leftClose() { }

        public override void Event_leftOpen() { }

        public override void Event_rightClose() { }

        public override void Event_rightOpen() { }
    }
}

using NeeqDMIs.Keyboard;
using NeeqDMIs.Music;
using RawInput_dll;

namespace Netytar.DMIbox.KeyboardBehaviors
{
    class KBselectScale : AKeyboardBehavior
    {
        private const string keyMaj = LVKeyNames.ADD;
        private const string keyMin = LVKeyNames.SUBTRACT;

        public override int ReceiveEvent(RawInputEventArg e)
        {
            if (e.KeyPressEvent.VKeyName == keyMaj && e.KeyPressEvent.KeyPressState == LKeyPressStates.MAKE)
            {
                NetytarRack.DMIBox.NetytarSurface.Scale = new Scale(NetytarRack.DMIBox.NetytarSurface.CheckedButton.Note.ToAbsNote(), ScaleCodes.maj);
                return 1;
            }
            if (e.KeyPressEvent.VKeyName == keyMin && e.KeyPressEvent.KeyPressState == LKeyPressStates.MAKE)
            {
                NetytarRack.DMIBox.NetytarSurface.Scale = new Scale(NetytarRack.DMIBox.NetytarSurface.CheckedButton.Note.ToAbsNote(), ScaleCodes.min);
            };
            return 0;
        }
    }
}

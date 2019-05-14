using RawInput_dll;

namespace NeeqDMIs.Keyboard
{
    public abstract class AKeyboardBehavior
    {
        public abstract int ReceiveEvent(RawInputEventArg e);
    }
}
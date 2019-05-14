using RawInput_dll;
using System;
using System.Threading;

namespace NeeqDMIs.Keyboard
{
    public abstract class AKeyboardBehavior
    {
        public abstract int ReceiveEvent(RawInputEventArg e);
    }
}
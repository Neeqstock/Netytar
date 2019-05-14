using EyeXFramework;

namespace NeeqDMIs.Eyetracking.EyeX
{
    public interface IEyeXEyePositionBehavior
    {
        void ReceiveEyePosition(EyePositionEventArgs e);
    }
}
using EyeXFramework;

namespace NeeqDMIs.Eyetracking.EyeX
{
    public interface IEyeXGazePointBehavior
    {
        void ReceiveGazePoint(GazePointEventArgs e);
    }
}
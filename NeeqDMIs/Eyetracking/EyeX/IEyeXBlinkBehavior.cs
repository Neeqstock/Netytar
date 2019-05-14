namespace NeeqDMIs.Eyetracking.EyeX
{
    public interface IEyeXBlinkBehavior
    {
        void Receive_leftOpen();
        void Receive_leftClose();
        void Receive_doubleOpen();
        void Receive_doubleClose();
        void Receive_rightClose();
        void Receive_rightOpen();
    }
}

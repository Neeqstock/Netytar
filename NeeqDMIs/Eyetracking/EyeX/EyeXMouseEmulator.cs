using NeeqDMIs.Eyetracking.Filters;
using NeeqDMIs.Mouse;
using System.Drawing;

namespace NeeqDMIs.Eyetracking.EyeX
{
    public class EyeXMouseEmulator
    {
        private IFilter filter;
        public IFilter Filter { get => filter; set => filter = value; }
        private Point currentGazePoint = new Point();

        private bool eyetrackerToMouse = false;
        private bool cursorVisible = true;

        public EyeXMouseEmulator(IFilter filter)
        {
            this.Filter = filter;
        }

        public bool EyetrackerToMouse
        {
            get { return eyetrackerToMouse; }
            set { eyetrackerToMouse = value; }
        }

        public bool CursorVisible
        {
            get { return cursorVisible; }
            set
            {
                cursorVisible = value;
                MouseController.ShowCursor(cursorVisible);
            }
        }

        public void ReceiveGazePoint(double x, double y)
        {
            if (eyetrackerToMouse)
            {
                currentGazePoint.X = (int)x;
                currentGazePoint.Y = (int)y;

                Filter.Push(currentGazePoint);

                MouseController.SetCursorPos(Filter.GetOutput().X, Filter.GetOutput().Y);
            }
        }
    }
}

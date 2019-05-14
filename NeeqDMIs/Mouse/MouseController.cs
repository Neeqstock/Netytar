using System.Runtime.InteropServices;

namespace NeeqDMIs.Mouse
{
    public static class MouseController
    {
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32")]
        public static extern int ShowCursor(bool bShow);
    }
}

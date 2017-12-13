using System;
using System.Runtime.InteropServices;

namespace ExamClient
{
    public class Win32
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(int hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetActiveWindow(int hWnd);

        public const int WM_SYSCOMMAND = 274;

        public const int SC_CLOSE = 61536;

        public const int SC_MINIMIZE = 61472;
    }
}

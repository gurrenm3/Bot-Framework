using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Bot_Framework_Shared
{
    public static class Taskbar
    {
        [DllImport("user32.dll")]
        private static extern int FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        static int Handle { get { return FindWindow("Shell_TrayWnd", ""); } }

        public static void Show()
        {
            ShowWindow(Handle, SW_SHOW);
        }

        public static void Hide()
        {
            ShowWindow(Handle, SW_HIDE);
        }
    }
}

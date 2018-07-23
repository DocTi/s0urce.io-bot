using System.Drawing;
using System.Threading;
using System.Diagnostics;
using WinApi.Enums;
using WinApi.Core.Window;

namespace s0urce.io_bot_core.ImageProcessing
{
    static class ScreenCapture
    {
        public static Bitmap Capture(Process process, int timeSleep = 0)
        {
            SetFocus(process);
            Thread.Sleep(timeSleep);

            var windowSize = Window.GetWindowRect(process);
            var image = new Bitmap(windowSize.Width, windowSize.Height);

            using (var g = Graphics.FromImage(image))
            {
                g.CopyFromScreen(new Point(windowSize.Left, windowSize.Top), Point.Empty, windowSize.Size);
            }

            return image;
        }

        public static void DisableAnimationWindow(Process process)
        {
            Window.DwmSetWindowAttribute(process, DWMWINDOWATTRIBUTE.DWMWA_TRANSITIONS_FORCEDISABLED, 1);
        }

        private static void SetFocus(Process process)
        {
            Window.ShowWindow(process, SHOWFLAGS.SW_MAXIMIZE);
            Window.SetForegroundWindow(process);
        }
    }
}

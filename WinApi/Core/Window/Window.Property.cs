using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WinApi.Enums;
using WinApi.Imports;
using WinApi.Structs;

namespace WinApi.Core.Window
{
    public partial class Window
    {
        public static Rectangle GetWindowRect(Process process)
        {
            var rect = new RECT();

            ImportedMethods.GetWindowRect(process.MainWindowHandle, ref rect);

            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        public static void DwmSetWindowAttribute(Process process, DWMWINDOWATTRIBUTE attribute, int state)
        {
            ImportedMethods.DwmSetWindowAttribute(process.MainWindowHandle, (int)attribute, ref state, Marshal.SizeOf(state));
        }

    }
}

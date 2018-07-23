using System.Runtime.InteropServices;

namespace WinApi.Imports
{
    partial class ImportedMethods
    {
        [DllImport("user32.dll")]
        internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
    }
}

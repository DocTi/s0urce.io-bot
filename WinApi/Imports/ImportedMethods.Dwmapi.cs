using System;
using System.Runtime.InteropServices;

namespace WinApi.Imports
{
    static partial class ImportedMethods
    {
        [DllImport("dwmapi", PreserveSig = true)]
        internal static extern int DwmSetWindowAttribute(IntPtr hWnd, int attr, ref int value, int attrLen);
    }
}

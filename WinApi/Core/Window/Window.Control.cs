using System.Diagnostics;
using WinApi.Enums;
using WinApi.Imports;

namespace WinApi.Core.Window
{
    public static partial class Window
    {
        public static void ShowWindow(Process process, SHOWFLAGS flag)
        {
            ImportedMethods.ShowWindow(process.MainWindowHandle, (int)flag);
        }

        public static void SetForegroundWindow(Process process)
        {
            ImportedMethods.SetForegroundWindow(process.MainWindowHandle);
        }
    }
}

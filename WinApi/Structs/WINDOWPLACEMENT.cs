using System.Drawing;
using System.Runtime.InteropServices;
using WinApi.Enums;

namespace WinApi.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public SHOWWINDOWCOMMANDS showCmd;
        public Point ptMinPosition;
        public Point ptMaxPosition;
        public Rectangle rcNormalPosition;
    }
}

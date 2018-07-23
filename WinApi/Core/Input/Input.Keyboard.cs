using WinApi.Enums;
using WinApi.Imports;

namespace WinApi.Core.Input
{
    public static partial class Input
    {
        public static void PressKey(KEYS key)
        {
            KeyDown(key);
            KeyUp(key);
        }

        public static void KeyDown(KEYS key)
        {
            ImportedMethods.keybd_event((byte)key, 0x45, (uint)DWFLAGS.KEYEVENTF_EXTENDEDKEY | 0, 0);
        }

        public static void KeyUp(KEYS key)
        {
            ImportedMethods.keybd_event((byte)key, 0x45, (uint)DWFLAGS.KEYEVENTF_EXTENDEDKEY | (uint)DWFLAGS.KEYEVENTF_KEYUP, 0);
        }
    }
}

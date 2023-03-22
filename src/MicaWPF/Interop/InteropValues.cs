namespace MicaWPF.Interop;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Interops")]
public sealed class InteropValues
{
    public static class HwndSourceMessages
    {
        public const int
        WM_NCHITTEST = 0x0084,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_MAXIMIZE = 0x0024,
        WM_NCPAINT = 0x0085,
        WM_ERASEBKGND = 0x0014,
        WM_PAINT = 0x000F,
        WM_GETMINMAXINFO = 0x0024,
        WM_MOVING = 0x0216,
        WM_SETCURSOR = 0x20,
        WM_GETTEXT = 0xD,
        WM_WINDOWPOSCHANGING = 0x46,
        WM_SIZING = 0x0214;
    }

    public static class HwndButtons
    {
        public const int GWL_STYLE = -16,
         WS_MAXIMIZEBOX = 0x10000,
         WS_MINIMIZEBOX = 0x20000,
         WS_SYSMENU = 0x80000;
    }

    public static class ExternDll
    {
        public const string
            User32 = "user32.dll",
            Gdi32 = "gdi32.dll",
            GdiPlus = "gdiplus.dll",
            Kernel32 = "kernel32.dll",
            Shell32 = "shell32.dll",
            MsImg = "msimg32.dll",
            NTdll = "ntdll.dll",
            WinInet = "wininet.dll",
            UxTheme = "uxtheme.dll",
            DnsApi = "dnsapi.dll",
            DwmApi = "dwmapi.dll";
    }

    public static class DwmValues
    {
        public const int
            True = 0x01,
            False = 0x00;
    }

    [Flags]
    public enum DWMWINDOWATTRIBUTE
    {
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_SYSTEMBACKDROP_TYPE = 38,
        DWMWA_MICA_EFFECT = 1029,
        DWMWA_WINDOW_CORNER_PREFERENCE = 33
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    public struct DWMCOLORIZATIONPARAMS
    {
        public uint clrColor;
        public uint clrAfterGlow;
        public uint nintensity;
        public uint clrAfterGlowBalance;
        public uint clrBlurBalance;
        public uint clrGlassReflectionintensity;
        public bool fOpaque;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct OSVERSIONINFOEX
    {
        internal int OSVersionInfoSize;
        internal int MajorVersion;
        internal int MinorVersion;
        internal int BuildNumber;
        internal int Revision;
        internal int PlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string CSDVersion;
        internal ushort ServicePackMajor;
        internal ushort ServicePackMinor;
        internal short SuiteMask;
        internal byte ProductType;
        internal byte Reserved;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MONITORINFO
    {
        public int cbSize;
        public RECT rcMonitor;
        public RECT rcWork;
        public uint dwFlags;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    }

    public enum MonitorOptions : uint
    {
        MONITOR_DEFAULTTONULL = 0x00000000,
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        MONITOR_DEFAULTTONEAREST = 0x00000002
    }

    public enum DWM_WINDOW_CORNER_PREFERENCE
    {
        DWMWCP_DEFAULT = 0,
        DWMWCP_DONOTROUND = 1,
        DWMWCP_ROUND = 2,
        DWMWCP_ROUNDSMALL = 3
    }
}

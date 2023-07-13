// <copyright file="InteropValues.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

namespace MicaWPF.Interop;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Interops")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "Interops")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:Accessible fields should begin with upper-case letter", Justification = "Interops")]
public sealed class InteropValues
{
    [Flags]
    public enum DWMWINDOWATTRIBUTE
    {
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_SYSTEMBACKDROP_TYPE = 38,
        DWMWA_MICA_EFFECT = 1029,
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,
    }

    public enum MonitorOptions : uint
    {
        MONITOR_DEFAULTTONULL = 0x00000000,
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        MONITOR_DEFAULTTONEAREST = 0x00000002,
    }

    public enum DWM_WINDOW_CORNER_PREFERENCE
    {
        DWMWCP_DEFAULT = 0,
        DWMWCP_DONOTROUND = 1,
        DWMWCP_ROUND = 2,
        DWMWCP_ROUNDSMALL = 3,
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

    public static class HwndSourceMessages
    {
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_MAXIMIZE = 0x0024;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_PAINT = 0x000F;
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int WM_MOVING = 0x0216;
        public const int WM_SETCURSOR = 0x20;
        public const int WM_GETTEXT = 0xD;
        public const int WM_WINDOWPOSCHANGING = 0x46;
        public const int WM_SIZING = 0x0214;
    }

    public static class HwndButtons
    {
        public const int GWL_STYLE = -16;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_SYSMENU = 0x80000;
    }

    public static class ExternDll
    {
        public const string User32 = "user32.dll";
        public const string Gdi32 = "gdi32.dll";
        public const string GdiPlus = "gdiplus.dll";
        public const string Kernel32 = "kernel32.dll";
        public const string Shell32 = "shell32.dll";
        public const string MsImg = "msimg32.dll";
        public const string NTdll = "ntdll.dll";
        public const string WinInet = "wininet.dll";
        public const string UxTheme = "uxtheme.dll";
        public const string DnsApi = "dnsapi.dll";
        public const string DwmApi = "dwmapi.dll";
    }

    public static class DwmValues
    {
        public const int True = 0x01;
        public const int False = 0x00;
    }
}

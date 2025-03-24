// <copyright file="InteropValues.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Runtime.InteropServices;

namespace MicaWPF.Core.Interop;

#pragma warning disable IDE0079 // Remove unnecessary suppression
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Interops")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "Interops")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:Accessible fields should begin with upper-case letter", Justification = "Interops")]
public static class InteropValues
{
    [Flags]
    public enum DWMWINDOWATTRIBUTE
    {
        DWMWA_NCRENDERING_ENABLED,
        DWMWA_NCRENDERING_POLICY,
        DWMWA_TRANSITIONS_FORCEDISABLED,
        DWMWA_ALLOW_NCPAINT,
        DWMWA_CAPTION_BUTTON_BOUNDS,
        DWMWA_NONCLIENT_RTL_LAYOUT,
        DWMWA_FORCE_ICONIC_REPRESENTATION,
        DWMWA_FLIP3D_POLICY,
        DWMWA_EXTENDED_FRAME_BOUNDS,
        DWMWA_HAS_ICONIC_BITMAP,
        DWMWA_DISALLOW_PEEK,
        DWMWA_EXCLUDED_FROM_PEEK,
        DWMWA_CLOAK,
        DWMWA_CLOAKED,
        DWMWA_FREEZE_REPRESENTATION,
        DWMWA_PASSIVE_UPDATE_MODE,
        DWMWA_USE_HOSTBACKDROPBRUSH,
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,
        DWMWA_BORDER_COLOR,
        DWMWA_CAPTION_COLOR,
        DWMWA_TEXT_COLOR,
        DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
        DWMWA_SYSTEMBACKDROP_TYPE,
        DWMWA_LAST,
        DWMWA_MICA_EFFECT = 1029,
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

    public enum DeviceCap
    {
        /// <summary>
        /// Device driver version.
        /// </summary>
        DRIVERVERSION = 0,

        /// <summary>
        /// Device classification.
        /// </summary>
        TECHNOLOGY = 2,

        /// <summary>
        /// Horizontal size in millimeters.
        /// </summary>
        HORZSIZE = 4,

        /// <summary>
        /// Vertical size in millimeters.
        /// </summary>
        VERTSIZE = 6,

        /// <summary>
        /// Horizontal width in pixels.
        /// </summary>
        HORZRES = 8,

        /// <summary>
        /// Vertical height in pixels.
        /// </summary>
        VERTRES = 10,

        /// <summary>
        /// Number of bits per pixel.
        /// </summary>
        BITSPIXEL = 12,

        /// <summary>
        /// Number of planes.
        /// </summary>
        PLANES = 14,

        /// <summary>
        /// Number of brushes the device has.
        /// </summary>
        NUMBRUSHES = 16,

        /// <summary>
        /// Number of pens the device has.
        /// </summary>
        NUMPENS = 18,

        /// <summary>
        /// Number of markers the device has.
        /// </summary>
        NUMMARKERS = 20,

        /// <summary>
        /// Number of fonts the device has.
        /// </summary>
        NUMFONTS = 22,

        /// <summary>
        /// Number of colors the device supports.
        /// </summary>
        NUMCOLORS = 24,

        /// <summary>
        /// Size required for device descriptor.
        /// </summary>
        PDEVICESIZE = 26,

        /// <summary>
        /// Curve capabilities.
        /// </summary>
        CURVECAPS = 28,

        /// <summary>
        /// Line capabilities.
        /// </summary>
        LINECAPS = 30,

        /// <summary>
        /// Polygonal capabilities.
        /// </summary>
        POLYGONALCAPS = 32,

        /// <summary>
        /// Text capabilities.
        /// </summary>
        TEXTCAPS = 34,

        /// <summary>
        /// Clipping capabilities.
        /// </summary>
        CLIPCAPS = 36,

        /// <summary>
        /// Bitblt capabilities.
        /// </summary>
        RASTERCAPS = 38,

        /// <summary>
        /// Length of the X leg.
        /// </summary>
        ASPECTX = 40,

        /// <summary>
        /// Length of the Y leg.
        /// </summary>
        ASPECTY = 42,

        /// <summary>
        /// Length of the hypotenuse.
        /// </summary>
        ASPECTXY = 44,

        /// <summary>
        /// Shading and Blending caps.
        /// </summary>
        SHADEBLENDCAPS = 45,

        /// <summary>
        /// Logical pixels inch in X.
        /// </summary>
        LOGPIXELSX = 88,

        /// <summary>
        /// Logical pixels inch in Y.
        /// </summary>
        LOGPIXELSY = 90,

        /// <summary>
        /// Number of entries in physical palette.
        /// </summary>
        SIZEPALETTE = 104,

        /// <summary>
        /// Number of reserved entries in palette.
        /// </summary>
        NUMRESERVED = 106,

        /// <summary>
        /// Actual color resolution.
        /// </summary>
        COLORRES = 108,

        // Printing related DeviceCaps. These replace the appropriate Escapes

        /// <summary>
        /// Physical Width in device units.
        /// </summary>
        PHYSICALWIDTH = 110,

        /// <summary>
        /// Physical Height in device units.
        /// </summary>
        PHYSICALHEIGHT = 111,

        /// <summary>
        /// Physical Printable Area x margin.
        /// </summary>
        PHYSICALOFFSETX = 112,

        /// <summary>
        /// Physical Printable Area y margin.
        /// </summary>
        PHYSICALOFFSETY = 113,

        /// <summary>
        /// Scaling factor x.
        /// </summary>
        SCALINGFACTORX = 114,

        /// <summary>
        /// Scaling factor y.
        /// </summary>
        SCALINGFACTORY = 115,

        /// <summary>
        /// Current vertical refresh rate of the display device (for displays only) in Hz.
        /// </summary>
        VREFRESH = 116,

        /// <summary>
        /// Vertical height of entire desktop in pixels.
        /// </summary>
        DESKTOPVERTRES = 117,

        /// <summary>
        /// Horizontal width of entire desktop in pixels.
        /// </summary>
        DESKTOPHORZRES = 118,

        /// <summary>
        /// Preferred blt alignment.
        /// </summary>
        BLTALIGNMENT = 119,
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
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MONITORINFO
    {
        public uint cbSize;
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
        public const int DWMWA_COLOR_NONE = unchecked((int)0xFFFFFFFE);
    }
}
#pragma warning restore IDE0079 // Remove unnecessary suppression
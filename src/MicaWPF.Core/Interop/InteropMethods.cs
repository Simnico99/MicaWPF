// <copyright file="InteropMethods.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Runtime.InteropServices;
using System.Security;
using static MicaWPF.Core.Interop.InteropValues;

namespace MicaWPF.Core.Interop;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#if NET7_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "Interops")]
public static partial class InteropMethods
{
    /// <summary>
    /// Hides all window buttons for the specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    public static void HideAllWindowButton(nint hwnd)
    {
        _ = SetWindowLong(hwnd, HwndButtons.GWL_STYLE, GetWindowLong(hwnd, HwndButtons.GWL_STYLE) & ~HwndButtons.WS_SYSMENU);
    }

    /// <summary>
    /// Sets a window attribute with a specific parameter.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="attribute">The attribute to set.</param>
    /// <param name="parameter">The parameter for the attribute.</param>
    /// <returns>The result of the DwmSetWindowAttribute function.</returns>
    public static int SetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute, int parameter)
    {
        return DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }

    /// <summary>
    /// Rounds the corners of the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="cornerPrefrence">The corner preference, defaults to rounded.</param>
    /// <returns>The result of the DwmSetWindowAttribute function.</returns>
    public static int RoundWindowCorner(nint hWnd, DWM_WINDOW_CORNER_PREFERENCE cornerPrefrence = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND)
    {
        var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
        var preference = (int)cornerPrefrence;
        return DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
    }

    /// <summary>
    /// Retrieves the colorization parameters for Desktop Window Manager (DWM).
    /// </summary>
    /// <returns>The current DWM colorization parameters.</returns>
    public static DWMCOLORIZATIONPARAMS GetDwmGetColorizationParameters()
    {
        DwmGetColorizationParameters(out var dwParameters);
        return dwParameters;
    }

    /// <summary>
    /// Retrieves the colorization parameters.
    /// </summary>
    /// <param name="dwParameters">The output parameter that receives colorization parameters.</param>
    [DllImport(ExternDll.DwmApi, EntryPoint = "#127", PreserveSig = false, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time", Justification = "Doesn't work.")]
    internal static extern void DwmGetColorizationParameters(out DWMCOLORIZATIONPARAMS dwParameters);

    /// <summary>
    /// Retrieves the device-capabilities value for a specified device.
    /// </summary>
    /// <param name="hDC">A handle to the device context.</param>
    /// <param name="nIndex">The item to return.</param>
    /// <returns>The device-capabilities value for the specified item.</returns>
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

    /// <summary>
    /// Extends the frame of the window into the client area.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="pMarInset">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
    /// <returns>Zero if the operation is successful; otherwise, a non-zero value.</returns>
    [LibraryImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int DwmExtendFrameIntoClientArea(nint hwnd, ref MARGINS pMarInset);

    /// <summary>
    /// Retrieves a handle to a display device context (DC) for the client area of a specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="hrgnclip">A clipping region that may be combined with the visible region of the DC.</param>
    /// <param name="fdwOptions">Specifies how the DC is created.</param>
    /// <returns>If the function succeeds, the return value is the handle to the DC for the specified window's client area. If the function fails, the return value is NULL.</returns>
    [LibraryImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial nint GetDCEx(nint hwnd, nint hrgnclip, uint fdwOptions);

    /// <summary>
    /// Retrieves a handle to a device context (DC) for the client area of the specified window or for the entire screen.
    /// </summary>
    /// <param name="ptr">A handle to the window. If this parameter is NULL, GetDC retrieves the DC for the entire screen.</param>
    /// <returns>The handle to the DC for the specified window's client area. If the function fails, the return value is NULL.</returns>
    [LibraryImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial nint GetDC(nint ptr);

    /// <summary>
    /// Releases a device context (DC), freeing it for use by other applications.
    /// </summary>
    /// <param name="window">A handle to the window whose DC is to be released.</param>
    /// <param name="dc">A handle to the DC to be released.</param>
    /// <returns>If the DC was released successfully, the return value is 1. If the DC was not released successfully, the return value is zero.</returns>
    [LibraryImport(ExternDll.User32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int ReleaseDC(nint window, nint dc);

    /// <summary>
    /// Retrieves device-specific information for the specified device.
    /// </summary>
    /// <param name="hdc">A handle to the DC.</param>
    /// <param name="nIndex">The item to be returned.</param>
    /// <returns>The device-specific information being queried. If the function fails, the return value is zero.</returns>
    [LibraryImport(ExternDll.Gdi32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int GetDeviceCaps(nint hdc, int nIndex);

    /// <summary>
    /// Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.
    /// </summary>
    /// <param name="handle">A handle to the window of interest.</param>
    /// <param name="flags">Determines the function's return value if the window does not intersect any display monitor.</param>
    /// <returns>If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.</returns>
    [LibraryImport(ExternDll.User32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial nint MonitorFromWindow(nint handle, uint flags);

    /// <summary>
    /// Retrieves information about a display monitor.
    /// </summary>
    /// <param name="hMonitor">A handle to the display monitor of interest.</param>
    /// <param name="lpmi">A pointer to a MONITORINFO or MONITORINFOEX structure that receives information about the specified display monitor.</param>
    /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
    [LibraryImport(ExternDll.User32, EntryPoint = "GetMonitorInfoW")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Retrieves the version of the operating system that is currently running under the Windows NT Kernel.
    /// </summary>
    /// <param name="versionInfo">An OSVERSIONINFOEX structure that receives the operating system version information.</param>
    /// <returns>If the function succeeds, the return value is a nonzero value.</returns>
    [SecurityCritical]
    [DllImport(ExternDll.NTdll, SetLastError = true, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time", Justification = "Cannot marshal OSVERSIONINFOEX")]
    internal static extern int RtlGetVersion(out OSVERSIONINFOEX versionInfo);

    [LibraryImport(ExternDll.User32, EntryPoint = "GetWindowLongPtrA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static partial int GetWindowLong(nint hWnd, int nIndex);

    [LibraryImport(ExternDll.User32, EntryPoint = "SetWindowLongPtrA")]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static partial int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);

    [LibraryImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static partial int DwmSetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);
}
#else
public static class InteropMethods
{
    /// <summary>
    /// Hides all window buttons for the specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    public static void HideAllWindowButton(nint hwnd)
    {
        _ = SetWindowLong(hwnd, HwndButtons.GWL_STYLE, GetWindowLong(hwnd, HwndButtons.GWL_STYLE) & ~HwndButtons.WS_SYSMENU);
    }

    /// <summary>
    /// Sets a window attribute with a specific parameter.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="attribute">The attribute to set.</param>
    /// <param name="parameter">The parameter for the attribute.</param>
    /// <returns>The result of the DwmSetWindowAttribute function.</returns>
    public static int SetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute, int parameter)
    {
        return DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }

    /// <summary>
    /// Rounds the corners of the specified window.
    /// </summary>
    /// <param name="hWnd">A handle to the window.</param>
    /// <param name="cornerPrefrence">The corner preference, defaults to rounded.</param>
    /// <returns>The result of the DwmSetWindowAttribute function.</returns>
    public static int RoundWindowCorner(nint hWnd, DWM_WINDOW_CORNER_PREFERENCE cornerPrefrence = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND)
    {
        var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
        var preference = (int)cornerPrefrence;
        return DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
    }

    /// <summary>
    /// Retrieves the colorization parameters for Desktop Window Manager (DWM).
    /// </summary>
    /// <returns>The current DWM colorization parameters.</returns>
    public static DWMCOLORIZATIONPARAMS GetDwmGetColorizationParameters()
    {
        DwmGetColorizationParameters(out var dwParameters);
        return dwParameters;
    }

    /// <summary>
    /// Extends the frame of the window into the client area.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="pMarInset">A pointer to a MARGINS structure that describes the margins to use when extending the frame into the client area.</param>
    /// <returns>Zero if the operation is successful; otherwise, a non-zero value.</returns>
    [DllImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int DwmExtendFrameIntoClientArea(nint hwnd, ref MARGINS pMarInset);

    /// <summary>
    /// Retrieves the colorization parameters.
    /// </summary>
    /// <param name="dwParameters">The output parameter that receives colorization parameters.</param>
    [DllImport(ExternDll.DwmApi, EntryPoint = "#127", PreserveSig = false, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern void DwmGetColorizationParameters(out DWMCOLORIZATIONPARAMS dwParameters);

    /// <summary>
    /// Retrieves a handle to a display device context (DC) for the client area of a specified window.
    /// </summary>
    /// <param name="hwnd">A handle to the window.</param>
    /// <param name="hrgnclip">A clipping region that may be combined with the visible region of the DC.</param>
    /// <param name="fdwOptions">Specifies how the DC is created.</param>
    /// <returns>If the function succeeds, the return value is the handle to the DC for the specified window's client area. If the function fails, the return value is NULL.</returns>
    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern nint GetDCEx(nint hwnd, nint hrgnclip, uint fdwOptions);

    /// <summary>
    /// Retrieves a handle to a device context (DC) for the client area of the specified window or for the entire screen.
    /// </summary>
    /// <param name="ptr">A handle to the window. If this parameter is NULL, GetDC retrieves the DC for the entire screen.</param>
    /// <returns>The handle to the DC for the specified window's client area. If the function fails, the return value is NULL.</returns>
    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern nint GetDC(nint ptr);

    /// <summary>
    /// Releases a device context (DC), freeing it for use by other applications.
    /// </summary>
    /// <param name="window">A handle to the window whose DC is to be released.</param>
    /// <param name="dc">A handle to the DC to be released.</param>
    /// <returns>If the DC was released successfully, the return value is 1. If the DC was not released successfully, the return value is zero.</returns>
    [DllImport(ExternDll.User32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int ReleaseDC(nint window, nint dc);

    /// <summary>
    /// Retrieves device-specific information for the specified device.
    /// </summary>
    /// <param name="hdc">A handle to the DC.</param>
    /// <param name="nIndex">The item to be returned.</param>
    /// <returns>The device-specific information being queried. If the function fails, the return value is zero.</returns>
    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int GetDeviceCaps(nint hdc, int nIndex);

    /// <summary>
    /// Retrieves the device-capabilities value for a specified device.
    /// </summary>
    /// <param name="hDC">A handle to the device context.</param>
    /// <param name="nIndex">The item to return.</param>
    /// <returns>The device-capabilities value for the specified item.</returns>
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

    /// <summary>
    /// Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.
    /// </summary>
    /// <param name="handle">A handle to the window of interest.</param>
    /// <param name="flags">Determines the function's return value if the window does not intersect any display monitor.</param>
    /// <returns>If the window intersects one or more display monitor rectangles, the return value is an HMONITOR handle to the display monitor that has the largest area of intersection with the window.</returns>
    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern nint MonitorFromWindow(nint handle, uint flags);

    /// <summary>
    /// Retrieves information about a display monitor.
    /// </summary>
    /// <param name="hMonitor">A handle to the display monitor of interest.</param>
    /// <param name="lpmi">A pointer to a MONITORINFO or MONITORINFOEX structure that receives information about the specified display monitor.</param>
    /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);

    /// <summary>
    /// Retrieves the version of the operating system that is currently running under the Windows NT Kernel.
    /// </summary>
    /// <param name="versionInfo">An OSVERSIONINFOEX structure that receives the operating system version information.</param>
    /// <returns>If the function succeeds, the return value is a nonzero value.</returns>
    [SecurityCritical]
    [DllImport(ExternDll.NTdll, SetLastError = true, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int RtlGetVersion(out OSVERSIONINFOEX versionInfo);

    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int GetWindowLong(nint hWnd, int nIndex);

    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int SetWindowLong(nint hWnd, int nIndex, int dwNewLong);

    [DllImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int DwmSetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);
}
#endif
#pragma warning restore IDE0079 // Remove unnecessary suppression
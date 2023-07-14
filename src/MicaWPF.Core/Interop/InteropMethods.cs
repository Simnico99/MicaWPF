// <copyright file="InteropMethods.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Security;
using static MicaWPF.Core.Interop.InteropValues;

namespace MicaWPF.Core.Interop;

#if NET7_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:Partial elements should be documented", Justification = "Interops")]
public sealed partial class InteropMethods
{
    public static void HideAllWindowButton(nint hwnd)
    {
        _ = SetWindowLong(hwnd, HwndButtons.GWL_STYLE, GetWindowLong(hwnd, HwndButtons.GWL_STYLE) & ~HwndButtons.WS_SYSMENU);
    }

    public static int SetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute, int parameter)
    {
        return DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }

    public static int RoundWindowCorner(nint hWnd, DWM_WINDOW_CORNER_PREFERENCE cornerPrefrence = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND)
    {
        var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
        var preference = (int)cornerPrefrence;
        return DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
    }

    public static DWMCOLORIZATIONPARAMS GetDwmGetColorizationParameters()
    {
        DwmGetColorizationParameters(out var dwParameters);
        return dwParameters;
    }

    [DllImport(ExternDll.DwmApi, EntryPoint = "#127", PreserveSig = false, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "SYSLIB1054:Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time", Justification = "Cannot marshal DWMCOLORIZATIONPARAMS")]
    internal static extern void DwmGetColorizationParameters(out DWMCOLORIZATIONPARAMS dwParameters);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

    [LibraryImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int DwmExtendFrameIntoClientArea(nint hwnd, ref MARGINS pMarInset);

    [LibraryImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial nint GetDCEx(nint hwnd, nint hrgnclip, uint fdwOptions);

    [LibraryImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial nint GetDC(nint ptr);

    [LibraryImport(ExternDll.User32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int ReleaseDC(nint window, nint dc);

    [LibraryImport(ExternDll.Gdi32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial int GetDeviceCaps(nint hdc, int nIndex);

    [LibraryImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static partial nint MonitorFromWindow(nint handle, uint flags);

    [LibraryImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);

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
public sealed class InteropMethods
{
    public static void HideAllWindowButton(nint hwnd)
    {
        _ = SetWindowLong(hwnd, HwndButtons.GWL_STYLE, GetWindowLong(hwnd, HwndButtons.GWL_STYLE) & ~HwndButtons.WS_SYSMENU);
    }

    public static int SetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE attribute, int parameter)
    {
        return DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }

    public static int RoundWindowCorner(nint hWnd, DWM_WINDOW_CORNER_PREFERENCE cornerPrefrence = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND)
    {
        var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
        var preference = (int)cornerPrefrence;
        return DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
    }

    public static DWMCOLORIZATIONPARAMS GetDwmGetColorizationParameters()
    {
        DwmGetColorizationParameters(out var dwParameters);
        return dwParameters;
    }

    [DllImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int DwmExtendFrameIntoClientArea(nint hwnd, ref MARGINS pMarInset);

    [DllImport(ExternDll.DwmApi, EntryPoint = "#127", PreserveSig = false, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern void DwmGetColorizationParameters(out DWMCOLORIZATIONPARAMS dwParameters);

    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern nint GetDCEx(nint hwnd, nint hrgnclip, uint fdwOptions);

    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern nint GetDC(nint ptr);

    [DllImport(ExternDll.User32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int ReleaseDC(nint window, nint dc);

    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int GetDeviceCaps(nint hdc, int nIndex);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern nint MonitorFromWindow(nint handle, uint flags);

    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern bool GetMonitorInfo(nint hMonitor, ref MONITORINFO lpmi);

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
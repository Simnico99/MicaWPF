using System.Security;
using static MicaWPF.Interop.InteropValues;

namespace MicaWPF.Interop;

internal class InteropMethods
{
    [DllImport(ExternDll.User32)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport(ExternDll.User32)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

    [DllImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

    [DllImport(ExternDll.DwmApi, EntryPoint = "#127", PreserveSig = false, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern void DwmGetColorizationParameters(out DWMCOLORIZATIONPARAMS dwParameters);

    [SecurityCritical]
    [DllImport(ExternDll.NTdll, SetLastError = true, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int RtlGetVersion(out OSVERSIONINFOEX versionInfo);

    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern IntPtr GetDC(IntPtr ptr);

    [DllImport(ExternDll.User32, SetLastError = true)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int ReleaseDC(IntPtr window, IntPtr dc);

    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern IntPtr MonitorFromWindow(IntPtr handle, uint flags);

    [DllImport(ExternDll.User32)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    public static void HideAllWindowButton(IntPtr hwnd)
    {
        SetWindowLong(hwnd, HwndButtonStates.GWL_STYLE, GetWindowLong(hwnd, HwndButtonStates.GWL_STYLE) & ~HwndButtonStates.WS_SYSMENU);
    }

    public static int SetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, int parameter)
    {
        return DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }
}

using System.Security;
using static MicaWPF.Interop.InteropValues;

namespace MicaWPF.Interop;

internal class InteropMethods
{
    [DllImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

    [DllImport(ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

    [SecurityCritical]
    [DllImport(ExternDll.NTdll, SetLastError = true, CharSet = CharSet.Unicode)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    internal static extern int RtlGetVersion(ref OSVERSIONINFOEX versionInfo);

    public static int SetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, int parameter)
    {
        return DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }
}

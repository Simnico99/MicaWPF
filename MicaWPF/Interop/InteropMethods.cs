namespace MicaWPF.Interop;

internal class InteropMethods
{
    [DllImport(InteropValues.ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, InteropValues.DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

    [DllImport(InteropValues.ExternDll.DwmApi)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref InteropValues.MARGINS pMarInset);

    public static int SetWindowAttribute(IntPtr hwnd, InteropValues.DWMWINDOWATTRIBUTE attribute, int parameter)
    {
        return DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }
}

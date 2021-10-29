using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace MicaWPF.Helpers
{
    public static class MicaHelper
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

        [Flags]
        private enum DwmWindowAttribute : uint
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_MICA_EFFECT = 1029
        }

        public static void UpdateStyleAttributes(this Window window)
        {
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;
            var darkThemeEnabled = ThemeHelper.GetWindowsTheme();

            int trueValue = 0x01;
            int falseValue = 0x00;

            _ = darkThemeEnabled == WindowsTheme.Dark
                ? DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)))
                : DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));

            _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
        }
    }
}
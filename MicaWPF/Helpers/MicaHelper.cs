using Microsoft.Win32;
using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;

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

        private static ManagementObject GetMngObj(string className)
        {
            var wmi = new ManagementClass(className);

            foreach (var o in wmi.GetInstances())
            {
                var mo = (ManagementObject)o;
                if (mo != null) return mo;
            }

            return null;
        }

        public static Version GetOsVer()
        {
            ManagementObject mo = GetMngObj("Win32_OperatingSystem");

            if (null == mo)
            {
                return new Version(0, 0, 0, 0);
            }

            return Version.Parse(mo["Version"].ToString());
        }

        public static void EnableMica(this Window window, WindowsTheme theme)
        {
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;
            var darkThemeEnabled = ThemeHelper.GetWindowsTheme();

            int trueValue = 0x01;
            int falseValue = 0x00;


            if (theme is WindowsTheme.Auto)
            {
                _ = darkThemeEnabled == WindowsTheme.Dark
                    ? DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)))
                    : DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));
            }
            else if (theme is WindowsTheme.Light)
            {
                _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));
            }
            else
            {
                _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)));
            }
            if (GetOsVer() >= new Version(10, 0, 22000, 0) && Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
            }
            else
            {

                WindowChrome.SetWindowChrome(
                    window,
                    new WindowChrome()
                    {
                        CaptionHeight = 20,
                        ResizeBorderThickness = new Thickness(8),
                        CornerRadius = new CornerRadius(0),
                        GlassFrameThickness = new Thickness(0,32,0,0),
                        UseAeroCaptionButtons = true
                    }
                    );
            }
        }
    }
}
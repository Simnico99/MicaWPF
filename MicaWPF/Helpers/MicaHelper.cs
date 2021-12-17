using MicaWPF.Controls;
using static MicaWPF.Helpers.PInvoke.ParameterTypes;
using static MicaWPF.Helpers.PInvoke.Methods;

namespace MicaWPF.Helpers;

public static class MicaHelper
{
    private delegate void NoArgDelegate();

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

    [Flags]
    private enum DwmWindowAttribute : uint
    {
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_MICA_EFFECT = 1029
    }

    private static void SetMica(MicaWindow window, WindowsTheme theme, OsVersion osVersion)
    {
        if (osVersion is OsVersion.Windows11OldMethod or OsVersion.Windows11NewMethod)
        {
            int trueValue = 0x01;
            int falseValue = 0x00;

            WindowChrome.SetWindowChrome(
            window,
            new WindowChrome()
            {
                CaptionHeight = 20,
                ResizeBorderThickness = new Thickness(8),
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(-1),
                UseAeroCaptionButtons = true
            });

            window.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            IntPtr windowHandle = new WindowInteropHelper(window).Handle;
            if (theme == WindowsTheme.Dark)
            {
                SetThemeBrushes(window, theme);
                _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)));
            }
            else
            {
                SetThemeBrushes(window, theme);
                _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));
            }

            if (osVersion == OsVersion.Windows11NewMethod)
            {
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, 2);
            }
            else
            {
                _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
            }

        }
        else
        {
            if (osVersion == OsVersion.WindowsOld || theme == WindowsTheme.Light)
            {
                SetThemeBrushes(window, theme);
            }
            else
            {
                SetThemeBrushes(window, theme);
            }
        }

    }

    private static void SetThemeBrushes(MicaWindow window, WindowsTheme theme)
    {
        if (theme == WindowsTheme.Light)
        {
            window.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 100, 100, 100));
            window.ForegroundColor = Color.FromArgb(0xFF, 0, 0, 0);
            window.HighLightColor = Color.FromArgb(0xFF, 230, 230, 230);
            window.BackgroundColor = Color.FromArgb(0xFF, 243, 243, 243);
        }
        else
        {
            window.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 100, 100, 100));
            window.ForegroundColor = Color.FromArgb(0xFF, 255, 255, 255);
            window.HighLightColor = Color.FromArgb(0xFF, 41, 41, 41);
            window.BackgroundColor = Color.FromArgb(0xFF, 32, 32, 32);
        }
    }

    public static void EnableMica(this MicaWindow window, WindowsTheme theme, bool isThemeAware)
    {
        OsVersion osVersion = OsHelper.GetOsVersion();

        if (theme == WindowsTheme.Auto || isThemeAware == true)
        {
            WindowsTheme currentWindowsTheme = ThemeHelper.GetWindowsTheme();
            SetMica(window, currentWindowsTheme, osVersion);
        }
        else
        {
            SetMica(window, theme, osVersion);
        }

        if (osVersion is not OsVersion.WindowsOld)
        {
            if (isThemeAware is true)
            {
                SystemEvents.UserPreferenceChanged += (s, e) =>
                {
                    switch (e.Category)
                    {
                        case UserPreferenceCategory.General:
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                EnableMica(window, WindowsTheme.Auto, false);
                            });
                            break;
                    }
                };
            }
        }
    }
}

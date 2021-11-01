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

    private static void SetMica(Window window, WindowsTheme theme, OsVersion osVersion)
    {
        if (osVersion == OsVersion.Windows11)
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
            }
            );

            window.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            IntPtr windowHandle = new WindowInteropHelper(window).Handle;
            _ = theme == WindowsTheme.Dark
                ? DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref trueValue, Marshal.SizeOf(typeof(int)))
                : DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE, ref falseValue, Marshal.SizeOf(typeof(int)));

            _ = DwmSetWindowAttribute(windowHandle, DwmWindowAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
        }
        else
        {
            window.Background = osVersion == OsVersion.WindowsOld || theme == WindowsTheme.Light
            ? new SolidColorBrush(Color.FromArgb(0xFF, 243, 243, 243))
            : new SolidColorBrush(Color.FromArgb(0xFF, 32, 32, 32));
        }
    }

    private static void ThemeAware(Window window)
    {
        _ = Task.Run(() =>
        {
            ManagementEventWatcher watcher = ThemeHelper.WatchThemeChange();
            watcher.EventArrived += (sender, args) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    EnableMica(window, WindowsTheme.Auto, true);
                });
            };
            watcher.Start();
        });
    }

    public static void EnableMica(this Window window, WindowsTheme theme, bool isThemeAware)
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
                ThemeAware(window);
            }
        }
    }
}

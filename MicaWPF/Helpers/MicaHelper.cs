using MicaWPF.Controls;
using static MicaWPF.Helpers.PInvokeHelper.ParameterTypes;
using static MicaWPF.Helpers.PInvokeHelper.Methods;

namespace MicaWPF.Helpers;

public static class MicaHelper
{
    private delegate void NoArgDelegate();



    private static void SetMica(MicaWindow window, WindowsTheme theme, OsVersion osVersion, BackdropType micaType, int captionHeight)
    {
        if (osVersion is OsVersion.Windows11Before22523 or OsVersion.Windows11After22523)
        {
            int trueValue = 0x01;
            int falseValue = 0x00;

            WindowChrome.SetWindowChrome(window,
                new WindowChrome()
                {
                    CaptionHeight = captionHeight,
                    ResizeBorderThickness = new Thickness(8),
                    CornerRadius = new CornerRadius(0),
                    GlassFrameThickness = new Thickness(-1),
                    UseAeroCaptionButtons = true
                });

            window.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            IntPtr windowHandle = new WindowInteropHelper(window).Handle;
            if (theme == WindowsTheme.Dark)
            {
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, trueValue);
                SetThemeBrushes(window, theme);
            }
            else
            {   
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, falseValue);
                SetThemeBrushes(window, theme);
            }

            if (osVersion == OsVersion.Windows11After22523)
            {
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, (int)micaType);
            }
            else
            {
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT, trueValue);
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

    public static void EnableMica(this MicaWindow window, WindowsTheme theme = WindowsTheme.Auto, bool isThemeAware = true, BackdropType micaType = BackdropType.Mica, int captionHeight = 20)
    {
        OsVersion osVersion = OsHelper.GetOsVersion();

        if (theme == WindowsTheme.Auto || isThemeAware == true)
        {
            WindowsTheme currentWindowsTheme = ThemeHelper.GetWindowsTheme();
            SetMica(window, currentWindowsTheme, osVersion, micaType, captionHeight);
        }
        else
        {
            SetMica(window, theme, osVersion, micaType, captionHeight);
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
                                EnableMica(window, WindowsTheme.Auto, false, micaType, captionHeight);
                            });
                            break;
                    }
                };
            }
        }
    }
}

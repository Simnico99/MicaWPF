using MicaWPF.Controls;
using static MicaWPF.Helpers.PInvokeHelper.Methods;
using static MicaWPF.Helpers.PInvokeHelper.ParameterTypes;

namespace MicaWPF.Helpers;

public static class MicaHelper
{
    private delegate void NoArgDelegate();

    private static void SetMica(Window window, WindowsTheme theme, OsVersion osVersion, BackdropType micaType, int captionHeight)
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

            if (theme == WindowsTheme.Dark && osVersion == OsVersion.Windows11Before22523 || osVersion == OsVersion.Windows11After22523)
            {
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, trueValue);
            }
            else if(osVersion == OsVersion.Windows11Before22523 || osVersion == OsVersion.Windows11After22523)
            {
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, falseValue);
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
        ThemeHelper.SetThemeBrushes(window, theme);
    }

    public static void EnableMica(this Window window, WindowsTheme theme = WindowsTheme.Auto, bool isThemeAware = true, BackdropType micaType = BackdropType.Mica, int captionHeight = 20, bool waitForManualThemeChange = false)
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

        if (waitForManualThemeChange)
        {
            _ = Task.Run(async () =>
            {
                if (window is MicaWindow micaWindow)
                {
                    var oldTheme = theme;
                    while (oldTheme == micaWindow.Theme)
                    {
                        await Task.Delay(500);
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        EnableMica(window, micaWindow.Theme, isThemeAware, micaType, captionHeight, true);
                    });
                }
            });
        }


        if (osVersion is not OsVersion.WindowsOld && isThemeAware)
        {
            SystemEvents.UserPreferenceChanged += (s, e) =>
            {
                switch (e.Category)
                {
                    case UserPreferenceCategory.General:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            EnableMica(window, WindowsTheme.Auto, false, micaType, captionHeight, false);
                        });
                        break;
                }
            };
        }
    }
}

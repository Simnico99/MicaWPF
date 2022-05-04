using MicaWPF.Controls;

namespace MicaWPF.Helpers;
public static class MicaHelper
{
    private static readonly AccentColorService _accentColorService = AccentColorService.GetCurrent();

    private static void SetWindowProperties(Window window, WindowsTheme theme, BackdropType micaType, int captionHeight, bool useSystemAccent)
    {
        if (OsHelper.GlobalOsVersion is OsVersion.Windows11Before22523 or OsVersion.Windows11After22523)
        {
            if (captionHeight != -1)
            {
                WindowChrome.SetWindowChrome(window,
                    new WindowChrome()
                    {
                        CaptionHeight = captionHeight,
                        ResizeBorderThickness = new Thickness(8),
                        CornerRadius = new CornerRadius(0),
                        GlassFrameThickness = new Thickness(-1),
                        UseAeroCaptionButtons = true
                    });
            }

            window.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            var windowHandle = new WindowInteropHelper(window).Handle;

            if (window is MicaWindow micaWindow)
            {
                if (!useSystemAccent)
                {
                    var accentColor = micaWindow.Accent?.Color;
                    if (accentColor is not null)
                    {
                        _accentColorService.SetAccents((Color)accentColor, theme);
                    }
                }
            }

            if (theme == WindowsTheme.Dark)
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.True);
            }
            else if (OsHelper.GlobalOsVersion is OsVersion.Windows11Before22523 or OsVersion.Windows11After22523)
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, InteropValues.DwmValues.False);
            }

            if (OsHelper.GlobalOsVersion == OsVersion.Windows11After22523)
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, (int)micaType);
            }
            else
            {
                InteropMethods.SetWindowAttribute(windowHandle, InteropValues.DWMWINDOWATTRIBUTE.DWMWA_MICA_EFFECT, InteropValues.DwmValues.True);
            }
        }

        if (useSystemAccent)
        {
            if (window is MicaWindow micaWindow)
            {
                _accentColorService.UpdateAccentsFromWindows(theme);
            }
        }

        ThemeDictionnaryHelper.SetCurrentThemeDictionary(window, ThemeHelper.WindowsThemeToResourceTheme(theme));
    }

    public static void EnableMica(this Window window, WindowsTheme theme = WindowsTheme.Auto, BackdropType micaType = BackdropType.Mica, int captionHeight = 20, bool useSystemAccent = true)
    {
        if (theme == WindowsTheme.Auto)
        {
            var currentWindowsTheme = ThemeHelper.GetWindowsTheme();
            SetWindowProperties(window, currentWindowsTheme, micaType, captionHeight, useSystemAccent);
        }
        else
        {
            SetWindowProperties(window, theme, micaType, captionHeight, useSystemAccent);
        }
    }
}

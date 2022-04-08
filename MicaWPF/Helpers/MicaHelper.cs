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

            if (captionHeight != -1) { 
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
            IntPtr windowHandle = new WindowInteropHelper(window).Handle;

            if (theme == WindowsTheme.Dark)
            {
                SetWindowAttribute(windowHandle, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, trueValue);
            }
            else if (osVersion is OsVersion.Windows11Before22523 or OsVersion.Windows11After22523)
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

    public static void EnableMica(this Window window, WindowsTheme theme = WindowsTheme.Auto, BackdropType micaType = BackdropType.Mica, int captionHeight = 20)
    {
        OsVersion osVersion = OsHelper.GetOsVersion();

        if (theme == WindowsTheme.Auto)
        {
            WindowsTheme currentWindowsTheme = ThemeHelper.GetWindowsTheme();
            SetMica(window, currentWindowsTheme, osVersion, micaType, captionHeight);
        }
        else
        {
            SetMica(window, theme, osVersion, micaType, captionHeight);
        }
    }
}

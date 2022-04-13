namespace MicaWPF.Helpers;

public static class ThemeHelper
{
    private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private const string RegistryValueName = "AppsUseLightTheme";

    public static WindowsTheme GetWindowsTheme()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
        var registryValueObject = key?.GetValue(RegistryValueName);
        if (registryValueObject == null)
        {
            return WindowsTheme.Light;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
    }

    private static SolidColorBrush SafeEdit(this SolidColorBrush color)
    {
        if (color.IsFrozen)
        {
            color = color.Clone();
        }
        return color;
    }

    private static void ReplaceBrush(Window window, string ressourceName, SolidColorBrush brush, double opacity = 1)
    {
        brush.SafeEdit().Opacity = opacity;
        window.Resources.Remove(ressourceName);
        window.Resources.Add(ressourceName, brush);
    }

    public static void SetThemeBrushes(Window window, WindowsTheme theme, bool UseWindowsAccentColor = true)
    {
        SolidColorBrush? color = null;

        if (theme == WindowsTheme.Light)
        {
            if (DefaultColorHelper.IsDefaultColor(WindowsTheme.Dark, "Foreground", (SolidColorBrush)window.Foreground))
            {
                window.Foreground = DefaultColorHelper.LightDefault["Foreground"];
            }

            if (DefaultColorHelper.IsDefaultColor(WindowsTheme.Dark, "Background", (SolidColorBrush)window.Background))
            {
                window.Background = DefaultColorHelper.LightDefault["Background"];
            }

            if (window is MicaWindow micaWindow)
            {
                if (DefaultColorHelper.IsDefaultColor(WindowsTheme.Dark, "Accent", micaWindow.Accent))
                {
                    if (!UseWindowsAccentColor)
                    {
                        micaWindow.Accent = DefaultColorHelper.LightDefault["Accent"];
                        color = micaWindow.Accent;
                    }
                }
                else
                {
                    color = micaWindow.Accent;
                }
            }

            ThemeDictionnaryHelper.SetCurrentThemeDictionary(window, WindowsThemeToResourceTheme(theme));
        }
        else
        {
            if (DefaultColorHelper.IsDefaultColor(WindowsTheme.Light, "Foreground", (SolidColorBrush)window.Foreground))
            {
                window.Foreground = DefaultColorHelper.DarkDefault["Foreground"];
            }

            if (DefaultColorHelper.IsDefaultColor(WindowsTheme.Light, "Background", (SolidColorBrush)window.Background))
            {
                window.Background = DefaultColorHelper.DarkDefault["Background"];
            }

            if (window is MicaWindow micaWindow)
            {
                if (DefaultColorHelper.IsDefaultColor(WindowsTheme.Light, "Accent", micaWindow.Accent))
                {
                    if (!UseWindowsAccentColor)
                    {
                        micaWindow.Accent = DefaultColorHelper.DarkDefault["Accent"];
                        color = micaWindow.Accent;
                    }
                }
                else
                {
                    color = micaWindow.Accent;
                }
            }

            ThemeDictionnaryHelper.SetCurrentThemeDictionary(window, WindowsThemeToResourceTheme(theme));
        }

        GenerateRuntimeColors(window, theme, color);
    }

    public static void GenerateRuntimeColors(Window window, WindowsTheme theme, SolidColorBrush? color = null)
    {
        var accentColor = color;
        if (color is null)
        {
            accentColor = GetWindowsAccentColor(theme);
        }

        if (accentColor is not null)
        {
            ReplaceBrush(window, "MicaAccentMidBrush", accentColor);
            ReplaceBrush(window, "MicaAccentDarkBrush", ChangeColorHue(accentColor.Color, 0.5));
            ReplaceBrush(window, "MicaAccentLightBorderBrush", accentColor, 0.92);
        }
    }

    public static SolidColorBrush GetWindowsAccentColor(WindowsTheme theme)
    {
        var solidGlassColor = SystemParameters.WindowGlassBrush as SolidColorBrush;
        if (theme == WindowsTheme.Dark)
        {
            return ChangeColorHue(solidGlassColor?.Color, 1.57);
        }
        else
        {
            return ChangeColorHue(solidGlassColor?.Color, 0.9);
        }
    }

    public static SolidColorBrush ChangeColorHue(Color? color, double factor)
    {
        if (color == null)
        {
            return new SolidColorBrush();
        }

        var r = (byte)(factor * color.Value.R).Clamp(0, 255);
        var g = (byte)(factor * color.Value.G).Clamp(0, 255);
        var b = (byte)(factor * color.Value.B).Clamp(0, 255);
        return new SolidColorBrush(Color.FromArgb(color.Value.A, r, g, b));
    }

    public static Uri WindowsThemeToResourceTheme(WindowsTheme windowsTheme)
    {
        if (windowsTheme == WindowsTheme.Dark)
        {
            return new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaDark.xaml");
        }
        else
        {
            return new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaLight.xaml");
        }
    }
}

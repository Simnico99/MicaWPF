using MicaWPF.Services;

namespace MicaWPF.Helpers;

public static class ThemeHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private const string _registryValueName = "AppsUseLightTheme";

    public static WindowsTheme GetWindowsTheme()
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        object registryValueObject = key?.GetValue(_registryValueName);
        if (registryValueObject == null)
        {
            return WindowsTheme.Light;
        }

        int registryValue = (int)registryValueObject;

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

    public static void SetThemeBrushes(Window window, WindowsTheme theme)
    {
        if (theme == WindowsTheme.Light)
        {
            window.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 100, 100, 100));

            ThemeService.SetCurrentThemeDictionary(window, WindowsThemeToResourceTheme(theme));
        }
        else
        {
            window.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 100, 100, 100));

            ThemeService.SetCurrentThemeDictionary(window, WindowsThemeToResourceTheme(theme));
        }
        GenerateRuntimeColors(window, theme);
    }

    public static void GenerateRuntimeColors(Window window, WindowsTheme theme)
    {
        SolidColorBrush accentColor = GetWindowsAccentColor(theme);

        ReplaceBrush(window, "MicaAccentMidBrush", accentColor);
        ReplaceBrush(window, "MicaAccentDarkBrush", ChangeColorHue(accentColor.Color, 0.5));
        ReplaceBrush(window, "MicaAccentLightBorderBrush", accentColor, 0.92);
    }

    public static SolidColorBrush GetWindowsAccentColor(WindowsTheme theme)
    {
        SolidColorBrush solidGlassColor = SystemParameters.WindowGlassBrush as SolidColorBrush;
        if (theme == WindowsTheme.Dark)
        {
            return ChangeColorHue(solidGlassColor.Color, 1.57);
        }
        else
        {
            return ChangeColorHue(solidGlassColor.Color, 0.9);
        }
    }

    public static SolidColorBrush ChangeColorHue(Color? color, double factor)
    {
        if (color == null)
        {
            return new SolidColorBrush();
        }

        byte r = (byte)(factor * color.Value.R).Clamp(0, 255);
        byte g = (byte)(factor * color.Value.G).Clamp(0, 255);
        byte b = (byte)(factor * color.Value.B).Clamp(0, 255);
        return new SolidColorBrush(Color.FromArgb(color.Value.A, r, g, b));
    }

    public static Uri WindowsThemeToResourceTheme(WindowsTheme windowsTheme)
    {
        if (windowsTheme == WindowsTheme.Dark)
        {
            return new Uri("pack://application:,,,/MicaWPF;component/Themes/MicaDark.xaml");
        }
        else
        {
            return new Uri("pack://application:,,,/MicaWPF;component/Themes/MicaLight.xaml");
        }
    }
}
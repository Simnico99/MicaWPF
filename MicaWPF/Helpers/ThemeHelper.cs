﻿namespace MicaWPF.Helpers;

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

    public static void SetThemeBrushes(Window window, WindowsTheme theme)
    {
        if (theme == WindowsTheme.Light)
        {
            window.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 100, 100, 100));
            if (window is MicaWindow micaWindow)
            {
                micaWindow.ForegroundColor = Color.FromArgb(0xFF, 0, 0, 0);
                micaWindow.AccentColor = Color.FromArgb(0xFF, 230, 230, 230);
                micaWindow.BackgroundColor = Color.FromArgb(0xFF, 243, 243, 243);
            }

            ThemeService.SetCurrentThemeDictionary(window, WindowsThemeToResourceTheme(theme));
        }
        else
        {
            window.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 100, 100, 100));
            if (window is MicaWindow micaWindow)
            {
                micaWindow.ForegroundColor = Color.FromArgb(0xFF, 255, 255, 255);
                micaWindow.AccentColor = Color.FromArgb(0xFF, 41, 41, 41);
                micaWindow.BackgroundColor = Color.FromArgb(0xFF, 32, 32, 32);

            }

            ThemeService.SetCurrentThemeDictionary(window, WindowsThemeToResourceTheme(theme));
        }
        GenerateRuntimeColors(window, theme);
    }

    public static void GenerateRuntimeColors(Window window, WindowsTheme theme)
    {
        var accentColor = GetWindowsAccentColor(theme);

        ReplaceBrush(window, "MicaAccentMidBrush", accentColor);
        ReplaceBrush(window, "MicaAccentDarkBrush", ChangeColorHue(accentColor.Color, 0.5));
        ReplaceBrush(window, "MicaAccentLightBorderBrush", accentColor, 0.92);
    }

    public static SolidColorBrush GetWindowsAccentColor(WindowsTheme theme)
    {
        var solidGlassColor = SystemParameters.WindowGlassBrush as SolidColorBrush;
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

        var r = (byte)(factor * color.Value.R).Clamp(0, 255);
        var g = (byte)(factor * color.Value.G).Clamp(0, 255);
        var b = (byte)(factor * color.Value.B).Clamp(0, 255);
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

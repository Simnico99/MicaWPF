namespace MicaWPF.Helpers;

public static class DefaultColorHelper
{
    public static Dictionary<string, SolidColorBrush> LightDefault { get; set; } = new() 
    {
        { "Foreground", new(Color.FromArgb(0xFF, 0, 0, 0)) },
        { "Background", new(Color.FromArgb(0xFF, 243, 243, 243))},
        { "Accent", new(Color.FromArgb(0xFF, 230, 230, 230)) }
    };

    public static Dictionary<string, SolidColorBrush> DarkDefault { get; set; } = new()
    {
        { "Foreground", new(Color.FromArgb(0xFF, 255, 255, 255)) },
        { "Background", new(Color.FromArgb(0xFF, 41, 41, 41)) },
        { "Accent", new(Color.FromArgb(0xFF, 32, 32, 32)) }
    };

    public static bool IsDefaultColor(WindowsTheme theme, string colorKey, SolidColorBrush? colorToCompare) 
    {
        if (theme == WindowsTheme.Auto)
        {
            theme = ThemeHelper.GetWindowsTheme();
        }

        if (theme == WindowsTheme.Light)
        {
            return colorToCompare == LightDefault[colorKey];
        }
        else 
        {
            return colorToCompare == DarkDefault[colorKey];
        }
    }

    public static SolidColorBrush GetThemedColor(WindowsTheme theme, string colorKey) 
    {
        if (theme == WindowsTheme.Light)
        {
            return LightDefault[colorKey];
        }
        else
        {
            return DarkDefault[colorKey];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Helpers;
public static class WindowsThemeHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string _registryValueName = "AppsUseLightTheme";

    /// <summary>
    /// Gets the Windows theme (light or dark) from the registry.
    /// </summary>
    /// <returns>The current Windows theme (light or dark).</returns>
    public static WindowsTheme GetCurrentWindowsTheme()
    {
        using var key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        var registryValueObject = key?.GetValue(_registryValueName);

        if (registryValueObject == null)
        {
            return WindowsTheme.Light;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
    }

    /// <summary>
    /// Converts a Windows theme (light or dark) to a resource theme.
    /// </summary>
    /// <param name="windowsTheme">The Windows theme to convert.</param>
    /// <returns>The corresponding resource theme URI.</returns>
    public static Uri WindowsThemeToResourceTheme(WindowsTheme windowsTheme)
    {
        return windowsTheme == WindowsTheme.Dark
            ? new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaDark.xaml")
            : new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaLight.xaml");
    }
}

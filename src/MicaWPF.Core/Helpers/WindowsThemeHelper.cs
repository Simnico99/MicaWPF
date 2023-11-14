// <copyright file="WindowsThemeHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Enums;
using MicaWPF.Core.Services;
using Microsoft.Win32;

namespace MicaWPF.Core.Helpers;

public static class WindowsThemeHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string _registryValueName = "AppsUseLightTheme";

    /// <summary>
    /// Gets the dark theme resource Uri.
    /// </summary>
    public static Uri DarkUri { get; } = new Uri($"pack://application:,,,/{MicaWPFServiceUtility.CurrentNamespace};component/Styles/Themes/MicaDark.xaml");

    /// <summary>
    /// Gets the light theme resource Uri.
    /// </summary>
    public static Uri LightUri { get; } = new Uri($"pack://application:,,,/{MicaWPFServiceUtility.CurrentNamespace};component/Styles/Themes/MicaLight.xaml");

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
            ? DarkUri
            : LightUri;
    }
}

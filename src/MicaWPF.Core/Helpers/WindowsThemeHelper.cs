﻿// <copyright file="WindowsThemeHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Enums;
using Microsoft.Win32;

namespace MicaWPF.Core.Helpers;

public static class WindowsThemeHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string _registryValueName = "AppsUseLightTheme";

    public static Uri DarkUri { get; set; } = new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaDark.xaml");

    public static Uri LightUri { get; set; } = new Uri("pack://application:,,,/MicaWPF;component/Styles/Themes/MicaLight.xaml");

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
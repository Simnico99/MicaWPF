// <copyright file="WindowsAccentHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Interop;
using MicaWPF.Core.Models;
using Microsoft.Win32;

namespace MicaWPF.Lite.Core.Helpers;

public static class WindowsAccentHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\DWM";
    private const string _registryValueName = "ColorPrevalence";

    /// <summary>
    /// Determines whether the title bar and borders are accented.
    /// </summary>
    /// <returns>
    /// Returns a boolean value indicating whether the title bar and borders are accented or not.
    /// </returns>
    public static bool AreTitleBarAndBordersAccented()
    {
        using var key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        var registryValueObject = key?.GetValue(_registryValueName);

        if (registryValueObject == null)
        {
            return false;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0;
    }

    /// <summary>
    /// Gets the accent color for the system.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="AccentColors"/> object containing the different variations of the accent color.
    /// </returns>
    public static AccentColors GetAccentColor()
    {
        InteropMethods.DwmGetColorizationParameters(out var colors);
        return new(ParseColor(colors.clrColor), default, default, default, default, default, default, true);
    }

    private static Color ParseColor(uint color)
    {
        var opaque = true;

        return Color.FromArgb(
            (byte)(opaque ? 255 : (color >> 24) & 0xff),
            (byte)((color >> 16) & 0xff),
            (byte)((color >> 8) & 0xff),
            (byte)((byte)color & 0xff));
    }
}

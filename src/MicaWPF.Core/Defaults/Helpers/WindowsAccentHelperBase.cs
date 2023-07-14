// <copyright file="WindowsAccentHelperBase.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Interop;
using Microsoft.Win32;

namespace MicaWPF.Core.Defaults.Helpers;

public class WindowsAccentHelperBase : IWindowsAccentHelper
{
    protected const string _registryKeyPath = @"Software\Microsoft\Windows\DWM";
    protected const string _registryValueName = "ColorPrevalence";

    /// <summary>
    /// Determines whether the title bar and borders are accented.
    /// </summary>
    /// <returns>
    /// Returns a boolean value indicating whether the title bar and borders are accented or not.
    /// </returns>
    public virtual bool AreTitleBarAndBordersAccented()
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
    public virtual AccentColors GetAccentColor()
    {
        InteropMethods.DwmGetColorizationParameters(out var colors);
        return new(ParseColor(colors.clrColor), default, default, default, default, default, default, true);
    }

    protected static Color ParseColor(uint color)
    {
        var opaque = true;

        return Color.FromArgb(
            (byte)(opaque ? 255 : color >> 24 & 0xff),
            (byte)(color >> 16 & 0xff),
            (byte)(color >> 8 & 0xff),
            (byte)((byte)color & 0xff));
    }
}

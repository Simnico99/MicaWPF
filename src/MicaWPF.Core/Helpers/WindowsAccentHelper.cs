// <copyright file="WindowsAccentHelper.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows.Media;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Interop;
using MicaWPF.Core.Models;
using Microsoft.Win32;

namespace MicaWPF.Core.Helpers;

/// <summary>
/// Helper method for getting windows accent.
/// </summary>
public class WindowsAccentHelper
{
    protected const string _registryKeyPath = @"Software\Microsoft\Windows\DWM";
    protected const string _registryValueName = "ColorPrevalence";

    /// <summary>
    /// Convert a <see cref="Color"/> to a <see cref="AccentColors"/> object."/>.
    /// </summary>
    /// <param name="systemAccent">The current color.</param>
    /// <returns>The <see cref="AccentColors"/> with color variations from the provided color.</returns>
    public static AccentColors GetWindowsColorVariations(Color systemAccent)
    {
        var drawingColor = System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B);
        var hsvColor = HSVColorHelper.ConvertToHSVColor(drawingColor);

        return new AccentColors(
            systemAccent,
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Secondary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Tertiary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Quaternary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Secondary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Tertiary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Quaternary));
    }

    /// <summary>
    /// Determines whether the title bar and window borders are accented.
    /// </summary>
    /// <returns>
    /// Returns a boolean indicating if the title bar and window borders are accented.
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
    /// Retrieves the current Windows accent colors.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="AccentColors"/> object containing the current Windows accent colors.
    /// </returns>
    public virtual AccentColors GetAccentColor()
    {
        var colors = InteropMethods.GetDwmGetColorizationParameters();
        return GetWindowsColorVariations(ParseColor(colors.clrColor));
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

    protected static Color GetThemeColorVariation((double Hue, double Saturation, double Value) hsv, WindowsTheme windowsTheme, AccentBrushType accentBrushType)
    {
        var hueCoefficient = 1 - hsv.Value < 0.15 ? 1 : 0;

        return accentBrushType switch
        {
            AccentBrushType.Primary => HSVColorHelper.RGBFromHSV(hsv.Hue, hsv.Saturation, hsv.Value),
            AccentBrushType.Secondary => GetOffSet(hsv.Hue, hueCoefficient, hsv.Saturation, hsv.Value, 0.3, windowsTheme),
            AccentBrushType.Tertiary => GetOffSet(hsv.Hue, hueCoefficient, hsv.Saturation, hsv.Value, 0.35, windowsTheme),
            AccentBrushType.Quaternary => GetOffSet(hsv.Hue, hueCoefficient, hsv.Saturation, hsv.Value, 0.65, windowsTheme),
            _ => throw new ArgumentOutOfRangeException(nameof(accentBrushType)),
        };
    }

    protected static Color GetOffSet(double hue, double hueCoefficient, double saturation, double value, double valueCoefficient, WindowsTheme windowsTheme)
    {
        return windowsTheme switch
        {
            WindowsTheme.Dark => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value + valueCoefficient, 1)),
            WindowsTheme.Light => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value - (valueCoefficient / 2), 1)),
            _ => throw new ArgumentOutOfRangeException(nameof(windowsTheme)),
        };
    }
}

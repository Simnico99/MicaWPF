﻿// <copyright file="WinRTHelper.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows.Media;
using MicaWPF.Core.Models;
#if NET5_0_OR_GREATER
using MicaWPFRuntimeComponent;
#endif
#if NETFRAMEWORK || NETCOREAPP3_1
using Windows.UI.ViewManagement;
#endif

namespace MicaWPF.Helpers;

public static class WinRTHelper
{
    public static AccentColors GetSystemAccentColor()
    {
#if NET5_0_OR_GREATER
        var tempColorAccent = default(Color);
        var tempColorAccentLight1 = default(Color);
        var tempColorAccentLight2 = default(Color);
        var tempColorAccentLight3 = default(Color);
        var tempColorAccentDark1 = default(Color);
        var tempColorAccentDark2 = default(Color);
        var tempColorAccentDark3 = default(Color);

        var uwpColors = new UWPColors();
        var colorsLongString = uwpColors.GetSystemColors();

        foreach (var colors in colorsLongString)
        {
            var colorValues = colors.Split(',');
            var colorResult = Color.FromArgb(byte.Parse(colorValues[0]), byte.Parse(colorValues[1]), byte.Parse(colorValues[2]), byte.Parse(colorValues[3]));
            switch (colorValues[4].ToString())
            {
                case "SystemAccentColor":
                    tempColorAccent = colorResult;
                    break;
                case "SystemAccentColorLight1":
                    tempColorAccentLight1 = colorResult;
                    break;
                case "SystemAccentColorLight2":
                    tempColorAccentLight2 = colorResult;
                    break;
                case "SystemAccentColorLight3":
                    tempColorAccentLight3 = colorResult;
                    break;
                case "SystemAccentColorDark1":
                    tempColorAccentDark1 = colorResult;
                    break;
                case "SystemAccentColorDark2":
                    tempColorAccentDark2 = colorResult;
                    break;
                case "SystemAccentColorDark3":
                    tempColorAccentDark3 = colorResult;
                    break;
                default:
                    break;
            }
        }

        return new(
            tempColorAccent,
            tempColorAccentLight1,
            tempColorAccentLight2,
            tempColorAccentLight3,
            tempColorAccentDark1,
            tempColorAccentDark2,
            tempColorAccentDark3);
#endif
#if NETFRAMEWORK || NETCOREAPP3_1
        return new AccentColors(
            UIColorConverter(UIColorType.Accent),
            UIColorConverter(UIColorType.AccentLight1),
            UIColorConverter(UIColorType.AccentLight2),
            UIColorConverter(UIColorType.AccentLight3),
            UIColorConverter(UIColorType.AccentDark1),
            UIColorConverter(UIColorType.AccentDark2),
            UIColorConverter(UIColorType.AccentDark3));
#endif
    }

#if NETFRAMEWORK || NETCOREAPP3_1
    private static Color UIColorConverter(UIColorType colorType)
    {
        var uiSettings = new UISettings();
        var color = uiSettings.GetColorValue(colorType);
        return Color.FromArgb(color.A, color.R, color.G, color.B);
    }
#endif
}

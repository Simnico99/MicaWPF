using System.Windows;
using MicaWPF.Controls;
using MicaWPF.Extensions;

namespace MicaWPF.Helpers;

internal class AccentHelper
{
    private static Color SetSecondaryVariationsForCurrentColor((double hue, double saturation, double value) hsv, WindowsTheme theme, bool isTertiary)
    {
        var hueCoefficient = 0;
        var hueCoefficient2 = 0;
        if (1 - hsv.value < 0.15)
        {
            hueCoefficient = 1;
        }

        if (hsv.value - 0.3 < 0)
        {
            hueCoefficient2 = 1;
        }

        var s = hsv.saturation;
        hueCoefficient = 1;

        if (theme is WindowsTheme.Dark && !isTertiary)
        {
            return HSVColorHelper.RGBFromHSV(Math.Min(hsv.hue + (hueCoefficient * 4), 360), s, Math.Min(hsv.value - 0.05, 1));
        }
        else if (!isTertiary)
        {
            return HSVColorHelper.RGBFromHSV(Math.Max(hsv.hue - (hueCoefficient2 * 4), 0), s, Math.Max(hsv.value - 0.095, 0));
        }

        if (theme is WindowsTheme.Dark && isTertiary)
        {
            return HSVColorHelper.RGBFromHSV(Math.Min(hsv.hue + (hueCoefficient * 8), 360), s, Math.Min(hsv.value + 0.35, 1));
        }
        else
        {
            return HSVColorHelper.RGBFromHSV(Math.Max(hsv.hue - (hueCoefficient2 * 8), 0), s, Math.Max(hsv.value - 0.195, 0));
        }
    }

    public static Color GetColorizationColor()
    {
        InteropMethods.DwmGetColorizationParameters(out var dmwParams);

        var values = BitConverter.GetBytes(dmwParams.clrColor);

        return Color.FromArgb(
            255,
            values[2],
            values[1],
            values[0]
        );
    }

    public static void Change(MicaWindow window, Color systemAccent, WindowsTheme themeType = WindowsTheme.Light)
    {
        Color primaryAccent, secondaryAccent, tertiaryAccent;

        primaryAccent = systemAccent;
        secondaryAccent = SetSecondaryVariationsForCurrentColor(ColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), themeType, false);
        tertiaryAccent = SetSecondaryVariationsForCurrentColor(ColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), themeType, true);

        UpdateColorResources(
            systemAccent,
            primaryAccent,
            secondaryAccent,
            tertiaryAccent,
            window
);
}

    public static void UpdateColorResources(Color systemAccent, Color primaryAccent, Color secondaryAccent, Color tertiaryAccent, MicaWindow window)
    {
        window.Resources["MicaWPF.Colors.SystemAccentColor"] = systemAccent;
        window.Resources["MicaWPF.Colors.SystemAccentColorLight1"] = primaryAccent;
        window.Resources["MicaWPF.Colors.SystemAccentColorLight2"] = secondaryAccent;
        window.Resources["MicaWPF.Colors.SystemAccentColorLight3"] = tertiaryAccent;

        window.Resources["MicaWPF.Brushes.SystemAccent"] = secondaryAccent.ToBrush();
        window.Resources["MicaWPF.Brushes.SystemFillColorAttention"] = secondaryAccent.ToBrush();
        window.Resources["MicaWPF.Brushes.AccentTextFillColorPrimary"] = tertiaryAccent.ToBrush();
        window.Resources["MicaWPF.Brushes.AccentTextFillColorSecondary"] = tertiaryAccent.ToBrush();
        window.Resources["MicaWPF.Brushes.AccentTextFillColorTertiary"] = secondaryAccent.ToBrush();
        window.Resources["MicaWPF.Brushes.AccentFillColorSelectedTextBackground"] = systemAccent.ToBrush();
        window.Resources["MicaWPF.Brushes.AccentFillColorDefault"] = secondaryAccent.ToBrush();

        window.Resources["MicaWPF.Brushes.AccentFillColorSecondary"] = secondaryAccent.ToBrush(0.9);
        window.Resources["MicaWPF.Brushes.AccentFillColorTertiary"] = secondaryAccent.ToBrush(0.8);
    }
}

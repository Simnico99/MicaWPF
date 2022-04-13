using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Helpers;

internal class AccentHelper
{
    public static Color GetColorizationColor()
    {
        InteropMethods.DwmGetColorizationParameters(out var dmwParams);

        byte[] values = BitConverter.GetBytes(dmwParams.clrColor);

        return Color.FromArgb(
            255,
            values[2],
            values[1],
            values[0]
        );
    }

    public static void Change(Color systemAccent, WindowsTheme themeType = WindowsTheme.Light,
        bool systemGlassColor = false)
    {
        if (systemGlassColor)
        {
            systemAccent = systemAccent.UpdateBrightness(6f);
        }

        Color primaryAccent, secondaryAccent, tertiaryAccent;

        if (themeType == WindowsTheme.Dark)
        {
            primaryAccent = systemAccent.Update(15f, -12f);
            secondaryAccent = systemAccent.Update(30f, -24f);
            tertiaryAccent = systemAccent.Update(45f, -36f);
        }
        else
        {
            primaryAccent = systemAccent.UpdateBrightness(-5f);
            secondaryAccent = systemAccent.UpdateBrightness(-10f);
            tertiaryAccent = systemAccent.UpdateBrightness(-15f);
        }

        UpdateColorResources(
            systemAccent,
            primaryAccent,
            secondaryAccent,
            tertiaryAccent
        );
    }

    private static void UpdateColorResources(Color systemAccent, Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {


        Application.Current.Resources["SystemAccentColor"] = systemAccent;
        Application.Current.Resources["SystemAccentColorLight1"] = primaryAccent;
        Application.Current.Resources["SystemAccentColorLight2"] = secondaryAccent;
        Application.Current.Resources["SystemAccentColorLight3"] = tertiaryAccent;

        Application.Current.Resources["SystemAccentBrush"] = secondaryAccent.ToBrush();
        Application.Current.Resources["SystemFillColorAttentionBrush"] = secondaryAccent.ToBrush();
        Application.Current.Resources["AccentTextFillColorPrimaryBrush"] = tertiaryAccent.ToBrush();
        Application.Current.Resources["AccentTextFillColorSecondaryBrush"] = tertiaryAccent.ToBrush();
        Application.Current.Resources["AccentTextFillColorTertiaryBrush"] = secondaryAccent.ToBrush();
        Application.Current.Resources["AccentFillColorSelectedTextBackgroundBrush"] = systemAccent.ToBrush();
        Application.Current.Resources["AccentFillColorDefaultBrush"] = secondaryAccent.ToBrush();

        Application.Current.Resources["AccentFillColorSecondaryBrush"] = secondaryAccent.ToBrush(0.9);
        Application.Current.Resources["AccentFillColorTertiaryBrush"] = secondaryAccent.ToBrush(0.8);
    }
}

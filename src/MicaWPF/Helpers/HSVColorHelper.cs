namespace MicaWPF.Helpers;

/// <summary>
/// An helper class to manipulate HSV Colors.
/// </summary>
public static class HSVColorHelper
{
    /// <summary>
    /// Convert a given <see cref="Color"/> to a HSV color (hue, saturation, value)
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to convert</param>
    /// <returns>The hue [0°..360°], saturation [0..1] and value [0..1] of the converted color</returns>
    internal static (double hue, double saturation, double value) ConvertToHSVColor(System.Drawing.Color color)
    {
        var min = Math.Min(Math.Min(color.R, color.G), color.B) / 255d;
        var max = Math.Max(Math.Max(color.R, color.G), color.B) / 255d;

        return (color.GetHue(), max == 0d ? 0d : (max - min) / max, max);
    }

    /// <summary>
    /// Gets the entire color spectrum.
    /// </summary>
    /// <returns>The entire color</returns>
    public static Color[] GetSpectrum()
    {
        var rgbs = new Color[360];

        for (var h = 0; h < 360; h++)
        {
            rgbs[h] = RGBFromHSV(h, 1f, 1f);
        }

        return rgbs;
    }

    /// <summary>
    /// Gets the current hue spectrum from the saturation and value.
    /// </summary>
    /// <returns>Colors between the provided staturation and the provided value.</returns>
    public static Color[] HueSpectrum(double saturation, double value)
    {
        var rgbs = new Color[7];

        for (var h = 0; h < 7; h++)
        {
            rgbs[h] = RGBFromHSV(h * 60, saturation, value);
        }

        return rgbs;
    }

    /// <summary>
    /// Convert HSV to RGB <see cref="Color"/>
    /// </summary>
    /// <returns>Converted HSV color to RGB.</returns>
    public static Color RGBFromHSV(double h, double s, double v)
    {
        if (h > 360 || h < 0 || s > 1 || s < 0 || v > 1 || v < 0)
        {
            return Color.FromRgb(0, 0, 0);
        }

        var c = v * s;
        var x = c * (1 - Math.Abs(((h / 60) % 2) - 1));
        var m = v - c;

        double r = 0, g = 0, b = 0;

        if (h < 60)
        {
            r = c;
            g = x;
        }
        else if (h < 120)
        {
            r = x;
            g = c;
        }
        else if (h < 180)
        {
            g = c;
            b = x;
        }
        else if (h < 240)
        {
            g = x;
            b = c;
        }
        else if (h < 300)
        {
            r = x;
            b = c;
        }
        else if (h <= 360)
        {
            r = c;
            b = x;
        }

        return Color.FromRgb((byte)((r + m) * 255), (byte)((g + m) * 255), (byte)((b + m) * 255));
    }
}

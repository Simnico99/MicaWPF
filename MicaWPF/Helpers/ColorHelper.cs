namespace MicaWPF.Helpers;

public class ColorHelper
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
}

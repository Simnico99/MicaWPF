using System.Windows.Data;

namespace MicaWPF.Converters;

public class ColorDarkerConverter : IValueConverter
{
    public static Color ChangeColorBrightness(Color color, double correctionFactor)
    {
        double red = color.R;
        double green = color.G;
        double blue = color.B;

        if (correctionFactor < 0)
        {
            correctionFactor = 1 + correctionFactor;
            red *= correctionFactor;
            green *= correctionFactor;
            blue *= correctionFactor;
        }
        else
        {
            red = (255 - red) * correctionFactor + red;
            green = (255 - green) * correctionFactor + green;
            blue = (255 - blue) * correctionFactor + blue;
        }

        red = red.Clamp(0, 255);
        green = green.Clamp(0, 255);
        blue = blue.Clamp(0, 255);

        return Color.FromArgb(color.A, (byte)red, (byte)green, (byte)blue);
    }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is Color colorValue)
        {
            return ChangeColorBrightness(colorValue, -0.3);
        }
        return new object();
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is Color colorValue)
        {
            return ChangeColorBrightness(colorValue, 0.3);
        }
        return new object();
    }
}

using System.Globalization;
using System.Windows.Data;

namespace MicaWPF.Converters;
internal sealed class BrushToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            return Color.FromArgb((byte)(brush.Opacity * brush.Color.A), brush.Color.R, brush.Color.G, brush.Color.B);
        }

        if (value is Color)
        {
            return value;
        }

        // We draw red to visibly see an invalid bind in the UI.
        return new Color { A = 255, R = 255, G = 0, B = 0 };
    }

    /// <summary>
    /// Not Implemented.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

namespace MicaWPF.Converters;
internal class BrushToColorConverter 
{
    public object Convert(object value)
    {
        if (value is SolidColorBrush brush)
        {
            return brush.Color;
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

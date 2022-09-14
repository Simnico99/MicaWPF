using System.Windows.Data;

namespace MicaWPF.Converters;
public sealed class DialogScrollViewerHeightConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is double height ? height - 100 : (object)500;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

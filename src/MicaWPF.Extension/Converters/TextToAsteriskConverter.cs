using System.Windows.Data;

namespace MicaWPF.Extension.Converters;
public class TextToAsteriskConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return new string('*', value?.ToString()?.Length ?? 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

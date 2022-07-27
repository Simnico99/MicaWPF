using System.Windows.Data;
using MicaWPF.Symbols;

namespace MicaWPF.Converters;
internal class IconNotEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is FluentSystemIcons.Regular icon)
        {
            return icon != FluentSystemIcons.Regular.Empty;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

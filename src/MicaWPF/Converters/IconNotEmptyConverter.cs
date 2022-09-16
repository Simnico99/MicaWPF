using MicaWPF.Symbols;
using System.Windows.Data;

namespace MicaWPF.Converters;
internal sealed class IconNotEmptyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is FluentSystemIcons.Regular icon ? icon != FluentSystemIcons.Regular.Empty : (object)false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

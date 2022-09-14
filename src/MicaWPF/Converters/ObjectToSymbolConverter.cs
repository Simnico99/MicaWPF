using System.Windows.Data;
using MicaWPF.Extensions;
using MicaWPF.Symbols;

namespace MicaWPF.Converters;

internal sealed class ObjectToSymbolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is FluentSystemIcons.Regular symbol
            ? symbol
            : value is FluentSystemIcons.Filled symbolFilled ? symbolFilled.Swap() : (object)FluentSystemIcons.Regular.Empty;
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

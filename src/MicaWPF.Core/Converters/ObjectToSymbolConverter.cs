// <copyright file="ObjectToSymbolConverter.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.Windows.Data;
using MicaWPF.Core.Extensions;
using MicaWPF.Core.Symbols;

namespace MicaWPF.Core.Converters;

public sealed class ObjectToSymbolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is FluentSystemIcons.Regular symbol
            ? symbol
            : value is FluentSystemIcons.Filled symbolFilled ? symbolFilled.Swap() : (object)FluentSystemIcons.Regular.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

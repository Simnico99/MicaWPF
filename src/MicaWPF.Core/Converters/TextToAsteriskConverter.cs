// <copyright file="TextToAsteriskConverter.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.Windows.Data;

namespace MicaWPF.Core.Converters;

/// <summary>
/// Converts the current text to asterisks.
/// </summary>
public sealed class TextToAsteriskConverter : IValueConverter
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

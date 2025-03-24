// <copyright file="ProgressThicknessConverter.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows.Data;

namespace MicaWPF.Core.Converters;

/// <summary>
/// Converts the current double to a thickness value for the <see cref="Controls.ProgressRing"/>.
/// </summary>
public sealed class ProgressThicknessConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value is double height ? height / 11 : (object)12.0d;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

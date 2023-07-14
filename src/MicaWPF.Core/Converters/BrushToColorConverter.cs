// <copyright file="BrushToColorConverter.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Globalization;
using System.Windows.Data;

namespace MicaWPF.Core.Converters;

public sealed class BrushToColorConverter : IValueConverter
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

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

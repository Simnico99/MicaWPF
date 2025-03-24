﻿// <copyright file="IsNullConverter.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows.Data;

namespace MicaWPF.Core.Converters;

/// <summary>
/// Null to bool.
/// </summary>
public sealed class IsNullConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return (value != null && value.ToString() == string.Empty) || value is null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

// <copyright file="ColorExtension.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

namespace MicaWPF.Core.Extensions;

/// <summary>
/// Extensions for <see cref="Color"/>
/// </summary>
public static class ColorExtensions
{
    /// <summary>
    /// Creates a <see cref="SolidColorBrush"/> from a <see cref="Color"/>.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <returns></returns>
    public static SolidColorBrush ToBrush(this Color color)
    {
        return new SolidColorBrush(color);
    }

    /// <summary>
    /// Creates a <see cref="SolidColorBrush"/> from a <see cref="Color"/> with defined brush opacity.
    /// </summary>
    /// <param name="color">Input color.</param>
    /// <param name="opacity">Degree of opacity.</param>
    /// <returns></returns>
    public static SolidColorBrush ToBrush(this Color color, double opacity)
    {
        return new SolidColorBrush { Color = color, Opacity = opacity };
    }
}

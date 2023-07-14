// <copyright file="ColorHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows.Media;

namespace MicaWPF.Core.Helpers;

/// <summary>
/// An helper class to help with <see cref="Color"/>.
/// </summary>
public static class ColorHelper
{
    private static readonly ComponentSelector _alphaSelector = color => color.A;
    private static readonly ComponentSelector _redSelector = color => color.R;
    private static readonly ComponentSelector _greenSelector = color => color.G;
    private static readonly ComponentSelector _blueSelector = color => color.B;

    private delegate byte ComponentSelector(Color color);

    /// <summary>
    /// Will interpolate between 2 colors.
    /// </summary>
    /// <returns>The interpolated <see cref="Color"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If lambda value is not between 0 or 1.</exception>
    public static Color InterpolateBetween(
        Color endPoint1,
        Color endPoint2,
        double lambda)
    {
        if (lambda is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(lambda));
        }

        var color = Color.FromArgb(
            InterpolateComponent(endPoint1, endPoint2, lambda, _alphaSelector),
            InterpolateComponent(endPoint1, endPoint2, lambda, _redSelector),
            InterpolateComponent(endPoint1, endPoint2, lambda, _greenSelector),
            InterpolateComponent(endPoint1, endPoint2, lambda, _blueSelector));

        return color;
    }

    private static byte InterpolateComponent(
        Color endPoint1,
        Color endPoint2,
        double lambda,
        ComponentSelector selector)
    {
        return (byte)(selector(endPoint1) + ((selector(endPoint2) - selector(endPoint1)) * lambda));
    }
}

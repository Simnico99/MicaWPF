using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Helpers;
public static class ColorHelper
{
    private delegate byte ComponentSelector(Color color);
    private static readonly ComponentSelector _alphaSelector = color => color.A;
    private static readonly ComponentSelector _redSelector = color => color.R;
    private static readonly ComponentSelector _greenSelector = color => color.G;
    private static readonly ComponentSelector _blueSelector = color => color.B;

    public static Color InterpolateBetween(
        Color endPoint1,
        Color endPoint2,
        double lambda)
    {
        if (lambda < 0 || lambda > 1)
        {
            throw new ArgumentOutOfRangeException("lambda");
        }

        var color = Color.FromArgb(
            InterpolateComponent(endPoint1, endPoint2, lambda, _alphaSelector),
            InterpolateComponent(endPoint1, endPoint2, lambda, _redSelector),
            InterpolateComponent(endPoint1, endPoint2, lambda, _greenSelector),
            InterpolateComponent(endPoint1, endPoint2, lambda, _blueSelector)
        );

        return color;
    }

    private static byte InterpolateComponent(
        Color endPoint1,
        Color endPoint2,
        double lambda,
        ComponentSelector selector)
    {
        return (byte)(selector(endPoint1) + ((selector(endPoint2) - selector(endPoint1))) * lambda);
    }
}

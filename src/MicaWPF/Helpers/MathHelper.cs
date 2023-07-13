// <copyright file="MathHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

namespace MicaWPF.Helpers;

internal static class MathHelper
{
    public static bool AreClose(double value1, double value2)
    {
        return value1 == value2 || IsVerySmall(value1 - value2);
    }

    public static bool IsVerySmall(double value)
    {
        return Math.Abs(value) < 1E-06;
    }

    public static bool GreaterThan(double value1, double value2)
    {
        return value1 > value2 && !AreClose(value1, value2);
    }

    public static bool LessThan(double value1, double value2)
    {
        return value1 < value2 && !AreClose(value1, value2);
    }
}

// <copyright file="MathHelper.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Helpers;

/// <summary>
/// Provides helper methods for mathematical operations.
/// </summary>
public static class MathHelper
{
    /// <summary>
    /// Determines whether two specified double-precision floating-point numbers are close to each other, considering the values very small differences.
    /// </summary>
    /// <param name="value1">The first double-precision floating-point number.</param>
    /// <param name="value2">The second double-precision floating-point number.</param>
    /// <returns>true if the value of value1 is approximately equal to the value of value2; otherwise, false.</returns>
    public static bool AreClose(double value1, double value2)
    {
        return value1 == value2 || IsVerySmall(value1 - value2);
    }

    /// <summary>
    /// Determines whether the specified double-precision floating-point number is very small (less than 1E-06).
    /// </summary>
    /// <param name="value">The double-precision floating-point number.</param>
    /// <returns>true if the value of the number is less than 1E-06; otherwise, false.</returns>
    public static bool IsVerySmall(double value)
    {
        return Math.Abs(value) < 1E-06;
    }

    /// <summary>
    /// Determines whether the first specified double-precision floating-point number is greater than the second specified double-precision floating-point number.
    /// </summary>
    /// <param name="value1">The first double-precision floating-point number.</param>
    /// <param name="value2">The second double-precision floating-point number.</param>
    /// <returns>true if the value of value1 is greater than the value of value2 and they are not approximately equal; otherwise, false.</returns>
    public static bool GreaterThan(double value1, double value2)
    {
        return value1 > value2 && !AreClose(value1, value2);
    }

    /// <summary>
    /// Determines whether the first specified double-precision floating-point number is less than the second specified double-precision floating-point number.
    /// </summary>
    /// <param name="value1">The first double-precision floating-point number.</param>
    /// <param name="value2">The second double-precision floating-point number.</param>
    /// <returns>true if the value of value1 is less than the value of value2 and they are not approximately equal; otherwise, false.</returns>
    public static bool LessThan(double value1, double value2)
    {
        return value1 < value2 && !AreClose(value1, value2);
    }
}

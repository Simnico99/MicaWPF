// <copyright file="MathExtension.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Extensions;

/// <summary>
/// Extensions of Int,Doubles,Etc...
/// </summary>
public static class MathExtension
{
    /// <summary>
    /// Clamp the value to a minimum and maxmimum value.
    /// </summary>
    /// <returns>The clamped result.</returns>
    public static T Clamp<T>(this T val, T min, T max)
        where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        return val.CompareTo(min) < 0 ? min : val.CompareTo(max) > 0 ? max : val;
    }
}

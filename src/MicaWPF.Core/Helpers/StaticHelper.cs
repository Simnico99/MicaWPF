// <copyright file="StaticHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Helpers;

/// <summary>
/// Provides a helper class for static methods.
/// </summary>
internal static class StaticHelper
{
    /// <summary>
    /// Initializes a reference type <paramref name="cache"/> with a specified <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="T">The type of the objects to initialize.</typeparam>
    /// <param name="value">The value to use for initialization.</param>
    /// <param name="cache">The reference to initialize.</param>
    /// <exception cref="InvalidOperationException">Thrown when the cache parameter is already initialized.</exception>
    public static void Init<T>(T value, ref T cache)
    {
        if (cache is not null)
        {
            throw new InvalidOperationException("Cannot set the value more than once.");
        }

        cache = value;
    }
}

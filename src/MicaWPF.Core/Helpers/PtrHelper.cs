// <copyright file="PtrHelper.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Runtime.CompilerServices;

namespace MicaWPF.Core.Helpers;

#if NET7_0_OR_GREATER
/// <summary>
/// Provides helper methods and properties for working with native integers (nint).
/// </summary>
public static class PtrHelper
{
    /// <summary>
    /// Gets the zero value for an IntPtr.
    /// </summary>
    public static nint Zero { get; } = nint.Zero;

    /// <summary>
    /// Creates a new native integer initialized to a specified value.
    /// </summary>
    /// <param name="value">The value to initialize the native integer with.</param>
    /// <returns>A native integer initialized to the specified value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static nint Create(int value)
    {
        return new nint(value);
    }
}
#else
/// <summary>
/// Provides helper methods and properties for working with IntPtr.
/// </summary>
public static class PtrHelper
{
    /// <summary>
    /// Gets the zero value for an IntPtr.
    /// </summary>
    public static IntPtr Zero { get; } = IntPtr.Zero;

    /// <summary>
    /// Creates a new IntPtr initialized to a specified value.
    /// </summary>
    /// <param name="value">The value to initialize the IntPtr with.</param>
    /// <returns>An IntPtr initialized to the specified value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr Create(int value)
    {
        return new IntPtr(value);
    }

    /// <summary>
    /// Converts the value of this instance to a 32-bit signed integer.
    /// </summary>
    /// <param name="nintPtr">The native integer to convert.</param>
    /// <returns>A 32-bit signed integer equal to the value of this instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32(this nint nintPtr)
    {
        var ptr = (IntPtr)nintPtr;
        return ptr.ToInt32();
    }
}

#endif

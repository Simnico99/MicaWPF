// <copyright file="StaticHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

namespace MicaWPF.Core.Helpers;

internal static class StaticHelper
{
    public static void Init<T>(T value, ref T cache)
    {
        if (cache is not null)
        {
            throw new InvalidOperationException("Cannot set the value more than once.");
        }

        cache = value;
    }
}

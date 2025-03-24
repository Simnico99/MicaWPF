// <copyright file="Glyph.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Symbols;

namespace MicaWPF.Core.Models;

/// <summary>
/// A static helper class to parse <see cref="string"/> instances into <see cref="FluentSystemIcons"/>.
/// </summary>
public static class Glyph
{
    public const FluentSystemIcons.Regular DefaultIcon = FluentSystemIcons.Regular.Heart28;
    public const FluentSystemIcons.Filled DefaultFilledIcon = FluentSystemIcons.Filled.Heart28;

    /// <summary>
    /// Parses a string into a <see cref="FluentSystemIcons.Regular"/> icon.
    /// If the string is null or empty, a default icon is returned.
    /// </summary>
    /// <param name="name">The string representation of a FluentSystemIcons.Regular enum value.</param>
    /// <returns>The parsed <see cref="FluentSystemIcons.Regular"/> icon. If the input string is null or empty, a default icon is returned.</returns>
    public static FluentSystemIcons.Regular Parse(string name)
    {
#if NET9_0_OR_GREATER
        return string.IsNullOrEmpty(name) ? DefaultIcon : Enum.Parse<FluentSystemIcons.Regular>(name);
#else
        return string.IsNullOrEmpty(name) ? DefaultIcon : (FluentSystemIcons.Regular)Enum.Parse(typeof(FluentSystemIcons.Regular), name);
#endif
    }

    /// <summary>
    /// Parses a string into a <see cref="FluentSystemIcons.Filled"/> icon.
    /// If the string is null or empty, a default icon is returned.
    /// </summary>
    /// <param name="name">The string representation of a FluentSystemIcons.Filled enum value.</param>
    /// <returns>The parsed <see cref="FluentSystemIcons.Filled"/> icon. If the input string is null or empty, a default icon is returned.</returns>
    public static FluentSystemIcons.Filled ParseFilled(string name)
    {
#if NET9_0_OR_GREATER
        return string.IsNullOrEmpty(name) ? DefaultFilledIcon : Enum.Parse<FluentSystemIcons.Filled>(name);
#else
        return string.IsNullOrEmpty(name) ? DefaultFilledIcon : (FluentSystemIcons.Filled)Enum.Parse(typeof(FluentSystemIcons.Filled), name);
#endif
    }
}

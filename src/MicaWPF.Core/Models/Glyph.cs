// <copyright file="Glyph.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Symbols;

namespace MicaWPF.Core.Models;

/// <summary>
/// A class to parse <see cref="string"/> into <see cref="FluentSystemIcons"/>.
/// </summary>
public static class Glyph
{
    public const FluentSystemIcons.Regular DefaultIcon = FluentSystemIcons.Regular.Heart28;
    public const FluentSystemIcons.Filled DefaultFilledIcon = FluentSystemIcons.Filled.Heart28;

    /// <summary>
    /// Parse the current string into <see cref="FluentSystemIcons.Regular"/>.
    /// </summary>
    /// <returns>The parsed icon.</returns>
    public static FluentSystemIcons.Regular Parse(string name)
    {
        return string.IsNullOrEmpty(name) ? DefaultIcon : (FluentSystemIcons.Regular)Enum.Parse(typeof(FluentSystemIcons.Regular), name);
    }

    /// <summary>
    /// Parse the current string into <see cref="FluentSystemIcons.Filled"/>.
    /// </summary>
    /// <returns>The parsed icon.</returns>
    public static FluentSystemIcons.Filled ParseFilled(string name)
    {
        return string.IsNullOrEmpty(name) ? DefaultFilledIcon : (FluentSystemIcons.Filled)Enum.Parse(typeof(FluentSystemIcons.Filled), name);
    }
}

// <copyright file="FluentSystemIconsExtension.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Models;
using MicaWPF.Core.Symbols;

namespace MicaWPF.Core.Extensions;

/// <summary>
/// Provides extension methods to work with Fluent System Icons.
/// </summary>
public static class FluentSystemIconsExtension
{
    /// <summary>
    /// Converts a regular icon to a filled icon.
    /// </summary>
    /// <param name="icon">The regular icon to convert.</param>
    /// <returns>A filled icon.</returns>
    public static FluentSystemIcons.Filled Swap(this FluentSystemIcons.Regular icon)
    {
        return Glyph.ParseFilled(icon.ToString());
    }

    /// <summary>
    /// Converts a filled icon to a regular icon.
    /// </summary>
    /// <param name="icon">The filled icon to convert.</param>
    /// <returns>A regular icon.</returns>
    public static FluentSystemIcons.Regular Swap(this FluentSystemIcons.Filled icon)
    {
        return Glyph.Parse(icon.ToString());
    }

    /// <summary>
    /// Gets the glyph character for a regular icon.
    /// </summary>
    /// <param name="icon">The regular icon.</param>
    /// <returns>The glyph character.</returns>
    public static char GetGlyph(this FluentSystemIcons.Regular icon)
    {
        return ToChar(icon);
    }

    /// <summary>
    /// Gets the glyph character for a filled icon.
    /// </summary>
    /// <param name="icon">The filled icon.</param>
    /// <returns>The glyph character.</returns>
    public static char GetGlyph(this FluentSystemIcons.Filled icon)
    {
        return ToChar(icon);
    }

    /// <summary>
    /// Gets the glyph character as a string for a regular icon.
    /// </summary>
    /// <param name="icon">The regular icon.</param>
    /// <returns>The glyph character as a string.</returns>
    public static string GetString(this FluentSystemIcons.Regular icon)
    {
        return icon.GetGlyph().ToString();
    }

    /// <summary>
    /// Gets the glyph character as a string for a filled icon.
    /// </summary>
    /// <param name="icon">The filled icon.</param>
    /// <returns>The glyph character as a string.</returns>
    public static string GetString(this FluentSystemIcons.Filled icon)
    {
        return icon.GetGlyph().ToString();
    }

    private static char ToChar(FluentSystemIcons.Regular icon)
    {
        return Convert.ToChar(icon);
    }

    private static char ToChar(FluentSystemIcons.Filled icon)
    {
        return Convert.ToChar(icon);
    }
}

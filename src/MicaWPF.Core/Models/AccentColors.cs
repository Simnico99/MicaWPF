// <copyright file="AccentColors.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows.Media;

namespace MicaWPF.Core.Models;

/// <summary>
/// Represents a set of accent colors.
/// </summary>
public readonly struct AccentColors
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccentColors"/> struct.
    /// </summary>
    /// <param name="systemAccentColor">The system accent color.</param>
    /// <param name="systemAccentColorLight1">The light 1 system accent color.</param>
    /// <param name="systemAccentColorLight2">The light 2 system accent color.</param>
    /// <param name="systemAccentColorLight3">The light 3 system accent color.</param>
    /// <param name="systemAccentColorDark1">The dark 1 system accent color.</param>
    /// <param name="systemAccentColorDark2">The dark 2 system accent color.</param>
    /// <param name="systemAccentColorDark3">The dark 3 system accent color.</param>
    /// <param name="isFallBack">A value indicating whether this is a fallback color set.</param>
    public AccentColors(Color systemAccentColor, Color systemAccentColorLight1, Color systemAccentColorLight2, Color systemAccentColorLight3, Color systemAccentColorDark1, Color systemAccentColorDark2, Color systemAccentColorDark3, bool isFallBack = false)
    {
        SystemAccentColor = systemAccentColor;
        SystemAccentColorLight1 = systemAccentColorLight1;
        SystemAccentColorLight2 = systemAccentColorLight2;
        SystemAccentColorLight3 = systemAccentColorLight3;
        SystemAccentColorDark1 = systemAccentColorDark1;
        SystemAccentColorDark2 = systemAccentColorDark2;
        SystemAccentColorDark3 = systemAccentColorDark3;
        IsFallBack = isFallBack;
    }

    /// <summary>
    /// Gets the system accent color.
    /// </summary>
    public Color SystemAccentColor { get; }

    /// <summary>
    /// Gets the light 1 system accent color.
    /// </summary>
    public Color SystemAccentColorLight1 { get; }

    /// <summary>
    /// Gets the light 2 system accent color.
    /// </summary>
    public Color SystemAccentColorLight2 { get; }

    /// <summary>
    /// Gets the light 3 system accent color.
    /// </summary>
    public Color SystemAccentColorLight3 { get; }

    /// <summary>
    /// Gets the dark 1 system accent color.
    /// </summary>
    public Color SystemAccentColorDark1 { get; }

    /// <summary>
    /// Gets the dark 2 system accent color.
    /// </summary>
    public Color SystemAccentColorDark2 { get; }

    /// <summary>
    /// Gets the dark 3 system accent color.
    /// </summary>
    public Color SystemAccentColorDark3 { get; }

    /// <summary>
    /// Gets a value indicating whether this is a fallback color set.
    /// </summary>
    public bool IsFallBack { get; }
}
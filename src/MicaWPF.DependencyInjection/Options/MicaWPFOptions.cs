// <copyright file="MicaWPFOptions.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows.Media;
using MicaWPF.Core.Enums;

namespace MicaWPF.DependencyInjection.Options;

/// <summary>
/// Represents options for the MicaWPF services.
/// </summary>
public sealed class MicaWPFOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether the title bar and borders should automatically match the accent border setting in windows.
    /// </summary>
    public bool IsTitleBarAndBorderAccentAware { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the application should automatically match the current system theme.
    /// </summary>
    public bool IsThemeAware { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the accent color should be updated from Windows.
    /// </summary>
    public bool UpdateAccentFromWindows { get; set; } = true;

    /// <summary>
    /// Gets or sets the theme to use in the application.
    /// </summary>
    public WindowsTheme Theme { get; set; } = WindowsTheme.Auto;

    /// <summary>
    /// Gets or sets the accent color to use in the application.
    /// </summary>
    public Color AccentColor { get; set; } = default;
}
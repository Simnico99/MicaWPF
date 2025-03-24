// <copyright file="IAccentColorService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Media;
using MicaWPF.Core.Events;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Models;

namespace MicaWPF.Core.Services;

/// <summary>
/// Service that manages the accent colors of the application.
/// </summary>
public interface IAccentColorService
{
    /// <summary>
    /// Gets contains the currently used WindowsAccentHelper.
    /// </summary>
    WindowsAccentHelper WindowsAccentHelper
    {
        get;
    }

    /// <summary>
    /// Gets event that is raised when the accent colors are changed.
    /// </summary>
    IWeakEvent<AccentColors> AccentColorChanged
    {
        get;
    }

    /// <summary>
    /// Gets contains information about the current accent colors.
    /// </summary>
    AccentColors AccentColors
    {
        get;
    }

    /// <summary>
    /// Gets a value indicating whether indicates if the accent colors were updated from Windows.
    /// </summary>
    bool AccentColorsUpdateFromWindows
    {
        get;
    }

    /// <summary>
    /// Gets or sets a value indicating whether indicates if the title bar and borders are accent aware.
    /// </summary>
    bool IsTitleBarAndBorderAccentAware
    {
        get; set;
    }

    /// <summary>
    /// Gets a value indicating whether indicates if the title bar and windows borders are colored.
    /// </summary>
    bool IsTitleBarAndWindowsBorderColored
    {
        get;
    }

    /// <summary>
    /// Refreshes the accent colors used in the application.
    /// If `AccentColorsUpdateFromWindows` is set to true, the accent colors will be updated from Windows.
    /// Otherwise, the accent colors will be updated to the system accent color.
    /// This is automatically done when the theme change.
    /// </summary>
    void RefreshAccentsColors();

    /// <summary>
    /// Updates the accent colors with the given color.
    /// </summary>
    /// <param name="systemAccent">The color to use as the system accent color.</param>
    void UpdateAccentsColors(Color systemAccent);

    /// <summary>
    /// Updates the accent colors with the system accent color obtained from Windows.
    /// </summary>
    void UpdateAccentsColorsFromWindows();

    /// <summary>
    /// Add border and titlebar accent color.
    /// </summary>
    /// <param name="window">The window to apply it to.</param>
    /// <param name="isEnabled">Is the accent color enabled</param>
    void IsTitleBarAndBorderAccentEnabled(Window window, bool isEnabled);
}
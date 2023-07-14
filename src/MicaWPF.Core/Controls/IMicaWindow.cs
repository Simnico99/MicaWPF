// <copyright file="IMicaWindow.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Shell;
using MicaWPF.Core.Enums;

namespace MicaWPF.Core.Controls;

/// <summary>
/// Defines the interface for a Mica-styled Window in WPF, which allows customization of the window's appearance and behavior.
/// </summary>
public interface IMicaWindow
{
    /// <summary>
    /// Gets or sets a value indicating whether the title color of the window changes when the window becomes inactive.
    /// </summary>
    /// <returns>A boolean value indicating whether the window's title color changes when inactive.</returns>
    bool ChangeTitleColorWhenInactive { get; set; }

    /// <summary>
    /// Gets or sets the content of the title bar of the window.
    /// This allows for customization of the title bar with any UI elements.
    /// </summary>
    /// <returns>The UIElement that represents the content of the title bar.</returns>
    UIElement TitleBarContent { get; set; }

    /// <summary>
    /// Gets or sets the height of the title bar of the window.
    /// </summary>
    /// <returns>An integer value representing the height of the title bar in device-independent units (1/96th inch per unit).</returns>
    int TitleBarHeight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the accent color should be applied to the title bar and the border of the window.
    /// </summary>
    /// <returns>A boolean value indicating whether the accent color is used on the title bar and the border of the window.</returns>
    bool UseAccentOnTitleBarAndBorder { get; set; }

    /// <summary>
    /// Gets or sets the current backdrop type of the window.
    /// </summary>
    BackdropType SystemBackdropType { get; set; }

    /// <summary>
    /// Gets or sets the title bar type of the window (WinUI or Win32).
    /// </summary>
    TitleBarType TitleBarType { get; set; }

    /// <summary>
    /// Gets or sets a custom WindowChrom for the MicaWindow.
    /// </summary>
    WindowChrome CustomWindowChrome { get; set; }

    /// <summary>
    /// Ends the initialization of a window that is created using markup. This method is called by the markup parser.
    /// </summary>
    void EndInit();

    /// <summary>
    /// Builds the current template's visual tree if necessary, and returns a value that indicates whether the visual tree was rebuilt by this call.
    /// </summary>
    void OnApplyTemplate();
}

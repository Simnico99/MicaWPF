// <copyright file="IButton.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using MicaWPF.Core.Symbols;

namespace MicaWPF.Core.Controls;

/// <summary>
/// Represents the contract for a button control in a Fluent UI, defining properties for customizing its appearance.
/// </summary>
public interface IButton
{
    /// <summary>
    /// Gets or sets the CornerRadius of the button. This determines the roundness of the button's corners.
    /// </summary>
    CornerRadius CornerRadius { get; set; }

    /// <summary>
    /// Gets or sets the icon of the button from the FluentSystemIcons.Regular set.
    /// This icon will be displayed inside the button.
    /// </summary>
    FluentSystemIcons.Regular Icon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the icon of the button is filled.
    /// When set to true, the icon will be displayed as filled; otherwise, it will be displayed as outlined.
    /// </summary>
    bool IconFilled { get; set; }
}

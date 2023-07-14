// <copyright file="IContentDialog.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Media;
using MicaWPF.Core.Enums;

namespace MicaWPF.Dialogs;

/// <summary>
/// A WinUI like content dialog.
/// </summary>
public interface IContentDialog
{
    /// <summary>
    /// Gets or sets the Style for the close button.
    /// </summary>
    Style CloseButtonStyle { get; set; }

    /// <summary>
    /// Gets or sets the Brush used for the inner border.
    /// </summary>
    Brush? InnerBorderBrush { get; set; }

    /// <summary>
    /// Gets or sets the content inside the dialog.
    /// </summary>
    object? InnerContent { get; set; }

    /// <summary>
    /// Gets or sets the text inside the dialog.
    /// </summary>
    string? InnerText { get; set; }

    /// <summary>
    /// Gets or sets the title text of the dialog.
    /// </summary>
    string? InnerTitleText { get; set; }

    /// <summary>
    /// Gets or sets the Style for the primary button.
    /// </summary>
    Style PrimaryButtonStyle { get; set; }

    /// <summary>
    /// Gets or sets the text for the primary button.
    /// </summary>
    string? PrimaryButtonText { get; set; }

    /// <summary>
    /// Gets the result of the content dialog.
    /// </summary>
    ContentDialogResult Result { get; }

    /// <summary>
    /// Gets or sets the Style for the secondary button.
    /// </summary>
    Style SecondaryButtonStyle { get; set; }

    /// <summary>
    /// Gets or sets the text for the secondary button.
    /// </summary>
    string? SecondaryButtonText { get; set; }

    /// <summary>
    /// Gets or sets the text for the tertiary button.
    /// </summary>
    string? TertiaryButtonText { get; set; }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    void InitializeComponent();

    /// <summary>
    /// Shows the dialog asynchronously.
    /// </summary>
    /// <returns>A Task that represents the asynchronous operation.</returns>
    Task ShowAsync();
}
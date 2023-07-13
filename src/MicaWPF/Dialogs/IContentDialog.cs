// <copyright file="IContentDialog.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Enums;

namespace MicaWPF.Dialogs;

public interface IContentDialog
{
    Style CloseButtonStyle { get; set; }

    Brush? InnerBorderBrush { get; set; }

    object? InnerContent { get; set; }

    string? InnerText { get; set; }

    string? InnerTitleText { get; set; }

    Style PrimaryButtonStyle { get; set; }

    string? PrimaryButtonText { get; set; }

    ContentDialogResult Result { get; }

    Style SecondaryButtonStyle { get; set; }

    string? SecondaryButtonText { get; set; }

    string? TertiaryButtonText { get; set; }

    void InitializeComponent();

    Task ShowAsync();
}
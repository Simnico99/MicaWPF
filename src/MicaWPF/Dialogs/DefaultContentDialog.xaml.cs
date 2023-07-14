// <copyright file="DefaultContentDialog.xaml.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MicaWPF.Core.Enums;

namespace MicaWPF.Dialogs;

/// <summary>
/// Interaction logic for DefaultContentDialog.xaml.
/// </summary>
public partial class DefaultContentDialog : ContentControl, IContentDialog
{
    private readonly TaskCompletionSource<bool> _taskCompletionSource = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultContentDialog"/> class.
    /// </summary>
    internal DefaultContentDialog()
    {
        InitializeComponent();
    }

    public ContentDialogResult Result { get; private set; }

    public Brush? InnerBorderBrush
    {
        get => MainBorder.BorderBrush;
        set
        {
            if (value is not null)
            {
                MainBorder.BorderBrush = value;
            }
        }
    }

    public string? InnerTitleText
    {
        get => TitleText.Text;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                TitleTextLabel.Visibility = Visibility.Visible;
                TitleText.Text = value;
            }
        }
    }

    public string? InnerText
    {
        get => CustomText.Text;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                CustomTextLabel.Visibility = Visibility.Visible;
                CustomText.Text = value;
            }
        }
    }

    public string? PrimaryButtonText
    {
        get => PrimaryButton.Content.ToString();
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                ButtonBorder.Visibility = Visibility.Visible;
                LeftColumn.Visible = true;
                PrimaryButton.Content = value;
            }
        }
    }

    public string? SecondaryButtonText
    {
        get => SecondaryButton.Content.ToString();
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                ButtonBorder.Visibility = Visibility.Visible;
                MiddleColumn.Visible = true;
                SecondaryButton.Content = value;
            }
        }
    }

    public string? TertiaryButtonText
    {
        get => TertiaryButton.Content.ToString();
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                ButtonBorder.Visibility = Visibility.Visible;
                RightColumn.Visible = true;
                TertiaryButton.Content = value;
            }
        }
    }

    public object? InnerContent
    {
        get => CustomContent.Content;
        set
        {
            if (value is not null)
            {
                CustomContent.Content = value;
            }
        }
    }

    public Style PrimaryButtonStyle
    {
        get => PrimaryButton.Style;
        set => PrimaryButton.Style = value;
    }

    public Style SecondaryButtonStyle
    {
        get => SecondaryButton.Style;
        set => SecondaryButton.Style = value;
    }

    public Style CloseButtonStyle
    {
        get => TertiaryButton.Style;
        set => TertiaryButton.Style = value;
    }

    public async Task ShowAsync()
    {
        Visibility = Visibility.Visible;
        _ = await _taskCompletionSource.Task;
    }

    private void PrimaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.PrimaryButton;
        _ = _taskCompletionSource.TrySetResult(true);
    }

    private void SecondaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.SecondaryButton;
        _ = _taskCompletionSource.TrySetResult(true);
    }

    private void TertiaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.TertiaryButton;
        _ = _taskCompletionSource.TrySetResult(true);
    }
}

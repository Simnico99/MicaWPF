using MicaWPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MicaWPF.Dialogs;
/// <summary>
/// Interaction logic for DefaultDialogWindow.xaml
/// </summary>
public partial class DefaultDialogWindow : MicaWindow, IContentDialog
{
    private readonly TaskCompletionSource<bool> _taskCompletionSource = new();
    public ContentDialogResult Result { get; private set; }

    public Brush? InnerBorderBrush 
    {
        get => throw new NotImplementedException();
        set => throw new NotImplementedException();
    }

    public string? InnerTitleText
    {
        get => Title;
        set
        {
            Title = value;
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

    internal DefaultDialogWindow()
    {
        InitializeComponent();
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

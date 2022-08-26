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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MicaWPF.Dialogs;
/// <summary>
/// Interaction logic for DefaultContentDialog.xaml
/// </summary>
public partial class DefaultContentDialog : ContentControl
{
    internal ContentDialogResult Result { get; private set; }
    private readonly TaskCompletionSource<bool> taskCompletionSource = new();

    internal Brush? InnerBorderBrush
    {
        get
        {
            return MainBorder.BorderBrush;
        }
        set
        {
            if (value is not null)
            {
                MainBorder.BorderBrush = value;
            }
        }
    }

    internal string? InnerTitleText
    {
        get
        {
            return TitleText.Text;
        }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                TitleTextLabel.Visibility = Visibility.Visible;
                TitleText.Text = value;
            }
        }
    }

    internal string? InnerText
    {
        get
        {
            return CustomText.Text;
        }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                CustomTextLabel.Visibility = Visibility.Visible;
                CustomText.Text = value;
            }
        }
    }

    internal string? PrimaryButtonText
    {
        get
        {
            return PrimaryButton.Content.ToString();
        }
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

    internal string? SecondaryButtonText
    {
        get
        {
            return SecondaryButton.Content.ToString();
        }
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

    internal string? TertiaryButtonText
    {
        get
        {
            return TertiaryButton.Content.ToString();
        }
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

    internal object? InnerContent
    {
        get
        {
            return CustomContent.Content;
        }
        set
        {
            if (value is not null)
            {
                CustomContent.Content = value;
            }
        }
    }

    internal Style PrimaryButtonStyle
    {
        get => PrimaryButton.Style;
        set
        {
            PrimaryButton.Style = value;
        }
    }
    internal Style SecondaryButtonStyle
    {
        get => SecondaryButton.Style;
        set
        {
            SecondaryButton.Style = value;
        }
    }
    internal Style CloseButtonStyle 
    {
        get => TertiaryButton.Style;
        set
        {
            TertiaryButton.Style = value;
        }
    }

    internal DefaultContentDialog()
    {
        InitializeComponent();
    }

    internal async Task ShowAsync()
    {
        Visibility = Visibility.Visible;
        await taskCompletionSource.Task;
    }

    private void PrimaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.PrimaryButton;
        taskCompletionSource.TrySetResult(true);
    }

    private void SecondaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.SecondaryButton;
        taskCompletionSource.TrySetResult(true);
    }

    private void TertiaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.TertiaryButton;
        taskCompletionSource.TrySetResult(true);
    }
}

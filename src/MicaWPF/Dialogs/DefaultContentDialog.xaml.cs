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
        get => MainBorder.BorderBrush;
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

    internal string? InnerText
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

    internal string? PrimaryButtonText
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

    internal string? SecondaryButtonText
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

    internal string? TertiaryButtonText
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

    internal object? InnerContent
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

    internal Style PrimaryButtonStyle
    {
        get => PrimaryButton.Style;
        set => PrimaryButton.Style = value;
    }
    internal Style SecondaryButtonStyle
    {
        get => SecondaryButton.Style;
        set => SecondaryButton.Style = value;
    }
    internal Style CloseButtonStyle
    {
        get => TertiaryButton.Style;
        set => TertiaryButton.Style = value;
    }

    internal DefaultContentDialog()
    {
        InitializeComponent();
    }

    internal async Task ShowAsync()
    {
        Visibility = Visibility.Visible;
        _ = await taskCompletionSource.Task;
    }

    private void PrimaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.PrimaryButton;
        _ = taskCompletionSource.TrySetResult(true);
    }

    private void SecondaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.SecondaryButton;
        _ = taskCompletionSource.TrySetResult(true);
    }

    private void TertiaryButton_Click(object sender, RoutedEventArgs e)
    {
        Visibility = Visibility.Collapsed;
        Result = ContentDialogResult.TertiaryButton;
        _ = taskCompletionSource.TrySetResult(true);
    }
}

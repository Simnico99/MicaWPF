using MicaWPF.Controls;
using MicaWPF.Extensions;
using System.Threading;
using System.Windows.Documents;

namespace MicaWPF.Dialogs;

/// <summary>
/// A WinUI style content dialog.
/// </summary>
public sealed class ContentDialog
{
    private static readonly object _lock = new();
    private static readonly SemaphoreSlim _semaphoreSlim = new(1); 

    public string? Title { get; set; }
    public string? PrimaryButtonText { get; set; }
    public string? SecondaryButtonText { get; set; }
    public string? CloseButtonText { get; set; }
    public object? Content { get; set; }
    public Brush? BorderBrush { get; set; }
    public double Height { get; set; } = double.NaN;
    public double Width { get; set; } = 320;
    public ContentDialogButton DefaultButton { get; set; }

    private static readonly Style _accentButtonStyle = (Style)Application.Current.FindResource("MicaWPF.Styles.AccentedButton");

    private static FrameworkElement? HookToWindow(MicaWindow? micaWindow, IContentDialog content)
    {
        if (micaWindow is not null)
        {
            var grid = micaWindow.FindVisualChildren<AdornerDecorator>().FirstOrDefault()?.Child as Grid;

            var canvas = new Grid()
            {
                Margin = new Thickness(0, -micaWindow.TitleBarHeight, 0, 0),
                Background = Application.Current.TryFindResource("MicaWPF.Brushes.SmokeFillColorDefault") as SolidColorBrush
            };

            _ = canvas.Children.Add(content as ContentControl);

            _ = (grid?.Children.Add(canvas));

            return canvas;
        }
        if (content is DefaultDialogWindow defaultDialogWindow)
        {
            lock (_lock)
            {
                defaultDialogWindow.Show();
            }
        }

        return content as ContentControl;
    }

    private static void DeleteDialog(ContentControl contentDialog, Grid grid)
    {
        ((Grid)contentDialog.Parent).Children.Remove(contentDialog);
        ((Grid)grid.Parent).Children.Remove(grid);
    }

    private static MicaWindow? FindMicaWindow(MicaWindow? micaWindow)
    {
        if (micaWindow is null)
        {
            foreach (var window in Application.Current.Windows)
            {
                if (window is null || window != Application.Current.MainWindow)
                {
                    continue;
                }

                if (window.GetType().IsSubclassOf(typeof(MicaWindow)))
                {
                    micaWindow = window as MicaWindow;
                }
            }

            if (micaWindow is null)
            {
                foreach (Window? window in Application.Current.Windows)
                {
                    if (window is null)
                    {
                        continue;
                    }

                    if (window.IsEnabled && window.IsVisible)
                    {
                        micaWindow = window as MicaWindow;
                        break;
                    }
                }
            }
        }

        return micaWindow is not null && micaWindow.IsVisible ? micaWindow : null;
    }

    public static async Task<ContentDialogResult> ShowAsync(MicaWindow micaWindow, string? text = null, string? titleText = null, string? primaryButtonText = null, string? secondaryButtonText = null, string? tertiarybuttonText = null, ContentDialogButton? defaultButton = null, object? customContent = null, double height = double.NaN, double width = 320, Brush? borderBrush = null)
    {
        var result = ContentDialogResult.Empty;

        await Application.Current.Dispatcher.Invoke(async () =>
        {
            var hook = FindMicaWindow(micaWindow);
            IContentDialog? content = null;

            if (hook is not null)
            {
                content = new DefaultContentDialog
                {
                    PrimaryButtonText = primaryButtonText,
                    SecondaryButtonText = secondaryButtonText,
                    TertiaryButtonText = tertiarybuttonText,
                    InnerText = text,
                    InnerContent = customContent,
                    InnerTitleText = titleText,
                    InnerBorderBrush = borderBrush,
                    Height = height,
                    Width = width
                };
            }
            else
            {
                content = new DefaultDialogWindow
                {
                    PrimaryButtonText = primaryButtonText,
                    SecondaryButtonText = secondaryButtonText,
                    TertiaryButtonText = tertiarybuttonText,
                    InnerText = text,
                    InnerContent = customContent,
                    InnerTitleText = titleText,
                    Icon = Application.Current.MainWindow.Icon,
                };
            }

            switch (defaultButton)
            {
                case ContentDialogButton.Primary:
                    content.PrimaryButtonStyle = _accentButtonStyle;
                    break;
                case ContentDialogButton.Secondary:
                    content.SecondaryButtonStyle = _accentButtonStyle;
                    break;
                case ContentDialogButton.Close:
                    content.CloseButtonStyle = _accentButtonStyle;
                    break;
                default:
                    break;
            }

            var adornee = HookToWindow(hook, content);

            await _semaphoreSlim.WaitAsync();

            await content.ShowAsync();

            result = content.Result;

            if (adornee is Grid grid)
            {
                DeleteDialog((ContentControl)content, grid);
            }
            if (adornee is DefaultDialogWindow defaultDialogWindow)
            {
                defaultDialogWindow.Close();
            }

            _semaphoreSlim.Release();
        });

        return result;
    }

    public async Task<ContentDialogResult> ShowAsync(MicaWindow? micaWindow = null)
    {
        var result = ContentDialogResult.Empty;

        await Application.Current.Dispatcher.Invoke(async () =>
        {
            var hook = FindMicaWindow(micaWindow);
            IContentDialog? content = null;

            if (hook is not null)
            {
                content = new DefaultContentDialog
                {
                    PrimaryButtonText = PrimaryButtonText,
                    SecondaryButtonText = SecondaryButtonText,
                    TertiaryButtonText = CloseButtonText,
                    InnerContent = Content,
                    InnerTitleText = Title,
                    InnerBorderBrush = BorderBrush,
                    Height = Height,
                    Width = Width
                };
            }
            else
            {
                content = new DefaultDialogWindow
                {
                    PrimaryButtonText = PrimaryButtonText,
                    SecondaryButtonText = SecondaryButtonText,
                    TertiaryButtonText = CloseButtonText,
                    InnerContent = Content,
                    InnerTitleText = Title,
                    Icon = Application.Current.MainWindow.Icon,
                };
            }

            switch (DefaultButton)
            {
                case ContentDialogButton.Primary:
                    content.PrimaryButtonStyle = _accentButtonStyle;
                    break;
                case ContentDialogButton.Secondary:
                    content.SecondaryButtonStyle = _accentButtonStyle;
                    break;
                case ContentDialogButton.Close:
                    content.CloseButtonStyle = _accentButtonStyle;
                    break;
                default:
                    break;
            }

            var adornee = HookToWindow(hook, content);

            await _semaphoreSlim.WaitAsync();

            await content.ShowAsync();

            result = content.Result;
            if (adornee is Grid grid)
            {
                DeleteDialog((ContentControl)content, grid);
            }
            if (adornee is DefaultDialogWindow defaultDialogWindow)
            {
                defaultDialogWindow.Close();
            }

            _semaphoreSlim.Release();
        });

        return result;
    }
}

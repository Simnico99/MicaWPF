using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using MicaWPF.Controls;
using MicaWPF.Extensions;

namespace MicaWPF.Dialogs;

public enum ContentDialogResult
{
    PrimaryButton,
    SecondaryButton,
    TertiaryButton,
    Empty
}

public sealed class ContentDialog
{
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

    private static Grid HookToWindow(MicaWindow micaWindow, ContentControl content)
    {
        var grid = micaWindow.FindVisualChildren<AdornerDecorator>().FirstOrDefault()?.Child as Grid;

        var canvas = new Grid()
        {
            Margin = new Thickness(0, -micaWindow.TitleBarHeight, 0, 0),
            Background = Application.Current.TryFindResource("MicaWPF.Brushes.SmokeFillColorDefault") as SolidColorBrush
        };

        canvas.Children.Add(content);

        grid?.Children.Add(canvas);

        return canvas;
    }

    private static void DeleteDialog(DefaultContentDialog contentDialog, Grid grid)
    {
        ((Grid)contentDialog.Parent).Children.Remove(contentDialog);
        ((Grid)grid.Parent).Children.Remove(grid);
    }

    private static MicaWindow FindMicaWindow(MicaWindow? micaWindow)
    {
        if (micaWindow is null)
        {
            foreach (var window in Application.Current.Windows)
            {
                if (window is null || window.GetType().Name != "MainWindow")
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
                foreach (Window window in Application.Current.Windows)
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

        if (micaWindow is null)
        {
            throw new ArgumentNullException(nameof(micaWindow), "No MicaWindow could be found automatically please specify one.");
        }

        return micaWindow;
    }

    public static async Task<ContentDialogResult> ShowAsync(MicaWindow micaWindow, string? text = null, string? titleText = null, string? primaryButtonText = null, string? secondaryButtonText = null, string? tertiarybuttonText = null, ContentDialogButton? defaultButton = null, object? customContent = null, double height = double.NaN, double width = 320, Brush? borderBrush = null)
    {
        var result = ContentDialogResult.Empty;

        await Application.Current.Dispatcher.Invoke(async () =>
        {
            var content = new DefaultContentDialog
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

            var grid = HookToWindow(FindMicaWindow(micaWindow), content);

            await content.ShowAsync();

            result = content.Result;
            DeleteDialog(content, grid);
        });

        return result;
    }

    public async Task<ContentDialogResult> ShowAsync(MicaWindow? micaWindow = null)
    {
        var result = ContentDialogResult.Empty;

        await Application.Current.Dispatcher.Invoke(async () =>
        {
            var content = new DefaultContentDialog
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

            var grid = HookToWindow(FindMicaWindow(micaWindow), content);

            await content.ShowAsync();

            result = content.Result;
            DeleteDialog(content, grid);
        });

        return result;
    }
}

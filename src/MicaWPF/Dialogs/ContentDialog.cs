using System;
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

public static class ContentDialog
{
    private const int DefaultWidth = 332;
    private const int DefaultMaxHeight = 500;

    private static Grid HookToWindow(MicaWindow micaWindow, ContentControl content)
    {
        var grid = micaWindow.FindVisualChildrens<AdornerDecorator>().FirstOrDefault()?.Child as Grid;

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

    public static async Task<ContentDialogResult> ShowAsync(MicaWindow micaWindow, string? text = null, string? titleText = null, string? primaryButtonText = null, string? secondaryButtonText = null, string? tertiarybuttonText = null, object? customContent = null, int maxHeight = DefaultMaxHeight, int width = DefaultWidth)
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
                MaxHeight = maxHeight,
                Width = width
            };

            var grid = HookToWindow(micaWindow, content);

            await content.ShowAsync();

            result = content.Result;
            DeleteDialog(content, grid);
        });

        return result;
    }
}

using MicaWPF.Extensions;

namespace MicaWPF.Helpers;
public static class WindowHelper
{
    public static async Task RefreshAllWindowsContentsAsync()
    {
        _ = await Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            foreach (var windowObj in Application.Current.Windows)
            {
                if (windowObj is Window window and not null)
                {
                    await window.RefreshContentAsync();
                }
            }
        });
    }

    public static void RefreshAllWindowsContents()
    {
        _ = Application.Current.Dispatcher.Invoke(async () =>
        {
            foreach (var windowObj in Application.Current.Windows)
            {
                if (windowObj is Window window and not null)
                {
                    await window.RefreshContentAsync();
                }
            }
        });
    }
}

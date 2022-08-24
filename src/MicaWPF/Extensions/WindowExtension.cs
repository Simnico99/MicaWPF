using MicaWPF.Controls;

namespace MicaWPF.Extensions;
public static class WindowExtension
{
    public static void EnableBackdrop(this Window window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.Current.EnableBackdrop(window, backdropType);
    }

    public static void EnableBackdrop(this MicaWindow window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.Current.EnableBackdrop(window, backdropType);
    }

    public static async Task RefreshContentAsync(this Window window)
    {
        await Application.Current.Dispatcher.InvokeAsync(() => window.RefreshChildrenStyle());
    }
}

using MicaWPF.Controls;

namespace MicaWPF.Extensions;

/// <summary>
/// Extensions of <see cref="Window"/> and <see cref="MicaWindow"/>.
/// </summary>
public static class WindowExtension
{
    /// <summary>
    /// Enable the backdrop on a <see cref="Window"/>.
    /// </summary>
    public static void EnableBackdrop(this Window window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.Current.EnableBackdrop(window, backdropType);
    }

    /// <summary>
    /// Enable the backdrop on a <see cref="MicaWindow"/>.
    /// </summary>
    public static void EnableBackdrop(this MicaWindow window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.Current.EnableBackdrop(window, backdropType);
    }

    /// <summary>
    /// Refresh the content of the entire <see cref="Window"/>.
    /// </summary>
    public static async Task RefreshContentAsync(this Window window)
    {
        await Application.Current.Dispatcher.InvokeAsync(() => window.RefreshChildrenStyle());
    }
}

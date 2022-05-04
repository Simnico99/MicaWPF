using MicaWPF.Controls;

namespace MicaWPF.Extensions;
public static class WindowExtension
{
    public static void EnableMica(this Window window, BackdropType backdropType = BackdropType.Mica, int captionHeight = 20)
    {
        ThemeService.GetCurrent().EnableMica(window, backdropType, captionHeight);
    }

    public static void EnableMica(this MicaWindow window, BackdropType backdropType = BackdropType.Mica, int captionHeight = 20)
    {
        ThemeService.GetCurrent().EnableMica(window, backdropType, captionHeight);
    }
}

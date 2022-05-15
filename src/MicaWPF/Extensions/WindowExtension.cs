using MicaWPF.Controls;

namespace MicaWPF.Extensions;
public static class WindowExtension
{
    public static void EnableMica(this Window window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.GetCurrent().EnableMica(window, backdropType);
    }

    public static void EnableMica(this MicaWindow window, BackdropType backdropType = BackdropType.Mica)
    {
        ThemeService.GetCurrent().EnableMica(window, backdropType);
    }
}

using MicaWPF.Events;

namespace MicaWPF.Services;
public interface IThemeService
{
    WindowsTheme CurrentTheme { get; }
    bool IsThemeAware { get; set; }
    ICollection<MicaEnabledWindow> MicaEnabledWindows { get; }
    IWeakEvent<WindowsTheme> ThemeChanged { get; }

    WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto);
    void EnableBackdrop(Window window, BackdropType micaType = BackdropType.Mica);
}
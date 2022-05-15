namespace MicaWPF.Services;
public interface IThemeService
{
    WindowsTheme CurrentTheme { get; }
    bool IsThemeAware { get; set; }
    ICollection<MicaEnabledWindow> MicaEnabledWindows { get; }

    WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto);
    void EnableMica(Window window, BackdropType micaType = BackdropType.Mica);
}

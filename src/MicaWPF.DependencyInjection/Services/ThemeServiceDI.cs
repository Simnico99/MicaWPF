using System.Windows;
using MicaWPF.Models;
using MicaWPF.Services;

namespace MicaWPF.DependencyInjection.Services;
internal class ThemeServiceDI : IThemeService
{
    private readonly IThemeService _localThemeService = ThemeService.GetCurrent();

    public WindowsTheme CurrentTheme => _localThemeService.CurrentTheme;

    public bool IsThemeAware { get => _localThemeService.IsThemeAware; set => _localThemeService.IsThemeAware = value; }

    public ICollection<MicaEnabledWindow> MicaEnabledWindows => _localThemeService.MicaEnabledWindows;

    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        return _localThemeService.ChangeTheme(windowsTheme);
    }

    public void EnableMica(Window window, BackdropType micaType = BackdropType.Mica)
    {
        _localThemeService.EnableMica(window, micaType);
    }
}

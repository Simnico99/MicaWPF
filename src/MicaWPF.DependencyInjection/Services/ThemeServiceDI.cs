using System.Windows;
using MicaWPF.DependencyInjection.Options;
using MicaWPF.Models;
using MicaWPF.Services;
using Microsoft.Extensions.Options;

namespace MicaWPF.DependencyInjection.Services;
internal class ThemeServiceDI : IThemeService
{
    private readonly IThemeService _localThemeService = ThemeService.GetCurrent();
    private readonly IOptions<MicaWPFOptions> _options;

    public ThemeServiceDI(IOptions<MicaWPFOptions> options)
    {
        _options = options;
        _localThemeService.IsThemeAware = _options.Value.ThemeOptions.IsThemeAware;
        _localThemeService.ChangeTheme(_options.Value.ThemeOptions.Theme);
    }

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

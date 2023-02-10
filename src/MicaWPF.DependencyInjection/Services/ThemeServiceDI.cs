using MicaWPF.DependencyInjection.Options;
using MicaWPF.Events;
using MicaWPF.Models;
using MicaWPF.Services;
using System.Windows;

namespace MicaWPF.DependencyInjection.Services;
internal sealed class ThemeServiceDI : IThemeService
{
    private readonly MicaWPFOptions _options;
    private readonly ThemeService _themeService = ThemeService.Current;

    public IWeakEvent<WindowsTheme> ThemeChanged => _themeService.ThemeChanged;
    public List<MicaEnabledWindow> MicaEnabledWindows => _themeService.MicaEnabledWindows;
    public WindowsTheme CurrentTheme => _themeService.CurrentTheme;
    public bool IsThemeAware => _themeService.IsThemeAware;

    public ThemeServiceDI(MicaWPFOptions options)
    {
        _options = options;
        _ = _themeService.ChangeTheme(_options.Theme);
    }

    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        return _themeService.ChangeTheme(windowsTheme);
    }

    public void EnableBackdrop(Window window, BackdropType micaType = BackdropType.Mica)
    {
        _themeService.EnableBackdrop(window, micaType);
    }
}
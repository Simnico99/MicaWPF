using MicaWPF.DependencyInjection.Options;
using MicaWPF.Events;
using MicaWPF.Models;
using MicaWPF.Services;
using System.Windows;

namespace MicaWPF.DependencyInjection.Services;
internal sealed class ThemeServiceDI : IThemeService
{
    private readonly MicaWPFOptions _options;
    public IWeakEvent<WindowsTheme> ThemeChanged => ThemeService.Current.ThemeChanged;
    public List<MicaEnabledWindow> MicaEnabledWindows => ThemeService.Current.MicaEnabledWindows;
    public WindowsTheme CurrentTheme => ThemeService.Current.CurrentTheme;
    public bool IsThemeAware => ThemeService.Current.IsThemeAware;

    public ThemeServiceDI(MicaWPFOptions options)
    {
        _options = options;
        _ = ThemeService.Current.ChangeTheme(_options.Theme);
    }

    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        return ThemeService.Current.ChangeTheme(windowsTheme);
    }

    public void EnableBackdrop(Window window, BackdropType micaType = BackdropType.Mica)
    {
        ThemeService.Current.EnableBackdrop(window, micaType);
    }
}

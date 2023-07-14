// <copyright file="ThemeServiceDI.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Events;
using MicaWPF.Core.Models;
using MicaWPF.Core.Services;
using MicaWPF.DependencyInjection.Options;
using MicaWPF.Services;

namespace MicaWPF.DependencyInjection.Services;

internal sealed class ThemeServiceDI : IThemeService
{
    private readonly MicaWPFOptions _options;
    private readonly IThemeService _themeService = MicaWPFControllerService.ThemeService;

    public ThemeServiceDI(MicaWPFOptions options)
    {
        _options = options;
        _ = _themeService.ChangeTheme(_options.Theme);
    }

    public IWeakEvent<WindowsTheme> ThemeChanged => _themeService.ThemeChanged;

    public List<BackdropEnabledWindow> BackdropEnabledWindows => _themeService.BackdropEnabledWindows;

    public WindowsTheme CurrentTheme => _themeService.CurrentTheme;

    public bool IsThemeAware => _themeService.IsThemeAware;

    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        return _themeService.ChangeTheme(windowsTheme);
    }

    public void EnableBackdrop(Window window, BackdropType micaType = BackdropType.Mica)
    {
        _themeService.EnableBackdrop(window, micaType);
    }

    public void SetAccentColorService(IAccentColorService accentColorService)
    {
        _themeService.SetAccentColorService(accentColorService);
    }
}
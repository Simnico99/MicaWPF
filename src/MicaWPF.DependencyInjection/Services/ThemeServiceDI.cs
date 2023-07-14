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

/// <summary>
/// The ThemeService through dependency injection.
/// </summary>
internal sealed class ThemeServiceDI : IThemeService
{
    private readonly MicaWPFOptions _options;
    private readonly IThemeService _themeService = MicaWPFServiceUtility.ThemeService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThemeServiceDI"/> class.
    /// </summary>
    /// <param name="options">MicaWPF options.</param>
    public ThemeServiceDI(MicaWPFOptions options)
    {
        _options = options;
        _ = _themeService.ChangeTheme(_options.Theme);
    }

    /// <inheritdoc/>
    public IWeakEvent<WindowsTheme> ThemeChanged => _themeService.ThemeChanged;

    /// <inheritdoc/>
    public List<BackdropEnabledWindow> BackdropEnabledWindows => _themeService.BackdropEnabledWindows;

    /// <inheritdoc/>
    public WindowsTheme CurrentTheme => _themeService.CurrentTheme;

    /// <inheritdoc/>
    public bool IsThemeAware => _themeService.IsThemeAware;

    /// <inheritdoc/>
    public WindowsTheme ChangeTheme(WindowsTheme windowsTheme = WindowsTheme.Auto)
    {
        return _themeService.ChangeTheme(windowsTheme);
    }

    /// <inheritdoc/>
    public void EnableBackdrop(Window window, BackdropType micaType = BackdropType.Mica)
    {
        _themeService.EnableBackdrop(window, micaType);
    }

    /// <inheritdoc/>
    public void SetAccentColorService(IAccentColorService accentColorService)
    {
        _themeService.SetAccentColorService(accentColorService);
    }
}
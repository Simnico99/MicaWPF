// <copyright file="MicaWPFControllerService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Helpers;

namespace MicaWPF.Core.Services;

/// <summary>
/// Controls and manages the services in the MicaWPF framework.
/// </summary>
public sealed class MicaWPFControllerService
{
    private static IAccentColorService? _accentColorService;
    private static IThemeService? _themeService;
    private static IThemeDictionaryService? _themeDictionaryService;
    private static string? _currentNamespace;

    static MicaWPFControllerService()
    {
        // Warning the order is very important.
        _currentNamespace ??= ServiceLocatorHelper.GetNamespace();
        _themeDictionaryService ??= ServiceLocatorHelper.GetService<IThemeDictionaryService>();
        _themeService ??= ServiceLocatorHelper.GetService<IThemeService>();
        _accentColorService ??= ServiceLocatorHelper.GetService<IAccentColorService>();
    }

    /// <summary>
    /// Gets or sets the Accent Color Service instance.
    /// </summary>
    public static IAccentColorService AccentColorService
    {
        get => _accentColorService ?? throw new ArgumentNullException("Value is not initialized yet.");
        set => StaticHelper.Init(value, ref _accentColorService);
    }

    /// <summary>
    /// Gets or sets the Theme Service instance.
    /// </summary>
    public static IThemeService ThemeService
    {
        get => _themeService ?? throw new ArgumentNullException("Value is not initialized yet.");
        set => StaticHelper.Init(value, ref _themeService);
    }

    /// <summary>
    /// Gets or sets the Theme Dictionary Service instance.
    /// </summary>
    public static IThemeDictionaryService ThemeDictionaryService
    {
        get => _themeDictionaryService ?? throw new ArgumentNullException("Value is not initialized yet.");
        set => StaticHelper.Init(value, ref _themeDictionaryService);
    }

    /// <summary>
    /// Gets or sets the current namespace string.
    /// </summary>
    public static string CurrentNamespace
    {
        get => _currentNamespace ?? throw new ArgumentNullException("Value is not initialized yet.");
        set => StaticHelper.Init(value, ref _currentNamespace);
    }
}

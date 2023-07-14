// <copyright file="MicaWPFControllerService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

namespace MicaWPF.Core.Services;

public sealed class MicaWPFControllerService
{
    static MicaWPFControllerService()
    {
        // Warning the order is very important.
        CurrentNamespace = ServiceLocator.GetNamespace();
        ThemeDictionaryService = ServiceLocator.GetService<ThemeDictionaryServiceBase>();
        ThemeService = ServiceLocator.GetService<ThemeServiceBase>();
        AccentColorService = ServiceLocator.GetService<AccentColorServiceBase>();
    }

    public static IAccentColorService AccentColorService { get; }

    public static IThemeService ThemeService { get; }

    public static IThemeDictionaryService ThemeDictionaryService { get; }

    public static string CurrentNamespace { get; }
}

// <copyright file="IThemeDictionaryService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.ComponentModel;
using MicaWPF.Core.Enums;

namespace MicaWPF.Core.Services;

/// <summary>
/// Service that manages the theme dictionnaries from MicaWPF.Core.
/// </summary>
public interface IThemeDictionaryService
{
    /// <summary>
    /// Event that is raised when the `ThemeSource` property is changed.
    /// </summary>
    event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the source of the current theme.
    /// </summary>
    Uri? ThemeSource { get; set; }

    /// <summary>
    /// Sets the source of the current theme.
    /// </summary>
    /// <param name="source">The source of the theme to set.</param>
    void SetThemeSource(Uri source);

    /// <summary>
    /// Force the current theme source to reload.
    /// </summary>
    void RefreshThemeSource();

    /// <summary>
    /// Gets the current theme from the loaded resource dictionnary.
    /// </summary>
    /// <returns>The current ap theme.</returns>
    WindowsTheme GetCurrentResourcesTheme();
}
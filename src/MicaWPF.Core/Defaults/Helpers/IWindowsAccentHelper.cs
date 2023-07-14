// <copyright file="IWindowsAccentHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Models;

namespace MicaWPF.Core.Defaults.Helpers;

/// <summary>
/// Defines the interface for a helper class that facilitates interaction with Windows accent color settings.
/// </summary>
public interface IWindowsAccentHelper
{
    /// <summary>
    /// Determines whether the title bar and window borders are accented.
    /// </summary>
    /// <returns>
    /// Returns a boolean indicating if the title bar and window borders are accented.
    /// </returns>
    bool AreTitleBarAndBordersAccented();

    /// <summary>
    /// Retrieves the current Windows accent colors.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="AccentColors"/> object containing the current Windows accent colors.
    /// </returns>
    AccentColors GetAccentColor();
}

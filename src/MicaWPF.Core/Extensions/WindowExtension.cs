// <copyright file="WindowExtension.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Services;

namespace MicaWPF.Core.Extensions;

/// <summary>
/// Extensions of <see cref="Window"/> and <see cref="MicaWindow"/>.
/// </summary>
public static class WindowExtension
{
    /// <summary>
    /// Refresh the content of the entire <see cref="Window"/>.
    /// </summary>
    public static async Task RefreshContentAsync(this Window window)
    {
        await Application.Current.Dispatcher.InvokeAsync(() => window.RefreshChildrenStyle());
    }

    /// <summary>
    /// Enable the backdrop on a <see cref="Window"/>.
    /// </summary>
    public static void EnableBackdrop(this Window window, BackdropType backdropType = BackdropType.Mica)
    {
        MicaWPFControllerService.ThemeService.EnableBackdrop(window, backdropType);
    }
}

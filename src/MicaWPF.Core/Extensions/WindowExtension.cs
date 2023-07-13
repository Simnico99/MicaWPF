// <copyright file="WindowExtension.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

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
}

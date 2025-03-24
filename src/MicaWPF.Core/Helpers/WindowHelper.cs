﻿// <copyright file="WindowHelper.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using MicaWPF.Core.Extensions;

namespace MicaWPF.Core.Helpers;

public static class WindowHelper
{
    /// <summary>
    /// Refreshes the contents of all the windows in the application asynchronously.
    /// </summary>
    public static async Task RefreshAllWindowsContentsAsync()
    {
        _ = await Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            foreach (var windowObj in Application.Current.Windows)
            {
                if (windowObj is Window window and not null)
                {
                    await window.RefreshContentAsync();
                }
            }
        });
    }

    /// <summary>
    /// Refreshes the contents of all the windows in the application.
    /// </summary
    public static void RefreshAllWindowsContents()
    {
        _ = Application.Current.Dispatcher.Invoke(async () =>
        {
            foreach (var windowObj in Application.Current.Windows)
            {
                if (windowObj is Window window and not null)
                {
                    await window.RefreshContentAsync();
                }
            }
        });
    }
}

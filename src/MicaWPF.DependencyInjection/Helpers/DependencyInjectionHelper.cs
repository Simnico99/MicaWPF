// <copyright file="DependencyInjectionHelper.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System;
using MicaWPF.Core.Services;
using MicaWPF.DependencyInjection.Options;
using MicaWPF.DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MicaWPF.DependencyInjection.Helpers;

public static class DependencyInjectionHelper
{
    /// <summary>
    /// Adds MicaWPF services to the host builder.
    /// MicaWPF is a set of services that provide theme and accent color
    /// functionality to a WPF application.
    /// The `options` parameter can be used to configure the behavior of
    /// MicaWPF.
    /// </summary>
    /// <param name="builder">The host builder instance to add MicaWPF services to.</param>
    /// <param name="options">An optional action that can be used to configure MicaWPF options.</param>
    /// <returns>The original host builder instance.</returns>
    public static IHostBuilder UseMicaWPF(this IHostBuilder builder, Action<MicaWPFOptions>? options = null)
    {
#if NET5_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(builder);
#else
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }
#endif

        var cfg = new MicaWPFOptions();

        options?.Invoke(cfg);

        builder.ConfigureServices((_, services) =>
        {
            services.TryAddSingleton(cfg);
            services.TryAddSingleton<IThemeService, ThemeServiceDI>();
            services.TryAddSingleton<IAccentColorService, AccentColorServiceDI>();
        });

        return builder;
    }
}

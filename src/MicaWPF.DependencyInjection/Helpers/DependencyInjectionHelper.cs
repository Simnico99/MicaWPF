// <copyright file="DependencyInjectionHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.DependencyInjection.Options;
using MicaWPF.DependencyInjection.Services;
using MicaWPF.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MicaWPF.DependencyInjection.Helpers;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Unused variable", Justification = "Do not cast to HostContextBuilder has it crashes the software.")]
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
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        var cfg = new MicaWPFOptions();

        options?.Invoke(cfg);

        _ = builder.ConfigureServices((_, services) =>
        {
            services.AddSingleton(cfg);
            services.AddSingleton<IThemeService, ThemeServiceDI>();
            services.AddSingleton<IAccentColorService, AccentColorServiceDI>();
        });

        return builder;
    }
}

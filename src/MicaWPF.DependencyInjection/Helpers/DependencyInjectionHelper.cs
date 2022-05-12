using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicaWPF.DependencyInjection.Services;
using MicaWPF.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MicaWPF.DependencyInjection.Helpers;
public static class DependencyInjectionHelper
{
    public static IHostBuilder UseMicaWPF(this IHostBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.ConfigureServices((_, collection) => 
        {
            collection.AddTransient<IThemeService, ThemeServiceDI>();
            collection.AddTransient<IAccentColorService, AccentColorServiceDI>();
        });

        return builder;
    }
}

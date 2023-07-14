// <copyright file="ServiceLocator.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.Reflection;

namespace MicaWPF.Core.Services;

internal static class ServiceLocator
{
    private static readonly Assembly[] _assemblies = AppDomain.CurrentDomain.GetAssemblies();

    public static string GetNamespace()
    {
        var currentAssembly = _assemblies.FirstOrDefault(x => x.GetName().Name is "MicaWPF");
        if (currentAssembly != null)
        {
            return "MicaWPF";
        }

        return "MicaWPF.Lite";
    }

    public static T GetService<T>()
        where T : class
    {
        var serviceType = typeof(T);

        var currentAssembly = _assemblies.First(x => x.GetName().Name is "MicaWPF" or "MicaWPF.Lite");

        var instance = GetInstanceFromAssembly<T>(currentAssembly, serviceType);
        if (instance != null)
        {
            return instance;
        }

        // Fallback to MicaWPF.Core
        currentAssembly = Assembly.GetAssembly(typeof(ServiceLocator));
        if (currentAssembly != null)
        {
            instance = GetInstanceFromAssembly<T>(currentAssembly, serviceType);
            if (instance != null)
            {
                return instance;
            }
        }

        throw new InvalidOperationException($"{typeof(T)} wasn't found in any assembly.");
    }

    private static T? GetInstanceFromAssembly<T>(Assembly assembly, Type serviceType)
        where T : class
    {
        var typeInAssembly = assembly
            .GetTypes()
            .FirstOrDefault(t => serviceType.IsAssignableFrom(t));

        if (typeInAssembly != null)
        {
            return (T?)Activator.CreateInstance(typeInAssembly);
        }

        return null;
    }
}

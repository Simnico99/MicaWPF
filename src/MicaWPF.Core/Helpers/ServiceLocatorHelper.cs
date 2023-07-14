// <copyright file="ServiceLocatorHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Reflection;

namespace MicaWPF.Core.Helpers;

/// <summary>
/// Provides utility methods to locate and instantiate services.
/// </summary>
internal static class ServiceLocatorHelper
{
    private static readonly Assembly[] _assemblies = AppDomain.CurrentDomain.GetAssemblies();

    /// <summary>
    /// Returns the namespace for the current assembly.
    /// </summary>
    /// <returns>The current namespace.</returns>
    public static string GetNamespace()
    {
        var currentAssembly = _assemblies.FirstOrDefault(x => x.GetName().Name is "MicaWPF");
        if (currentAssembly != null)
        {
            return "MicaWPF";
        }

        return "MicaWPF.Lite";
    }

    /// <summary>
    /// Attempts to locate and instantiate a service of the given type.
    /// </summary>
    /// <typeparam name="T">The type of service to locate.</typeparam>
    /// <returns>The service instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the service cannot be found in any assembly.</exception>
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
        currentAssembly = Assembly.GetAssembly(typeof(ServiceLocatorHelper));
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

    /// <summary>
    /// Attempts to create an instance of a specific type from the specified assembly.
    /// </summary>
    /// <typeparam name="T">The type of instance to create.</typeparam>
    /// <param name="assembly">The assembly to create the instance from.</param>
    /// <param name="serviceType">The type of service.</param>
    /// <returns>The created instance or null if it couldn't be created.</returns>
    private static T? GetInstanceFromAssembly<T>(Assembly assembly, Type serviceType)
        where T : class
    {
        var typeInAssembly = assembly
            .GetTypes()
            .FirstOrDefault(t => serviceType.IsAssignableFrom(t) && !t.IsInterface);

        if (typeInAssembly != null)
        {
            return (T?)Activator.CreateInstance(typeInAssembly);
        }

        return null;
    }
}
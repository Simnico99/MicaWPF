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
    /// Returns the namespace for the current MicaWPF assembly if lite is used or not.
    /// </summary>
    /// <returns>The current namespace.</returns>
    public static string GetNamespace()
    {
        return _assemblies.Any(x => x.GetName().Name == "MicaWPF") ? "MicaWPF" : "MicaWPF.Core";
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

        // Simplified assembly search with pattern matching
        var currentAssembly = _assemblies.FirstOrDefault(x => x.GetName().Name is "MicaWPF" or "MicaWPF.Lite");

        if (currentAssembly?.GetName().Name == "MicaWPF.Lite")
        {
            currentAssembly = _assemblies.FirstOrDefault(x => x.GetName().Name is "MicaWPF.Core");
        }

        if (currentAssembly is not null)
        {
            var instance = GetInstanceFromAssembly<T>(currentAssembly, serviceType);
            if (instance is not null)
            {
                return instance;
            }
        }

        // Utilizing the 'Assembly.GetAssembly' method directly
        var fallbackAssembly = Assembly.GetAssembly(typeof(ServiceLocatorHelper));
        var fallbackInstance = fallbackAssembly is not null ? GetInstanceFromAssembly<T>(fallbackAssembly, serviceType) : null;

        return fallbackInstance ?? throw new InvalidOperationException($"{serviceType} wasn't found in any assembly.");
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
        var typeInAssembly = assembly.GetTypes().FirstOrDefault(t => serviceType.IsAssignableFrom(t) && !t.IsInterface);

        return typeInAssembly is not null ? Activator.CreateInstance(typeInAssembly) as T : null;
    }
}
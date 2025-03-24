// <copyright file="SnapLayoutHelper.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using Microsoft.Win32;

namespace MicaWPF.Core.Helpers;

/// <summary>
/// Provides a helper method to determine if the Windows snap layout feature is enabled.
/// </summary>
public static class SnapLayoutHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
    private const string _registryValueName = "EnableSnapAssistFlyout";

    /// <summary>
    /// Determines whether the Windows snap layout feature is enabled.
    /// This is determined by checking a specific registry value.
    /// </summary>
    /// <returns>A boolean value indicating whether the Windows snap layout feature is enabled.</returns>
    public static bool IsSnapLayoutEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        var registryValueObject = key?.GetValue(_registryValueName);

        if (registryValueObject == null)
        {
            return true;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0;
    }
}

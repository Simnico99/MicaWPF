﻿namespace MicaWPF.Helpers;
public static class SnapLayoutHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
    private const string _registryValueName = "EnableSnapAssistFlyout";

    public static bool IsSnapLayoutEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        var registryValueObject = key?.GetValue(_registryValueName);

        if (registryValueObject == null)
        {
            return false;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0;
    }
}
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Management;
using System.Security.Principal;

namespace MicaWPF.Helpers;

public enum WindowsTheme
{
    Light,
    Dark,
    Auto
}

public class ThemeHelper
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

    private const string _registryValueName = "AppsUseLightTheme";

    public static ManagementEventWatcher WatchThemeChange()
    {
        WindowsIdentity currentUser = WindowsIdentity.GetCurrent();
        string query = string.Format(
            CultureInfo.InvariantCulture,
            @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
            currentUser.User.Value,
            _registryKeyPath.Replace(@"\", @"\\"),
            _registryValueName);

        ManagementEventWatcher watcher = new();

        try
        {
            watcher = new ManagementEventWatcher(query);

            return watcher;
        }
        catch (Exception)
        {
            return watcher;
        }

    }


    public static WindowsTheme GetWindowsTheme()
    {
        using RegistryKey key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        object registryValueObject = key?.GetValue(_registryValueName);
        if (registryValueObject == null)
        {
            return WindowsTheme.Light;
        }

        int registryValue = (int)registryValueObject;

        return registryValue > 0 ? WindowsTheme.Light : WindowsTheme.Dark;
    }
}

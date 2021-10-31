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

	public static void WatchThemeChange()
	{
		var currentUser = WindowsIdentity.GetCurrent();
		string query = string.Format(
			CultureInfo.InvariantCulture,
			@"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
			currentUser.User.Value,
			_registryKeyPath.Replace(@"\", @"\\"),
			_registryValueName);

		try
		{
			var watcher = new ManagementEventWatcher(query);
			watcher.EventArrived += (sender, args) =>
			{
				WindowsTheme newWindowsTheme = GetWindowsTheme();
				// React to new theme
			};

			// Start listening for events
			watcher.Start();
		}
		catch (Exception)
		{
			// This can fail on Windows 7
		}

		WindowsTheme initialTheme = GetWindowsTheme();
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

using Microsoft.Win32;

namespace MicaWPF.Helpers
{
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
}
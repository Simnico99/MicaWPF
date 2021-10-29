using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MicaWPF.Helpers
{
    public enum WindowsTheme
    {
        Light,
        Dark
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
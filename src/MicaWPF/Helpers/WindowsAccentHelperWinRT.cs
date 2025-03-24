// <copyright file="WindowsAccentHelperWinRT.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Helpers;
using MicaWPF.Core.Interop;
using MicaWPF.Core.Models;
using Microsoft.Win32;

namespace MicaWPF.Helpers;

public sealed class WindowsAccentHelperWinRT : WindowsAccentHelper
{
    public override bool AreTitleBarAndBordersAccented()
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

    public override AccentColors GetAccentColor()
    {
        try
        {
            var colors = WinRTHelper.GetSystemAccentColor();

            if (OsHelper.IsWindows11_OrGreater)
            {
                return colors;
            }

            return GetWindowsColorVariations(colors.SystemAccentColor);
        }
        catch
        {
            var colors = InteropMethods.GetDwmGetColorizationParameters();
            return GetWindowsColorVariations(ParseColor(colors.clrColor));
        }
    }
}

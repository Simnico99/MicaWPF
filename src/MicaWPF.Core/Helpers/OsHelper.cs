// <copyright file="OsHelper.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using MicaWPF.Core.Interop;

namespace MicaWPF.Core.Helpers;

/// <summary>
/// Helper class to get the current OS Version.
/// </summary>
public static class OsHelper
{
    private static readonly Version _osVersion = GetOSVersion();
    private static Version? _versionCache;

    /// <summary>
    /// Gets a value indicating whether windows NT.
    /// </summary>
    public static bool IsWindowsNT { get; } = Environment.OSVersion.Platform == PlatformID.Win32NT;

    /// <summary>
    /// Gets a value indicating whether windows 7.
    /// </summary>
    public static bool IsWindows7 { get; } = IsWindowsNT && _osVersion.Major == 6 && _osVersion.Minor == 1;

    /// <summary>
    /// Gets a value indicating whether windows 7 Or Greater.
    /// </summary>
    public static bool IsWindows7_OrGreater { get; } = IsWindowsNT && new Version(6, 1) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 8.
    /// </summary>
    public static bool IsWindows8 { get; } = IsWindowsNT && _osVersion.Major == 6 && _osVersion.Minor == 2;

    /// <summary>
    /// Gets a value indicating whether windows 8 Or Greater.
    /// </summary>
    public static bool IsWindows8_OrGreater { get; } = IsWindowsNT && new Version(6, 2) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 8.1.
    /// </summary>
    public static bool IsWindows81 { get; } = IsWindowsNT && _osVersion.Major == 6 && _osVersion.Minor == 3;

    /// <summary>
    /// Gets a value indicating whether windows 8.1 Or Greater.
    /// </summary>
    public static bool IsWindows81_OrGreater { get; } = IsWindowsNT && new Version(6, 3) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10.
    /// </summary>
    public static bool IsWindows10 { get; } = IsWindowsNT && _osVersion.Major == 10 && _osVersion.Minor == 0 && _osVersion.Build < 22000;

    /// <summary>
    /// Gets a value indicating whether windows 10 Or Greater.
    /// </summary>
    public static bool IsWindows10_OrGreater { get; } = IsWindowsNT && new Version(10, 0) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Threshold1 Version 1507 Build 10240.
    /// </summary>
    public static bool IsWindows10_1507 { get; } = IsWindowsNT && new Version(10, 0, 10240) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Threshold1 Version 1507 Build 10240 Or Greater.
    /// </summary>
    public static bool IsWindows10_1507_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 10240) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Threshold2 Version 1511 Build 10586 (November Update).
    /// </summary>
    public static bool IsWindows10_1511 { get; } = IsWindowsNT && new Version(10, 0, 10586) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Threshold2 Version 1511 Build 10586 Or Greater (November Update).
    /// </summary>
    public static bool IsWindows10_1511_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 10586) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone1 Version 1607 Build 14393 (Anniversary Update).
    /// </summary>
    public static bool IsWindows10_1607 { get; } = IsWindowsNT && new Version(10, 0, 14393) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone1 Version 1607 Build 14393 Or Greater (Anniversary Update).
    /// </summary>
    public static bool IsWindows10_1607_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 14393) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone2 Version 1703 Build 15063 (Creators Update).
    /// </summary>
    public static bool IsWindows10_1703 { get; } = IsWindowsNT && new Version(10, 0, 15063) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone2 Version 1703 Build 15063 Or Greater (Creators Update).
    /// </summary>
    public static bool IsWindows10_1703_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 15063) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone3 Version 1709 Build 16299 (Fall Creators Update).
    /// </summary>
    public static bool IsWindows10_1709 { get; } = IsWindowsNT && new Version(10, 0, 16299) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone3 Version 1709 Build 16299 Or Greater (Fall Creators Update).
    /// </summary>
    public static bool IsWindows10_1709_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 16299) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone4 Version 1803 Build 17134 (April 2018 Update).
    /// </summary>
    public static bool IsWindows10_1803 { get; } = IsWindowsNT && new Version(10, 0, 17134) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone4 Version 1803 Build 17134 Or Greater (April 2018 Update).
    /// </summary>
    public static bool IsWindows10_1803_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 17134) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone5 Version 1809 Build 17763 (October 2018 Update).
    /// </summary>
    public static bool IsWindows10_1809 { get; } = IsWindowsNT && new Version(10, 0, 17763) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 Redstone5 Version 1809 Build 17763 Or Greater (October 2018 Update).
    /// </summary>
    public static bool IsWindows10_1809_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 17763) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 19H1 Version 1903 Build 18362 (May 2019 Update).
    /// </summary>
    public static bool IsWindows10_1903 { get; } = IsWindowsNT && new Version(10, 0, 18362) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 19H1 Version 1903 Build 18362 Or Greater (May 2019 Update).
    /// </summary>
    public static bool IsWindows10_1903_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 18362) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 19H2 Version 1909 Build 18363 (November 2019 Update).
    /// </summary>
    public static bool IsWindows10_1909 { get; } = IsWindowsNT && new Version(10, 0, 18363) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 19H2 Version 1909 Build 18363 Or Greater (November 2019 Update).
    /// </summary>
    public static bool IsWindows10_1909_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 18363) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 20H1 Version 2004 Build 19041 (May 2020 Update).
    /// </summary>
    public static bool IsWindows10_2004 { get; } = IsWindowsNT && new Version(10, 0, 19041) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 20H1 Version 2004 Build 19041 Or Greater (May 2020 Update).
    /// </summary>
    public static bool IsWindows10_2004_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 19041) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 20H2 Version 2009 Build 19042 (October 2020 Update).
    /// </summary>
    public static bool IsWindows10_2009 { get; } = IsWindowsNT && new Version(10, 0, 19042) == _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 20H2 Version 2009 Build 19042 Or Greater (October 2020 Update).
    /// </summary>
    public static bool IsWindows10_2009_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 19042) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 21H1 Build 19043.
    /// </summary>
    public static bool IsWindows10_21H1 { get; } = IsWindowsNT && new Version(10, 0, 19043) <= _osVersion;

    /// <summary>
    /// Gets a value indicating whether windows 10 21H1 Build 19043 Or Greater (May 2021 Update).
    /// </summary>
    public static bool IsWindows10_21H1_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 19043) <= _osVersion;

    /// <summary>
    ///     Gets a value indicating whether windows 11 Build 22000.
    /// </summary>
    /// 
    public static bool IsWindows11 { get; } = IsWindowsNT && _osVersion.Major == 10 && _osVersion.Minor == 0 && _osVersion.Build >= 22000;

    /// <summary>
    ///     Gets a value indicating whether windows 11 Build 22000 Or Greater.
    /// </summary>
    public static bool IsWindows11_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 22000) <= _osVersion;

    /// <summary>
    ///     Gets a value indicating whether windows 11 Build 22523.
    /// </summary>
    public static bool IsWindows11_22523 { get; } = IsWindowsNT && new Version(10, 0, 22523) == _osVersion;

    /// <summary>
    ///     Gets a value indicating whether windows 11 Build 22523 Or Greater.
    /// </summary>
    public static bool IsWindows11_22523_OrGreater { get; } = IsWindowsNT && new Version(10, 0, 22523) <= _osVersion;

    /// <summary>
    /// Get the current OS version.
    /// </summary>
    /// <returns>The actual os <see cref="Version"/> (Major,Minor,Build,Revision).</returns>
    public static Version GetOSVersion()
    {
        if (_versionCache is null)
        {
            if (InteropMethods.RtlGetVersion(out var osv) != 0)
            {
                throw new PlatformNotSupportedException("MicaWPF can only run on Windows.");
            }

            _versionCache = new Version(osv.MajorVersion, osv.MinorVersion, osv.BuildNumber, osv.Revision);
        }

        return _versionCache;
    }
}

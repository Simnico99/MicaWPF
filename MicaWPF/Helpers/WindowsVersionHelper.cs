using System.Security;

namespace MicaWPF.Helpers;

internal class WindowsVersionHelper
{
    /// <summary>
    /// taken from https://stackoverflow.com/a/49641055
    /// </summary>
    /// <param name="versionInfo"></param>
    /// <returns></returns>
    [SecurityCritical]
    [DllImport("ntdll.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    internal static extern int RtlGetVersion(ref OSVERSIONINFOEX versionInfo);
    [StructLayout(LayoutKind.Sequential)]
    internal struct OSVERSIONINFOEX
    {
        // The OSVersionInfoSize field must be set to Marshal.SizeOf(typeof(OSVERSIONINFOEX))
        internal int OSVersionInfoSize;
        internal int MajorVersion;
        internal int MinorVersion;
        internal int BuildNumber;
        internal int PlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string CSDVersion;
        internal ushort ServicePackMajor;
        internal ushort ServicePackMinor;
        internal short SuiteMask;
        internal byte ProductType;
        internal byte Reserved;
    }

    public static Version GetVersion()
    {
        var osVersionInfo = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };
        if (RtlGetVersion(ref osVersionInfo) != 0)
        {
            throw new Exception("Unsuported OS!");
        }

        return new Version(osVersionInfo.MajorVersion, osVersionInfo.MinorVersion, osVersionInfo.BuildNumber);
    }
}

namespace MicaWPF.Helpers;

public static class OsHelper
{
    public static Version GetOsPreciseVersion() 
    {
        var osVersionInfo = new InteropValues.OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(InteropValues.OSVERSIONINFOEX)) };
        if (InteropMethods.RtlGetVersion(ref osVersionInfo) != 0)
        {
            throw new Exception("Unsuported OS!");
        }

        return new Version(osVersionInfo.MajorVersion, osVersionInfo.MinorVersion, osVersionInfo.BuildNumber);
    }

    public static OsVersion GetOsGlobalVersion()
    {
        var version = GetOsPreciseVersion();

        if (version.Major <= 6)
        {
            return OsVersion.WindowsOld;
        }
        else if (version.Major >= 10 && version.Build < 22000)
        {
            return OsVersion.Windows10;
        }
        else if (version.Major >= 10 && version.Build >= 22000 && version.Build < 22523)
        {
            return OsVersion.Windows11Before22523;
        }
        else if (version.Major >= 10 && version.Build >= 22523)
        {
            return OsVersion.Windows11After22523;
        }

        return OsVersion.WindowsOld;
    }
}

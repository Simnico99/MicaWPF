namespace MicaWPF.Helpers;

public static class OsHelper
{
    private static OsVersion? _globalOsVersion;

    public static OsVersion GlobalOsVersion
    {
        get 
        {
            if(_globalOsVersion is not null)
            {
                return (OsVersion)_globalOsVersion;
            }
            return (OsVersion)GetOsGlobalVersion()!;
        }
        private set { _globalOsVersion = value; }
    }

    private static Version? _preciseOsVersion;

    public static Version PreciseOsVersion
    {
        get
        {
            if (_preciseOsVersion is not null)
            {
                return _preciseOsVersion;
            }
            return GetOsPreciseVersion();
        }
        private set { _preciseOsVersion = value; }
    }


    private static Version GetOsPreciseVersion()
    {
        var osVersionInfo = new InteropValues.OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(InteropValues.OSVERSIONINFOEX)) };
        if (InteropMethods.RtlGetVersion(ref osVersionInfo) != 0)
        {
            throw new Exception("Unsuported OS!");
        }

        return _preciseOsVersion = new Version(osVersionInfo.MajorVersion, osVersionInfo.MinorVersion, osVersionInfo.BuildNumber);
    }

    private static OsVersion? GetOsGlobalVersion()
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

        return _globalOsVersion = OsVersion.WindowsOld;
    }
}

namespace MicaWPF.Helpers;

public static class OsHelper
{


    public static OsVersion GetOsVersion()
    {


        Version version = Environment.OSVersion.Version;


        if (version.Major <= 6)
        {
            return OsVersion.WindowsOld;
        }
        else if (version.Major >= 10 && version.Build < 22000)
        {
            return OsVersion.Windows10;
        }
        else if (version.Major >= 10 && version.Build >= 22000)
        {
            return OsVersion.Windows11;
        }

        return OsVersion.WindowsOld;
    }
}

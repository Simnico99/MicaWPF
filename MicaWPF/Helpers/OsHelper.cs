namespace MicaWPF.Helpers;

public static class OsHelper
{
    public static OsVersion GetOsVersion()
    {
        Version version = WindowsVersionHelper.GetVersion();

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

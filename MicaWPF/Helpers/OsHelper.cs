using System;
using System.Management;

namespace MicaWPF.Helpers;

public static class OsHelper
{
    private static ManagementObject GetManagementObject(string className)
    {
        ManagementClass wmi = new(className);

        foreach (ManagementBaseObject o in wmi.GetInstances())
        {
            ManagementObject mo = (ManagementObject)o;
            if (mo != null)
            {
                return mo;
            }
        }

        return null;
    }

    public static OsVersion GetOsVersion()
    {
        ManagementObject mo = GetManagementObject("Win32_OperatingSystem");

        Version version = Version.Parse(mo["Version"].ToString());


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
            return OsVersion.Windows10;
        }

        return OsVersion.WindowsOld;
    }
}

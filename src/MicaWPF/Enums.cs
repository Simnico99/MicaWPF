namespace MicaWPF;

public enum RevealMode
{
    Hidden,
    Visible
}
public enum ElementPosition
{
    Left,
    Right
}


public enum WindowsTheme
{
    Light,
    Dark,
    Auto
}

public enum OsVersion
{
    WindowsOld,
    Windows10,
    Windows11Before22523, // Before 22523
    Windows11After22523 // After 22523
}

public enum BackdropType
{
    None = 1,
    Mica = 2,
    Acrylic = 3,
    Tabbed = 4
}

public enum AccentBrushType
{
    Primary,
    Secondary,
    Tertiary,
    Quaternary
}

public enum TitleBarType
{
    Win32,
    WinUI
}

[Flags]
internal enum Facility
{
    Null,
    Rpc,
    Dispatch,
    Storage,
    Itf,
    Win32 = 7,
    Windows,
    Control = 10,
    Ese = 3678,
    WinCodec = 2200
}

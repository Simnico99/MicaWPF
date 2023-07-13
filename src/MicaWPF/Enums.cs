// <copyright file="Enums.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

namespace MicaWPF;

/// <summary>
/// Results of a Content Dialog.
/// </summary>
public enum ContentDialogResult
{
    PrimaryButton,
    SecondaryButton,
    TertiaryButton,
    Empty,
}

/// <summary>
/// The button that must be colored on the content dialog.
/// </summary>
public enum ContentDialogButton
{
    Primary,
    Secondary,
    Close,
}

/// <summary>
/// Is the password visible or not.
/// </summary>
public enum RevealMode
{
    Hidden,
    Visible,
}

/// <summary>
/// Element position.
/// </summary>
public enum ElementPosition
{
    Left,
    Right,
}

/// <summary>
/// The theme used by Windows.
/// </summary>
public enum WindowsTheme
{
    Light,
    Dark,
    Auto,
}

/// <summary>
/// The type of backdrop used by a Window.
/// </summary>
public enum BackdropType
{
    None = 1,
    Mica = 2,
    Acrylic = 3,
    Tabbed = 4,
}

/// <summary>
/// The type of accent brush.
/// </summary>
public enum AccentBrushType
{
    Primary,
    Secondary,
    Tertiary,
    Quaternary,
}

/// <summary>
/// The type of title bar.
/// </summary>
public enum TitleBarType
{
    Win32,
    WinUI,
}

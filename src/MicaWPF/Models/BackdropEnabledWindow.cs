namespace MicaWPF.Models;

/// <summary>
/// Represents a readonly struct that contains information about a window and its backdrop type.
/// </summary>
public readonly struct BackdropEnabledWindow
{
    /// <summary>
    /// Gets the Window associated with this BackdropEnabledWindow instance.
    /// </summary>
    public Window Window { get; }

    /// <summary>
    /// Gets the BackdropType associated with this BackdropEnabledWindow instance.
    /// </summary>
    public BackdropType BackdropType { get; }

    /// <summary>
    /// Initializes a new instance of the BackdropEnabledWindow class with the specified Window and BackdropType.
    /// </summary>
    /// <param name="window">The Window associated with this BackdropEnabledWindow instance.</param>
    /// <param name="backdropType">The BackdropType associated with this BackdropEnabledWindow instance.</param>
    public BackdropEnabledWindow(Window window, BackdropType backdropType)
    {
        Window = window;
        BackdropType = backdropType;
    }
}

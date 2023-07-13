// <copyright file="BackdropEnabledWindow.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Core.Enums;

namespace MicaWPF.Core.Models;

/// <summary>
/// Represents a readonly struct that contains information about a window and its backdrop type.
/// </summary>
public readonly struct BackdropEnabledWindow
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BackdropEnabledWindow"/> struct.
    /// </summary>
    /// <param name="window">The Window associated with this BackdropEnabledWindow instance.</param>
    /// <param name="backdropType">The BackdropType associated with this BackdropEnabledWindow instance.</param>
    public BackdropEnabledWindow(Window window, BackdropType backdropType)
    {
        Window = window;
        BackdropType = backdropType;
    }

    /// <summary>
    /// Gets the Window associated with this BackdropEnabledWindow instance.
    /// </summary>
    public Window Window { get; }

    /// <summary>
    /// Gets the BackdropType associated with this BackdropEnabledWindow instance.
    /// </summary>
    public BackdropType BackdropType { get; }
}

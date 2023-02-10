using MicaWPF.Events;

namespace MicaWPF.Services;

public interface IAccentColorService
{
    /// <summary>
    /// Event that is raised when the accent colors are changed.
    /// </summary>
    IWeakEvent<AccentColors> AccentColorChanged { get; }

    /// <summary>
    /// Contains information about the current accent colors.
    /// </summary>
    AccentColors AccentColors { get; }

    /// <summary>
    /// Indicates if the accent colors were updated from Windows.
    /// </summary>
    bool AccentUpdateFromWindows { get; }

    /// <summary>
    /// Indicates if the title bar and borders are accent aware.
    /// </summary>
    bool IsTitleBarAndBorderAccentAware { get; set; }

    /// <summary>
    /// Indicates if the title bar and windows borders are colored.
    /// </summary>
    bool IsTitleBarAndWindowsBorderColored { get; }

    /// <summary>
    /// Updates the accent colors with the given color.
    /// </summary>
    /// <param name="systemAccent">The color to use as the system accent color</param>
    void UpdateAccents(Color systemAccent);

    /// <summary>
    /// Updates the accent colors with the system accent color obtained from Windows.
    /// </summary>
    void UpdateAccentsFromWindows();
}
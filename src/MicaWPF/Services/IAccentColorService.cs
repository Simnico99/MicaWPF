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
    bool AccentColorsUpdateFromWindows { get; }

    /// <summary>
    /// Indicates if the title bar and borders are accent aware.
    /// </summary>
    bool IsTitleBarAndBorderAccentAware { get; set; }

    /// <summary>
    /// Indicates if the title bar and windows borders are colored.
    /// </summary>
    bool IsTitleBarAndWindowsBorderColored { get; }

    /// <summary>
    /// Refreshes the accent colors used in the application. 
    /// If `AccentColorsUpdateFromWindows` is set to true, the accent colors will be updated from Windows. 
    /// Otherwise, the accent colors will be updated to the system accent color.
    /// This is automatically done when the theme change.
    /// </summary>
    void RefreshAccentsColors();

    /// <summary>
    /// Updates the accent colors with the given color.
    /// </summary>
    /// <param name="systemAccent">The color to use as the system accent color</param>
    void UpdateAccentsColors(Color systemAccent);

    /// <summary>
    /// Updates the accent colors with the system accent color obtained from Windows.
    /// </summary>
    void UpdateAccentsColorsFromWindows();
}
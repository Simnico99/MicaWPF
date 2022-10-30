using MicaWPF.Events;

namespace MicaWPF.Services;
public interface IAccentColorService
{
    IWeakEvent<AccentColors> AccentColorChanged { get; }
    AccentColors AccentColors { get; }
    bool AccentUpdateFromWindows { get; }
    bool IsTitleBarAndBorderAccentAware { get; set; }
    bool IsTitleBarAndWindowsBorderColored { get; }

    void UpdateAccents(Color systemAccent);
    void UpdateAccentsFromWindows();
}
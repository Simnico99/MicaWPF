using MicaWPF.Events;

namespace MicaWPF.Services;
public interface IAccentColorService
{
    IWeakEvent<Color> AccentColorChanged { get; }
    bool AccentUpdateFromWindows { get; }
    Color SystemAccentColor { get; }
    Color SystemAccentColorDark1 { get; }
    Color SystemAccentColorDark2 { get; }
    Color SystemAccentColorDark3 { get; }
    Color SystemAccentColorLight1 { get; }
    Color SystemAccentColorLight2 { get; }
    Color SystemAccentColorLight3 { get; }
    void UpdateAccents(Color systemAccent);
    void UpdateAccentsFromWindows();
}

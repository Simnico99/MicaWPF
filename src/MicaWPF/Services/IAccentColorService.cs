namespace MicaWPF.Services;

public interface IAccentColorService
{
    AccentColors AccentColors { get; }
    bool AccentUpdateFromWindows { get; }

    void UpdateAccents(Color systemAccent);
    void UpdateAccentsFromWindows();
}
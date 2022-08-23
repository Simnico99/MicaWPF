using System.Windows.Media;
using MicaWPF.DependencyInjection.Options;
using MicaWPF.Events;
using MicaWPF.Services;

namespace MicaWPF.DependencyInjection.Services;
internal class AccentColorServiceDI : IAccentColorService
{
    private readonly AccentColorService _localThemeService = AccentColorService.GetCurrent();
    private readonly MicaWPFOptions _options;
    public IWeakEvent<Color> AccentColorChanged => _localThemeService.AccentColorChanged;

    public AccentColorServiceDI(MicaWPFOptions options)
    {
        _options = options;

        if (_options.UpdateAccentFromWindows)
        {
            _localThemeService.UpdateAccentsFromWindows();
        }
        else
        {
            _localThemeService.UpdateAccents(options.AccentColor);
        }
    }

    public bool AccentUpdateFromWindows => _localThemeService.AccentUpdateFromWindows;

    public Color SystemAccentColor => _localThemeService.SystemAccentColor;

    public Color SystemAccentColorDark1 => _localThemeService.SystemAccentColorDark1;

    public Color SystemAccentColorDark2 => _localThemeService.SystemAccentColorDark2;

    public Color SystemAccentColorDark3 => _localThemeService.SystemAccentColorDark3;

    public Color SystemAccentColorLight1 => _localThemeService.SystemAccentColorLight1;

    public Color SystemAccentColorLight2 => _localThemeService.SystemAccentColorLight2;

    public Color SystemAccentColorLight3 => _localThemeService.SystemAccentColorLight3;

    public void UpdateAccents(Color systemAccent)
    {
        _localThemeService.UpdateAccents(systemAccent);
    }

    public void UpdateAccentsFromWindows()
    {
        _localThemeService.UpdateAccentsFromWindows();
    }
}

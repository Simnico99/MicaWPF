using System.Windows.Media;
using MicaWPF.DependencyInjection.Options;
using MicaWPF.Models;
using MicaWPF.Services;

namespace MicaWPF.DependencyInjection.Services;
internal class AccentColorServiceDI : IAccentColorService
{
    private readonly AccentColorService _localThemeService = AccentColorService.GetCurrent();
    private readonly MicaWPFOptions _options;

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

    public AccentColors AccentColors => _localThemeService.AccentColors;

    public void UpdateAccents(Color systemAccent)
    {
        _localThemeService.UpdateAccents(systemAccent);
    }

    public void UpdateAccentsFromWindows()
    {
        _localThemeService.UpdateAccentsFromWindows();
    }
}

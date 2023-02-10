using MicaWPF.DependencyInjection.Options;
using MicaWPF.Events;
using MicaWPF.Models;
using MicaWPF.Services;
using System.Windows.Media;

namespace MicaWPF.DependencyInjection.Services;
internal sealed class AccentColorServiceDI : IAccentColorService
{
    private readonly MicaWPFOptions _options;
    private readonly AccentColorService _accentColorService = AccentColorService.Current;

    public IWeakEvent<AccentColors> AccentColorChanged => _accentColorService.AccentColorChanged;
    public AccentColors AccentColors => _accentColorService.AccentColors;
    public bool AccentColorsUpdateFromWindows => _accentColorService.AccentColorsUpdateFromWindows;
    public bool IsTitleBarAndBorderAccentAware
    {
        get => _accentColorService.IsTitleBarAndBorderAccentAware;
        set => _accentColorService.IsTitleBarAndBorderAccentAware = value;
    }
    public bool IsTitleBarAndWindowsBorderColored => _accentColorService.IsTitleBarAndWindowsBorderColored;

    public AccentColorServiceDI(MicaWPFOptions options)
    {
        _options = options;

        if (_options.UpdateAccentFromWindows)
        {
            _accentColorService.UpdateAccentsColorsFromWindows();
        }
        else
        {
            _accentColorService.UpdateAccentsColors(_options.AccentColor);
        }

        _accentColorService.IsTitleBarAndBorderAccentAware = _options.IsTitleBarAndBorderAccentAware;
    }

    public void UpdateAccentsColors(Color systemAccent)
    {
        _accentColorService.UpdateAccentsColors(systemAccent);
    }

    public void UpdateAccentsColorsFromWindows()
    {
        _accentColorService.UpdateAccentsColorsFromWindows();
    }

    public void RefreshAccentsColors()
    {
        _accentColorService.RefreshAccentsColors();
    }
}
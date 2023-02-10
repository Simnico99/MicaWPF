using MicaWPF.DependencyInjection.Options;
using MicaWPF.Events;
using MicaWPF.Models;
using MicaWPF.Services;
using System.Windows.Media;

namespace MicaWPF.DependencyInjection.Services;
internal sealed class AccentColorServiceDI : IAccentColorService
{
    private readonly MicaWPFOptions _options;
    public IWeakEvent<AccentColors> AccentColorChanged => AccentColorService.Current.AccentColorChanged;

    public AccentColorServiceDI(MicaWPFOptions options)
    {
        _options = options;

        if (_options.UpdateAccentFromWindows)
        {
            AccentColorService.Current.UpdateAccentsColorsFromWindows();
        }
        else
        {
            AccentColorService.Current.UpdateAccentsColors(options.AccentColor);
        }

        AccentColorService.Current.IsTitleBarAndBorderAccentAware = _options.IsTitleBarAndBorderAccentAware;
    }

    public bool AccentColorsUpdateFromWindows => AccentColorService.Current.AccentColorsUpdateFromWindows;

    public AccentColors AccentColors => AccentColorService.Current.AccentColors;

    public bool IsTitleBarAndBorderAccentAware { get => AccentColorService.Current.IsTitleBarAndBorderAccentAware; set => AccentColorService.Current.IsTitleBarAndBorderAccentAware = value; }

    public bool IsTitleBarAndWindowsBorderColored => AccentColorService.Current.IsTitleBarAndWindowsBorderColored;

    public void UpdateAccentsColors(Color systemAccent)
    {
        AccentColorService.Current.UpdateAccentsColors(systemAccent);
    }

    public void UpdateAccentsColorsFromWindows()
    {
        AccentColorService.Current.UpdateAccentsColorsFromWindows();
    }

    public void RefreshAccentsColors()
    {
        AccentColorService.Current.RefreshAccentsColors();
    }
}
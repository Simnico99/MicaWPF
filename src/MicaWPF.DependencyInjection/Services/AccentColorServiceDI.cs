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
            AccentColorService.Current.UpdateAccentsFromWindows();
        }
        else
        {
            AccentColorService.Current.UpdateAccents(options.AccentColor);
        }

        AccentColorService.Current.IsTitleBarAndBorderAccentAware = _options.IsTitleBarAndBorderAccentAware;
    }

    public bool AccentUpdateFromWindows => AccentColorService.Current.AccentUpdateFromWindows;

    public AccentColors AccentColors => AccentColorService.Current.AccentColors;

    public bool IsTitleBarAndBorderAccentAware { get => AccentColorService.Current.IsTitleBarAndBorderAccentAware; set => AccentColorService.Current.IsTitleBarAndBorderAccentAware = value; }

    public bool IsTitleBarAndWindowsBorderColored => AccentColorService.Current.IsTitleBarAndWindowsBorderColored;

    public void UpdateAccents(Color systemAccent)
    {
        AccentColorService.Current.UpdateAccents(systemAccent);
    }

    public void UpdateAccentsFromWindows()
    {
        AccentColorService.Current.UpdateAccentsFromWindows();
    }
}
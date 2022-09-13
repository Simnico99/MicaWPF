using System.Windows.Media;
using MicaWPF.DependencyInjection.Options;
using MicaWPF.Events;
using MicaWPF.Services;

namespace MicaWPF.DependencyInjection.Services;
internal sealed class AccentColorServiceDI : IAccentColorService
{
    private readonly MicaWPFOptions _options;
    public IWeakEvent<Color> AccentColorChanged => AccentColorService.Current.AccentColorChanged;

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

    public Color SystemAccentColor => AccentColorService.Current.SystemAccentColor;

    public Color SystemAccentColorDark1 => AccentColorService.Current.SystemAccentColorDark1;

    public Color SystemAccentColorDark2 => AccentColorService.Current.SystemAccentColorDark2;

    public Color SystemAccentColorDark3 => AccentColorService.Current.SystemAccentColorDark3;

    public Color SystemAccentColorLight1 => AccentColorService.Current.SystemAccentColorLight1;

    public Color SystemAccentColorLight2 => AccentColorService.Current.SystemAccentColorLight2;

    public Color SystemAccentColorLight3 => AccentColorService.Current.SystemAccentColorLight3;

    public void UpdateAccents(Color systemAccent)
    {
        AccentColorService.Current.UpdateAccents(systemAccent);
    }

    public void UpdateAccentsFromWindows()
    {
        AccentColorService.Current.UpdateAccentsFromWindows();
    }
}

// <copyright file="AccentColorServiceDI.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using System.Windows.Media;
using MicaWPF.Core.Events;
using MicaWPF.Core.Models;
using MicaWPF.Core.Services;
using MicaWPF.DependencyInjection.Options;
using MicaWPF.Services;

namespace MicaWPF.DependencyInjection.Services;

internal sealed class AccentColorServiceDI : IAccentColorService
{
    private readonly MicaWPFOptions _options;
    private readonly IAccentColorService _accentColorService = MicaWPFControllerService.AccentColorService;

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

    public IWeakEvent<AccentColors> AccentColorChanged => _accentColorService.AccentColorChanged;

    public AccentColors AccentColors => _accentColorService.AccentColors;

    public bool AccentColorsUpdateFromWindows => _accentColorService.AccentColorsUpdateFromWindows;

    public bool IsTitleBarAndBorderAccentAware
    {
        get => _accentColorService.IsTitleBarAndBorderAccentAware;
        set => _accentColorService.IsTitleBarAndBorderAccentAware = value;
    }

    public bool IsTitleBarAndWindowsBorderColored => _accentColorService.IsTitleBarAndWindowsBorderColored;

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
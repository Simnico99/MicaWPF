// <copyright file="AccentColorServiceDI.cs" company="Zircon Fusion">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Media;
using MicaWPF.Core.Events;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Models;
using MicaWPF.Core.Services;
using MicaWPF.DependencyInjection.Options;

namespace MicaWPF.DependencyInjection.Services;

/// <summary>
/// The AccentColorService through dependency injection.
/// </summary>
internal sealed class AccentColorServiceDI : IAccentColorService
{
    private readonly MicaWPFOptions _options;
    private readonly IAccentColorService _accentColorService = MicaWPFServiceUtility.AccentColorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccentColorServiceDI"/> class.
    /// </summary>
    /// <param name="options">MicaWPF options.</param>
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

    /// <inheritdoc/>
    public WindowsAccentHelper WindowsAccentHelper => _accentColorService.WindowsAccentHelper;

    /// <inheritdoc/>
    public IWeakEvent<AccentColors> AccentColorChanged => _accentColorService.AccentColorChanged;

    /// <inheritdoc/>
    public AccentColors AccentColors => _accentColorService.AccentColors;

    /// <inheritdoc/>
    public bool AccentColorsUpdateFromWindows => _accentColorService.AccentColorsUpdateFromWindows;

    /// <inheritdoc/>
    public bool IsTitleBarAndBorderAccentAware
    {
        get => _accentColorService.IsTitleBarAndBorderAccentAware;
        set => _accentColorService.IsTitleBarAndBorderAccentAware = value;
    }

    /// <inheritdoc/>
    public bool IsTitleBarAndWindowsBorderColored => _accentColorService.IsTitleBarAndWindowsBorderColored;

    /// <inheritdoc/>
    public void UpdateAccentsColors(Color systemAccent)
    {
        _accentColorService.UpdateAccentsColors(systemAccent);
    }

    /// <inheritdoc/>
    public void UpdateAccentsColorsFromWindows()
    {
        _accentColorService.UpdateAccentsColorsFromWindows();
    }

    /// <inheritdoc/>
    public void RefreshAccentsColors()
    {
        _accentColorService.RefreshAccentsColors();
    }

    /// <inheritdoc/>
    public void IsTitleBarAndBorderAccentEnabled(Window window, bool isEnabled)
    {
        _accentColorService.IsTitleBarAndBorderAccentEnabled(window, isEnabled);
    }
}
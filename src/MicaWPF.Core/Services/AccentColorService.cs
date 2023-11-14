// <copyright file="AccentColorService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is open-source and free for use, modification, and distribution.
// </copyright>

using System.Windows;
using System.Windows.Media;
using MicaWPF.Core.Controls.MicaWindow;
using MicaWPF.Core.Enums;
using MicaWPF.Core.Events;
using MicaWPF.Core.Helpers;
using MicaWPF.Core.Models;
using Microsoft.Win32;

namespace MicaWPF.Core.Services;

/// <summary>
/// Service that manages the accent colors of the application.
/// </summary>
public class AccentColorService : IAccentColorService
{
    private bool _isTitleBarAndBorderAccentAware;
    private bool _isCheckingTitleBarAndBorderAccent;

    public AccentColorService()
    {
        UpdateAccentsColorsFromWindows();
        IsTitleBarAndWindowsBorderColored = WindowsAccentHelper.AreTitleBarAndBordersAccented();
        IsTitleBarAndBorderAccentAware = true;
    }

    public virtual WindowsAccentHelper WindowsAccentHelper { get; } = new WindowsAccentHelper();

    public IWeakEvent<AccentColors> AccentColorChanged { get; } = new WeakEvent<AccentColors>();

    public AccentColors AccentColors { get; private set; } = default;

    public bool AccentColorsUpdateFromWindows { get; private set; } = true;

    public bool IsTitleBarAndWindowsBorderColored { get; private set; }

    public bool IsTitleBarAndBorderAccentAware
    {
        get => _isTitleBarAndBorderAccentAware;
        set => SetTitleBarAndBorderAccentAware(value);
    }

    public void RefreshAccentsColors()
    {
        if (AccentColorsUpdateFromWindows)
        {
            UpdateAccentsColorsFromWindows();
        }
        else
        {
            UpdateAccentsColors(AccentColors.SystemAccentColor);
        }
    }

    public void UpdateAccentsColors(Color systemAccent)
    {
        AccentColorsUpdateFromWindows = false;
        AccentColors = WindowsAccentHelper.GetWindowsColorVariations(systemAccent);

        UpdateFromInternalColors();
    }

    public void UpdateAccentsColorsFromWindows()
    {
        AccentColors = WindowsAccentHelper.GetAccentColor();

        UpdateFromInternalColors();
    }

    private void UpdateColorResources(Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {
        var colorKeys = new string[] { "Primary", "Secondary", "Tertiary", "Light3", "Light2", "Light1", string.Empty, "Dark1", "Dark2", "Dark3" };
        var accentColors = new Color[] { primaryAccent, secondaryAccent, tertiaryAccent, AccentColors.SystemAccentColorLight3, AccentColors.SystemAccentColorLight2, AccentColors.SystemAccentColorLight1, AccentColors.SystemAccentColor, AccentColors.SystemAccentColorDark1, AccentColors.SystemAccentColorDark2, AccentColors.SystemAccentColorDark3 };

        for (var i = 0; i < colorKeys.Length; i++)
        {
            Application.Current.Resources[$"MicaWPF{MicaWPFServiceUtility.BrushNamespace}.Colors.SystemAccentColor{colorKeys[i]}"] = accentColors[i];
        }

        WindowHelper.RefreshAllWindowsContents();
        AccentColorChanged.Publish(AccentColors);
    }

    private void UpdateFromInternalColors()
    {
        switch (MicaWPFServiceUtility.ThemeService.CurrentTheme)
        {
            case WindowsTheme.Dark:
                UpdateColorResources(AccentColors.SystemAccentColorLight1, AccentColors.SystemAccentColorLight2, AccentColors.SystemAccentColorLight3);
                break;
            default:
                UpdateColorResources(AccentColors.SystemAccentColorDark1, AccentColors.SystemAccentColorDark2, AccentColors.SystemAccentColorDark3);
                break;
        }

        MicaWPFServiceUtility.ThemeDictionaryService.RefreshThemeSource();
    }

    private void SetAccentColorOnTitleBarAndBorders(bool isEnabled)
    {
        IsTitleBarAndWindowsBorderColored = isEnabled;
        var windows = Application.Current.Windows;
        foreach (var window in windows)
        {
            if (window is IMicaWindow micaWindow)
            {
                micaWindow.UseAccentOnTitleBarAndBorder = isEnabled;
            }
        }
    }

    private void SetTitleBarAndBorderAccentAware(bool isTitleBarAndBorderAccentAware)
    {
        _isTitleBarAndBorderAccentAware = isTitleBarAndBorderAccentAware;

        if (IsTitleBarAndBorderAccentAware && !_isCheckingTitleBarAndBorderAccent)
        {
            _isCheckingTitleBarAndBorderAccent = true;
            if (OsHelper.IsWindows10_OrGreater && IsTitleBarAndBorderAccentAware)
            {
                SystemEvents.UserPreferenceChanged += SystemEventsUserPreferenceChanged;
            }
        }
        else if (!IsTitleBarAndBorderAccentAware && _isCheckingTitleBarAndBorderAccent)
        {
            SystemEvents.UserPreferenceChanged -= SystemEventsUserPreferenceChanged;
            _isCheckingTitleBarAndBorderAccent = false;
        }
    }

    private void SystemEventsUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        switch (e.Category)
        {
            case UserPreferenceCategory.General:
                if (IsTitleBarAndBorderAccentAware)
                {
                    Application.Current.Dispatcher.Invoke(() => SetAccentColorOnTitleBarAndBorders(WindowsAccentHelper.AreTitleBarAndBordersAccented()));
                    SetTitleBarAndBorderAccentAware(IsTitleBarAndBorderAccentAware);
                }

                if (AccentColorsUpdateFromWindows)
                {
                    Application.Current.Dispatcher.Invoke(() => UpdateAccentsColorsFromWindows());
                }

                break;
        }
    }
}

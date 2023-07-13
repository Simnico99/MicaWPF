// <copyright file="AccentColorService.cs" company="Zircon Technology">
// This software is distributed under the MIT license and its code is free of use.
// </copyright>

using MicaWPF.Controls;
using MicaWPF.Events;

namespace MicaWPF.Services;

/// <summary>
/// Service that manages the accent colors of the application.
/// </summary>
public sealed class AccentColorService : IAccentColorService
{
    private bool _isTitleBarAndBorderAccentAware;
    private bool _isCheckingTitleBarAndBorderAccent;

    static AccentColorService()
    {
        Current = new AccentColorService();
        var localCurrent = (AccentColorService)Current;
        localCurrent.UpdateAccentsColorsFromWindows();
        localCurrent.IsTitleBarAndWindowsBorderColored = WindowsAccentHelper.AreTitleBarAndBordersAccented();
        localCurrent.IsTitleBarAndBorderAccentAware = true;
    }

    private AccentColorService()
    {
    }

    /// <summary>
    /// Gets the current instance of <see cref="AccentColorService"/> but as the interface <see cref="IAccentColorService"/>.
    /// </summary>
    public static IAccentColorService Current { get; }

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
        var drawingColor = System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B);
        var hsvColor = HSVColorHelper.ConvertToHSVColor(drawingColor);

        AccentColors = new AccentColors(
            systemAccent,
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Secondary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Tertiary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Quaternary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Secondary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Tertiary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Quaternary));

        UpdateFromInternalColors();
    }

    public void UpdateAccentsColorsFromWindows()
    {
        AccentColorsUpdateFromWindows = true;

        AccentColors = WindowsAccentHelper.GetAccentColor();

        if (OsHelper.IsWindows10 || AccentColors.IsFallBack)
        {
            UpdateAccentsColors(AccentColors.SystemAccentColor);
        }

        UpdateFromInternalColors();
    }

    private static Color GetThemeColorVariation((double Hue, double Saturation, double Value) hsv, WindowsTheme windowsTheme, AccentBrushType accentBrushType)
    {
        var hueCoefficient = 1 - hsv.Value < 0.15 ? 1 : 0;

        return accentBrushType switch
        {
            AccentBrushType.Primary => HSVColorHelper.RGBFromHSV(hsv.Hue, hsv.Saturation, hsv.Value),
            AccentBrushType.Secondary => GetOffSet(hsv.Hue, hueCoefficient, hsv.Saturation, hsv.Value, 0.3, windowsTheme),
            AccentBrushType.Tertiary => GetOffSet(hsv.Hue, hueCoefficient, hsv.Saturation, hsv.Value, 0.35, windowsTheme),
            AccentBrushType.Quaternary => GetOffSet(hsv.Hue, hueCoefficient, hsv.Saturation, hsv.Value, 0.65, windowsTheme),
            _ => throw new ArgumentOutOfRangeException(nameof(accentBrushType)),
        };
    }

    private static Color GetOffSet(double hue, double hueCoefficient, double saturation, double value, double valueCoefficient, WindowsTheme windowsTheme)
    {
        return windowsTheme switch
        {
            WindowsTheme.Dark => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value + valueCoefficient, 1)),
            WindowsTheme.Light => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value - (valueCoefficient / 2), 1)),
            _ => throw new ArgumentOutOfRangeException(nameof(windowsTheme)),
        };
    }

    private void UpdateColorResources(Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {
        var colorKeys = new string[] { "Primary", "Secondary", "Tertiary", "Light3", "Light2", "Light1", string.Empty, "Dark1", "Dark2", "Dark3" };
        var accentColors = new Color[] { primaryAccent, secondaryAccent, tertiaryAccent, AccentColors.SystemAccentColorLight3, AccentColors.SystemAccentColorLight2, AccentColors.SystemAccentColorLight1, AccentColors.SystemAccentColor, AccentColors.SystemAccentColorDark1, AccentColors.SystemAccentColorDark2, AccentColors.SystemAccentColorDark3 };

        for (var i = 0; i < colorKeys.Length; i++)
        {
            Application.Current.Resources[$"MicaWPF.Colors.SystemAccentColor{colorKeys[i]}"] = accentColors[i];
        }

        AccentColorChanged.Publish(AccentColors);
    }

    private void UpdateFromInternalColors()
    {
        switch (ThemeService.Current.CurrentTheme)
        {
            case WindowsTheme.Dark:
                UpdateColorResources(AccentColors.SystemAccentColorLight1, AccentColors.SystemAccentColorLight2, AccentColors.SystemAccentColorLight3);
                break;
            default:
                UpdateColorResources(AccentColors.SystemAccentColorDark1, AccentColors.SystemAccentColorDark2, AccentColors.SystemAccentColorDark3);
                break;
        }

        WindowHelper.RefreshAllWindowsContents();
        ThemeDictionaryService.Current.RefreshThemeSource();
    }

    private void SetAccentColorOnTitleBarAndBorders(bool isEnabled)
    {
        IsTitleBarAndWindowsBorderColored = isEnabled;
        var windows = Application.Current.Windows;
        foreach (var window in windows)
        {
            if (window is MicaWindow micaWindow)
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

                break;
        }
    }
}

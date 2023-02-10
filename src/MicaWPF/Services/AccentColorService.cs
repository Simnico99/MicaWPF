using MicaWPF.Controls;
using MicaWPF.Events;
#if NET5_0_OR_GREATER
using MicaWPFRuntimeComponent;
#endif
#if NETFRAMEWORK || NETCOREAPP3_1
using Windows.UI.ViewManagement;
#endif

namespace MicaWPF.Services;

public sealed class AccentColorService : IAccentColorService
{
    private const string _registryKeyPath = @"Software\Microsoft\Windows\DWM";
    private const string _registryValueName = "ColorPrevalence";

    private bool _isTitleBarAndBorderAccentAware;
    private bool _isCheckingTitleBarAndBorderAccent;

    public IWeakEvent<AccentColors> AccentColorChanged { get; } = new WeakEvent<AccentColors>();
    public static AccentColorService Current { get; }

    public AccentColors AccentColors { get; private set; } = new AccentColors();
    public bool AccentUpdateFromWindows { get; private set; } = true;
    public bool IsTitleBarAndWindowsBorderColored { get; private set; }
    public bool IsTitleBarAndBorderAccentAware
    {
        get => _isTitleBarAndBorderAccentAware;
        set => SetTitleBarAndBorderAccentAware(value);
    }

    static AccentColorService()
    {
        Current = new();
        Current.UpdateAccentsFromWindows();
        Current.IsTitleBarAndWindowsBorderColored = GetAccentColorEnabledOnTitleBarAndBorders();
        Current.IsTitleBarAndBorderAccentAware = true;
    }

    private AccentColorService() { }

    public void UpdateAccents(Color systemAccent)
    {
        AccentUpdateFromWindows = false;
        var drawingColor = System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B);
        var hsvColor = HSVColorHelper.ConvertToHSVColor(drawingColor);

        AccentColors = new AccentColors(
            systemAccent,
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Secondary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Tertiary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Dark, AccentBrushType.Quaternary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Secondary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Tertiary),
            GetThemeColorVariation(hsvColor, WindowsTheme.Light, AccentBrushType.Quaternary)
            );

        UpdateFromInternalColors();
    }

    public void UpdateAccentsFromWindows()
    {
        AccentUpdateFromWindows = true;

        AccentColors = WindowsAccentHelper.GetAccentColor();

        if (OsHelper.IsWindows10 || AccentColors.IsFallBack)
        {
            UpdateAccents(AccentColors.SystemAccentColor);
        }

        UpdateFromInternalColors();
    }

    private void UpdateColorResources(Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {
        var colorKeys = new string[] { "Primary", "Secondary", "Tertiary", "Light3", "Light2", "Light1", "", "Dark1", "Dark2", "Dark3" };
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
        ThemeService.RefreshTheme();
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
                    Application.Current.Dispatcher.Invoke(() => SetAccentColorOnTitleBarAndBorders(GetAccentColorEnabledOnTitleBarAndBorders()));
                    SetTitleBarAndBorderAccentAware(IsTitleBarAndBorderAccentAware);
                }
                break;
        }
    }

    private static bool GetAccentColorEnabledOnTitleBarAndBorders()
    {
        using var key = Registry.CurrentUser.OpenSubKey(_registryKeyPath);
        var registryValueObject = key?.GetValue(_registryValueName);

        if (registryValueObject == null)
        {
            return false;
        }

        var registryValue = (int)registryValueObject;

        return registryValue > 0;
    }

    private static Color GetThemeColorVariation((double hue, double saturation, double value) hsv, WindowsTheme windowsTheme, AccentBrushType accentBrushType)
    {
        var hueCoefficient = 1 - hsv.value < 0.15 ? 1 : 0;

        return accentBrushType switch
        {
            AccentBrushType.Primary => HSVColorHelper.RGBFromHSV(hsv.hue, hsv.saturation, hsv.value),
            AccentBrushType.Secondary => GetOffSet(hsv.hue, hueCoefficient, hsv.saturation, hsv.value, 0.3, windowsTheme),
            AccentBrushType.Tertiary => GetOffSet(hsv.hue, hueCoefficient, hsv.saturation, hsv.value, 0.35, windowsTheme),
            AccentBrushType.Quaternary => GetOffSet(hsv.hue, hueCoefficient, hsv.saturation, hsv.value, 0.65, windowsTheme),
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
}

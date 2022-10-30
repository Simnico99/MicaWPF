using System.Runtime;
using MicaWPF.Extensions;
#if NET5_0_OR_GREATER
using MicaWPFRuntimeComponent;
#endif
#if NETFRAMEWORK || NETCOREAPP3_1
using Windows.UI.ViewManagement;
#endif

namespace MicaWPF.Services;

public class AccentColorService : IAccentColorService
{
    private static readonly AccentColorService _systemColorsHandler = new();
    private bool _isInit = false;
    public bool AccentUpdateFromWindows { get; private set; } = true;
    public AccentColors AccentColors { get; private set; } = new();

    internal void Init()
    {
        if (!_isInit)
        {
            _isInit = true;
            if (AccentUpdateFromWindows)
            {
                UpdateAccentsFromWindows();
                return;
            }
        }
    }

    private AccentColorService() { }

    private static Color GetOffSet(double hue, double hueCoefficient, double saturation, double value, double valueCoefficient, WindowsTheme windowsTheme)
    {
        return windowsTheme switch
        {
            WindowsTheme.Dark => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value + valueCoefficient, 1)),
            WindowsTheme.Light => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value - (valueCoefficient / 2), 1)),
            _ => throw new ArgumentOutOfRangeException(nameof(windowsTheme)),
        };
    }

    private static Color GetThemeColorVariation((double hue, double saturation, double value) hsv, WindowsTheme windowsTheme, AccentBrushType accentBrushType)
    {
        var hueCoefficient = 0;
        if (1 - hsv.value < 0.15)
        {
            hueCoefficient = 1;
        }

        return accentBrushType switch
        {
            AccentBrushType.Primary => HSVColorHelper.RGBFromHSV(hsv.hue, hsv.saturation, hsv.value),
            AccentBrushType.Secondary => GetOffSet(hsv.hue, hueCoefficient, hsv.saturation, hsv.value, 0.3, windowsTheme),
            AccentBrushType.Tertiary => GetOffSet(hsv.hue, hueCoefficient, hsv.saturation, hsv.value, 0.35, windowsTheme),
            AccentBrushType.Quaternary => GetOffSet(hsv.hue, hueCoefficient, hsv.saturation, hsv.value, 0.65, windowsTheme),
            _ => throw new ArgumentOutOfRangeException(nameof(accentBrushType)),
        };
    }

    private void UpdateColorResources(Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {

        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorPrimary"] = primaryAccent;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorSecondary"] = secondaryAccent;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorTertiary"] = tertiaryAccent;

        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight3"] = AccentColors.SystemAccentColorLight3;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight2"] = AccentColors.SystemAccentColorLight2;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight1"] = AccentColors.SystemAccentColorLight1;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColor"] = AccentColors.SystemAccentColor;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorDark1"] = AccentColors.SystemAccentColorDark1;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorDark2"] = AccentColors.SystemAccentColorDark2;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorDark3"] = AccentColors.SystemAccentColorDark3;
    }

    private void UpdateFromInternalColors()
    {
        switch (ThemeService.GetCurrent().CurrentTheme)
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

    public void UpdateAccentsFromWindows()
    {
        _isInit = true;
        AccentUpdateFromWindows = true;

        AccentColors = WindowsAccentHelper.GetAccentColor();

        if (OsHelper.IsWindows10 || AccentColors.IsFallBack)
        {
            UpdateAccents(AccentColors.SystemAccentColor);
        }

        UpdateFromInternalColors();
    }


    public void UpdateAccents(Color systemAccent)
    {
        _isInit = true;
        AccentUpdateFromWindows = false;

        AccentColors = new AccentColors(
            systemAccent,
            GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Dark, AccentBrushType.Secondary),
            GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Dark, AccentBrushType.Tertiary),
            GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Dark, AccentBrushType.Quaternary),
            GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Light, AccentBrushType.Secondary),
            GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Light, AccentBrushType.Tertiary),
            GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Light, AccentBrushType.Quaternary)
            );

        UpdateFromInternalColors();
    }

    public static AccentColorService GetCurrent()
    {
        return _systemColorsHandler;
    }
}

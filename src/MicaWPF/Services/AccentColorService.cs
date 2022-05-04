using MicaWPF.Extensions;
using MicaWPFRuntimeComponent;

namespace MicaWPF.Services;

public class AccentColorService
{
    private static readonly AccentColorService _systemColorsHandler = new();
    private bool _isInit = false;
    public bool AccentUpdateFromWindows { get; private set; } = true;

    public Color SystemAccentColor { get; private set; }
    public Color SystemAccentColorLight1 { get; private set; }
    public Color SystemAccentColorLight2 { get; private set; }
    public Color SystemAccentColorLight3 { get; private set; }
    public Color SystemAccentColorDark1 { get; private set; }
    public Color SystemAccentColorDark2 { get; private set; }
    public Color SystemAccentColorDark3 { get; private set; }

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

    private void SetColorProperty(string propertyName, object value)
    {
        var type = GetType();
        var propertyInfo = type.GetProperty(propertyName);

        propertyInfo?.SetValue(this, Convert.ChangeType(value, propertyInfo.PropertyType), null);
    }

    private static Color GetOffSet(double hue, double hueCoefficient, double saturation, double value, double valueCoefficient, WindowsTheme windowsTheme)
    {
        return windowsTheme switch
        {
            WindowsTheme.Dark => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value + valueCoefficient, 1)),
            WindowsTheme.Light => HSVColorHelper.RGBFromHSV(Math.Min(hue + hueCoefficient, 360), saturation, Math.Min(value - valueCoefficient / 2, 1)),
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

    private static void UpdateColorResources(Color systemAccent, Color primaryAccent, Color secondaryAccent, Color tertiaryAccent)
    {
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColor"] = systemAccent;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight1"] = primaryAccent;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight2"] = secondaryAccent;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight3"] = tertiaryAccent;

        Application.Current.Resources["MicaWPF.Brushes.SystemAccent"] = secondaryAccent.ToBrush();
        Application.Current.Resources["MicaWPF.Brushes.SystemFillColorAttention"] = secondaryAccent.ToBrush();
        Application.Current.Resources["MicaWPF.Brushes.AccentTextFillColorPrimary"] = tertiaryAccent.ToBrush();
        Application.Current.Resources["MicaWPF.Brushes.AccentTextFillColorSecondary"] = tertiaryAccent.ToBrush();
        Application.Current.Resources["MicaWPF.Brushes.AccentTextFillColorTertiary"] = secondaryAccent.ToBrush();
        Application.Current.Resources["MicaWPF.Brushes.AccentFillColorSelectedTextBackground"] = systemAccent.ToBrush();
        Application.Current.Resources["MicaWPF.Brushes.AccentFillColorDefault"] = secondaryAccent.ToBrush();

        Application.Current.Resources["MicaWPF.Brushes.AccentFillColorSecondary"] = secondaryAccent.ToBrush(0.9);
        Application.Current.Resources["MicaWPF.Brushes.AccentFillColorTertiary"] = secondaryAccent.ToBrush(0.8);
    }

    private void UpdateFromInternalColors()
    {
        switch (ThemeService.GetCurrent().CurrentTheme)
        {
            case WindowsTheme.Dark:
                UpdateColorResources(SystemAccentColor, SystemAccentColorLight1, SystemAccentColorLight2, SystemAccentColorLight3);
                break;
            default:
                UpdateColorResources(SystemAccentColor, SystemAccentColorDark1, SystemAccentColorDark2, SystemAccentColorDark3);
                break;
        }
    }

    public void UpdateAccentsFromWindows()
    {
        _isInit = true;
        AccentUpdateFromWindows = true;

        var uwpColors = new UWPColors();
        var colorsLongString = uwpColors.GetSystemColors();

        foreach (var colors in colorsLongString)
        {
            var colorValues = colors.Split(',');
            var colorResult = Color.FromArgb(byte.Parse(colorValues[0]), byte.Parse(colorValues[1]), byte.Parse(colorValues[2]), byte.Parse(colorValues[3]));
            SetColorProperty(colorValues[4], colorResult);
        }

        UpdateFromInternalColors();
    }

    public void UpdateAccents(Color systemAccent)
    {
        _isInit = true;
        AccentUpdateFromWindows = false;
        SystemAccentColor = systemAccent;
        SystemAccentColorLight1 = GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Dark, AccentBrushType.Secondary);
        SystemAccentColorLight2 = GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Dark, AccentBrushType.Tertiary);
        SystemAccentColorLight3 = GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Dark, AccentBrushType.Quaternary);
        SystemAccentColorDark1 = GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Light, AccentBrushType.Secondary);
        SystemAccentColorDark2 = GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Light, AccentBrushType.Tertiary);
        SystemAccentColorDark3 = GetThemeColorVariation(HSVColorHelper.ConvertToHSVColor(System.Drawing.Color.FromArgb(systemAccent.R, systemAccent.G, systemAccent.B)), WindowsTheme.Light, AccentBrushType.Quaternary);

        UpdateFromInternalColors();
    }

    public static AccentColorService GetCurrent()
    {
        return _systemColorsHandler;
    }
}

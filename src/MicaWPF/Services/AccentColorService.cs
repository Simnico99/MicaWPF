using MicaWPF.Events;
#if NET5_0_OR_GREATER
using MicaWPFRuntimeComponent;
#endif
#if NETFRAMEWORK || NETCOREAPP3_1
using Windows.UI.ViewManagement;
#endif

namespace MicaWPF.Services;

public class AccentColorService : IAccentColorService
{
    public IWeakEvent<Color> AccentColorChanged { get; } = new WeakEvent<Color>();

    private static readonly AccentColorService _accentColorService = new();
    private bool _isInit = false;
    public bool AccentUpdateFromWindows { get; private set; } = true;
    public static AccentColorService Current { get => _accentColorService; }

    public Color SystemAccentColor
    { get; private set; }
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

#if NET5_0_OR_GREATER
    private void SetColorProperty(string propertyName, object value)
    {
        var type = GetType();
        var propertyInfo = type.GetProperty(propertyName);

        propertyInfo?.SetValue(this, Convert.ChangeType(value, propertyInfo.PropertyType), null);
    }
#endif

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

        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight3"] = SystemAccentColorLight3;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight2"] = SystemAccentColorLight2;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorLight1"] = SystemAccentColorLight1;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColor"] = SystemAccentColor;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorDark1"] = SystemAccentColorDark1;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorDark2"] = SystemAccentColorDark2;
        Application.Current.Resources["MicaWPF.Colors.SystemAccentColorDark3"] = SystemAccentColorDark3;

        AccentColorChanged.Publish(SystemAccentColor);
    }

    private void UpdateFromInternalColors()
    {
        switch (ThemeService.Current.CurrentTheme)
        {
            case WindowsTheme.Dark:
                UpdateColorResources(SystemAccentColorLight1, SystemAccentColorLight2, SystemAccentColorLight3);
                break;
            default:
                UpdateColorResources(SystemAccentColorDark1, SystemAccentColorDark2, SystemAccentColorDark3);
                break;
        }

        WindowHelper.RefreshAllWindowsContents();
        ThemeService.RefreshTheme();
    }

    public void UpdateAccentsFromWindows()
    {
        _isInit = true;
        AccentUpdateFromWindows = true;

#if NET5_0_OR_GREATER
        var uwpColors = new UWPColors();
        var colorsLongString = uwpColors.GetSystemColors();

        foreach (var colors in colorsLongString)
        {
            var colorValues = colors.Split(',');
            var colorResult = Color.FromArgb(byte.Parse(colorValues[0]), byte.Parse(colorValues[1]), byte.Parse(colorValues[2]), byte.Parse(colorValues[3]));
            SetColorProperty(colorValues[4], colorResult);
        }

        if (OsHelper.IsWindows10)
        {
            UpdateAccents(SystemAccentColor);
        }

        UpdateFromInternalColors();
    }
#endif
#if NETFRAMEWORK || NETCOREAPP3_1

        SystemAccentColor = UIColorConverter(UIColorType.Accent);
        SystemAccentColorDark1 = UIColorConverter(UIColorType.AccentDark1);
        SystemAccentColorDark2 = UIColorConverter(UIColorType.AccentDark2);
        SystemAccentColorDark3 = UIColorConverter(UIColorType.AccentDark3);

        SystemAccentColor = UIColorConverter(UIColorType.Accent);
        SystemAccentColorLight1 = UIColorConverter(UIColorType.AccentLight1);
        SystemAccentColorLight2 = UIColorConverter(UIColorType.AccentLight2);
        SystemAccentColorLight3 = UIColorConverter(UIColorType.AccentLight3);

        UpdateFromInternalColors();
    }

    private Color UIColorConverter(UIColorType colorType)
    {
        var uiSettings = new UISettings();
        var color = uiSettings.GetColorValue(colorType);
        return Color.FromArgb(color.A, color.R, color.G, color.B);
    }
#endif

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
}

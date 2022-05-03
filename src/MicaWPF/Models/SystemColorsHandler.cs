using MicaWPF.Controls;
using MicaWPFRuntimeComponent;

namespace MicaWPF.Models;

public class SystemColorsHandler
{
    public Color SystemAccentColor { get; private set; }
    public Color SystemAccentColorLight1 { get; private set; }
    public Color SystemAccentColorLight2 { get; private set; }
    public Color SystemAccentColorLight3 { get; private set; }
    public Color SystemAccentColorDark1 { get; private set; }
    public Color SystemAccentColorDark2 { get; private set; }
    public Color SystemAccentColorDark3 { get; private set; }

    public SystemColorsHandler()
    {
        var uwpColors = new UWPColors();
        var colorsLongString = uwpColors.GetSystemColors();

        foreach (var colors in colorsLongString)
        {
            var colorValues = colors.Split(',');
            var colorResult = Color.FromArgb(byte.Parse(colorValues[0]), byte.Parse(colorValues[1]), byte.Parse(colorValues[2]), byte.Parse(colorValues[3]));
            SetValue(colorValues[4], colorResult);
        }
    }

    public void UpdateAccent(MicaWindow micaWindow, WindowsTheme theme = WindowsTheme.Light)
    {
        switch (theme)
        {
            case WindowsTheme.Dark:
                AccentHelper.UpdateColorResources(SystemAccentColor, SystemAccentColorLight1, SystemAccentColorLight2, SystemAccentColorLight3, micaWindow);
                break;
            default:
                AccentHelper.UpdateColorResources(SystemAccentColor, SystemAccentColorDark1, SystemAccentColorDark2, SystemAccentColorDark3, micaWindow);
                break;
        }
    }

    private void SetValue(string propertyName, object value)
    {
        var type = GetType();
        var propertyInfo = type.GetProperty(propertyName);

        propertyInfo?.SetValue(this, Convert.ChangeType(value, propertyInfo.PropertyType), null);
    }
}

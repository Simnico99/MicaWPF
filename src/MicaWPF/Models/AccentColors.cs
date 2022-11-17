namespace MicaWPF.Models;
public readonly struct AccentColors
{
    public Color SystemAccentColor { get; }
    public Color SystemAccentColorLight1 { get; }
    public Color SystemAccentColorLight2 { get; }
    public Color SystemAccentColorLight3 { get; }
    public Color SystemAccentColorDark1 { get; }
    public Color SystemAccentColorDark2 { get; }
    public Color SystemAccentColorDark3 { get; }
    internal bool IsFallBack { get; }

    public AccentColors(Color systemAccentColor, Color systemAccentColorLight1, Color systemAccentColorLight2, Color systemAccentColorLight3, Color systemAccentColorDark1, Color systemAccentColorDark2, Color systemAccentColorDark3, bool isFallBack = false)
    {
        SystemAccentColor = systemAccentColor;
        SystemAccentColorLight1 = systemAccentColorLight1;
        SystemAccentColorLight2 = systemAccentColorLight2;
        SystemAccentColorLight3 = systemAccentColorLight3;
        SystemAccentColorDark1 = systemAccentColorDark1;
        SystemAccentColorDark2 = systemAccentColorDark2;
        SystemAccentColorDark3 = systemAccentColorDark3;
        IsFallBack = isFallBack;
    }
}

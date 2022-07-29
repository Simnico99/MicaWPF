using MicaWPF.Symbols;

namespace MicaWPF.Models;
public static class Glyph
{
    public const FluentSystemIcons.Regular DefaultIcon = FluentSystemIcons.Regular.Heart28;
    public const FluentSystemIcons.Filled DefaultFilledIcon = FluentSystemIcons.Filled.Heart28;

    public static FluentSystemIcons.Regular Parse(string name)
    {
        return string.IsNullOrEmpty(name) ? DefaultIcon : (FluentSystemIcons.Regular)Enum.Parse(typeof(FluentSystemIcons.Regular), name);
    }

    public static FluentSystemIcons.Filled ParseFilled(string name)
    {
        return string.IsNullOrEmpty(name) ? DefaultFilledIcon : (FluentSystemIcons.Filled)Enum.Parse(typeof(FluentSystemIcons.Filled), name);
    }
}

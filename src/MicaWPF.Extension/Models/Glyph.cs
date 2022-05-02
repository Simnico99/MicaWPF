using MicaWPF.Extension.Symbols;

namespace MicaWPF.Extension.Models;
public static class Glyph
{
    public const FluentSystemIcons.Regular DefaultIcon = FluentSystemIcons.Regular.Heart28;

    public const FluentSystemIcons.Filled DefaultFilledIcon = FluentSystemIcons.Filled.Heart28;

    public static FluentSystemIcons.Regular Parse(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return DefaultIcon;
        }

        return (FluentSystemIcons.Regular)Enum.Parse(typeof(FluentSystemIcons.Regular), name);
    }

    public static FluentSystemIcons.Filled ParseFilled(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return DefaultFilledIcon;
        }

        return (FluentSystemIcons.Filled)Enum.Parse(typeof(FluentSystemIcons.Filled), name);
    }
}

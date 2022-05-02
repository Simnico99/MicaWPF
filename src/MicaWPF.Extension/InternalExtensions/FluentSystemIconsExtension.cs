using MicaWPF.Extension.Models;
using MicaWPF.Extension.Symbols;

namespace MicaWPF.Extension.InternalExtensions;
internal static class FluentSystemIconsExtension
{
    public static FluentSystemIcons.Filled Swap(this FluentSystemIcons.Regular icon)
    {
        // TODO: It is possible that the alternative icon does not exist
        return Glyph.ParseFilled(icon.ToString());
    }

    public static FluentSystemIcons.Regular Swap(this FluentSystemIcons.Filled icon)
    {
        // TODO: It is possible that the alternative icon does not exist
        return Glyph.Parse(icon.ToString());
    }

    public static char GetGlyph(this FluentSystemIcons.Regular icon)
    {
        return ToChar(icon);
    }

    public static char GetGlyph(this FluentSystemIcons.Filled icon)
    {
        return ToChar(icon);
    }

    public static string GetString(this FluentSystemIcons.Regular icon)
    {
        return icon.GetGlyph().ToString();
    }

    public static string GetString(this FluentSystemIcons.Filled icon)
    {
        return icon.GetGlyph().ToString();
    }

    private static char ToChar(FluentSystemIcons.Regular icon)
    {
        return Convert.ToChar(icon);
    }

    private static char ToChar(FluentSystemIcons.Filled icon)
    {
        return Convert.ToChar(icon);
    }
}

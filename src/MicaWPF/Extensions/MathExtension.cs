namespace MicaWPF.Extensions;
public static class MathExtension
{
    public static T Clamp<T>(this T val, T min, T max) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        return val.CompareTo(min) < 0 ? min : val.CompareTo(max) > 0 ? max : val;
    }
}

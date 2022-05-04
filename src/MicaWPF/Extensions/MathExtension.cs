namespace MicaWPF.Extensions;
public static class MathExtension
{
    public static T Clamp<T>(this T val, T min, T max) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
    {
        if (val.CompareTo(min) < 0)
        {
            return min;
        }
        else if (val.CompareTo(max) > 0)
        {
            return max;
        }
        else
        {
            return val;
        }
    }
}

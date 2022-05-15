namespace MicaWPF.Helpers;
internal static class MathHelper
{
    internal const double DBL_EPSILON = 2.2204460492503131e-016;

    public static bool AreClose(double value1, double value2)
    {
        return value1 == value2 || IsVerySmall(value1 - value2);
    }

    public static bool IsVerySmall(double value)
    {
        return Math.Abs(value) < 1E-06;
    }

    public static bool IsZero(double value)
    {
        return Math.Abs(value) < 10.0 * DBL_EPSILON;
    }


    /* Unmerged change from project 'MicaWPF (net6.0-windows10.0.19041.0)'
    Before:
        public static bool IsFiniteDouble(double x) => !double.IsInfinity(x) && !double.IsNaN(x);
    After:
        public static bool IsFiniteDouble(double x)
        {
            return !double.IsInfinity(x) && !double.IsNaN(x);
    */

    /* Unmerged change from project 'MicaWPF (net5.0-windows10.0.19041.0)'
    Before:
        public static bool IsFiniteDouble(double x) => !double.IsInfinity(x) && !double.IsNaN(x);
    After:
        public static bool IsFiniteDouble(double x)
        {
            return !double.IsInfinity(x) && !double.IsNaN(x);
    */

    /* Unmerged change from project 'MicaWPF (netcoreapp3.1)'
    Before:
        public static bool IsFiniteDouble(double x) => !double.IsInfinity(x) && !double.IsNaN(x);
    After:
        public static bool IsFiniteDouble(double x)
        {
            return !double.IsInfinity(x) && !double.IsNaN(x);
    */

    /* Unmerged change from project 'MicaWPF (net48)'
    Before:
        public static bool IsFiniteDouble(double x) => !double.IsInfinity(x) && !double.IsNaN(x);
    After:
        public static bool IsFiniteDouble(double x)
        {
            return !double.IsInfinity(x) && !double.IsNaN(x);
    */
    public static bool IsFiniteDouble(double x)
    {
        return !double.IsInfinity(x) && !double.IsNaN(x);
    }


    /* Unmerged change from project 'MicaWPF (net6.0-windows10.0.19041.0)'
    Before:
        public static double DoubleFromMantissaAndExponent(double x, int exp) => x * Math.Pow(2.0, exp);
    After:
        public static double DoubleFromMantissaAndExponent(double x, int exp)
        {
            return x * Math.Pow(2.0, exp);
    */

    /* Unmerged change from project 'MicaWPF (net5.0-windows10.0.19041.0)'
    Before:
        public static double DoubleFromMantissaAndExponent(double x, int exp) => x * Math.Pow(2.0, exp);
    After:
        public static double DoubleFromMantissaAndExponent(double x, int exp)
        {
            return x * Math.Pow(2.0, exp);
    */

    /* Unmerged change from project 'MicaWPF (netcoreapp3.1)'
    Before:
        public static double DoubleFromMantissaAndExponent(double x, int exp) => x * Math.Pow(2.0, exp);
    After:
        public static double DoubleFromMantissaAndExponent(double x, int exp)
        {
            return x * Math.Pow(2.0, exp);
    */

    /* Unmerged change from project 'MicaWPF (net48)'
    Before:
        public static double DoubleFromMantissaAndExponent(double x, int exp) => x * Math.Pow(2.0, exp);
    After:
        public static double DoubleFromMantissaAndExponent(double x, int exp)
        {
            return x * Math.Pow(2.0, exp);
    */
    public static double DoubleFromMantissaAndExponent(double x, int exp)
    {
        return x * Math.Pow(2.0, exp);
    }

    public static bool GreaterThan(double value1, double value2)
    {
        return value1 > value2 && !AreClose(value1, value2);
    }

    public static bool GreaterThanOrClose(double value1, double value2)
    {
        if (value1 <= value2)
        {
            return AreClose(value1, value2);
        }
        return true;
    }

    public static bool LessThan(double value1, double value2)
    {
        return value1 < value2 && !AreClose(value1, value2);
    }

    public static bool LessThanOrClose(double value1, double value2)
    {
        if (value1 >= value2)
        {
            return AreClose(value1, value2);
        }
        return true;
    }

    public static double EnsureRange(double value, double? min, double? max)
    {
        if (min.HasValue && value < min.Value)
        {
            return min.Value;
        }
        if (max.HasValue && value > max.Value)
        {
            return max.Value;
        }
        return value;
    }

    public static double SafeDivide(double lhs, double rhs, double fallback)
    {
        if (!IsVerySmall(rhs))
        {
            return lhs / rhs;
        }
        return fallback;
    }
}

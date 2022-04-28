namespace MicaWPF.Helpers;

public class HSLColor
{
    public float Hue;
    public float Saturation;
    public float Luminosity;

    public HSLColor(float H, float S, float L)
    {
        Hue = H;
        Saturation = S;
        Luminosity = L;
    }

    public static HSLColor FromRGB(Color Clr)
    {
        return FromRGB(Clr.R, Clr.G, Clr.B);
    }

    public static HSLColor FromRGB(byte R, byte G, byte B)
    {
        var _R = (R / 255f);
        var _G = (G / 255f);
        var _B = (B / 255f);

        var _Min = Math.Min(Math.Min(_R, _G), _B);
        var _Max = Math.Max(Math.Max(_R, _G), _B);
        var _Delta = _Max - _Min;

        float H = 0;
        float S = 0;
        var L = (float)((_Max + _Min) / 2.0f);

        if (_Delta != 0)
        {
            if (L < 0.5f)
            {
                S = (float)(_Delta / (_Max + _Min));
            }
            else
            {
                S = (float)(_Delta / (2.0f - _Max - _Min));
            }


            if (_R == _Max)
            {
                H = (_G - _B) / _Delta;
            }
            else if (_G == _Max)
            {
                H = 2f + (_B - _R) / _Delta;
            }
            else if (_B == _Max)
            {
                H = 4f + (_R - _G) / _Delta;
            }
        }

        return new HSLColor(H, S, L);
    }

    private float Hue_2_RGB(float v1, float v2, float vH)
    {
        if (vH < 0)
        {
            vH += 1;
        }

        if (vH > 1)
        {
            vH -= 1;
        }

        if ((6 * vH) < 1)
        {
            return (v1 + (v2 - v1) * 6 * vH);
        }

        if ((2 * vH) < 1)
        {
            return (v2);
        }

        if ((3 * vH) < 2)
        {
            return (v1 + (v2 - v1) * ((2 / 3) - vH) * 6);
        }

        return (v1);
    }

    public Color ToRGB()
    {
        byte r, g, b;
        if (Saturation == 0)
        {
            r = (byte)Math.Round(Luminosity * 255d);
            g = (byte)Math.Round(Luminosity * 255d);
            b = (byte)Math.Round(Luminosity * 255d);
        }
        else
        {
            double t1, t2;
            var th = Hue / 6.0d;

            if (Luminosity < 0.5d)
            {
                t2 = Luminosity * (1d + Saturation);
            }
            else
            {
                t2 = (Luminosity + Saturation) - (Luminosity * Saturation);
            }
            t1 = 2d * Luminosity - t2;

            double tr, tg, tb;
            tr = th + (1.0d / 3.0d);
            tg = th;
            tb = th - (1.0d / 3.0d);

            tr = ColorCalc(tr, t1, t2);
            tg = ColorCalc(tg, t1, t2);
            tb = ColorCalc(tb, t1, t2);
            r = (byte)Math.Round(tr * 255d);
            g = (byte)Math.Round(tg * 255d);
            b = (byte)Math.Round(tb * 255d);
        }
        return Color.FromRgb(r, g, b);
    }
    private static double ColorCalc(double c, double t1, double t2)
    {

        if (c < 0)
        {
            c += 1d;
        }

        if (c > 1)
        {
            c -= 1d;
        }

        if (6.0d * c < 1.0d)
        {
            return t1 + (t2 - t1) * 6.0d * c;
        }

        if (2.0d * c < 1.0d)
        {
            return t2;
        }

        if (3.0d * c < 2.0d)
        {
            return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
        }

        return t1;
    }
}

using UnityEngine;

public enum EaseType
{
    Linear, Clerp, Spring,
    QuadIn, QuadOut, QuadInOut,
    CubicIn, CubicOut, CubicInOut,
    QuartIn, QuartOut, QuartInOut,
    QuintIn, QuintOut, QuintInOut,
    SineIn, SineOut, SineInOut,
    ExpoIn, ExpoOut, ExpoInOut,
    CircIn, CircOut, CircInOut,
    BounceIn, BounceOut, BounceInOut,
    ElasticIn, ElasticOut, ElasticInOut,
    Custom
}

public static class Easing
{
    public static float Ease(this EaseType type, float from, float to, float t)
    {
        return Ease(from, to, t, type);
    }

    public static float Ease(this EaseType type, float t)
    {
        return Ease(0, 1, t, type);
    }

    private static float Ease(float start, float end, float t, EaseType type)
    {
        switch (type)
        {
            default:
            case EaseType.Linear: return Linear(start, end, t);
            case EaseType.Clerp: return Clerp(start, end, t);
            case EaseType.Spring: return Spring(start, end, t);
            case EaseType.QuadIn: return QuadIn(start, end, t);
            case EaseType.QuadOut: return QuadOut(start, end, t);
            case EaseType.QuadInOut: return QuadInOut(start, end, t);
            case EaseType.CubicIn: return CubicIn(start, end, t);
            case EaseType.CubicOut: return CubicOut(start, end, t);
            case EaseType.CubicInOut: return CubicInOut(start, end, t);
            case EaseType.QuartIn: return QuartIn(start, end, t);
            case EaseType.QuartOut: return QuartOut(start, end, t);
            case EaseType.QuartInOut: return QuartInOut(start, end, t);
            case EaseType.QuintIn: return QuintIn(start, end, t);
            case EaseType.QuintOut: return QuintOut(start, end, t);
            case EaseType.QuintInOut: return QuintInOut(start, end, t);
            case EaseType.SineIn: return SineIn(start, end, t);
            case EaseType.SineOut: return SineOut(start, end, t);
            case EaseType.SineInOut: return SineInOut(start, end, t);
            case EaseType.ExpoIn: return ExpoIn(start, end, t);
            case EaseType.ExpoOut: return ExpoOut(start, end, t);
            case EaseType.ExpoInOut: return ExpoInOut(start, end, t);
            case EaseType.CircIn: return CircIn(start, end, t);
            case EaseType.CircOut: return CircOut(start, end, t);
            case EaseType.CircInOut: return CircInOut(start, end, t);
            case EaseType.BounceIn: return BounceIn(start, end, t);
            case EaseType.BounceOut: return BounceOut(start, end, t);
            case EaseType.BounceInOut: return BounceInOut(start, end, t);
            case EaseType.ElasticIn: return ElasticIn(start, end, t);
            case EaseType.ElasticOut: return ElasticOut(start, end, t);
            case EaseType.ElasticInOut: return ElasticInOut(start, end, t);
        }
    }

    private static float Linear(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value);
    }

    private static float Clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) * 0.5f);
        float retval = 0.0f;
        float diff = 0.0f;
        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else
        {
            retval = start + (end - start) * value;
        }
        return retval;
    }

    private static float Spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    private static float QuadIn(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    private static float QuadOut(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    private static float QuadInOut(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
        {
            return end * 0.5f * value * value + start;
        }
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }

    private static float CubicIn(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value + start;
    }

    private static float CubicOut(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value + 1) + start;
    }

    private static float CubicInOut(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
        {
            return end * 0.5f * value * value * value + start;
        }
        value -= 2;
        return end * 0.5f * (value * value * value + 2) + start;
    }

    private static float QuartIn(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value + start;
    }

    private static float QuartOut(float start, float end, float value)
    {
        value--;
        end -= start;
        return -end * (value * value * value * value - 1) + start;
    }

    private static float QuartInOut(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
        {
            return end * 0.5f * value * value * value * value + start;
        }
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }

    private static float QuintIn(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    private static float QuintOut(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1) + start;
    }

    private static float QuintInOut(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
        {
            return end * 0.5f * value * value * value * value * value + start;
        }
        value -= 2;
        return end * 0.5f * (value * value * value * value * value + 2) + start;
    }

    private static float SineIn(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + end + start;
    }

    private static float SineOut(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + start;
    }

    private static float SineInOut(float start, float end, float value)
    {
        end -= start;
        return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + start;
    }

    private static float ExpoIn(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Pow(2, 10 * (value - 1)) + start;
    }

    private static float ExpoOut(float start, float end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
    }

    private static float ExpoInOut(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
        {
            return end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + start;
        }
        value--;
        return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }

    private static float CircIn(float start, float end, float value)
    {
        end -= start;
        return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
    }

    private static float CircOut(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * Mathf.Sqrt(1 - value * value) + start;
    }

    private static float CircInOut(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
        {
            return -end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + start;
        }
        value -= 2;
        return end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + start;
    }

    private static float BounceIn(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - BounceOut(0, end, d - value) + start;
    }

    private static float BounceOut(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }

    private static float BounceInOut(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        if (value < d * 0.5f)
        {
            return BounceIn(0, end, value * 2) * 0.5f + start;
        }
        else
        {
            return BounceOut(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
        }
    }

    private static float BackIn(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        float s = 1.70158f;
        return end * (value) * value * ((s + 1) * value - s) + start;
    }

    private static float BackOut(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value = (value) - 1;
        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }

    private static float BackInOut(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value /= .5f;
        if ((value) < 1)
        {
            s *= (1.525f);
            return end * 0.5f * (value * value * (((s) + 1) * value - s)) + start;
        }
        value -= 2;
        s *= (1.525f);
        return end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + start;
    }

    private static float ElasticIn(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0)
        {
            return start;
        }

        if ((value /= d) == 1)
        {
            return start + end;
        }

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
    }

    private static float ElasticOut(float start, float end, float value)
    {
        /* GFX47 MOD END */
        //Thank you to rafael.marteleto for fixing this as a port over from Pedro's UnityTween
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0)
        {
            return start;
        }

        if ((value /= d) == 1)
        {
            return start + end;
        }

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
    }

    private static float ElasticInOut(float start, float end, float value)
    {
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0)
        {
            return start;
        }

        if ((value /= d * 0.5f) == 2)
        {
            return start + end;
        }

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if (value < 1)
        {
            return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        }
        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
    }

    private static float Punch(float amplitude, float value)
    {
        float s = 9;
        if (value == 0)
        {
            return 0;
        }
        else if (value == 1)
        {
            return 0;
        }
        float period = 1 * 0.3f;
        s = period / (2 * Mathf.PI) * Mathf.Asin(0);
        return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
    }
}
// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

/**
 * Utility methods and constants for single-precision floating point math. Extends {@link MathUtil}
 * with shim methods that call through to {@link Math} and convert the results to float.
 */
public class FloatMath : MathUtil
{
    /** The ratio of a circle's circumference to its diameter. */
    public const float PI = (float)Math.PI;

    /** The base value of the natural logarithm. */
    public const float E = (float)Math.E;

    /**
     * Computes and returns the sine of the given angle.
     *
     * @see Math#sin
     */
    public static float Sin (float a)
    {
        return (float)Math.Sin(a);
    }

    /**
     * Computes and returns the cosine of the given angle.
     *
     * @see Math#cos
     */
    public static float Cos (float a)
    {
        return (float)Math.Cos(a);
    }

    /**
     * Computes and returns the tangent of the given angle.
     *
     * @see Math#tan
     */
    public static float Tan (float a)
    {
        return (float)Math.Tan(a);
    }

    /**
     * Computes and returns the arc sine of the given value.
     *
     * @see Math#asin
     */
    public static float Asin (float a)
    {
        return (float)Math.Asin(a);
    }

    /**
     * Computes and returns the arc cosine of the given value.
     *
     * @see Math#acos
     */
    public static float Acos (float a)
    {
        return (float)Math.Acos(a);
    }

    /**
     * Computes and returns the arc tangent of the given value.
     *
     * @see Math#atan
     */
    public static float Atan (float a)
    {
        return (float)Math.Atan(a);
    }

    /**
     * Computes and returns the arc tangent of the given values.
     *
     * @see Math#atan2
     */
    public static float Atan2 (float y, float x)
    {
        return (float)Math.Atan2(y, x);
    }

    /**
     * Converts from radians to degrees.
     *
     * @see Math#toDegrees
     */
    public static float ToDegrees (float a)
    {
        return a * (180f / PI);
    }

    /**
     * Converts from degrees to radians.
     *
     * @see Math#toRadians
     */
    public static float ToRadians (float a)
    {
        return a * (PI / 180f);
    }

    /**
     * Returns the square root of the supplied value.
     *
     * @see Math#sqrt
     */
    public static float Sqrt (float v)
    {
        return (float)Math.Sqrt(v);
    }

    /**
     * Returns e to the power of the supplied value.
     *
     * @see Math#exp
     */
    public static float Exp (float v)
    {
        return (float)Math.Exp(v);
    }

    /**
     * Returns the natural logarithm of the supplied value.
     *
     * @see Math#log
     */
    public static float Log (float v)
    {
        return (float)Math.Log(v);
    }

    /**
     * Returns the base 10 logarithm of the supplied value.
     *
     * @see Math#log10
     */
    public static float Log10 (float v)
    {
        return (float)Math.Log10(v);
    }

    /**
     * Returns v to the power of e.
     *
     * @see Math#pow
     */
    public static float Pow (float v, float e)
    {
        return (float)Math.Pow(v, e);
    }

    /**
     * Returns the floor of v.
     *
     * @see Math#floor
     */
    public static float Floor (float v)
    {
        return (float)Math.Floor(v);
    }

    /**
     * Returns the ceiling of v.
     *
     * @see Math#ceil
     */
    public static float Ceiling (float v)
    {
        return (float)Math.Ceiling(v);
    }
}
}


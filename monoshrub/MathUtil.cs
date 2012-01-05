// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

/**
 * Math utility methods.
 */
public class MathUtil
{
    /** A small number. */
    public const float EPSILON = 0.00001f;

    /** The circle constant, Tau (&#964;) http://tauday.com/ */
    public const float TAU = (float)(Math.PI * 2);

    /** Twice Pi. */
    public const float TWO_PI = TAU;

    /** Pi times one half. */
    public const float HALF_PI = (float)(Math.PI * 0.5);

    /**
     * A cheaper version of {@link Math#round} that doesn't handle the special cases.
     */
    public static int Round (float v) {
        return (v < 0f) ? (int)(v - 0.5f) : (int)(v + 0.5f);
    }

    /**
     * Returns the floor of v as an integer without calling the relatively expensive
     * {@link Math#floor}.
     */
    public static int IFloor (float v) {
        int iv = (int)v;
        return (v < 0f) ? ((iv == v || iv == int.MinValue) ? iv : (iv - 1)) : iv;
    }

    /**
     * Returns the ceiling of v as an integer without calling the relatively expensive
     * {@link Math#ceil}.
     */
    public static int ICeil (float v) {
        int iv = (int)v;
        return (v > 0f) ? ((iv == v || iv == int.MaxValue) ? iv : (iv + 1)) : iv;
    }

    /**
     * Clamps a value to the range [lower, upper].
     */
    public static float Clamp (float v, float lower, float upper) {
        return Math.Min(Math.Max(v, lower), upper);
    }

    /**
     * Rounds a value to the nearest multiple of a target.
     */
    public static float RoundNearest (float v, float target) {
        target = Math.Abs(target);
        if (v >= 0) {
            return target * FloatMath.Floor((v + 0.5f * target) / target);
        } else {
            return target * FloatMath.Ceiling((v - 0.5f * target) / target);
        }
    }

    /**
     * Checks whether the value supplied is in [lower, upper].
     */
    public static bool IsWithin (float v, float lower, float upper) {
        return v >= lower && v <= upper;
    }

    /**
     * Returns a random value according to the normal distribution with the provided mean and
     * standard deviation.
     *
     * @param normal a normally distributed random value.
     * @param mean the desired mean.
     * @param stddev the desired standard deviation.
     */
    public static float Normal (float normal, float mean, float stddev) {
        return stddev*normal + mean;
    }

    /**
     * Returns a random value according to the exponential distribution with the provided mean.
     *
     * @param random a uniformly distributed random value.
     * @param mean the desired mean.
     */
    public static float Exponential (float random, float mean) {
        return -FloatMath.Log(1f - random) * mean;
    }

    /**
     * Linearly interpolates between two angles, taking the shortest path around the circle.
     * This assumes that both angles are in [-pi, +pi].
     */
    public static float Lerpa (float a1, float a2, float t) {
        float ma1 = MirrorAngle(a1), ma2 = MirrorAngle(a2);
        float d = Math.Abs(a2 - a1), md = Math.Abs(ma1 - ma2);
        return (d < md) ? Lerp(a1, a2, t) : MirrorAngle(Lerp(ma1, ma2, t));
    }

    /**
     * Linearly interpolates between v1 and v2 by the parameter t.
     */
    public static float Lerp (float v1, float v2, float t) {
        return v1 + t*(v2 - v1);
    }

    /**
     * Determines whether two values are "close enough" to equal.
     */
    public static bool EpsilonEquals (float v1, float v2) {
        return Math.Abs(v1 - v2) < EPSILON;
    }

    /**
     * Returns the (shortest) distance between two angles, assuming that both angles are in
     * [-pi, +pi].
     */
    public static float AngularDistance (float a1, float a2) {
        float ma1 = MirrorAngle(a1), ma2 = MirrorAngle(a2);
        return Math.Min(Math.Abs(a1 - a2), Math.Abs(ma1 - ma2));
    }

    /**
     * Returns the (shortest) difference between two angles, assuming that both angles are in
     * [-pi, +pi].
     */
    public static float AngularDifference (float a1, float a2) {
        float ma1 = MirrorAngle(a1), ma2 = MirrorAngle(a2);
        float diff = a1 - a2, mdiff = ma2 - ma1;
        return (Math.Abs(diff) < Math.Abs(mdiff)) ? diff : mdiff;
    }

    /**
     * Returns an angle in the range [-pi, pi).
     */
    public static float NormalizeAngle (float a) {
        while (a < -FloatMath.PI) {
            a += TWO_PI;
        }
        while (a >= FloatMath.PI) {
            a -= TWO_PI;
        }
        return a;
    }

    /**
     * Returns an angle in the range [0, 2pi).
     */
    public static float NormalizeAnglePositive (float a) {
        while (a < 0f) {
            a += TWO_PI;
        }
        while (a >= TWO_PI) {
            a -= TWO_PI;
        }
        return a;
    }

    /**
     * Returns the mirror angle of the specified angle (assumed to be in [-pi, +pi]).
     */
    public static float MirrorAngle (float a) {
        return (a > 0f ? (float) Math.PI : (float) -Math.PI) - a;
    }
}
}


// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

/**
 * Vector-related utility methods.
 */
public class Vectors
{
    /** A unit vector in the X+ direction. */
    public static Vector UNIT_X = new Vector(1f, 0f);

    /** A unit vector in the Y+ direction. */
    public static Vector UNIT_Y = new Vector(0f, 1f);

    /** The zero vector. */
    public static Vector ZERO = new Vector(0f, 0f);

    /** A vector containing the minimum floating point value for all components */
    public static Vector MIN_VALUE = new Vector(float.MinValue, float.MinValue);

    /** A vector containing the maximum floating point value for all components. */
    public static Vector MAX_VALUE = new Vector(float.MaxValue, float.MaxValue);

    /**
     * Creates a new vector from polar coordinates.
     */
    public static Vector FromPolar (float magnitude, float angle) {
        return new Vector(magnitude * (float) Math.Cos(angle), magnitude * (float) Math.Sin(angle));
    }

    /**
     * Returns the magnitude of the specified vector.
     */
    public static float Length (float x, float y) {
        return FloatMath.Sqrt(LengthSq(x, y));
    }

    /**
     * Returns the square of the magnitude of the specified vector.
     */
    public static float LengthSq (float x, float y) {
        return (x*x + y*y);
    }

    /**
     * Returns true if the supplied vector has zero magnitude.
     */
    public static bool IsZero (float x, float y) {
        return x == 0 && y == 0;
    }

    /**
     * Returns true if the supplied vector's x and y components are {@link MathUtil#EPSILON} close
     * to zero magnitude.
     */
    public static bool IsEpsilonZero (float x, float y) {
        return IsEpsilonZero(x, y, MathUtil.EPSILON);
    }

    /**
     * Returns true if the supplied vector's x and y components are {@code epsilon} close to zero
     * magnitude.
     */
    public static bool IsEpsilonZero (float x, float y, float epsilon) {
        return Math.Abs(x) <= epsilon && Math.Abs(y) <= epsilon;
    }

    /**
     * Returns true if the supplied vectors' x and y components are equal to one another within
     * {@link MathUtil#EPSILON}.
     */
    public static bool EpsilonEquals (Vector v1, Vector v2) {
        return EpsilonEquals(v1, v2, MathUtil.EPSILON);
    }

    /**
     * Returns true if the supplied vectors' x and y components are equal to one another within
     * {@code epsilon}.
     */
    public static bool EpsilonEquals (Vector v1, Vector v2, float epsilon) {
        return Math.Abs(v1.x - v2.x) <= epsilon && Math.Abs(v1.y - v2.y) <= epsilon;
    }

    /**
     * Transforms a vector as specified.
     */
    public static Vector Transform (float x, float y, float sx, float sy, float rotation) {
        return Transform(x, y, sx, sy, FloatMath.Sin(rotation), FloatMath.Cos(rotation));
    }

    /**
     * Transforms a vector as specified, storing the result in the vector provided.
     * @return a reference to the result vector, for chaining.
     */
    public static Vector Transform (float x, float y, float sx, float sy, float sina, float cosa) {
        return new Vector((x*cosa - y*sina) * sx, (x*sina + y*cosa) * sy);
    }

    /**
     * Inverse transforms a point as specified, storing the result in the point provided.
     * @return a reference to the result vector, for chaining.
     */
    public static Vector InverseTransform (float x, float y, float sx, float sy, float rotation) {
        float sinnega = FloatMath.Sin(-rotation), cosnega = FloatMath.Cos(-rotation);
        float nx = (x * cosnega - y * sinnega); // unrotate
        float ny = (x * sinnega + y * cosnega);
        return new Vector(nx / sx, ny / sy); // unscale
    }
}
}


// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

/**
 * Point-related utility methods.
 */
public class Points
{
    /** The point at the origin. */
    public static Point ZERO = new Point(0f, 0f);

    /**
     * Returns the squared Euclidean distance between the specified two points.
     */
    public static float DistanceSq (float x1, float y1, float x2, float y2) {
        x2 -= x1;
        y2 -= y1;
        return x2 * x2 + y2 * y2;
    }

    /**
     * Returns the Euclidean distance between the specified two points.
     */
    public static float Distance (float x1, float y1, float x2, float y2) {
        return FloatMath.Sqrt(DistanceSq(x1, y1, x2, y2));
    }

    /**
     * Returns true if the supplied points' x and y components are equal to one another within
     * {@link MathUtil#EPSILON}.
     */
    public static bool EpsilonEquals (Point p1, Point p2) {
        return EpsilonEquals(p1, p2, FloatMath.EPSILON);
    }

    /**
     * Returns true if the supplied points' x and y components are equal to one another within
     * {@code epsilon}.
     */
    public static bool EpsilonEquals (Point p1, Point p2, float epsilon) {
        return Math.Abs(p1.x - p2.x) < epsilon && Math.Abs(p1.y - p2.y) < epsilon;
    }

    /** Transforms a point as specified, storing the result in the point provided.
     * @return a reference to the result point, for chaining. */
    public static Point Transform (float x, float y, float sx, float sy, float rotation,
                                   float tx, float ty) {
        return Transform(x, y, sx, sy, FloatMath.Sin(rotation), FloatMath.Cos(rotation), tx, ty);
    }

    /** Transforms a point as specified, storing the result in the point provided.
     * @return a reference to the result point, for chaining. */
    public static Point Transform (float x, float y, float sx, float sy, float sina, float cosa,
                                   float tx, float ty) {
        return new Point((x*cosa - y*sina) * sx + tx, (x*sina + y*cosa) * sy + ty);
    }

    /** Inverse transforms a point as specified, storing the result in the point provided.
     * @return a reference to the result point, for chaining. */
    public static Point InverseTransform (float x, float y, float sx, float sy, float rotation,
                                          float tx, float ty) {
        x -= tx; y -= ty; // untranslate
        float sinnega = FloatMath.Sin(-rotation), cosnega = FloatMath.Cos(-rotation);
        float nx = (x * cosnega - y * sinnega); // unrotate
        float ny = (x * sinnega + y * cosnega);
        return new Point(nx / sx, ny / sy); // unscale
    }

    /**
     * Returns a string describing the supplied point, of the form <code>+x+y</code>,
     * <code>+x-y</code>, <code>-x-y</code>, etc.
     */
    public static String PointToString (float x, float y) {
        return MathUtil.ToString(x) + MathUtil.ToString(y);
    }
}
}


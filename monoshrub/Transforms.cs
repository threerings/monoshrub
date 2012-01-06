// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

/**
 * {@link Transform} related utility methods.
 */
public class Transforms
{
    /**
     * Multiplies the supplied two affine transforms, storing the result in {@code into}. {@code
     * into} may refer to the same instance as {@code a} or {@code b}.
     * @return {@code into} for chaining.
     */
    public static AffineTransform Multiply (
        AffineTransform a, AffineTransform b, AffineTransform into) {
        return Multiply(a.m00, a.m01, a.m10, a.m11, a.Tx, a.Ty,
                        b.m00, b.m01, b.m10, b.m11, b.Tx, b.Ty, into);
    }

    /**
     * Multiplies the supplied two affine transforms, storing the result in {@code into}. {@code
     * into} may refer to the same instance as {@code a}.
     * @return {@code into} for chaining.
     */
    public static AffineTransform Multiply (
        AffineTransform a, float m00, float m01, float m10, float m11, float tx, float ty,
        AffineTransform into) {
        return Multiply(a.m00, a.m01, a.m10, a.m11, a.Tx, a.Ty,
                        m00, m01, m10, m11, tx, ty, into);
    }

    /**
     * Multiplies the supplied two affine transforms, storing the result in {@code into}. {@code
     * into} may refer to the same instance as {@code b}.
     * @return {@code into} for chaining.
     */
    public static AffineTransform Multiply (
        float m00, float m01, float m10, float m11, float tx, float ty,
        AffineTransform b, AffineTransform into) {
        return Multiply(m00, m01, m10, m11, tx, ty,
                        b.m00, b.m01, b.m10, b.m11, b.Tx, b.Ty, into);
    }

    /**
     * Multiplies the supplied two affine transforms, storing the result in {@code into}.
     * @return {@code into} for chaining.
     */
    public static AffineTransform Multiply (
        float am00, float am01, float am10, float am11, float atx, float aty,
        float bm00, float bm01, float bm10, float bm11, float btx, float bty,
        AffineTransform into) {
        into.m00 = am00 * bm00 + am10 * bm01;
        into.m01 = am01 * bm00 + am11 * bm01;
        into.m10 = am00 * bm10 + am10 * bm11;
        into.m11 = am01 * bm10 + am11 * bm11;
        into.Tx  = am00 *  btx + am10 *  bty + atx;
        into.Ty  = am01 *  btx + am11 *  bty + aty;
        return into;
    }
}
}


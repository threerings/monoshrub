// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;
using Microsoft.Xna.Framework;

namespace monoshrub {

public class NonUniformTransform : Transform
{
    /** Identifies the uniform transform in {@link #generality}. */
    public const int GENERALITY = 3;

    public float ScaleX { get; set; }
    public float ScaleY { get; set; }
    public float Rotation { get; set; }
    public float Tx { get; set; }
    public float Ty { get; set; }
    public int Generality { get { return GENERALITY; } }

    public NonUniformTransform ()
    {
        this.ScaleX = 1;
        this.ScaleY = 1;
    }

    public NonUniformTransform (float scaleX, float scaleY, float rotation, float tx, float ty)
    {
        this.ScaleX = scaleX;
        this.ScaleY = scaleY;
        this.Rotation = rotation;
        this.Tx = tx;
        this.Ty = ty;
    }

    public Transform ScaleXBy (float scaleX)
    {
        if (scaleX == 0) {
            throw new ArgumentException("scale must not be 0", "scaleX");
        }
        this.Tx *= scaleX;
        this.ScaleX *= scaleX;
        return this;
    }

    public Transform ScaleYBy (float scaleY)
    {
        if (scaleY == 0) {
            throw new ArgumentException("scale must not be 0", "scaleY");
        }
        this.Ty *= scaleY;
        this.ScaleY *= scaleY;
        return this;
    }

    public Transform Scale (float scaleX, float scaleY)
    {
        ScaleXBy(scaleX);
        ScaleYBy(scaleY);
        return this;
    }

    public Transform Rotate (float angle)
    {
        float otx = this.Tx, oty = this.Ty;
        if (otx != 0 || oty != 0) {
            float sina = FloatMath.Sin(angle);
            float cosa = FloatMath.Cos(angle);
            this.Tx = otx*cosa - oty*sina;
            this.Ty = otx*sina + oty*cosa;
        }
        this.Rotation += angle;
        return this;
    }

    public Transform TranslateX (float tx)
    {
        this.Tx += tx;
        return this;
    }

    public Transform TranslateY (float ty)
    {
        this.Ty += ty;
        return this;
    }

    public Transform Translate (float tx, float ty)
    {
        TranslateX(tx);
        TranslateY(ty);
        return this;
    }

    public Transform Invert () {
        Vector iscale = new Vector(1f / ScaleX, 1f / ScaleY);
        Vector t = new Vector(Tx, Ty).Negate().Rotate(-Rotation).Scale(iscale);
        return new NonUniformTransform(iscale.x, iscale.y, -Rotation, t.x, t.y);
    }

    public Transform Concatenate (Transform other) {
        if (this.Generality < other.Generality) {
            return other.PreConcatenate(this);
        }

        float otx = other.Tx, oty = other.Ty;
        float sina = FloatMath.Sin(this.Rotation), cosa = FloatMath.Cos(this.Rotation);
        float ntx = (otx*cosa - oty*sina) * this.ScaleX + this.Tx;
        float nty = (otx*sina + oty*cosa) * this.ScaleY + this.Ty;

        float nrotation = MathUtil.NormalizeAngle(this.Rotation + other.Rotation);
        float nscaleX = this.ScaleX * other.ScaleX;
        float nscaleY = this.ScaleY * other.ScaleY;
        return new NonUniformTransform(nscaleX, nscaleY, nrotation, ntx, nty);
    }

    public Transform PreConcatenate (Transform other) {
        if (this.Generality < other.Generality) {
            return other.Concatenate(this);
        }

        float sina = FloatMath.Sin(other.Rotation), cosa = FloatMath.Cos(other.Rotation);
        float ntx = (Tx*cosa - Ty*sina) * other.ScaleX + other.Tx;
        float nty = (Tx*sina + Ty*cosa) * other.ScaleY + other.Ty;
        float nrotation = MathUtil.NormalizeAngle(other.Rotation + this.Rotation);
        float nscaleX = other.ScaleX * this.ScaleX;
        float nscaleY = other.ScaleY * this.ScaleY;
        return new NonUniformTransform(nscaleX, nscaleY, nrotation, ntx, nty);
    }

    public Transform Lerp (Transform other, float t) {
        if (this.Generality < other.Generality) {
            return other.Lerp(this, -t); // TODO: is this correct?
        }

        float ntx = MathUtil.Lerpa(this.Tx, other.Tx, t);
        float nty = MathUtil.Lerpa(this.Ty, other.Ty, t);
        float nrotation = MathUtil.Lerpa(this.Rotation, other.Rotation, t);
        float nscaleX = MathUtil.Lerp(this.ScaleX, other.ScaleX, t);
        float nscaleY = MathUtil.Lerp(this.ScaleY, other.ScaleY, t);
        return new NonUniformTransform(nscaleX, nscaleY, nrotation, ntx, nty);
    }

    public Point Transform (Point p) {
        return Points.Transform(p.x, p.y, ScaleX, ScaleY, Rotation, Tx, Ty);
    }

    public Point InverseTransform (Point p) {
        return Points.InverseTransform(p.x, p.y, ScaleX, ScaleY, Rotation, Tx, Ty);
    }

    public Vector Transform (Vector v) {
        return Vectors.Transform(v.x, v.y, ScaleX, ScaleY, Rotation);
    }

    public Vector InverseTransform (Vector v) {
        return Vectors.InverseTransform(v.x, v.y, ScaleX, ScaleY, Rotation);
    }

    public Transform Clone () {
        return new NonUniformTransform(ScaleX, ScaleY, Rotation, Tx, Ty);
    }
}
}


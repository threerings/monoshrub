// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

public struct Vector
{
    public float x;
    public float y;

    public Vector (float x, float y) {
        this.x = x;
        this.y = y;
    }

    public float Angle {
        get { return FloatMath.Atan2(y, x); }
        set {
            float l = this.Length;
            x = l * FloatMath.Cos(value);
            y = l * FloatMath.Sin(value);
        }
    }

    public float LengthSq {
        get { return (x*x + y*y); }
    }

    public float Length {
        get { return FloatMath.Sqrt(this.LengthSq); }
        set {
            float scale = value / this.Length;
            this.x *= scale;
            this.y *= scale;
        }
    }

    public bool IsZero {
        get { return Vectors.IsZero(x, y); }
    }

    public float Dot (Vector other) {
        return x*other.x + y*other.y;
    }

    public Vector Negate () {
        return new Vector(-x, -y);
    }

    public Vector Normalize () {
        return Scale(1f / this.Length);
    }

    public float Distance (Vector other) {
        return FloatMath.Sqrt(DistanceSq(other));
    }

    public float DistanceSq (Vector other) {
        float dx = x - other.x, dy = y - other.y;
        return dx*dx + dy*dy;
    }

    public float AngleBetween (Vector other) {
        float cos = Dot(other) / (this.Length * other.Length);
        return cos >= 1f ? 0f : FloatMath.Acos(cos);
    }

    public Vector Scale (float v) {
        return new Vector(x*v, y*v);
    }

    public Vector Scale (Vector other) {
        return new Vector(x*other.x, y*other.y);
    }

    public Vector Add (Vector other) {
        return Add(other.x, other.y);
    }

    public Vector Subtract (Vector other) {
        return Add(-other.x, -other.y);
    }

    public Vector Add (float x, float y) {
        return new Vector(this.x + x, this.y + y);
    }

    public Vector AddScaled (Vector other, float v) {
        return new Vector(x + other.x*v, y + other.y*v);
    }

    public Vector Rotate (float angle) {
        float sina = FloatMath.Sin(angle), cosa = FloatMath.Cos(angle);
        return new Vector(x*cosa - y*sina, x*sina + y*cosa);
    }

    public Vector RotateAndAdd (float angle, Vector add) {
        float sina = FloatMath.Sin(angle), cosa = FloatMath.Cos(angle);
        return new Vector(x*cosa - y*sina + add.x, x*sina + y*cosa + add.y);
    }

    public Vector RotateScaleAndAdd (float angle, float scale, Vector add) {
        float sina = FloatMath.Sin(angle), cosa = FloatMath.Cos(angle);
        return new Vector((x*cosa - y*sina)*scale + add.x,
                          (x*sina + y*cosa)*scale + add.y);
    }

    public Vector Lerp (Vector other, float t, Vector result) {
        float dx = other.x - x, dy = other.y - y;
        return new Vector(x + t*dx, y + t*dy);
    }

    override public string ToString () {
        return Vectors.VectorToString(x, y);
    }
}
}


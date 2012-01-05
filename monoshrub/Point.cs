// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

public struct Point
{
    public float x;
    public float y;

    public Point (float x, float y) {
        this.x = x;
        this.y = y;
    }

    public float DistanceSq (float px, float py) {
        return Points.DistanceSq(x, y, px, py);
    }

    public float DistanceSq (Point p) {
        return Points.DistanceSq(x, y, p.x, p.y);
    }

    public float Distance (float px, float py) {
        return Points.Distance(x, y, px, py);
    }

    public float Distance (Point p) {
        return Points.Distance(x, y, p.x, p.y);
    }

    public float Direction (Point other) {
        return FloatMath.Atan2(other.y - y, other.x - x);
    }

    public Point Mult (float s) {
        return new Point(x * s, y * s);
    }

    public Point Add (float x, float y) {
        return new Point(this.x + x, this.y + y);
    }

    public Vector Subtract (float x, float y) {
        return new Vector(this.x - x, this.y - y);
    }

    public Point Rotate (float angle) {
        float sina = FloatMath.Sin(angle), cosa = FloatMath.Cos(angle);
        return new Point(x*cosa - y*sina, x*sina + y*cosa);
    }
}
}


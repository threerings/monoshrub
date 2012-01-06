//
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;
using Microsoft.Xna.Framework;

namespace monoshrub {

public interface Transform
{
    float ScaleX { get; set; }
    float ScaleY { get; set; }
    float Rotation { get; set; }
    float Tx { get; set; }
    float Ty { get; set; }
    int Generality { get; }

    Transform ScaleXBy (float scaleX);
    Transform ScaleYBy (float scaleY);
    Transform Scale (float scaleX, float scaleY);
    Transform Rotate (float angle);
    Transform TranslateX (float tx);
    Transform TranslateY (float ty);
    Transform Translate (float tx, float ty);
    Transform Invert ();
    Transform Concatenate (Transform other);
    Transform PreConcatenate (Transform other);
    Transform Lerp (Transform other, float t);

    Point Transform (Point p);
    Point InverseTransform (Point p);

    Vector Transform (Vector v);
    Vector InverseTransform (Vector v);

    Transform Clone ();
}

}


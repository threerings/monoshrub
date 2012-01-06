// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

public class AffineTransform : Transform
{
     /** Identifies the affine transform in {@link #generality}. */
    public const int GENERALITY = 4;

    /** The scale, rotation and shear components of this transform. */
    public float m00, m01, m10, m11;

    /** The translation components of this transform. */
    public float Tx { get; set; }
    public float Ty { get; set; }
    public Vector Translation { get { return new Vector(Tx, Ty); } set { Tx = value.x; Ty = value.y; } }

    public int Generality { get { return GENERALITY; } }

    /** Creates an affine transform configured with the identity transform. */
    public AffineTransform () : this(1, 0, 0, 1, 0, 0) {}

    /** Creates an affine transform from the supplied scale, rotation and translation. */
    public AffineTransform (float scale, float angle, float tx, float ty) : this(scale, scale, angle, tx, ty) {}

    /** Creates an affine transform from the supplied scale, rotation and translation. */
    public AffineTransform (float scaleX, float scaleY, float angle, float tx, float ty) {
        float sina = FloatMath.Sin(angle), cosa = FloatMath.Cos(angle);
        this.m00 =  cosa * scaleX; this.m01 = sina * scaleY;
        this.m10 = -sina * scaleX; this.m11 = cosa * scaleY;
        this.Tx  =  tx;            this.Ty  = ty;
    }

    /** Creates an affine transform with the specified transform matrix. */
    public AffineTransform (float m00, float m01, float m10, float m11, float tx, float ty) {
        this.m00 = m00; this.m01 = m01;
        this.m10 = m10; this.m11 = m11;
        this.Tx  = tx;  this.Ty  = ty;
    }

    public float ScaleX {
        get {
            return FloatMath.Sqrt(m00*m00 + m01*m01);
        }
        set {
            // normalize the scale to 1, then re-apply
            float mult = value / this.ScaleX;
            m00 *= mult;
            m01 *= mult;
        }
    }

    public float ScaleY {
        get {
            return FloatMath.Sqrt(m10*m10 + m11*m11);
        }
        set {
            // normalize the scale to 1, then re-apply
            float mult = value / this.ScaleY;
            m10 *= mult;
            m11 *= mult;
        }
    }

    public Vector Scale { get { return new Vector(ScaleX, ScaleY); } set { ScaleX = value.x; ScaleY = value.y; } }

    public float Rotation {
        get {
            // use the iterative polar decomposition algorithm described by Ken Shoemake:
            // http://www.cs.wisc.edu/graphics/Courses/838-s2002/Papers/polar-decomp.pdf
    
            // start with the contents of the upper 2x2 portion of the matrix
            float n00 = m00, n10 = m10;
            float n01 = m01, n11 = m11;
            for (int ii = 0; ii < 10; ii++) {
                // store the results of the previous iteration
                float o00 = n00, o10 = n10;
                float o01 = n01, o11 = n11;
    
                // compute average of the matrix with its inverse transpose
                float det = o00*o11 - o10*o01;
                if (Math.Abs(det) == 0f) {
                    // determinant is zero; matrix is not invertible
                    throw new NoninvertibleTransformException(this.ToString());
                }
                float hrdet = 0.5f / det;
                n00 = +o11 * hrdet + o00*0.5f;
                n10 = -o01 * hrdet + o10*0.5f;
    
                n01 = -o10 * hrdet + o01*0.5f;
                n11 = +o00 * hrdet + o11*0.5f;
    
                // compute the difference; if it's small enough, we're done
                float d00 = n00 - o00, d10 = n10 - o10;
                float d01 = n01 - o01, d11 = n11 - o11;
                if (d00*d00 + d10*d10 + d01*d01 + d11*d11 < MathUtil.EPSILON) {
                    break;
                }
            }
            // now that we have a nice orthogonal matrix, we can extract the rotation
            return FloatMath.Atan2(n01, n00);
        }
        set {
            // extract the scale, then reapply rotation and scale together
            float sx = this.ScaleX, sy = this.ScaleY;
            float sina = FloatMath.Sin(value), cosa = FloatMath.Cos(value);
            m00 =  cosa * sx; m01 = sina * sx;
            m10 = -sina * sy; m11 = cosa * sy;
        }
    }

    public Transform SetTransform (float m00, float m01, float m10, float m11, float tx, float ty) {
        this.m00 = m00;
        this.m01 = m01;
        this.m10 = m10;
        this.m11 = m11;
        this.Tx = tx;
        this.Ty = ty;
        return this;
    }

    public Transform ScaleXBy (float scaleX) {
        m00 *= scaleX;
        m01 *= scaleX;
        Tx  *= scaleX;
        return this;
    }

    public Transform ScaleYBy (float scaleY) {
        m10 *= scaleY;
        m11 *= scaleY;
        Ty  *= scaleY;
        return this;
    }

    public Transform ScaleBy (float scaleX, float scaleY) {
        ScaleXBy(scaleX);
        ScaleYBy(scaleY);
        return this;
    }

    public Transform Rotate (float angle) {
        float sina = FloatMath.Sin(angle), cosa = FloatMath.Cos(angle);
        return Transforms.Multiply(cosa, sina, -sina, cosa, 0, 0, this, this);
    }

    public Transform Translate (float tx, float ty) {
        return Transforms.Multiply(this, 1, 0, 0, 1, tx, ty, this);
    }

    public Transform TranslateX (float tx) {
        return Transforms.Multiply(this, 1, 0, 0, 1, tx, 0, this);
    }

    public Transform TranslateY (float ty) {
        return Transforms.Multiply(this, 1, 0, 0, 1, 0, ty, this);
    }

    public Transform Invert () {
        // compute the determinant, storing the subdeterminants for later use
        float det = m00*m11 - m10*m01;
        if (Math.Abs(det) == 0f) {
            // determinant is zero; matrix is not invertible
            throw new NoninvertibleTransformException(this.ToString());
        }
        float rdet = 1f / det;
        return new AffineTransform(
            +m11 * rdet,              -m10 * rdet,
            -m01 * rdet,              +m00 * rdet,
            (m10*Ty - m11*Tx) * rdet, (m01*Tx - m00*Ty) * rdet);
    }

    public Transform Concatenate (Transform other) {
        if (this.Generality < other.Generality) {
            return other.PreConcatenate(this);
        }
        if (other is AffineTransform) {
            return Transforms.Multiply(this, (AffineTransform)other, new AffineTransform());
        } else {
            AffineTransform oaff = new AffineTransform(other);
            return Transforms.Multiply(this, oaff, oaff);
        }
    }

    public Transform PreConcatenate (Transform other) {
        if (this.Generality < other.Generality) {
            return other.Concatenate(this);
        }
        if (other is AffineTransform) {
            return Transforms.Multiply((AffineTransform)other, this, new AffineTransform());
        } else {
            AffineTransform oaff = new AffineTransform(other);
            return Transforms.Multiply(oaff, this, oaff);
        }
    }

    public Transform Lerp (Transform other, float t) {
        if (this.Generality < other.Generality) {
            return other.Lerp(this, -t); // TODO: is this correct?
        }

        AffineTransform ot = (other is AffineTransform) ?
            (AffineTransform)other : new AffineTransform(other);
        return new AffineTransform(
            m00 + t*(ot.m00 - m00), m01 + t*(ot.m01 - m01),
            m10 + t*(ot.m10 - m10), m11 + t*(ot.m11 - m11),
            Tx  + t*(ot.Tx  - Tx ), Ty  + t*(ot.Ty  - Ty ));
    }

    public Point Transform (Point p) {
        float x = p.x, y = p.y;
        return new Point(m00*x + m10*y + Tx, m01*x + m11*y + Ty);
    }

    public Point InverseTransform (Point p) {
        float x = p.x - Tx, y = p.y - Ty;
        float det = m00 * m11 - m01 * m10;
        if (Math.Abs(det) == 0f) {
            // determinant is zero; matrix is not invertible
            throw new NoninvertibleTransformException(this.ToString());
        }
        float rdet = 1 / det;
        return new Point((x * m11 - y * m10) * rdet,
                        (y * m00 - x * m01) * rdet);
    }

    public Vector Transform (Vector v) {
        float x = v.x, y = v.y;
        return new Vector(m00*x + m10*y, m01*x + m11*y);
    }

    public Vector InverseTransform (Vector v) {
        float x = v.x, y = v.y;
        float det = m00 * m11 - m01 * m10;
        if (Math.Abs(det) == 0f) {
            // determinant is zero; matrix is not invertible
            throw new NoninvertibleTransformException(this.ToString());
        }
        float rdet = 1 / det;
        return new Vector((x * m11 - y * m10) * rdet,
                        (y * m00 - x * m01) * rdet);
    }

    public Transform Clone () {
        return new AffineTransform(m00, m01, m10, m11, Tx, Ty);
    }

    override public string ToString () {
        return "affine [" + MathUtil.ToString(m00) + " " + MathUtil.ToString(m01) + " " +
            MathUtil.ToString(m10) + " " + MathUtil.ToString(m11) + " " + this.Translation + "]";
    }

    // we don't publicize this because it might encourage someone to do something stupid like
    // create a new AffineTransform from another AffineTransform using this instead of clone()
    protected AffineTransform (Transform other) : this(other.ScaleX, other.ScaleY, other.Rotation, other.Tx, other.Ty) {}
}
}


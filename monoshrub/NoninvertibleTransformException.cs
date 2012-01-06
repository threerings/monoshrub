// 
// monoshrub - Copyright 2012 Three Rings Design, Inc.

using System;

namespace monoshrub {

/**
 * An exception thrown by {@code Transform} when a request for an inverse transform cannot be
 * satisfied.
 */
public class NoninvertibleTransformException : Exception
{
    public NoninvertibleTransformException (string s) : base(s) {}
}
}


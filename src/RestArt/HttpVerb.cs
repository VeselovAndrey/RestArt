// -----------------------------------------------------------------------
// <copyright file="HttpVerb.cs">
// Copyright (c) 2015-2016 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    /// <summary>Supported HTTP verbs.</summary>
    public enum HttpVerb
    {
        Unspecified = 0,

        Get = 1,
        Post = 2,
        Put = 3,
        Delete = 4
    }
}
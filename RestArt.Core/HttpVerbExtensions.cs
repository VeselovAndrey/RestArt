// -----------------------------------------------------------------------
// <copyright file="HttpVerbExtensions.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core
{
    using System;
    using System.Net.Http;

    internal static class HttpVerbExtensions
    {
        /// <summary>Converts <see cref="HttpVerb"/> value to <see cref="HttpMethod"/>.</summary>
        /// <param name="verb">The <see cref="HttpVerb"/> value.</param>
        /// <returns>The <see cref="HttpMethod"/> value.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="verb"/> contains invalid value.</exception>
        public static HttpMethod ToHttpMethod(this HttpVerb verb)
        {
            switch (verb) {

                case HttpVerb.Get:
                    return HttpMethod.Get;

                case HttpVerb.Post:
                    return HttpMethod.Post;

                case HttpVerb.Put:
                    return HttpMethod.Put;

                case HttpVerb.Delete:
                    return HttpMethod.Delete;

                default:
                    throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
            }
        }
    }
}

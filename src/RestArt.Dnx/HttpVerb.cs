// -----------------------------------------------------------------------
// <copyright file="HttpVerb.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
 
namespace RestArt
{
    using System;
    using System.Net.Http;

    // Supported HTTP verbs
    public enum HttpVerb
    {
        Unspecified = 0,

        Get = 1,
        Post = 2,
        Put = 3,
        Delete = 4
    }

    internal static class HttpVerbExtensions
    {
        public static HttpMethod ToHttpMethod(this HttpVerb verb)
        {
            switch (verb) {

                case HttpVerb.Get: return HttpMethod.Get;

                case HttpVerb.Post: return HttpMethod.Post;

                case HttpVerb.Put: return HttpMethod.Put;

                case HttpVerb.Delete: return HttpMethod.Delete;

                default:
                    throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
            }
        }
    }
}

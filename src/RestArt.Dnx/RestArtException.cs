// -----------------------------------------------------------------------
// <copyright file="RestArtException.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    using System;
    using System.Net;

    public class RestArtException<TErrorDescription> : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public string Raw { get; }

        public TErrorDescription ErrorDescription { get; }

        public RestArtException(HttpStatusCode statusCode, TErrorDescription errorDescription, string raw)
        {
            this.ErrorDescription = errorDescription;
            this.Raw = raw;
            this.StatusCode = statusCode;
        }
    }
}
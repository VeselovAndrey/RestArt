// -----------------------------------------------------------------------
// <copyright file="RestArtException.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    using System;
    using System.Net;

    /// <summary>The exception that is thrown when server returns an error code.</summary>
    /// <typeparam name="TErrorDescription">The type of the error object. The type properties should match fields of the response JSON error object.</typeparam>
    public class RestArtException<TErrorDescription> : Exception
    {
        /// <summary>Gets the response status code.
        /// <seealso cref="HttpStatusCode"/>
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>Gets the error as the instance of the <see cref="TErrorDescription"/> type.</summary>
        public TErrorDescription ErrorDescription { get; }

        /// <summary>Gets the raw response.</summary>
        public string Raw { get; }

        public RestArtException(HttpStatusCode statusCode, TErrorDescription errorDescription, string raw)
        {
            this.StatusCode = statusCode;
            this.ErrorDescription = errorDescription;
            this.Raw = raw;
        }
    }
}
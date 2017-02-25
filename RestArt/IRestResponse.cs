﻿// -----------------------------------------------------------------------
// <copyright file="RestResponse.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    using System.Net;

    /// <summary>Represents the REST API response.</summary>
    /// <typeparam name="TResponse">The type of the response object. The type properties should match fields of the response JSON object.</typeparam>
    public interface IRestResponse<out TResponse>
        where TResponse : class
    {
        /// <summary>Gets the response status code. 
        /// <seealso cref="HttpStatusCode"/>
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>Gets the response as the instance of the <see cref="TResponse"/> type.</summary>
        TResponse Value { get; }

        /// <summary>Gets the raw response.</summary>
        string Raw { get; }
    }
}

﻿// -----------------------------------------------------------------------
// <copyright file="IRestRequest.cs">
// Copyright (c) 2015-2016 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    using System.Collections.Generic;

    /// <summary>REST request (command) description.</summary>
    public interface IRestRequest
    {
        /// <summary>The request <see cref="HttpVerb">verb</see>.<seealso cref="HttpVerb"/></summary>
        HttpVerb Verb { get; }

        /// <summary>The command name.</summary>
        string Command { get; }

        /// <summary>The request headers. </summary>
        IDictionary<string, string> Headers { get; }
        
        /// <summary>The request parameters. </summary>
        IDictionary<string, object> Parameters { get; }
    }
}
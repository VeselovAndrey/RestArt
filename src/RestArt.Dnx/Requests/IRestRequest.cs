// -----------------------------------------------------------------------
// <copyright file="IRestRequest.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
 
namespace RestArt.Requests
{
    using System.Collections.Generic;

    public interface IRestRequest
    {
        HttpVerb Verb { get; }

        string Command { get; }

        IDictionary<string, string> Headers { get; }

        IDictionary<string, object> Parameters { get; }
    }
}
// -----------------------------------------------------------------------
// <copyright file="RestRequest.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Requests
{
    using System.Collections.Generic;

    public class RestRequest : IRestRequest
    {
        public HttpVerb Verb { get; }

        public string Command { get; }

        public IDictionary<string, string> Headers { get; }

        public IDictionary<string, object> Parameters { get; }

        public RestRequest(HttpVerb verb, string command, IDictionary<string, string> headers, IDictionary<string, object> parameters)
        {
            this.Verb = verb;
            this.Command = command;
            this.Headers = headers;
            this.Parameters = parameters;
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file="RestRequest.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Requests
{
    using System.Collections.Generic;
    using RestArt.Convertors;

    public class RestRequest : IRestRequest
    {
        private static readonly ObjectConvertor _objectConvertor = new ObjectConvertor();

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

        public RestRequest(HttpVerb verb, string command, object headers, object parameters)
        {
            this.Verb = verb;
            this.Command = command;
            this.Headers = RestRequest._objectConvertor.ToStringDictionary(headers);
            this.Parameters = RestRequest._objectConvertor.ToObjectDictionary(parameters);
        }
    }
}
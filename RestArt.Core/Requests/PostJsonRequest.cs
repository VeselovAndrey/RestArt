// -----------------------------------------------------------------------
// <copyright file="PostJsonRequest.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core.Requests
{
    using System.Collections.Generic;

    public class PostJsonRequest : IRestRequest
    {
        public HttpVerb Verb => HttpVerb.Post;

        public string Command { get; }

        public object JsonObject { get; set; }

        public IDictionary<string, string> Headers { get; }

        public IDictionary<string, object> Parameters { get; }

        public PostJsonRequest(string command, object jsonObject, IDictionary<string, string> headers, IDictionary<string, object> parameters)
        {
            this.Command = command;
            this.JsonObject = jsonObject;
            this.Headers = headers;
            this.Parameters = parameters;
        }
    }
}

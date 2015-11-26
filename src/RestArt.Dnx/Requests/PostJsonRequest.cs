// -----------------------------------------------------------------------
// <copyright file="PostJsonRequest.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Requests
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

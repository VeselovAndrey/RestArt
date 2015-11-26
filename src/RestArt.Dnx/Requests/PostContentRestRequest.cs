// -----------------------------------------------------------------------
// <copyright file="PostContentRestRequest.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
 
namespace RestArt.Requests
{
    using System.Collections.Generic;
    using System.IO;

    public class PostContentRestRequest : IRestRequest
    {
        public HttpVerb Verb => HttpVerb.Post;

        public string Command { get; }

        public Stream Content { get; }

        public IDictionary<string, string> Headers { get; }

        public IDictionary<string, object> Parameters { get; }

        public PostContentRestRequest(string command, Stream content, IDictionary<string, string> headers, IDictionary<string, object> parameters)
        {
            this.Command = command;
            this.Content = content;
            this.Headers = headers;
            this.Parameters = parameters;
        }
    }
}

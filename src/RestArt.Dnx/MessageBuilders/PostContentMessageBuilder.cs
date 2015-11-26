// -----------------------------------------------------------------------
// <copyright file="PostContentMessageBuilder.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.MessageBuilders
{
    using System;
    using System.Net.Http;
    using System.Text;
    using RestArt.Requests;

    internal class PostContentMessageBuilder : MessageBuilderBase
    {
        protected override bool IsCanProcessRequest(IRestRequest request)
        {
            return request.GetType() == typeof(PostContentRestRequest);
        }

        protected override HttpRequestMessage BuildMessage(IRestRequest request)
        {
            var internalRequest = request as PostContentRestRequest;
            if (internalRequest == null)
                throw new ArgumentException($"'{nameof(request)}' is not the PostContentRestRequest.");

            // build url with parameters
            bool fistParam = true;
            var cmdBuilder = new StringBuilder(request.Command);

            foreach (var param in request.Parameters) {
                var divider = fistParam ? "?" : "&";

                cmdBuilder.Append($"{divider}{param.Key}={param.Value.ToString()}");
                fistParam = false;
            }

            // Create POST request
            var message = new HttpRequestMessage(request.Verb.ToHttpMethod(), cmdBuilder.ToString()) {
                Content = new StreamContent(internalRequest.Content)
            };

            return message;
        }
    }
}

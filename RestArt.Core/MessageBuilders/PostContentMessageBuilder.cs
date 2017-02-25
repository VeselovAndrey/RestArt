﻿// -----------------------------------------------------------------------
// <copyright file="PostContentMessageBuilder.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core.MessageBuilders
{
    using System;
    using System.Net.Http;
    using System.Text;
    using RestArt.Core.Requests;

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

            if (request.Parameters != null) {
                foreach (var param in request.Parameters) {
                    var divider = fistParam ? "?" : "&";

                    cmdBuilder.Append($"{divider}{param.Key}={param.Value.ToString()}");
                    fistParam = false;
                }
            }

            // Create POST request
            var message = new HttpRequestMessage(request.Verb.ToHttpMethod(), cmdBuilder.ToString()) {
                Content = new StreamContent(internalRequest.Content)
            };

            return message;
        }
    }
}

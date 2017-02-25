// -----------------------------------------------------------------------
// <copyright file="MessageBuilderBase.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core.MessageBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    internal abstract class MessageBuilderBase : IMessageBuilder
    {
        public HttpRequestMessage Build(IRestRequest request, IDictionary<string, string> persistentHeaders)
        {
            if (!this.IsCanProcessRequest(request))
                return null;

            HttpRequestMessage message = this.BuildMessage(request);

            // Set headers
            message.Headers.Add("Accept", "application/json");

            // Add persistent headers
            if (request.Headers != null) {
                foreach (var header in request.Headers)
                    this.AddHeader(header.Key, header.Value, message);
            }

            // Add message headers
            if (persistentHeaders != null) {
                foreach (var header in persistentHeaders)
                    if (request.Headers == null || !request.Headers.ContainsKey(header.Key))
                        this.AddHeader(header.Key, header.Value, message);
            }

            return message;
        }

        protected abstract bool IsCanProcessRequest(IRestRequest request);

        protected abstract HttpRequestMessage BuildMessage(IRestRequest request);

        private void AddHeader(string key, string value, HttpRequestMessage message)
        {
            string headerKey = key?.Trim();
            if (string.IsNullOrEmpty(headerKey))
                return;

            bool isContentHeader = headerKey.StartsWith("Content")
                                       || string.Equals(headerKey, "Allow", StringComparison.OrdinalIgnoreCase)
                                       || string.Equals(headerKey, "Expires", StringComparison.OrdinalIgnoreCase)
                                       || string.Equals(headerKey, "LastModified", StringComparison.OrdinalIgnoreCase);

            if (isContentHeader)
                message.Content?.Headers.Add(headerKey, value); // ignore content headers when there is no content in the message
            else
                message.Headers.Add(headerKey, value);
        }
    }
}

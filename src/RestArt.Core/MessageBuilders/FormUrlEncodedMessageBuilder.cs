// -----------------------------------------------------------------------
// <copyright file="FormUrlEncodedMessageBuilder.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core.MessageBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using RestArt.Core.Requests;


    internal class FormUrlEncodedMessageBuilder : MessageBuilderBase, IMessageBuilder
    {
        protected override bool IsCanProcessRequest(IRestRequest request)
        {
            return (request.Verb == HttpVerb.Post || request.Verb == HttpVerb.Put)
                && request.GetType() == typeof(RestRequest)
                && (request.Parameters == null || request.Parameters.All(p => !(p.Value is FileParameter)));
        }

        protected override HttpRequestMessage BuildMessage(IRestRequest request)
        {
            // Select request content type and create content
            HttpContent content = request.Parameters != null ?
                new FormUrlEncodedContent(request.Parameters.Select(pair => new KeyValuePair<string, string>(pair.Key, pair.Value.ToString()))) :
                null;

            // Build message
            var message = new HttpRequestMessage(request.Verb.ToHttpMethod(), request.Command) {
                Content = content
            };

            return message;
        }
    }
}
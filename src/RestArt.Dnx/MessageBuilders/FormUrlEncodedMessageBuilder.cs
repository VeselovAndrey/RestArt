// -----------------------------------------------------------------------
// <copyright file="FormUrlEncodedMessageBuilder.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.MessageBuilders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using RestArt.Requests;

    internal class FormUrlEncodedMessageBuilder : MessageBuilderBase, IMessageBuilder
    {
        protected override bool IsCanProcessRequest(IRestRequest request)
        {
            return (request.Verb == HttpVerb.Post || request.Verb == HttpVerb.Put)
                && request.GetType() == typeof(RestRequest)
                && request.Parameters.All(p => !(p.Value is FileParameter));
        }

        protected override HttpRequestMessage BuildMessage(IRestRequest request)
        {
            // Select request content type and create content
            HttpContent content = new FormUrlEncodedContent(request.Parameters.Select(pair => new KeyValuePair<string, string>(pair.Key, pair.Value.ToString())));

            // Build message
            var message = new HttpRequestMessage(request.Verb.ToHttpMethod(), request.Command) {
                Content = content
            };

            return message;
        }
    }
}
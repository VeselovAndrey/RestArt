// -----------------------------------------------------------------------
// <copyright file="GetLikeMessageBuilder.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.MessageBuilders
{
    using System;
    using System.Net.Http;
    using System.Text;
    using RestArt.Requests;

    internal class GetLikeMessageBuilder : MessageBuilderBase, IMessageBuilder
    {
        protected override bool IsCanProcessRequest(IRestRequest request)
        {
            return (request.Verb == HttpVerb.Get || request.Verb == HttpVerb.Delete) 
                && request.GetType() == typeof(RestRequest);
        }

        protected override HttpRequestMessage BuildMessage(IRestRequest request)
        {
            var urlBuilder = new StringBuilder(request.Command);
            bool firstParam = true;

            foreach (var param in request.Parameters) {
                var divider = firstParam ? "?" : "&";
                firstParam = false;

                urlBuilder.AppendFormat("{0}{1}={2}", divider, param.Key, Uri.EscapeUriString(param.Value.ToString()));
            }

            var message = new HttpRequestMessage(request.Verb.ToHttpMethod(), urlBuilder.ToString());

            return message;
        }
    }
}
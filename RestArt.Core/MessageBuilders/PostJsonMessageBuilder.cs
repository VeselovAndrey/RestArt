// -----------------------------------------------------------------------
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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using RestArt.Core.Requests;

    internal class PostJsonMessageBuilder : MessageBuilderBase
    {
        protected override bool IsCanProcessRequest(IRestRequest request)
        {
            return request.GetType() == typeof(PostJsonRequest);
        }

        protected override HttpRequestMessage BuildMessage(IRestRequest request)
        {
            var internalRequest = request as PostJsonRequest;
            if (internalRequest == null)
                throw new ArgumentException($"'{nameof(request)}' is not the PostJsonRequest.");

            // Build content
            var settings = new JsonSerializerSettings {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string postData = JsonConvert.SerializeObject(internalRequest.JsonObject, settings);
            byte[] formData = Encoding.GetEncoding("UTF-8").GetBytes(postData);

            // Create parameters string
            var urlBuilder = new StringBuilder(request.Command);
            bool firstParam = true;

            if (request.Parameters != null) {
                foreach (var param in request.Parameters) {
                    var divider = firstParam ? "?" : "&";
                    firstParam = false;

                    urlBuilder.AppendFormat("{0}{1}={2}", divider, param.Key, Uri.EscapeUriString(param.Value.ToString()));
                }
            }

            // Create message content
            HttpContent content = new ByteArrayContent(formData);

            // Build message
            var message = new HttpRequestMessage(request.Verb.ToHttpMethod(), urlBuilder.ToString()) {
                Content = content
            };

            return message;
        }
    }
}

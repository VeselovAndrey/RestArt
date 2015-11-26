// -----------------------------------------------------------------------
// <copyright file="MultipartFormDataMessageBuilder.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.MessageBuilders
{
    using System.Linq;
    using System.Net.Http;
    using RestArt.Requests;

    internal class MultipartFormDataMessageBuilder : MessageBuilderBase, IMessageBuilder
    {
        protected override bool IsCanProcessRequest(IRestRequest request)
        {
            return (request.Verb == HttpVerb.Post || request.Verb == HttpVerb.Put)
                && request.GetType() == typeof(RestRequest)
                && request.Parameters.Any(p => p.Value is FileParameter);
        }

        protected override HttpRequestMessage BuildMessage(IRestRequest request)
        {
            // Create message content
            var content = new MultipartFormDataContent();
            foreach (var param in request.Parameters) {
                var file = param.Value as FileParameter;

                if (file != null) {
                    var contentPart = new ByteArrayContent(file.Content);
                    contentPart.Headers.Add("Content-Type", file.ContentType);
                    content.Add(contentPart, param.Key, file.Name);
                }
                else {
                    var contentPart = new StringContent(param.Value.ToString());
                    content.Add(contentPart, param.Key);
                }
            }

            // Build message
            var message = new HttpRequestMessage(request.Verb.ToHttpMethod(), request.Command) {
                Content = content
            };

            return message;
        }
    }
}
// -----------------------------------------------------------------------
// <copyright file="RestArtClient.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using RestArt.MessageBuilders;
    using RestArt.Requests;

    public class RestArtClient
    {
        private readonly Uri _baseUri;
        private readonly ConcurrentDictionary<string, string> _headers = new ConcurrentDictionary<string, string>();

        private static readonly IEnumerable<IMessageBuilder> _pipeline;

        static RestArtClient()
        {
            // Get all message builders
            Type builderType = typeof(IMessageBuilder);

            RestArtClient._pipeline = builderType.GetTypeInfo().Assembly.GetTypes()
                .Where(t => {
                    var typeInfo = t.GetTypeInfo();
                    return builderType.IsAssignableFrom(t) && !typeInfo.IsInterface && !typeInfo.IsAbstract;
                })
                .Select(t => Activator.CreateInstance(t) as IMessageBuilder)
                .ToArray();
        }

        public RestArtClient(string baseUrl)
        {
            this._baseUri = new Uri(baseUrl);
        }

        public void AddOrUpdatePersistentHeader(string key, string value)
        {
            this._headers.AddOrUpdate(key.Trim(), value, (s, s1) => value);
        }

        public void RemovePersistentHeader(string key)
        {
            string value = null;
            this._headers.TryRemove(key, out value);
        }

        public void ClearPersistentHeaders()
        {
            this._headers.Clear();
        }

        public async Task<RestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request)
            where TResponse : class
        {
            // Build message
            HttpRequestMessage message = null;

            foreach (var builder in _pipeline) {
                message = builder.Build(request, this._headers);
                if (message != null)
                    break;
            }

            if (message == null)
                throw new InvalidOperationException($"Unable to process request: {request.GetType().FullName} / {request.Verb}");

            // Send request and process response
            var client = new HttpClient() {
                BaseAddress = this._baseUri
            };

            var restResponse = new RestResponse<TResponse>();

            using (HttpResponseMessage response = await client.SendAsync(message)) {
                restResponse.StatusCode = response.StatusCode;
                restResponse.Raw = await response.Content.ReadAsStringAsync();
            }

            restResponse.Value = JsonConvert.DeserializeObject<TResponse>(restResponse.Raw);

            return restResponse;
        }
    }
}

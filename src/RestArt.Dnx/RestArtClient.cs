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
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using RestArt.MessageBuilders;
    using RestArt.Requests;

    public class RestArtClient : IRestArtClient
    {
        private static readonly IEnumerable<IMessageBuilder> _pipeline;

        private readonly Uri _baseUri;
        private readonly ConcurrentDictionary<string, string> _headers = new ConcurrentDictionary<string, string>();

        private readonly Type _exceptionType = typeof(RestArtException<>);
        private Type _commonErrorType;

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
            string value;
            this._headers.TryRemove(key, out value);
        }

        public void ClearPersistentHeaders()
        {
            this._headers.Clear();
        }

        public void SetErrorType(Type errorType)
        {
            this._commonErrorType = errorType;
        }

        public Task<RestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request)
            where TResponse : class
        {
            return this.ExecuteRequestAsync<TResponse>(request, this._commonErrorType);
        }

        public Task<RestResponse<TResponse>> ExecuteAsync<TResponse>(IRestRequest request, Type errorType)
            where TResponse : class
        {
            return this.ExecuteRequestAsync<TResponse>(request, errorType ?? this._commonErrorType);
        }

        private async Task<RestResponse<TResponse>> ExecuteRequestAsync<TResponse>(IRestRequest request, Type errorType)
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

            using (HttpResponseMessage response = await client.SendAsync(message).ConfigureAwait(false)) {
                restResponse.StatusCode = response.StatusCode;
                restResponse.Raw = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);
            }

            // Handle errors
            if (400 <= (int)restResponse.StatusCode) {
                if (errorType != null) {
                    object errorDescription = this.DeserializeObject(errorType, restResponse);
                    throw this.BuildExceptionInstance(errorType, restResponse.StatusCode, errorDescription, restResponse.Raw);
                }
            }

            restResponse.Value = this.DeserializeObject(typeof(TResponse), restResponse) as TResponse;

            return restResponse;
        }

        private object DeserializeObject<TResponse>(Type resultType, RestResponse<TResponse> restResponse)
            where TResponse : class
        {
            object errorDescription;
            try {
                errorDescription = JsonConvert.DeserializeObject(restResponse.Raw, resultType);

                if (errorDescription == null)
                    throw new RestArtException<InvalidJsonFormat>(restResponse.StatusCode, new InvalidJsonFormat(), restResponse.Raw);
            }
            catch (JsonReaderException) {
                throw new RestArtException<InvalidRestResponse>(restResponse.StatusCode, new InvalidRestResponse(), restResponse.Raw);
            }

            return errorDescription;
        }

        private Exception BuildExceptionInstance(Type errorDescriptionType, HttpStatusCode statusCode, object errorDescription, string rawResponse)
        {
            Type[] genericExceptionArgs = { errorDescriptionType };
            Type genericExceptionType = this._exceptionType.MakeGenericType(genericExceptionArgs);

            Exception expection = (Exception)Activator.CreateInstance(genericExceptionType, statusCode, errorDescription, rawResponse);

            return expection;
        }
    }
}
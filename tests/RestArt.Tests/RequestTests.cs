// -----------------------------------------------------------------------
// <copyright file="RequestTests.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Tests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using RestArt.Core.Requests;
    using RestArt.Tests.Models;
    using Xunit;

    public class RequestTests
    {
        private readonly string _restUrl = "http://demo6342033.mockable.io/";

        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>() {
            ["stringParam"] = "Some String",
            ["boolParam"] = true,
            ["intParam"] = 42,
            ["FloatParam"] = 77.7f
        };

        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>() {
            ["header1"] = "Test-One",
            ["header2"] = "Test-Two",
            ["Content-Language"] = "en-US"
        };

        private readonly IRestArtClient _client;

        public RequestTests()
        {
            this._client = new RestArtClient(this._restUrl);
            this._client.AddOrUpdatePersistentHeader("PersistentHeader", "Persistent-Value");
        }

        [Fact]
        public async Task ExecuteGetAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item content";
            const int expectedResponseCode = 200;

            var request = new RestRequest(HttpVerb.Get, "RestArt", this._headers, this._parameters);

            // Act
            IRestResponse<TestResponse> response = await this._client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }

        [Fact]
        public async Task ExecutePostAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item created";
            const int expectedResponseCode = 200;

            var request = new RestRequest(HttpVerb.Post, "RestArt", this._headers, this._parameters);

            // Act
            IRestResponse<TestResponse> response = await this._client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }


        [Fact]
        public async Task ExecutePostWithObjectsAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item created";
            const int expectedResponseCode = 200;

            var parameters = new {
                StringParam = "Some String",
                BoolParam = true,
                IntParam = 42,
                FloatParam = 77.7f
            };

            var headers = new {
                Header1 = "Test-One",
                Header2 = "Test-Two"
            };

            var request = new RestRequest(HttpVerb.Post, "RestArt", headers, parameters);

            // Act
            IRestResponse<TestResponse> response = await this._client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }

        [Fact]
        public async Task ExecutePostWithObjectsAndDictionaryAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item created";
            const int expectedResponseCode = 200;

            var headers = new {
                Header1 = "Test-One",
                Header2 = "Test-Two"
            };

            var request = new RestRequest(HttpVerb.Post, "RestArt", headers, this._parameters);

            // Act
            IRestResponse<TestResponse> response = await this._client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }

        [Fact]
        public async Task ExecutePostWithoutParametersAndHeadersAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item created";
            const int expectedResponseCode = 200;

            var request = new RestRequest(HttpVerb.Post, "RestArt", null, null);

            // Act
            IRestResponse<TestResponse> response = await this._client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }

        [Fact]
        public async Task ExecutePutAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item updated";
            const int expectedResponseCode = 200;

            var request = new RestRequest(HttpVerb.Put, "RestArt", this._headers, this._parameters);

            // Act
            IRestResponse<TestResponse> response = await this._client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }

        [Fact]
        public async Task ExecuteDeleteAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item deleted";
            const int expectedResponseCode = 200;

            var request = new RestRequest(HttpVerb.Delete, "RestArt", this._headers, this._parameters);

            // Act
            IRestResponse<TestResponse> response = await this._client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }

        [Fact]
        public void AppveyorTest()
        {
            Assert.Equal(1, 2);
        }
    }
}

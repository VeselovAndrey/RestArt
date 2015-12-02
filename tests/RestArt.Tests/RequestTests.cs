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
    using RestArt.Requests;
    using RestArt.Tests.Models;
    using Xunit;

    public class RequestTests
    {
        private readonly string _restUrl = "http://demo8536676.mockable.io/";

        [Fact]
        public async Task ExecuteGetAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item content";
            const int expectedResponseCode = 200;

            var parameters = new Dictionary<string, object>() {
                ["stringParam"] = "Some String",
                ["boolParam"] = true,
                ["intParam"] = 42,
                ["FlotParam"] = 77.7f
            };

            var headers = new Dictionary<string, string>() {
                ["header1"] = "Test-One",
                ["header2"] = "Test-Two",
                ["Content-Language"] = "en-US"
            };

            var request = new RestRequest(HttpVerb.Get, "RestArt", headers, parameters);

            IRestArtClient client = new RestArtClient(this._restUrl);
            client.AddOrUpdatePersistentHeader("PersistentHeader", "ph-value");

            // Act
            RestResponse<TestResponse> response = await client.ExecuteAsync<TestResponse>(request);

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

            var parameters = new Dictionary<string, object>() {
                ["stringParam"] = "Some String",
                ["boolParam"] = true,
                ["intParam"] = 42,
                ["FlotParam"] = 77.7f
            };

            var headers = new Dictionary<string, string>() {
                ["header1"] = "Test-One",
                ["header2"] = "Test-Two",
                ["Content-Language"] = "en-US"
            };

            var request = new RestRequest(HttpVerb.Post, "RestArt", headers, parameters);

            IRestArtClient client = new RestArtClient(this._restUrl);
            client.AddOrUpdatePersistentHeader("PersistentHeader", "ph-value");

            // Act
            RestResponse<TestResponse> response = await client.ExecuteAsync<TestResponse>(request);

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

            var parameters = new Dictionary<string, object>() {
                ["stringParam"] = "Some String",
                ["boolParam"] = true,
                ["intParam"] = 42,
                ["FlotParam"] = 77.7f
            };

            var headers = new Dictionary<string, string>() {
                ["header1"] = "Test-One",
                ["header2"] = "Test-Two",
                ["Content-Language"] = "en-US"
            };

            var request = new RestRequest(HttpVerb.Put, "RestArt", headers, parameters);

            IRestArtClient client = new RestArtClient(this._restUrl);
            client.AddOrUpdatePersistentHeader("PersistentHeader", "ph-value");

            // Act
            RestResponse<TestResponse> response = await client.ExecuteAsync<TestResponse>(request);

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

            var parameters = new Dictionary<string, object>() {
                ["stringParam"] = "Some String",
                ["boolParam"] = true,
                ["intParam"] = 42,
                ["FlotParam"] = 77.7f
            };

            var headers = new Dictionary<string, string>() {
                ["header1"] = "Test-One",
                ["header2"] = "Test-Two",
                ["Content-Language"] = "en-US"
            };

            var request = new RestRequest(HttpVerb.Delete, "RestArt", headers, parameters);

            IRestArtClient client = new RestArtClient(this._restUrl);
            client.AddOrUpdatePersistentHeader("PersistentHeader", "ph-value");

            // Act
            RestResponse<TestResponse> response = await client.ExecuteAsync<TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }
    }
}

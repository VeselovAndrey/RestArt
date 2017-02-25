// -----------------------------------------------------------------------
// <copyright file="PostContentRequestTests.cs">
// Copyright (c) 2016-2017 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Core.IntegrationTests
{
	using System.Collections.Generic;
	using System.IO;
	using System.Net;
	using System.Threading.Tasks;
	using RestArt.Core.Requests;
	using Xunit;

	public class PostContentRequestTests
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

        public PostContentRequestTests()
        {
            this._client = new RestArtClient(this._restUrl);
            this._client.AddOrUpdatePersistentHeader("PersistentHeader", "Persistent-Value");
        }

        [Fact]
        public async Task ExecutePostWithStreamContentAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item created";
            const int expectedResponseCode = 200;

            var content = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            var request = new PostContentRestRequest("RestArt", content, this._headers, this._parameters);

            // Act
            IRestResponse<Models.TestResponse> response = await this._client.ExecuteAsync<Models.TestResponse>(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Raw);
            Assert.NotNull(response.Value);
            Assert.Equal(expectedResponseCode, response.Value.Code);
            Assert.Equal(expectedResponseMessage, response.Value.Msg);
        }

        [Fact]
        public async Task ExecutePostWithStreamContentButWithoutHeadersAndParametersAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item created";
            const int expectedResponseCode = 200;

            var content = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });
            var request = new PostContentRestRequest("RestArt", content, null, null);

            // Act
            IRestResponse<Models.TestResponse> response = await this._client.ExecuteAsync<Models.TestResponse>(request);

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

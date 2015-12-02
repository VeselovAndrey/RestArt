// -----------------------------------------------------------------------
// <copyright file="PostContentRequestTests.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// License: Apache License 2.0
// Contacts: http://andrey.moveax.com andrey@moveax.com
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using RestArt.Requests;
    using RestArt.Tests.Models;
    using Xunit;

    public class PostContentRequestTests
    {
        private readonly string _restUrl = "http://demo8536676.mockable.io/";

        [Fact]
        public async Task ExecutePostWithStreamContentAsync()
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

            var content = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 });

            var request = new PostContentRestRequest("RestArt", content, headers, parameters);

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

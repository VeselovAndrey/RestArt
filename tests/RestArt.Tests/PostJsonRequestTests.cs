// -----------------------------------------------------------------------
// <copyright file="PostContentRequestTests.cs">
// Copyright (c) 2015 Andrey Veselov. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace RestArt.UnitTests
{
    using System.Net;
    using System.Threading.Tasks;
    using RestArt.Requests;
    using RestArt.UnitTests.Models;
    using Xunit;

    public class PostJsonRequestTests
    {
        private readonly string _restUrl = "http://demo8536676.mockable.io/";

        [Fact]
        public async Task ExecuteJsonPostAsync()
        {
            // Arrange
            const string expectedResponseMessage = "Item created";
            const int expectedResponseCode = 200;
            
            var jsonObject = new {
                Id = 42,
                Name = "Test Object",
                Delta = 56.78f
            };
            
            var command = new PostJsonRequest("RestArt", jsonObject, null, null);

            var client = new RestArtClient(this._restUrl);
            client.AddOrUpdatePersistentHeader("PersistentHeader", "ph-value");

            // Act
            RestResponse<TestResponse> response = await client.ExecuteAsync<TestResponse>(command);

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
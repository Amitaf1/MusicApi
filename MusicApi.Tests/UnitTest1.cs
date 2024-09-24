using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace MusicApi.Tests
{
    public class MusicControllerTests : IClassFixture<WebApplicationFactory<MusicApi.Startup>>
    {
        private readonly HttpClient _client;

        public MusicControllerTests(WebApplicationFactory<MusicApi.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAlbums_ReturnsOkResult_WithAlbums()
        {
            // Act
            var response = await _client.GetAsync("/music");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var albums = await response.Content.ReadAsAsync<AlbumResponse[]>();
            albums.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetAlbumById_ReturnsOkResult_WithExistingAlbum()
        {
            // Arrange
            int existingAlbumId = 1; // Assuming album with ID 1 exists

            // Act
            var response = await _client.GetAsync($"/music/{existingAlbumId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var album = await response.Content.ReadAsAsync<AlbumResponse>();
            album.Should().NotBeNull();
            album.Id.Should().Be(existingAlbumId);
        }

        [Fact]
        public async Task GetAlbumById_ReturnsNotFound_WithNonExistingAlbum()
        {
            // Arrange
            int nonExistingAlbumId = 999; // Assuming this ID does not exist

            // Act
            var response = await _client.GetAsync($"/music/{nonExistingAlbumId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateAlbum_ReturnsCreatedResult_WithNewAlbum()
        {
            // Arrange
            var newAlbumRequest = new AlbumRequest
            {
                Artist = "New Artist",
                Name = "New Album",
                ReleaseDate = DateTime.Now,
                Genre = "Pop",
                Price = 9.99M
            };

            var content = new StringContent(JsonConvert.SerializeObject(newAlbumRequest), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/music", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var createdAlbum = await response.Content.ReadAsAsync<AlbumResponse>();
            createdAlbum.Should().NotBeNull();
            createdAlbum.Artist.Should().Be(newAlbumRequest.Artist);
        }

        [Fact]
        public async Task UpdateAlbum_ReturnsNoContent_WhenAlbumUpdated()
        {
            // Arrange
            int existingAlbumId = 4;
            var updatedAlbumRequest = new AlbumRequest
            {
                Artist = "Chappell Roan",
                Name = "The Rise and Fall of a Midwest Princess",
                Genre = "Pop",
                Price = 666
            };

            var content = new StringContent(JsonConvert.SerializeObject(updatedAlbumRequest), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/music/{existingAlbumId}", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteAlbum_ReturnsNoContent_WhenAlbumDeleted()
        {
            // Arrange
            int existingAlbumId = 4;

            // Act
            var response = await _client.DeleteAsync($"/music/{existingAlbumId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}

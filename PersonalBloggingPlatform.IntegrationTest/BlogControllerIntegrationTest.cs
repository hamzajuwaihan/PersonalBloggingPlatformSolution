using FluentAssertions;
using PersonalBloggingPlatform.Core.DTO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace PersonalBloggingPlatform.IntegrationTest
{
    public class BlogControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public BlogControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        #region AddingAndRetrievingBlogsTests
        [Fact]
        public async Task GetAllBlogs_ToReturnEmptyList()
        {
            //Arrange



            //Act
            HttpResponseMessage response = await _client.GetAsync("/api/blogs");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        }

        [Fact]
        public async Task GetAllBlogs_WithBlogs_ReturnsListOfBlogs()
        {
            // Arrange
            var addBlogRequest = new AddBlogRequest
            {
                BlogTitle = "Test Blog",
                BlogBody = "This is a test blog content"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/blogs", addBlogRequest);
            postResponse.EnsureSuccessStatusCode();

            // Act
            var getResponse = await _client.GetAsync("/api/blogs");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var blogs = await getResponse.Content.ReadFromJsonAsync<List<BlogResponse>>();
            blogs.Should().NotBeNull();
            blogs.Count.Should().Be(1);

        }
        #endregion
    }
}
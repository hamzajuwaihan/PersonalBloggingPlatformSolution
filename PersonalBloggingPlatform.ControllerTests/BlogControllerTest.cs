using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonalBloggingPlatform.Core.DTO;
using PersonalBloggingPlatform.Core.ServiceContracts;
using PersonalBloggingPlatform.Web.Controllers;

namespace PersonalBloggingPlatform.ControllerTests
{
    public class BlogControllerTest
    {
        private readonly IBlogAddService _blogAddService;
        private readonly IBlogGetService _blogGetService;
        private readonly Mock<IBlogAddService> _blogAddServiceMock;
        private readonly Mock<IBlogGetService> _blogGetServiceMock;
        private readonly IFixture _fixture;

        public BlogControllerTest()
        {
            _fixture = new Fixture();
            _blogAddServiceMock = new Mock<IBlogAddService>();
            _blogGetServiceMock = new Mock<IBlogGetService>();
            _blogAddService = _blogAddServiceMock.Object;
            _blogGetService = _blogGetServiceMock.Object;
            
        }

        #region AddBlogTests

        [Fact]
        public async Task BlogController_AddBlog_ValidBlog_ReturnsOk()
        {
            //Arrange
            var request = _fixture.Create<AddBlogRequest>();
            var response = _fixture.Create<BlogResponse>();

            _blogAddServiceMock.Setup(temp => temp.AddBlog(It.IsAny<AddBlogRequest>())).ReturnsAsync(response);

            var controller = new BlogsController(_blogAddService, _blogGetService);

            //Act
            var result = await controller.AddBlog(request);

            //Assert
            result.Should().NotBeNull();
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().Be(response);
        }

        [Fact]
        public async Task BlogController_AddBlog_NullBlog_ReturnsInternalServerError()
        {
            //Arrange
            AddBlogRequest request = null;

            _blogAddServiceMock.Setup(temp => temp.AddBlog(It.IsAny<AddBlogRequest>())).ThrowsAsync(new NullReferenceException());

            var controller = new BlogsController(_blogAddService, _blogGetService);

            //Act
            var result = await controller.AddBlog(request);

            //Assert
            result.Should().NotBeNull();
            var statusResult = result as ObjectResult;
            statusResult.Should().NotBeNull();
            statusResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task BlogController_AddBlog_InvalidBlogTitle_ReturnsInternalServerError()
        {
            //Arrange
            var request = _fixture.Create<AddBlogRequest>();

            _blogAddServiceMock.Setup(temp => temp.AddBlog(It.IsAny<AddBlogRequest>())).ThrowsAsync(new ArgumentException());

            var controller = new BlogsController(_blogAddService, _blogGetService);

            //Act
            var result = await controller.AddBlog(request);

            //Assert
            result.Should().NotBeNull();
            var statusResult = result as ObjectResult;
            statusResult.Should().NotBeNull();
            statusResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task BlogController_AddBlog_InvalidBlogBody_ReturnsInternalServerError()
        {
            //Arrange
            var request = _fixture.Build<AddBlogRequest>().Without(temp => temp.BlogBody).Create();

            _blogAddServiceMock.Setup(temp => temp.AddBlog(It.IsAny<AddBlogRequest>())).ThrowsAsync(new ArgumentException());

            var controller = new BlogsController(_blogAddService, _blogGetService);

            //Act
            var result = await controller.AddBlog(request);

            //Assert
            result.Should().NotBeNull();
            var statusResult = result as ObjectResult;
            statusResult.Should().NotBeNull();
            statusResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        #endregion

        #region GetAllBlogsTests

        [Fact]
        public async Task BlogController_GettAllBlogs_EmptyList()
        {
            //Arrange
            List<BlogResponse> blogResponses = _fixture.CreateMany<BlogResponse>(0).ToList();

            _blogGetServiceMock.Setup(temp => temp.GetAllBlogs()).ReturnsAsync(blogResponses);

            var controller = new BlogsController(_blogAddServiceMock.Object, _blogGetServiceMock.Object);

            //Act
            var result = await controller.GetAllBlogs();

            //Assert
            result.Should().NotBeNull();

            var noContentResult = result.Result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }


        [Fact]
        public async Task BlogController_GettAllBlogs_ValidList()
        {

            //Arrange
            List<BlogResponse> blogResponses = _fixture.CreateMany<BlogResponse>(5).ToList();

            _blogGetServiceMock.Setup(temp => temp.GetAllBlogs()).ReturnsAsync(blogResponses);

            var controller = new BlogsController(_blogAddService, _blogGetService);

            //Act
            var result = await controller.GetAllBlogs();

            //assert
            result.Should().NotBeNull();
            var actionResult = result as ActionResult<List<BlogResponse>>;
            var statusCode = result.Result as OkObjectResult;
        }

        #endregion

        #region GetBlogByIdTests

        [Fact]
        public async Task GetBlogById_NullId_ReturnsBadRequest()
        {
            //Arrange
            Guid id = Guid.Empty;
            var controller = new BlogsController(_blogAddServiceMock.Object, _blogGetServiceMock.Object);

            //Act
            var result = await controller.GetBlogById(id);

            //Assert
            result.Should().NotBeNull();
            var badRequestResult = result.Result as BadRequestResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);

        }

        [Fact]
        public async Task GetBlogById_ValidId_ReturnsBlogResponse()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            var blogResponse = _fixture.Create<BlogResponse>();

            _blogGetServiceMock.Setup(temp => temp.GetBlogById(id)).ReturnsAsync(blogResponse);

            var controller = new BlogsController(_blogAddServiceMock.Object, _blogGetServiceMock.Object);

            //Act

            var result = await controller.GetBlogById(id);

            //Assert
            result.Should().NotBeNull();
            var actionResult = result as ActionResult<BlogResponse>;
            actionResult.Should().NotBeNull();
            actionResult.Value.Should().Be(blogResponse);

        }

        #endregion
    }
}
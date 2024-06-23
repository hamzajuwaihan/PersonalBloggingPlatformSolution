using AutoFixture;
using FluentAssertions;
using Moq;
using PersonalBloggingPlatform.Core.Domain.Entities;
using PersonalBloggingPlatform.Core.Domain.RepositoryContracts;
using PersonalBloggingPlatform.Core.DTO;
using PersonalBloggingPlatform.Core.ServiceContracts;
using PersonalBloggingPlatform.Core.Services;

namespace PersonalBloggingPlatform.ServiceTests
{
    public class BlogServiceTests
    {
        private readonly IBlogAddService _blogAddService;
        private readonly IBlogRepository _blogRepository;
        private readonly Mock<IBlogRepository> _blogRepositoryMock;


        private readonly IFixture _fixture;


        public BlogServiceTests()
        {
            _fixture = new Fixture();
            _blogRepositoryMock = new Mock<IBlogRepository>();
            _blogRepository = _blogRepositoryMock.Object;
            _blogAddService = new AddBlogService(_blogRepository);
        }

        #region AddBlogTests
        //When adding a blog 
        [Fact]
        public async Task AddBlog_NullBlog_ToBeNullReferenceException()
        {
            //Arrange
            var request = (AddBlogRequest)null;

            //Act
            Func<Task> action = async () => await _blogAddService.AddBlog(request);

            //Assert
            await action.Should().ThrowAsync<NullReferenceException>(); // Expecting NullReferenceException
        }

        [Fact]
        public async Task AddBlog_ValidBlog_ReturnValidBlog()
        {

            //Arrange
            var request = _fixture.Create<AddBlogRequest>();
            Blog blog = request.ToBlog();

            AddBlogResponse blogResponseExpected = blog.ToAddBlogResponse();

            _blogRepositoryMock.Setup(temp => temp.AddBlog(It.IsAny<Blog>())).ReturnsAsync(blog);

            //Act
            AddBlogResponse result = await _blogAddService.AddBlog(request);

            blogResponseExpected.Id = result.Id;


            //Assert
            result.Should().NotBeNull();
            blogResponseExpected.Should().NotBe(Guid.Empty);
            blogResponseExpected.Should().Be(result);
        }

        [Fact]
        public async Task AddBlog_InvalidBlogTitle_ToBeArgumentException()
        {
            //Arrange

            AddBlogRequest addBlogRequest = _fixture.Build<AddBlogRequest>().Without(temp => temp.BlogTitle).Create();

            //Act
            Func<Task> action = async () => await _blogAddService.AddBlog(addBlogRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>(); // Expecting ArgumentException


        }

        [Fact]
        public async Task AddBlog_InvalidBlogBody_ToBeArgumentException()
        {
            //Arrange

            AddBlogRequest addBlogRequest = _fixture.Build<AddBlogRequest>().Without(temp => temp.BlogBody).Create();

            //Act
            Func<Task> action = async () => await _blogAddService.AddBlog(addBlogRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentException>(); // Expecting ArgumentException


        }
        #endregion
    }
}
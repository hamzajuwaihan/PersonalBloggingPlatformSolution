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
        private readonly IBlogGetService _blogGetService;
        private readonly IFixture _fixture;
        private readonly IBlogDeleteService _deleteBlogService;
        private readonly IBlogUpdateService _updateBlogService;

        public BlogServiceTests()
        {
            _fixture = new Fixture();
            _blogRepositoryMock = new Mock<IBlogRepository>();
            _blogRepository = _blogRepositoryMock.Object;
            _blogAddService = new AddBlogService(_blogRepository);
            _blogGetService = new GetBlogService(_blogRepository);
            _deleteBlogService = new DeleteBlogService(_blogRepository);
            _updateBlogService = new UpdateBlogService(_blogRepository);

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

            BlogResponse blogResponseExpected = blog.ToAddBlogResponse();

            _blogRepositoryMock.Setup(temp => temp.AddBlog(It.IsAny<Blog>())).ReturnsAsync(blog);

            //Act
            BlogResponse result = await _blogAddService.AddBlog(request);

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


        #region GetBlogTests

        [Fact]
        public async Task GetBlog_NullId()
        {

            //Arrange
            Guid id = Guid.Empty;

            //Act
            Func<Task> action = async () => await _blogGetService.GetBlogById(id);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>(); // Expecting ArgumentException
        }

        [Fact]
        public async Task GetBlog_ValidId()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Blog blog = _fixture.Build<Blog>().With(b => b.Id, id).Create(); // Ensure the blog has the expected ID

            _blogRepositoryMock.Setup(temp => temp.GetBlogById(id)).ReturnsAsync(blog); // Setup to return the blog with the specific ID

            // Act
            BlogResponse result = await _blogGetService.GetBlogById(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }


        [Fact]
        public async Task GetBlog_InvalidId()
        {
            // Arrange
            Guid invalidId = Guid.NewGuid();

            // Setup the repository mock to return null when GetBlogById is called with the invalid ID
            _blogRepositoryMock.Setup(temp => temp.GetBlogById(invalidId)).ReturnsAsync((Blog?)null);

            // Act
            BlogResponse? result = await _blogGetService.GetBlogById(invalidId);

            // Assert
            result.Should().BeNull();
        }


        [Fact]
        public async Task GetBlog_GetAllBlogsPopulatedList()
        {

            //Arrange
            List<Blog> blogs = _fixture.CreateMany<Blog>().ToList();

            _blogRepositoryMock.Setup(temp => temp.GetAllBlogs()).ReturnsAsync(blogs);

            //Act
            List<BlogResponse> result = await _blogGetService.GetAllBlogs();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(blogs.Count);
        }

        [Fact]
        public async Task GetBlog_GetAllBlogs_EmptyList()
        {

            //Arrange
            List<Blog> blogs = new List<Blog>();

            _blogRepositoryMock.Setup(temp => temp.GetAllBlogs()).ReturnsAsync(blogs);

            //Act
            List<BlogResponse> result = await _blogGetService.GetAllBlogs();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(0);
        }

        #endregion

        #region DeleteBlogTests

        [Fact]
        public async Task DeleteBlog_NullId()
        {
            //Arrange
            Guid id = Guid.Empty;

            //Act
            Func<Task> action = async () => await _deleteBlogService.DeleteBlog(id);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>(); // Expecting ArgumentNullException
        }

        [Fact]
        public async Task DeleteBlog_ValidId()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            _blogRepositoryMock.Setup(temp => temp.DeleteBlog(id)).ReturnsAsync(true);

            // Act
            bool result = await _deleteBlogService.DeleteBlog(id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteBlog_InvalidId()
        {
            // Arrange
            Guid invalidId = Guid.NewGuid();

            _blogRepositoryMock.Setup(temp => temp.DeleteBlog(invalidId)).ReturnsAsync(false);

            // Act
            bool result = await _deleteBlogService.DeleteBlog(invalidId);

            // Assert
            result.Should().BeFalse();
        }
        #endregion

        #region UpdateBlogTests
        [Fact]
        public async Task UpdateBlog_NullId()
        {
            //Arrange
            UpdateBlogRequest updateBlogRequest = _fixture.Build<UpdateBlogRequest>().Without(x => x.Id).Create();
            //Act
            Func<Task> action = async () => await _updateBlogService.UpdateBlogById(updateBlogRequest);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>(); // Expecting ArgumentNullException
        }

        [Fact]
        public async Task UpdateBlog_NullBlogRequest()
        {
            //Arrange
           
            UpdateBlogRequest blog = null;

            //Act
            Func<Task> action = async () => await _updateBlogService.UpdateBlogById(blog);

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>(); // Expecting ArgumentNullException
        }

        [Fact]
        public async Task UpdateBlog_ValidBlog()
        {
            // Arrange
            
            UpdateBlogRequest updateBlogRequest = _fixture.Create<UpdateBlogRequest>();
            Blog blog = updateBlogRequest.ToBlog();

            
            
            _blogRepositoryMock.Setup(temp => temp.UpdateBlogById(It.IsAny<Blog>())).ReturnsAsync(blog);

            // Act
            var result = await _updateBlogService.UpdateBlogById(updateBlogRequest);

            
            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(blog.Id);
        }

        #endregion
    }



}

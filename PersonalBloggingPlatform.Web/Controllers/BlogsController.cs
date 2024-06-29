using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBloggingPlatform.Core.DTO;
using PersonalBloggingPlatform.Core.ServiceContracts;

namespace PersonalBloggingPlatform.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogAddService _blogAddService;
        private readonly IBlogGetService _blogGetService;
        private readonly IBlogDeleteService _deleteBlogService;
        private readonly IBlogUpdateService _updateBlogService;

        public BlogsController(IBlogAddService blogAddService, IBlogGetService blogGetService, IBlogDeleteService deleteBlogService, IBlogUpdateService updateBlogService)
        {
            _blogAddService = blogAddService;
            _blogGetService = blogGetService;
            _deleteBlogService = deleteBlogService;
            _updateBlogService = updateBlogService;
        }
        /// <summary>
        /// Adds a blog to the database.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Newly added blog object</returns>
        [HttpPost]
        public async Task<IActionResult> AddBlog([FromBody] AddBlogRequest request)
        {
            try
            {
                var response = await _blogAddService.AddBlog(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all blogs from DB.
        /// </summary>
        /// <returns>List of Blogs</returns>
        [HttpGet]
        public async Task<ActionResult<List<BlogResponse>>> GetAllBlogs()
        {
            var result = await _blogGetService.GetAllBlogs();

            if(result.Count == 0)
            {
                return NoContent();
            }

            return result;
        }

        /// <summary>
        /// Search for Blog based on ID.
        /// </summary>
        /// <param name="id">Blog Id</param>
        /// <returns>Blog or not found</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogResponse>> GetBlogById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }
            var result = await _blogGetService.GetBlogById(id);

            if(result == null)
            {
                return NotFound();
            }

            return result;
        }

        /// <summary>
        /// To delete a blog based on ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean value</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBlogById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return BadRequest();
            }

            var result = await _deleteBlogService.DeleteBlog(id);

            if(result)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// Update blog based on ID.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="blogToBeUpdated"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<BlogResponse>> UpdateBlogById(Guid Id, [FromBody] UpdateBlogRequest blogToBeUpdated)
        {

            if(Id == Guid.Empty)
            {
                return BadRequest();
            }

            var blog = await _blogGetService.GetBlogById(Id);

            if(blog == null)
            {
                return NotFound();
            }

            var result = await _updateBlogService.UpdateBlogById(blogToBeUpdated);

            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

    }
}

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

        public BlogsController(IBlogAddService blogAddService, IBlogGetService blogGetService)
        {
            _blogAddService = blogAddService;
            _blogGetService = blogGetService;
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
            var result = await _blogGetService.GetBlogById(id);

            if(result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}

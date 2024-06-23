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

        public BlogsController(IBlogAddService blogAddService)
        {
            _blogAddService = blogAddService;
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
    }
}

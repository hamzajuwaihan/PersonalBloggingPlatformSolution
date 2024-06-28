using PersonalBloggingPlatform.Core.Domain.Entities;
using PersonalBloggingPlatform.Core.Domain.RepositoryContracts;
using PersonalBloggingPlatform.Core.DTO;
using PersonalBloggingPlatform.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.Services
{
    public class GetBlogService : IBlogGetService
    {
        private readonly IBlogRepository _blogRepoistory;

        public GetBlogService(IBlogRepository blogRepository)
        {
            _blogRepoistory = blogRepository;
        }

        public async Task<List<BlogResponse>> GetAllBlogs()
        {
            List<Blog> blogs = await _blogRepoistory.GetAllBlogs();

            if (blogs.Count == 0)
            {
                return new List<BlogResponse>();
            }

            return blogs.Select(blog => blog.ToBlogResponse()).ToList();
        }

        public async Task<BlogResponse?> GetBlogById(Guid blogID)
        {
            if (blogID == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            Blog? result = await _blogRepoistory.GetBlogById(blogID);

            if (result == null)
            {
                return null;
            }

            return result.ToBlogResponse();
        }
    }
}

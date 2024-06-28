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
    public class AddBlogService : IBlogAddService
    {
        private readonly IBlogRepository _blogRepository;

        public AddBlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<BlogResponse> AddBlog(AddBlogRequest? request)
        {
            if(request == null)
            {
                throw new NullReferenceException();
            }

            if(string.IsNullOrEmpty(request.BlogTitle) || string.IsNullOrEmpty(request.BlogBody))
            {
                throw new ArgumentException();
            }

         

            Blog newBlog = request.ToBlog();

            newBlog.Id = Guid.NewGuid();
            newBlog.CreatedAt = DateTime.Now;

            Blog response = await _blogRepository.AddBlog(newBlog);

            return response.ToAddBlogResponse() ;

        }
    }
}

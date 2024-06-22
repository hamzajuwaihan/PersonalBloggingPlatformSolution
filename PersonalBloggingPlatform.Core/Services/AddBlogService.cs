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

        public Task<AddBlogResponse> AddBlog(AddBlogRequest? request)
        {

            throw new NotImplementedException();
        }
    }
}

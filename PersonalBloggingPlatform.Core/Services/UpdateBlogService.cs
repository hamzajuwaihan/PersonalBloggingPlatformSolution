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
    public class UpdateBlogService : IBlogUpdateService
    {
        private readonly IBlogRepository _blogRepoistory;

        public UpdateBlogService(IBlogRepository blogRepository)
        {
            _blogRepoistory = blogRepository;
        }

        public async Task<BlogResponse?> UpdateBlogById(UpdateBlogRequest? updateBlogRequest)
        {
            if(updateBlogRequest?.Id == Guid.Empty || updateBlogRequest == null)
            {
                throw new ArgumentNullException();
            }
            
            var result = await _blogRepoistory.UpdateBlogById(updateBlogRequest.ToBlog());

            if(result == null)
            {
                return null;
            }

            return result.ToBlogResponse();
        }
    }
}

using PersonalBloggingPlatform.Core.Domain.RepositoryContracts;
using PersonalBloggingPlatform.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.Services
{
    public class DeleteBlogService : IBlogDeleteService
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public Task<bool> DeleteBlog(Guid? blogID)
        {
            if(blogID == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            var result = _blogRepository.DeleteBlog(blogID);
            return result;
        }
    }
}

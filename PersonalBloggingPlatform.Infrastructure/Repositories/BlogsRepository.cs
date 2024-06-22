using PersonalBloggingPlatform.Core.Domain.Entities;
using PersonalBloggingPlatform.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Infrastructure.Repositories
{
    public class BlogsRepository : IBlogRepository
    {
        public Task<Blog> AddBlog(Blog blog)
        {
            throw new NotImplementedException();
        }
    }
}

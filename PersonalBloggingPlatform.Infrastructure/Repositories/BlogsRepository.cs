using PersonalBloggingPlatform.Core.Domain.Entities;
using PersonalBloggingPlatform.Core.Domain.RepositoryContracts;
using PersonalBloggingPlatform.Infrastructure.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Infrastructure.Repositories
{
    public class BlogsRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Blog> AddBlog(Blog blog)
        {
            await _context.Blogs.AddAsync(blog);

            await _context.SaveChangesAsync();

            return blog;
        }
    }
}

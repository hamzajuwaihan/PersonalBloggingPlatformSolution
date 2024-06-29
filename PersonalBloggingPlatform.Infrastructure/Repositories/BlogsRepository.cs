using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> DeleteBlog(Guid? blogID)
        {
            var result = await _context.Blogs.FirstOrDefaultAsync(blog => blog.Id == blogID);
            if (result != null) {
                _context.Blogs.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Blog>> GetAllBlogs()
        {
            return await _context.Blogs.ToListAsync();


        }

        public async Task<Blog?> GetBlogById(Guid id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(blog => blog.Id == id);
        }
    }
}

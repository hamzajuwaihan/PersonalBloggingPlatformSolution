using PersonalBloggingPlatform.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.DTO
{
    public class AddBlogResponse
    {
        public Guid Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogBody { get; set; }
        public DateTime CreatedAt { get; set; }

        
    }

    public static class AddBlogResponseExtensions
    {
        public static AddBlogResponse ToAddBlogResponse(this Blog blog)
        {
            return new AddBlogResponse
            {
                Id = blog.Id,
                BlogTitle = blog.BlogTitle,
                BlogBody = blog.BlogBody,
                CreatedAt = blog.CreatedAt
            };
        }
    }

     
}

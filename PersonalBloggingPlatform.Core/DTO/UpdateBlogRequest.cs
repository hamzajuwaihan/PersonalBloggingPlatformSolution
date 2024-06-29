using PersonalBloggingPlatform.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.DTO
{
    public class UpdateBlogRequest
    {
        public Guid Id { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogBody { get; set; }
        
        public Blog ToBlog()
        {
            return new Blog
            {
                Id = Id,
                BlogTitle = BlogTitle,
                BlogBody = BlogBody
            };
        }
    }
}

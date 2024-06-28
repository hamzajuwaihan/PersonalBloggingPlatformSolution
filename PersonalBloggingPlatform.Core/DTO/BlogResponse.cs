using PersonalBloggingPlatform.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.DTO
{
    public class BlogResponse
    {
        public Guid Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogBody { get; set; }
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object? obj)
        {

            if(obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(BlogResponse)) return false;

            BlogResponse blogToCompare = (BlogResponse)obj;


            return this.Id == blogToCompare.Id &&
                this.BlogTitle == blogToCompare.BlogTitle &&
                this.BlogBody == blogToCompare.BlogBody &&
                this.CreatedAt == blogToCompare.CreatedAt;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class AddBlogResponseExtensions
    {
        public static BlogResponse ToAddBlogResponse(this Blog blog)
        {
            return new BlogResponse
            {
                Id = blog.Id,
                BlogTitle = blog.BlogTitle,
                BlogBody = blog.BlogBody,
                CreatedAt = blog.CreatedAt
            };
        }
        public static BlogResponse ToBlogResponse(this Blog blog)
        {
            return new BlogResponse
            {
                Id = blog.Id,
                BlogTitle = blog.BlogTitle,
                BlogBody = blog.BlogBody,
                CreatedAt = blog.CreatedAt
            };
        }
    }

    

     
}

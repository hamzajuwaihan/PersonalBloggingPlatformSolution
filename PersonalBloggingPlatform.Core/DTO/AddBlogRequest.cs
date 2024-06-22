using PersonalBloggingPlatform.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.DTO
{
    public class AddBlogRequest
    {
        //[Required(ErrorMessage = "Blog Title can't be blank")]
        public string? BlogTitle { get; set; }
        //[Required(ErrorMessage = "Blog Body can't be blank")]
        public string? BlogBody { get; set; }

        public Blog ToBlog()
        {
            return new Blog
            {
                BlogTitle = BlogTitle,
                BlogBody = BlogBody
            };
        }
        
    }
}

using PersonalBloggingPlatform.Core.Domain.Entities;
using PersonalBloggingPlatform.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.ServiceContracts
{
    public interface IBlogGetService
    {

        public Task<BlogResponse?> GetBlogById(Guid blogID);

        public Task<List<BlogResponse>?> GetAllBlogs();



    }
}

using PersonalBloggingPlatform.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.ServiceContracts
{
    public interface IBlogAddService
    {
        /// <summary>
        /// Adds a new blog to the database.
        /// </summary>
        /// <param name="request"> blog to Add</param>
        /// <returns>Returns the same blog details, along with new generated Blog Guid.</returns>
        Task<AddBlogResponse> AddBlog(AddBlogRequest? request);
    }
}

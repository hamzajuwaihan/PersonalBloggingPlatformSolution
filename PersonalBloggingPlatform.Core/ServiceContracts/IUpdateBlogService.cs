using PersonalBloggingPlatform.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.ServiceContracts
{
    public interface IUpdateBlogService
    {

        public Task<BlogResponse?> UpdateBlogById(UpdateBlogRequest? updateBlogRequest);

    }
}

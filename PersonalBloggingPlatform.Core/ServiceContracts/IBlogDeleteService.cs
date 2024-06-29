using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBloggingPlatform.Core.ServiceContracts
{
    public interface IBlogDeleteService
    {

        public Task<bool> DeleteBlog(Guid? blogID);


    }
}

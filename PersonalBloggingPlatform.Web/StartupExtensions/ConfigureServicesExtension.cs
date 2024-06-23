using Microsoft.EntityFrameworkCore;
using PersonalBloggingPlatform.Core.Domain.RepositoryContracts;
using PersonalBloggingPlatform.Core.ServiceContracts;
using PersonalBloggingPlatform.Core.Services;
using PersonalBloggingPlatform.Infrastructure.AppDbContext;
using PersonalBloggingPlatform.Infrastructure.Repositories;

namespace PersonalBloggingPlatform.Web.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBlogRepository, BlogsRepository>();
            services.AddScoped<IBlogAddService, AddBlogService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            return services;

        }
    }
}

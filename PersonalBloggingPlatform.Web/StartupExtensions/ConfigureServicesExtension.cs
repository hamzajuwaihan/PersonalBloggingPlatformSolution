using Microsoft.AspNetCore.Mvc;
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
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
            });
            services.AddScoped<IBlogRepository, BlogsRepository>();
            services.AddScoped<IBlogAddService, AddBlogService>();
            services.AddScoped<IBlogGetService, GetBlogService>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
            });


            return services;

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Infrastructure;

namespace TestTask.Mvc.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddLibraryContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LibraryConnection"), 
                    serverOptions =>
                    {
                        serverOptions.MigrationsAssembly("TestTask.Migrations");
                    });
            });
        }
    }
}
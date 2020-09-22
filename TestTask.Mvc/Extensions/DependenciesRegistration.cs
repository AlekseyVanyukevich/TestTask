using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Domain.Mappers;
using TestTask.Domain.Services;

namespace TestTask.Mvc.Extensions
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ILibraryService, LibraryService>();
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(LibraryProfile));
        }
    }
}
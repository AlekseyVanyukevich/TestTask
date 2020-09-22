using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestTask.Infrastructure;
using TestTask.Infrastructure.Repositories;

namespace TestTask.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // var serviceProvider = new ServiceCollection()
            //     .AddLogging()
            //     .AddScoped<IBookRepository, BookRepository>()
            //     .AddDbContext<LibraryDbContext>()
            //     .BuildServiceProvider();
            //
            // var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
            //     .CreateLogger<Program>();
            // var bookService = serviceProvider.GetRequiredService<IBookRepository>();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddTransient<App>()
                        .AddScoped<ILibraryRepository, LibraryRepository>()
                        .AddDbContext<LibraryDbContext>(
                            options => options.UseSqlServer("server=.\\SQLEXPRESS;database=TestTask;Trusted_Connection=True;"));
                }).UseConsoleLifetime();
            
            var host = builder.Build();

            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;
            try
            {
                var app = services.GetRequiredService<App>();
                app.Run().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}

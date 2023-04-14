using GlobalBlueHomework.Application.DependencyInjections;
using GlobalBlueHomework.Api.Swagger.DependencyInjections;
using GlobalBlueHomework.Api.DependencyInjections;

namespace GlobalBlueHomework.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddControllers();

            services
                .AddApiDependencyInjections(configuration)
                .AddApplicationDependencyInjections()
                .ConfigureSwaggerGen(configuration: configuration);

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.UseSwagger(configuration: configuration);

            app.Run();
        }
    }
}
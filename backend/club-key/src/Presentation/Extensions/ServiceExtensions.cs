namespace Presentation.Extensions;

using Application;
using Infrastructure;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfraestructureServices(configuration);
    }

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost", builder =>
            {
                builder.AllowAnyOrigin() // El puerto de tu frontend
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}
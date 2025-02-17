namespace Infrastructure;

using Application.Contracts.Identity;
using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Models.Email;
using Application.Models.Token;
using Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Repositories;
using Services.Auth;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // ConnectionString settings
        var connectionString = configuration.GetConnectionString("ConnectionString")
                               ?? throw new ArgumentNullException(nameof(configuration), "La cadena de conexión no está configurada.");
        
        services.AddDbContext<ClubKeyDbContext>(options =>
            options.UseMySql(connectionString, 
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(ClubKeyDbContext).Assembly.FullName)
            ));

        // Email Service
        services.AddTransient<IEmailService, EmailService>();
        // Authentication Service
        services.AddTransient<IAuthService, AuthService>();
        
        // Generic Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

        return services;
    }
}
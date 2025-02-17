namespace Identity;

using System.Text;
using Application.Contracts.Identity;
using Application.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;
using Persistence;
using Services;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Jwt settings
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // ConnectionString settings
        var connectionString = configuration.GetConnectionString("IdentityConnectionString")
                               ?? throw new ArgumentNullException(nameof(configuration), "La cadena de conexión no está configurada.");

        services.AddDbContext<ClubKeyIdentityDbContext>(options =>
            options.UseMySql(connectionString, 
                ServerVersion.AutoDetect(connectionString),
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(ClubKeyIdentityDbContext).Assembly.FullName)
            ));
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ClubKeyIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] 
                                           ?? throw new ArgumentNullException(nameof(configuration), "La JWT key no está configurada.")))
            };
        });

        return services;
    }
}
namespace Presentation.Extensions;

using Domain.Users;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DatabaseExtensions
{
    public async static Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var provider = scope.ServiceProvider;

        try
        {
            var context = provider.GetRequiredService<ClubKeyDbContext>();
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            // 📌 Apply migrations
            await context.Database.MigrateAsync();

            // 📌 Load initial data
            await ClubKeyDbContextData.LoadDataAsync(context, userManager, roleManager, loggerFactory);
        }
        catch (Exception ex)
        {
            var logger = provider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocurrió un error al inicializar la base de datos.");
        }
    }
}
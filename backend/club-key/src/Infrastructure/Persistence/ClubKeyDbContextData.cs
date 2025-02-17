namespace Infrastructure.Persistence;

using Domain.Roles;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class ClubKeyDbContextData
{
    public async static Task LoadDataAsync(
        ClubKeyDbContext context,
        UserManager<User> usuarioManager,
        RoleManager<IdentityRole> roleManager,
        ILoggerFactory loggerFactory
    )
    {
        try
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            if(!usuarioManager.Users.Any())
            {
                var adminUser = new User
                {
                    Name = "Miguel",
                    LastName = "Barreto",
                    Email = "mabt2206@gmail.com",
                    NormalizedEmail = "MABT2206@GMAIL.COM",
                    UserName = "miguelbarreto",
                    NormalizedUserName = "MIGUELBARRETO",
                    PhoneNumber = "928798931",
                    EmailConfirmed = true
                };
                await usuarioManager.CreateAsync(adminUser, "M1gue!B@rreto2206");
                await usuarioManager.AddToRoleAsync(adminUser, Role.ADMIN);

                var operatorUser = new User
                {
                    Name = "Jimena",
                    LastName = "Sulca",
                    Email = "jimena.sulca@gmail.com",
                    NormalizedEmail = "JIMENA.SULCA@GMAIL.COM",
                    UserName = "jimenasulca",
                    NormalizedUserName = "JIMENASULCA",
                    PhoneNumber = "123567895",
                    EmailConfirmed = true
                };
                await usuarioManager.CreateAsync(operatorUser, "jimena!SulcA123$.");
                await usuarioManager.AddToRoleAsync(operatorUser, Role.USER);
                
                var supervisorUser = new User
                {
                    Name = "Carlos",
                    LastName = "Fernandez",
                    Email = "carlos.fernandez@gmail.com",
                    NormalizedEmail = "CARLOS.FERNANDEZ@GMAIL.COM",
                    UserName = "carlosfernandez",
                    NormalizedUserName = "CARLOSFERNANDEZ",
                    PhoneNumber = "987654321",
                    EmailConfirmed = true
                };
                await usuarioManager.CreateAsync(supervisorUser, "C@rlosF3rnandez!");
                await usuarioManager.AddToRoleAsync(supervisorUser, Role.USER);

                var guestUser = new User
                {
                    Name = "Lucia",
                    LastName = "Mendoza",
                    Email = "lucia.mendoza@gmail.com",
                    NormalizedEmail = "LUCIA.MENDOZA@GMAIL.COM",
                    UserName = "luciamendoza",
                    NormalizedUserName = "LUCIAMENDOZA",
                    PhoneNumber = "765432189",
                    EmailConfirmed = true
                };
                await usuarioManager.CreateAsync(guestUser, "LuciaMendoza@2024");
                await usuarioManager.AddToRoleAsync(guestUser, Role.ADMIN);
            }
        }
        catch(Exception e)
        {
            var logger = loggerFactory.CreateLogger<ClubKeyDbContext>();
            logger.LogError(e.Message);
        }
    }
}

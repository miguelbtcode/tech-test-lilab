namespace Presentation.Extensions;

using Domain.Users;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

public static class IdentityExtensions
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        IdentityBuilder identityBuilder = services.AddIdentityCore<User>();
        identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
        identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
        identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();
        identityBuilder.AddEntityFrameworkStores<ClubKeyDbContext>();
        identityBuilder.AddSignInManager<SignInManager<User>>();
    }
}
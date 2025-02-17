namespace Infrastructure.Services.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts.Identity;
using Application.Models.Token;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private JwtSettings _jwtSettings {get;}
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtSettings = jwtSettings.Value;
    }

    public string CreateToken(User usuario, IList<string>? roles)
    {
        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.NameId, usuario.UserName!),
            new("userId", usuario.Id),
            new("email", usuario.Email!)
        };
        claims.AddRange(roles!.Select(rol => new Claim(ClaimTypes.Role, rol)));

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }

    public string GetSessionUser()
    {
        var username = _httpContextAccessor.HttpContext!.User?.Claims?
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return username!;
    }
}

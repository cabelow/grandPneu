using GrandPneu.Api.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrandPneu.Api.Helpers;

public static class JwtHelper
{
    public static string GenerateToken(UserResponseDto user, IConfiguration config)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("id", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT_KEY"] ?? throw new InvalidOperationException("JWT_KEY não encontrado")));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["JWT_ISSUER"] ?? throw new InvalidOperationException("JWT_ISSUER não encontrado"),
            audience: config["JWT_AUDIENCE"] ?? throw new InvalidOperationException("JWT_AUDIENCE não encontrado"),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

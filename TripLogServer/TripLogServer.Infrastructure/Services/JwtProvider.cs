using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TripLogServer.Application.Services;
using TripLogServer.Domain.Entities;

namespace TripLogServer.Infrastructure.Services;
internal class JwtProvider(IConfiguration configuration) : IJwtProvider
{
    public string CreateToken(AppUser user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(ClaimTypes.Name,user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email,user.Email ?? string.Empty),
            new Claim("IsAuthor",user.IsAuthor.ToString(),ClaimValueTypes.Boolean)
        };

        DateTime expireDate = DateTime.Now.AddDays(1);

        SymmetricSecurityKey symmetricSecurityKey = new(
            Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecretKey").Value ?? ""));

        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken securityToken = new(
            issuer: configuration.GetSection("Jwt:Issuer").Value,
            audience: configuration.GetSection("Jwt:Audience").Value,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expireDate,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(securityToken);

        return token;
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookedIn.WebApi.Domain;
using Microsoft.IdentityModel.Tokens;

namespace BookedIn.WebApi.Auth;

public interface ITokenService
{
    string GenerateToken(User user);
}

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
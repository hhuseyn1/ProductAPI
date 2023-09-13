using Microsoft.IdentityModel.Tokens;
using Source.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Source.Services;

public static class JWTAuthService
{
    public static string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>() { 
            new Claim(ClaimTypes.Name,user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eifrubediweubrgufenidurgbnfiedurgnifediirfnemokefmnekfkenfnekfenf"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}

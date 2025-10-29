using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CursoInfoeste.Services
{
    public class TokenService(IConfiguration _configuration)
    {

        public string GenerateToken()
        {
            var claims = new List<Claim>()
            {
                new Claim("TenantId", "1"),
                new Claim("Nome", "Infoeste"),

            };

            var secret = _configuration.GetValue<string>("Security:Jwt:Secret");
            var audience = _configuration.GetValue<string>("Security:Jwt:Audience");
            var issuer = _configuration.GetValue<string>("Security:Jwt:Issuer");
            var expiresIn = _configuration.GetValue<int>("Security:Jwt:ExpiresIn");


            var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expiresIn),
            audience: audience, //Publico
            issuer: issuer, //Emissor
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), SecurityAlgorithms.HmacSha256Signature)
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

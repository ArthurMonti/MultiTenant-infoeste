using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CursoInfoeste.Services
{
    public class TokenService(Persistencia persistencia)
    {

        public string GenerateToken()
        {

            var claims = new List<Claim>()
            {
                new Claim("TenantId", persistencia.TenantId.ToString()),
                new Claim("Teste", "infoeste"),
                new Claim(ClaimTypes.Name, "infoeste")
            };

            var Secret = "63591fd534fc0f518cec6c60843c2cf9";
            var issuer = "infoeste";
            var audience = "infoeste";
            var ExpirationTime = 10;

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

            // Definindo as credenciais de assinatura (algoritmo HMACSHA256)
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(10),
            signingCredentials: credenciais
            );

            // Gerando o token JWT como string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

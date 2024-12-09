using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Desafio.Blog.Luza.Core.Domain.Entities;
using Desafio.Blog.Luza.Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Desafio.Blog.Luza.Adapters.Infrastructure.Auth
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(ApplicationUser usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var chaveJwt = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(chaveJwt))
            {
                throw new InvalidOperationException("A chave JWT ('Jwt:Key') não está configurada.");
            }

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveJwt));
            var creds = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlexPro.Api.Application.Interfaces;
using FlexPro.Api.Domain.Entities;
using FlexPro.Api.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace FlexPro.Api.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        public JwtTokenGenerator(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;

            _userManager = userManager;
        }
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]);
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            if (!string.IsNullOrWhiteSpace(user.Departamento))
                claims.Add(new Claim("Departamento", user.Departamento));

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            Log.Information("Token gerado: {Token}", tokenString);
            return tokenString;
        }
    }

}

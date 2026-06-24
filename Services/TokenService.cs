using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api_gestion_ecole.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api_gestion_ecole.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly  SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8
                                        .GetBytes(_configuration["JWT:SigninKey"] ?? ""));
        }

        public async Task<string> CreateToken(AppUser appUser, UserManager<AppUser> userManager)
        {
            List<Claim> claims = await GetAllValidClaimsAsync(appUser, userManager);

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires  = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]  
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<List<Claim>> GetAllValidClaimsAsync(AppUser appUser, UserManager<AppUser> userManager)
        {
            List<Claim> claims = [
                new Claim(JwtRegisteredClaimNames.Name, appUser.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email ?? string.Empty),
            ];
        
            var userRoles = await userManager.GetRolesAsync(appUser);
            if(userRoles == null) return claims;

            foreach(var userRole in userRoles)
            {
                if(userRole != null)
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }
    }
}
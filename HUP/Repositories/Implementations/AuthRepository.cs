using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HUP.Core.Entities.Identity;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HUP.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly HUPDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(HUPDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> LoginAsync(string nationalId, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NationalID == nationalId && u.IsActive);
            if (user == null)
                return null;
            if (!VerifyPassword(password, user.PasswordHash))
                return null;
            return user;
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.NationalID),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("roleId", user.RoleId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddHours(1);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expire,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Implement your hashing and comparison logic here. Example assumes SHA256 Hash
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HUP.Application.Services.Interfaces;
using HUP.Application.DTOs.AuthDtos;
using HUP.Application.DTOs.IdentityDtos;
using HUP.Repositories.Interfaces;
using HUP.Application.Mappers;
using HUP.Core.Entities.Identity;
using HUP.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HUP.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cache;
        private readonly IPermissionService _permission;

        public AuthService(IUserRepository authRepository,  IConfiguration configuration,  ICacheService cache, IPermissionService permission)
        {
            _repository = authRepository;
            _configuration = configuration;
            _cache = cache;
            _permission = permission;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            //add hashing technique
            var user = await _repository.GetByCredentialsAsync(loginDto.NationalId);
            if (user == null || user.PasswordHash != loginDto.Password)
                return null;
            
            var token = GenerateJwtToken(user);
            // role string format (key)
            // "user:{user.Id}:role"
            string key = $"user:{user.Id}:role"; 
            await _cache.SetAsync(key, user.RoleId.ToString(), 2); 
            await _permission.SetUserPermissionsAsync(user.Id, user.RoleId);
            
            return new AuthResponseDto{Token = token, User = UserMapper.ToDto(user)};
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.NationalId),
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
    }
}
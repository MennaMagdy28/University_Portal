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
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HUP.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cache;
        private readonly IPermissionService _permission;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _hasher;

        public AuthService(IUserRepository authRepository,  IConfiguration configuration,
            ICacheService cache, IPermissionService permission, IPasswordHasher<User> hasher, IUserService service)
        {
            _repository = authRepository;
            _configuration = configuration;
            _cache = cache;
            _permission = permission;
            _hasher = hasher;
            _userService = service;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Retrieve user by national ID
            var user = await _repository.GetByCredentialsAsync(loginDto.NationalId);
            if (user == null) return null;
            // Verify password using IPasswordHasher from Identity package
            var pass = _hasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (pass == PasswordVerificationResult.Failed) return null;
            
            var token = GenerateJwtToken(user);
            // role string format (key)
            // "user:{user.Id}:role"
            var key = $"user:{user.Id}:role";
            // cache the role for 2 minutes for security purposes
            await _cache.SetAsync(key, user.RoleId.ToString(), 2);
            // set user permissions in cache
            await _permission.SetUserPermissionsAsync(user.Id, user.RoleId);
 
            //profile status to navigate to update password or insert missing information
            var isExpired = user.PasswordExpiryDate <= DateTime.Now;
            var status = await _userService.GetProfileStatus(user.Id, isExpired);

            return new AuthResponseDto{Token = token, User = UserMapper.ToDto(user), Status = status};
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            { 
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("NationalId", user.NationalId),
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

        public async Task<bool> UpdatePassword(string currentPassword, string newPassword , Guid userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null) return false;
            // Verify current password
            var verification = _hasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);
            if (verification == PasswordVerificationResult.Failed) return false;
            // hash the new password
            var hashed = _hasher.HashPassword(user, newPassword);
            //update password
            user.PasswordHash = hashed;
            user.UpdatedAt = DateTime.Now;

            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
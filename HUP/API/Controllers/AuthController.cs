using HUP.Application.DTOs.LoginDtos;
using HUP.Application.DTOs.UserDtos;
using HUP.Core.Entities.UserModels;
using HUP.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetByNationalIdAsync(loginDto.NationalID);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid National ID or Password");
            }

            if (!user.IsActive)
            {
                return Unauthorized("Account is deactivated");
            }

            var token = GenerateJwtToken(user);
            var userDto = new UserResponseDto
            {
                Id = user.Id,
                NationalID = user.NationalID,
                UniversityEmail = user.UniversityEmail,
                FullName = user.FullName,
                Phone = user.Phone,
                Role = user.Role,
                IsActive = user.IsActive
            };

            return Ok(new LoginResponseDto
            {
                Token = token,
                User = userDto,
                Expiry = DateTime.UtcNow.AddHours(24)
            });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("NationalID", user.NationalID)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

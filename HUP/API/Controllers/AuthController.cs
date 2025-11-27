using Microsoft.AspNetCore.Mvc;
using HUP.Application.Services.Interfaces;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.Security.Claims;
using HUP.Application.DTOs.AuthDtos;
using Microsoft.AspNetCore.Authorization;

namespace HUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.NationalId) || string.IsNullOrEmpty(loginDto.Password))
                return BadRequest("National Id and Password are required.");

            var response = await _authService.LoginAsync(loginDto);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid National ID or Password." });
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePassword dto)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var success = await _authService.UpdatePassword(dto.CurrentPassword, dto.NewPassword, userId);
            if (!success) return BadRequest("Password change failed. Check your current password.");

            return Ok("Password updated successfully.");
        }
    }
}

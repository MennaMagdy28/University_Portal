using Microsoft.AspNetCore.Mvc;
using HUP.Application.Services.Interfaces;
using HUP.Core.DTOs.AuthDtos;
using System.Threading.Tasks;

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

            var userDto = await _authService.LoginAsync(loginDto);
            if (userDto == null)
                return Unauthorized("Invalid credentials.");

            var token = await _authService.GenerateJwtToken(userDto);
            return Ok(new { token, user = userDto });
        }
    }
}

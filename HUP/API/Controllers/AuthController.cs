using Microsoft.AspNetCore.Mvc;
using HUP.Application.Services.Interfaces;
using System.Threading.Tasks;
using System.Security.Authentication;
using HUP.Application.DTOs.AuthDtos;

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

            // 3. Return Result
            return Ok(response);
        }
    }
}

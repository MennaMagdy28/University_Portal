using HUP.Application.DTOs.UserDtos;

namespace HUP.Application.DTOs.LoginDtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserResponseDto User { get; set; }
        public DateTime Expiry { get; set; }
    }
}

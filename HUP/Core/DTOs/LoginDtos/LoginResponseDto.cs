using HUP.Core.DTOs.UserDtos;

namespace HUP.Core.DTOs.LoginDtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserResponseDto User { get; set; }
        public DateTime Expiry { get; set; }
    }
}

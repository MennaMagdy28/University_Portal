using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Application.DTOs.IdentityDtos;

namespace HUP.Application.DTOs.AuthDtos;

public class AuthResponseDto 
{ 
    public string Token { get; set; } 
    public UserDto User { get; set; }
    public ProfileStatus Status { get; set; }
}

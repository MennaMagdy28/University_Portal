using System.Threading.Tasks;
using HUP.Application.DTOs.AuthDtos;
using HUP.Application.DTOs.IdentityDtos;
using HUP.Core.Entities.Identity;

namespace HUP.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        string GenerateJwtToken(User userDto);
        Task<bool> UpdatePassword(string currentPass, string newPass, Guid userId);
    }
}

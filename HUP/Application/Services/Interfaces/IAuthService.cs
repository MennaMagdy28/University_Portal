using HUP.Core.DTOs.IdentityDtos;
using System.Threading.Tasks;
using HUP.Core.DTOs.AuthDtos;

namespace HUP.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> LoginAsync(LoginDto loginDto);
        Task<string> GenerateJwtToken(UserDTO userDto);
    }
}

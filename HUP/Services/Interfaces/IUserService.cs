using HUP.Core.DTOs.UserDtos;
using HUP.Core.Enums;

namespace HUP.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<IEnumerable<UserResponseDto>> GetUsersByRoleAsync(RoleType role);
        Task<UserResponseDto> CreateUserAsync(UserCreateDto userDto);
        Task<UserResponseDto> UpdateUserAsync(int id, UserUpdateDto userDto);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}

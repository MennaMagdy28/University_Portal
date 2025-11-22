using HUP.Application.Services.Interfaces;
using HUP.Core.DTOs.AuthDtos;
using HUP.Core.DTOs.IdentityDtos;
using HUP.Core.Entities.Identity;
using HUP.Repositories.Interfaces;
using HUP.Application.Mappers

namespace HUP.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _authRepository.LoginAsync(loginDto.NationalId, loginDto.Password);
            return UserMapper.ToDto(user);
        }

        Task<string> GenerateJwtToken(UserDTO userDto)
        {
            var user = UserMapper.ToEntity(userDto);
            var token = await _authRepository.GenerateJwtToken(user);
            return token;
        }
    }
}
using HUP.Application.DTOs.IdentityDtos;
using HUP.Core.Entities.Identity;
using Riok.Mapperly.Abstractions;

namespace HUP.Application.Mappers
{
    [Mapper]
    public static partial class UserMapper
    {
        // Map UserDto to User entity and vice versa
        public static partial User ToEntity(UserDto userDto);
        public static partial UserDto ToDto(User user);
    }
}
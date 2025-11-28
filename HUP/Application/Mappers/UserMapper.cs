using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Core.Entities.Identity;
using HUP.Core.Models;
using Riok.Mapperly.Abstractions;

namespace HUP.Application.Mappers
{
    [Mapper]
    public static partial class UserMapper
    {
        public static partial User ToEntity(UserDto userDto);
        public static partial UserDto ToDto(User user);
        public static partial UsersListResponse ToListDto(UserSummary user);
        public static partial ProfileInfoDto ToProfileDto(User user);
        public static partial PersonalInfoDto ToPersonalDto(UserPersonalInfo personalInfo);
        public static partial ContactInfoDto ToContactDto(UserContact contactInfo);
        public static partial UserPersonalInfo ToPersonalEntity(CreatePersonalInfo personalInfo);
        public static partial UserContact ToContactEntity(CreateContactInfo contactInfo);
        public static partial User ToCreateEntity(CreateUserDto userDto);



    }
}
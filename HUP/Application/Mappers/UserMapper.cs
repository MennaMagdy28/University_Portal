using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Core.Entities.Identity;
using HUP.Core.Models;
using Riok.Mapperly.Abstractions;

namespace HUP.Application.Mappers
{
    [Mapper(AllowNullPropertyAssignment = false)]
    public static partial class UserMapper
    {
        // Map UserDto to User entity and vice versa
        public static partial User ToEntity(UserDto userDto);
        public static partial UserDto ToDto(User user);
        public static partial UsersListResponse ToListDto(UserSummary user);

        public static ProfileInfoDto ToProfileDto(User user)
        {
            var dto = new ProfileInfoDto();
            dto.ContactInfo = ToContactDto(user.ContactInfo);
            dto.PersonalInfo = ToPersonalDto(user.PersonalInfo);
            dto.FullName = user.FullName;
            dto.Email = user.Email;
            dto.NationalId = user.NationalId;
            return dto;
        }
        public static partial PersonalInfoDto ToPersonalDto(UserPersonalInfo personalInfo);
        public static partial ContactInfoDto ToContactDto(UserContact contactInfo);
        public static partial UserPersonalInfo ToPersonalEntity(CreatePersonalInfo personalInfo);
        public static partial UserContact ToContactEntity(CreateContactInfo contactInfo);

        public static User ToCreateEntity(CreateUserDto userDto)
        {
            var user = new User();
            user.ContactInfo = ToContactEntity(userDto.ContactInfo);
            user.PersonalInfo = ToPersonalEntity(userDto.PersonalInfo);
            user.NationalId = userDto.NationalId;
            user.Email = userDto.Email;
            user.FullName = userDto.FullName;
            user.PasswordHash = userDto.PasswordHash;
            user.RoleId = userDto.RoleId;
            return user;
        }

        [MapProperty(nameof(UpdateInfoDto.FullEnglishName), nameof(User.PersonalInfo.FullEnglishName))]
        [MapProperty(nameof(UpdateInfoDto.Address), nameof(User.ContactInfo.Address))]
        [MapProperty(nameof(UpdateInfoDto.City), nameof(User.ContactInfo.City))]
        [MapProperty(nameof(UpdateInfoDto.BirthPlace), nameof(User.PersonalInfo.BirthPlace))]
        [MapProperty(nameof(UpdateInfoDto.AltEmail), nameof(User.ContactInfo.AltEmail))]
        [MapProperty(nameof(UpdateInfoDto.PhoneNumber), nameof(User.ContactInfo.PhoneNumber))]

        public static partial User ToUpdate(UpdateInfoDto infoDto);


    }
}
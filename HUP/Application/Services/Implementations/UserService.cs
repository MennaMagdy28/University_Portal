using System.Reflection;
using HUP.Application.DTOs.IdentityDtos;
using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Common.Helpers;
using HUP.Core.Entities.Identity;
using HUP.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HUP.Application.Services.Implementations;

public class UserService :IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _hasher;


    public UserService(IUserRepository repository, IPasswordHasher<User> passwordHasher)
    {
        _repository = repository;
        _hasher = passwordHasher;
    }
    public async Task<(UserPersonalInfo, UserContact)> GetUserInfo(Guid userId)
    {
        var data = await _repository.GetUserInformation(userId);
        return data;
    }


    // This method checks for missing information in UserPersonalInfo and UserContact
    public async Task<List<string?>> GetMissingInfo(Guid userId)
    {
        var data = await _repository.GetUserInformation(userId);
        //list to hold missing fields
        var missing = new List<string?>();
        //loop on all properties inside item1 (UserPersonalInformation)
        foreach (var prop in data.Item1.GetType().GetProperties())
        {
            //get value of the property
            var value = prop.GetValue(data.Item1);
            //check if value is null or empty
            if (ValidationHelper.IsValueEmpty(value))
            {
                //add property name to missing list
                missing.Add(prop.Name);
            }
        }
        //loop on all properties inside item1 (UserContact) same as above
        foreach (var prop in data.Item2.GetType().GetProperties())
        {
            var value = prop.GetValue(data.Item2);
            if (ValidationHelper.IsValueEmpty(value))
            {
                missing.Add(prop.Name);
            }
        }

        return missing;
    }
    public async Task<ProfileStatus> GetProfileStatus(Guid userId, bool isPasswordExpired)
    {
        ProfileStatus status = new() 
        {
            // set PasswordExpired based on the input parameter (from AuthService)
            PasswordExpired = isPasswordExpired,
            MissingFields = await GetMissingInfo(userId)
        };
        // Profile is incomplete if there are any missing fields
        status.ProfileIncomplete = status.MissingFields.Count > 0;
    
        return status; 
    }

    public async Task<IEnumerable<UsersListResponse>> GetAllUsers()
    {
        var users = await _repository.GetUserList();
        var usersList = users.Select(u => UserMapper.ToListDto(u));
        return usersList;
    }

    public async Task<ProfileInfoDto> GetUserById(Guid userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        var userProfileData = UserMapper.ToProfileDto(user);
        userProfileData.PersonalInfo = UserMapper.ToPersonalDto(user.PersonalInfo);
        userProfileData.ContactInfo = UserMapper.ToContactDto(user.ContactInfo);
        return userProfileData;
    }

    public async Task<bool> InsertMissingData(Guid userId, MissingInfoDto dto)
    {
    var user = await _repository.GetByIdAsync(userId);
    if (user == null) return false;
    
    var missingFields = await GetMissingInfo(userId);

    foreach (var field in missingFields)
    {
        switch (field)
        {
            // --- Personal Info Section ---
            case nameof(dto.BirthPlace):
                if (!string.IsNullOrEmpty(dto.BirthPlace))
                    user.PersonalInfo.BirthPlace = dto.BirthPlace;
                break;

            case nameof(MissingInfoDto.FullEnglishName):
                if (!string.IsNullOrEmpty(dto.FullEnglishName))
                    user.PersonalInfo.FullEnglishName = dto.FullEnglishName;
                break;
            // --- Contact Info Section ---
            case nameof(MissingInfoDto.Address):
                if (!string.IsNullOrEmpty(dto.Address))
                    user.ContactInfo.Address = dto.Address;
                break;

            case nameof(MissingInfoDto.City):
                if (!string.IsNullOrEmpty(dto.City))
                    user.ContactInfo.City = dto.City;
                break;

            case nameof(MissingInfoDto.PhoneNumber):
                if (!string.IsNullOrEmpty(dto.PhoneNumber))
                    user.ContactInfo.PhoneNumber = dto.PhoneNumber;
                break;

            case nameof(MissingInfoDto.AltEmail):
                if (!string.IsNullOrEmpty(dto.AltEmail))
                    user.ContactInfo.AltEmail = dto.AltEmail;
                break;

            case nameof(MissingInfoDto.Phone):
                if (!string.IsNullOrEmpty(dto.Phone))
                    user.ContactInfo.Phone = dto.Phone;
                break;
        }

    }
    await _repository.SaveChangesAsync();
    return true;
}

    public async Task AddAsync(CreateUserDto dto)
    {
        var user = UserMapper.ToCreateEntity(dto);
        user.Id = new Guid();
        user.CreatedAt = DateTime.Now;
        user.PasswordExpiryDate = DateTime.Now;
        var hashedPass = _hasher.HashPassword(user, dto.PasswordHash);
        user.PasswordHash = hashedPass;
        user.IsActive = true;
        user.PersonalInfo = UserMapper.ToPersonalEntity(dto.PersonalInfo);
        user.ContactInfo = UserMapper.ToContactEntity(dto.ContactInfo);
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> Exists(string nationalId)
    {
        var user = await _repository.GetByCredentialsAsync(nationalId);
        return (user != null);
    }
}
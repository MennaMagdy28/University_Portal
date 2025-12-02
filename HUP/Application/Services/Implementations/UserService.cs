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

public class UserService : IUserService
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
        return userProfileData;
    }

    public async Task<bool> InsertMissingData(Guid userId, UpdateInfoDto dto)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) return false;

        var missingFields = await GetMissingInfo(userId);

        user = UserMapper.ToUpdate(dto);

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
        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> Exists(string nationalId)
    {
        var user = await _repository.GetByCredentialsAsync(nationalId);
        return (user != null);
    }

    public async Task Remove(Guid userId)
    {
        await _repository.RemoveAsync(userId);
        await _repository.SaveChangesAsync();
    }

    public async Task<string?> SoftDelete(Guid userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) return null;
        user.IsDeleted = true;
        user.UpdatedAt = DateTime.Now;
        await _repository.SaveChangesAsync();
        return "Removed Successfully.";
    }

    public async Task<bool> Update(Guid userId, UpdateInfoDto dto)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null) return false;

        user = UserMapper.ToUpdate(dto);
        
        await _repository.SaveChangesAsync();
        return true;
    }
}
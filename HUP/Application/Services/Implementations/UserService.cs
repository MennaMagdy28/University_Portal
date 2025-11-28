using HUP.Application.DTOs.IdentityDtos;
using HUP.Application.Services.Interfaces;
using HUP.Common.Helpers;
using HUP.Core.Entities.Identity;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations;

public class UserService :IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
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
}
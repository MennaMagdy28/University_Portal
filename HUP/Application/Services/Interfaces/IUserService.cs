using HUP.Application.DTOs.IdentityDtos;
using HUP.Application.DTOs.IdentityDtos.UserDtos;
using HUP.Core.Entities.Identity;

namespace HUP.Application.Services.Interfaces;

public interface IUserService
{
    // This method retrieves user personal information and contact details
    public Task<(UserPersonalInfo, UserContact)> GetUserInfo(Guid userId);
    // This method checks for missing information in UserPersonalInfo and UserContact
    public Task<List<string?>> GetMissingInfo(Guid userId);
    // This method gets the overall profile status including missing fields and password expiration
    public Task<ProfileStatus> GetProfileStatus(Guid userId, bool isPasswordExpired);

    public Task<IEnumerable<UsersListResponse>> GetAllUsers();
    public Task<ProfileInfoDto> GetUserById(Guid userId);
    public Task<bool> InsertMissingData(Guid userId, UpdateInfoDto dto);
    public Task AddAsync(CreateUserDto dto);
    public Task<bool> Exists(string nationalId);
    public Task Remove(Guid userId);
    public Task<string> SoftDelete(Guid userId);
    public Task<bool> Update(Guid userId, UpdateInfoDto dto);
}
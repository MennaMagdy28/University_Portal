using HUP.Core.Entities.Identity;
using HUP.Core.Models;

namespace HUP.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByCredentialsAsync(string nationalId);
    Task<(UserPersonalInfo?, UserContact?)> GetUserInformation(Guid userId);
    Task<IEnumerable<UserSummary>> GetUserList();
    Task<User> GetUserProfileAsync(Guid userId);
}
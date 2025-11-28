using HUP.Core.Entities.Identity;

namespace HUP.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByCredentialsAsync(string nationalId);
    Task<(UserPersonalInfo?, UserContact?)> GetUserInformation(Guid userId);
}
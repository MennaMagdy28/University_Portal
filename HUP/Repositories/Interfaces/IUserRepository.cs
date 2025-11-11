using HUP.Core.Enums;
using HUP.Core.Entities.UserModels;

namespace HUP.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNationalIdAsync(string nationalId);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetByRoleAsync(RoleType role);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> UserExistsAsync(string nationalId, string email);
    }
}

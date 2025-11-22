using HUP.Core.Entities.Identity;
using System.Threading.Tasks;

namespace HUP.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> LoginAsync(string nationalId, string password);
        Task<string> GenerateJwtToken(User user);
    }
}

using HUP.Core.Enums;
using HUP.Core.Entities.UserModels;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly HUPDbContext _context;

        public UserRepository(HUPDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByNationalIdAsync(string nationalId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.NationalID == nationalId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UniversityEmail == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(RoleType role)
        {
            return await _context.Users
                .Where(u => u.Role == role && u.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(user);
            }
        }

        public async Task<bool> UserExistsAsync(string nationalId, string email)
        {
            return await _context.Users
                .AnyAsync(u => u.NationalID == nationalId || u.UniversityEmail == email);
        }
    }
}

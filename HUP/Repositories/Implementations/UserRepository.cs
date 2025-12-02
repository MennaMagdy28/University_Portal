using HUP.Core.Entities.Identity;
using HUP.Core.Models;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly HupDbContext _context;
    public UserRepository(HupDbContext context)
    {
        _context = context;
    }
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.ContactInfo)
            .Include(u => u.PersonalInfo)
            .Where(u => u.Id == id && !u.IsDeleted).FirstOrDefaultAsync();
        return await _context.Users
            .Include(u => u.ContactInfo)
            .Include(u => u.PersonalInfo)
            .Where(u => u.Id == id && !u.IsDeleted).FirstOrDefaultAsync();
    }
    // return the user by login credentials
    // Args: nationalId, password is the hashed password from the service layer
    // return: null if not found or user (success)
    public async Task<User> GetByCredentialsAsync(string nationalId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.NationalId == nationalId && u.IsActive);
        if (user == null)
            return null;
        return user;
    }

    public async Task<(UserPersonalInfo?, UserContact?)> GetUserInformation(Guid userId)
    {
        var info = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(u => new
            {
                u.PersonalInfo,
                u.ContactInfo
            }).FirstOrDefaultAsync();
        return (info.PersonalInfo, info.ContactInfo);
    }

    public async Task<IEnumerable<UserSummary>> GetUserList()
    {
        var list = await _context.Users.AsNoTracking()
            .Select(u => new UserSummary
            {
                Id = u.Id, FullName = u.FullName, NationalId = u.NationalId, RoleName = u.UserRole.Name
            }).ToListAsync();
        return list;
    }

    public async Task<User> GetUserProfileAsync(Guid userId)
    {
        var user = await _context.Users
            .Include(u => u.PersonalInfo)
            .Include(u => u.ContactInfo)
            .Where(u => u.Id == userId).FirstOrDefaultAsync();
        return user;
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.PersonalInfo)
            .Include(u =>u.ContactInfo)
            .Include(u => u.UserRole)
            .ToListAsync();
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        var entity = await _context.Users.FindAsync(id);
        if (entity != null)
            _context.Users.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
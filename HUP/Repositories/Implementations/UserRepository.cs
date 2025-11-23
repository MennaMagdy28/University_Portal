using HUP.Core.Entities.Identity;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly HUPDbContext _context;
    public UserRepository(HUPDbContext context)
    {
        _context = context;
    }
    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }
    // return the user by login credentials
    // Args: nationalId, password is the hashed password from the service layer
    // return: null if not found or user (success)
    public async Task<User> GetByCredentialsAsync(string nationalId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.NationalID == nationalId && u.IsActive);
        if (user == null)
            return null;
        return user;
    }
    

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }

    public void SoftDelete(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    
}
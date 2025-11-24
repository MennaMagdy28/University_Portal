using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;

namespace HUP.Repositories.Implementations;

public class FacultyRepository : IFacultyRepository
{
    public Task<Faculty> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Faculty>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Faculty entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Faculty entity)
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
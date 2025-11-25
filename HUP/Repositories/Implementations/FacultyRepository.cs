using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations;

public class FacultyRepository : IFacultyRepository
{
    private readonly HupDbContext _context;

    public FacultyRepository(HupDbContext context)
    {
        _context = context;
    }
    public async Task<Faculty> GetByIdAsync(Guid id)
    {
        var faculty = await _context.Faculties.FindAsync(id);
        return faculty;
    }

    public async Task<IEnumerable<Faculty>> GetAllAsync()
    {
        var faculties =await _context.Faculties.Where(f =>f.IsDeleted == false).ToListAsync();
        return faculties;
    }

    public async Task AddAsync(Faculty entity)
    {
        await _context.Faculties.AddAsync(entity);
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(Faculty entity)
    {
        _context.Faculties.Update(entity);
    }

    public void SoftDelete(Guid id)
    {
        var faculty = _context.Faculties.Find(id);
        if (faculty != null)
        {
            faculty.IsDeleted = true;
            _context.Faculties.Update(faculty);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
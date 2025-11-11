using HUP.Core.Entities.AcademicModels;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly HUPDbContext _context;

        public FacultyRepository(HUPDbContext context)
        {
            _context = context;
        }

        public async Task<Faculty> GetByIdAsync(int id)
        {
            return await _context.Faculties
                .Include(f => f.Departments)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Faculty>> GetAllAsync()
        {
            return await _context.Faculties
                .Include(f => f.Departments)
                .Where(f => f.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(Faculty faculty)
        {
            await _context.Faculties.AddAsync(faculty);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Faculty faculty)
        {
            faculty.UpdatedAt = DateTime.UtcNow;
            _context.Faculties.Update(faculty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var faculty = await GetByIdAsync(id);
            if (faculty != null)
            {
                faculty.IsActive = false;
                faculty.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(faculty);
            }
        }
    }
}

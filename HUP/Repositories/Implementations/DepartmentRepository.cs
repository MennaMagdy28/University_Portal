using HUP.Core.Entities.Academics;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HupDbContext _context;
        public DepartmentRepository(HupDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Department entity)
        {
            await _context.Departments.AddAsync(entity);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                .AsNoTracking()
                .Where(d => !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<Department> GetByIdAsync(Guid id)
        {
            var dept = await _context.Departments
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
            return dept;
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity != null)
                _context.Departments.Remove(entity);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetByFacultyIdAsync(Guid facultyId)
        {
            var departments = await _context.Departments.Where(d => d.FacultyId == facultyId).ToListAsync();
            return departments;
        }
    }
}

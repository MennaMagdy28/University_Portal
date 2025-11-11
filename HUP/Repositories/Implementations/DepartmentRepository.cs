using HUP.Core.Entities.AcademicModels;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HUPDbContext _context;

        public DepartmentRepository(HUPDbContext context)
        {
            _context = context;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.Faculty)
                .Include(d => d.Programs)
                .Include(d => d.Courses)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.Faculty)
                .Where(d => d.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetByFacultyIdAsync(int facultyId)
        {
            return await _context.Departments
                .Include(d => d.Faculty)
                .Where(d => d.FacultyID == facultyId && d.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            department.UpdatedAt = DateTime.UtcNow;
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await GetByIdAsync(id);
            if (department != null)
            {
                department.IsActive = false;
                department.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(department);
            }
        }
    }
}

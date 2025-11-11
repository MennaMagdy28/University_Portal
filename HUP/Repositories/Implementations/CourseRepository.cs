using HUP.Core.Entities.AcademicModels;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly HUPDbContext _context;

        public CourseRepository(HUPDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Prerequisite)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Prerequisite)
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Prerequisite)
                .Where(c => c.DepartmentID == departmentId && c.IsActive)
                .ToListAsync();
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            course.UpdatedAt = DateTime.UtcNow;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await GetByIdAsync(id);
            if (course != null)
            {
                course.IsActive = false;
                course.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(course);
            }
        }
    }
}

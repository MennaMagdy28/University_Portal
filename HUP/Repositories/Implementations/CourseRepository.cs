using HUP.Repositories.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace HUP.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        private readonly HupDbContext _context;
        public CourseRepository(HupDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetByIdAsync(Guid id) {
            var course = await _context.Courses
                .Include(c => c.Prerequisite)
                .FirstOrDefaultAsync(c => c.Id == id);
            return course;
        }

        public async Task<IEnumerable<Course>> GetAllAsync() 
        {
            return await _context.Courses
                .Include(c => c.Prerequisite)
                .ToListAsync();
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }

        public void Update(Course course) 
        {
            _context.Courses.Update(course);
        }

        public void SoftDelete(Guid courseId) 
        {
            var course = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            if (course != null)
            {
                course.IsDeleted = true;
                course.UpdatedAt = DateTime.UtcNow;
                _context.Courses.Update(course);
            }
        }

        public void Remove(Guid courseId)
        {
            var course = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}

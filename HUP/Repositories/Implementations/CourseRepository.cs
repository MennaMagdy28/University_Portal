using HUP.Repositories.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Data;
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

        public async Task RemoveAsync(Guid courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
                _context.Courses.Remove(course);
        }

        public async Task SaveChangesAsync()
        {
             await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByLevelAsync(int level)
        {
            return await _context.Courses
                .Include(c => c.Prerequisite)
                .Where(c => c.Level == level && !c.IsDeleted)
                .OrderBy(c => c.CourseCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByDepartmentAsync(Guid departmentId)
        {
            return await _context.ProgramPlan
                .Include(pp => pp.Course)
                .Where(pp => pp.DepartmentId == departmentId && !pp.Course.IsDeleted)
                .Select(pp => pp.Course)
                .Distinct()
                .OrderBy(c => c.Level)
                .ThenBy(c => c.CourseCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByIdsAsync(List<Guid> courseIds)
        {
            if (!courseIds.Any())
                return new List<Course>();

            return await _context.Courses
                .Where(c => courseIds.Contains(c.Id) && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCurrentOfferingsAsync(Guid courseId, Guid departmentId)
        {
            var currentSemester = await _context.Semesters
                .FirstOrDefaultAsync(s => s.IsActive);

            if (currentSemester == null)
                return new List<Course>();

            var offerings = await _context.CourseOfferings
                .Include(co => co.Course)
                .Where(co => co.CourseId == courseId
                          && co.DepartmentId == departmentId
                          && co.SemesterId == currentSemester.Id
                          && !co.IsDeleted)
                .Select(co => co.Course)
                .Distinct()
                .ToListAsync();

            return offerings;
        }

        public async Task<IEnumerable<Course>> GetPrerequisitesAsync(Guid courseId)
        {
            var prerequisites = new List<Course>();
            var currentCourseId = courseId;

            while (currentCourseId != Guid.Empty)
            {
                var course = await _context.Courses
                    .Include(c => c.Prerequisite)
                    .FirstOrDefaultAsync(c => c.Id == currentCourseId);

                if (course == null || course.PrerequisiteId == null)
                    break;

                prerequisites.Add(course.Prerequisite);
                currentCourseId = course.PrerequisiteId.Value;
            }

            return prerequisites;
        }
    }
}

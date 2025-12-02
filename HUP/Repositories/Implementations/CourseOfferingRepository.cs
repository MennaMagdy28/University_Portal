using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class CourseOfferingRepository : ICourseOfferingRepository
    {
        private readonly HupDbContext _context;
        public CourseOfferingRepository(HupDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId)
        {
            var courseOfferings = await _context.CourseOfferings
                .Include(co => co.Course)
                .Include(co => co.Semester)
                .Where(co => co.DepartmentId == departmentId && co.SemesterId == semesterId && !co.IsDeleted)
                .ToListAsync();
            return courseOfferings;
            
        }
        public async Task<IEnumerable<CourseOffering>> GetAvailableToRegisterAsync(Guid studentId)
        {
            var availableCourses = await _context.CourseOfferings
                .Where(co => co.Semester.IsActive && !co.IsDeleted)
                .Where(co => !_context.Enrollments  // Exclude courses the student is already registered / done / in progress
                        .Where(e => e.StudentId == studentId &&
                            (e.Status == EnrollmentStatus.Completed ||
                             e.Status == EnrollmentStatus.Registered ||
                             e.Status == EnrollmentStatus.InProgress))
                        .Select(e => e.CourseOfferingId)
                        .Contains(co.CourseId))
                .Where(co => co.Course.PrerequisiteId == null || // If course has no prerequisite → allowed
                        _context.Enrollments.Any(e => e.StudentId == studentId &&
                                                 e.CourseOfferingId == co.Course.PrerequisiteId &&
                                                 e.Status == EnrollmentStatus.Completed)) // If has prerequisite → student must have COMPLETED it
                .Include(co => co.Course)
                .Include(co => co.Schedules)
                .ToListAsync();
            return availableCourses;
        }

        public async Task<CourseOffering?> GetExistingAsync(Guid courseId, Guid deptId, Guid semesterId)
        {
            var entity = await _context.CourseOfferings.Where(co => co.CourseId == courseId
                                                                    && co.DepartmentId == deptId
                                                                    && co.SemesterId == semesterId
                                                                    && !co.IsDeleted).FirstOrDefaultAsync();
            return entity;
        }

        public async Task AddAsync(CourseOffering entity)
        {            
            await _context.CourseOfferings.AddAsync(entity);
        }

        public async Task<IEnumerable<CourseOffering>> GetAllAsync()
        {
            return await _context.CourseOfferings
                .Include(co => co.Course)
                .Include(co => co.Semester)
                .Where(co => !co.IsDeleted)
                .ToListAsync();
        }

        public async Task<CourseOffering> GetByIdAsync(Guid id)
        {
            var co = await _context.CourseOfferings
                .Include(co => co.Course)
                .Include(co => co.Semester)
                .FirstOrDefaultAsync(co => co.Id == id && !co.IsDeleted);
            return co;
        }

        public async Task RemoveAsync(Guid courseId)
        {
            var entity = await _context.CourseOfferings.FindAsync(courseId);

            if (entity != null)
                _context.CourseOfferings.Remove(entity);
        }
 
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
    
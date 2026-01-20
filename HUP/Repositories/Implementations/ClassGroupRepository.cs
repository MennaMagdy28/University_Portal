using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class ClassGroupRepository : IClassGroupRepository
    {
        private readonly HupDbContext _context;

        public ClassGroupRepository(HupDbContext context)
        {
            _context = context;
        }

        public async Task<ClassGroup> GetByIdAsync(Guid id)
        {
            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(cg => cg.Instructor)
                    .ThenInclude(i => i.User)
                .Include(cg => cg.Enrollments)
                .FirstOrDefaultAsync(cg => cg.Id == id && cg.IsActive && !cg.IsDeleted);
        }

        public async Task<IEnumerable<ClassGroup>> GetAllAsync()
        {
            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(cg => cg.Instructor)
                    .ThenInclude(i => i.User)
                .Where(cg => cg.IsActive && !cg.IsDeleted)
                .OrderBy(cg => cg.DayOfWeek)
                .ThenBy(cg => cg.StartTime)
                .ToListAsync();
        }

        public async Task AddAsync(ClassGroup entity)
        {
            await _context.ClassGroups.AddAsync(entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await _context.ClassGroups.FindAsync(id);
            if (entity != null)
                _context.ClassGroups.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClassGroup>> GetByCourseOfferingAsync(Guid courseOfferingId)
        {
            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(cg => cg.Instructor)
                    .ThenInclude(i => i.User)
                .Where(cg => cg.CourseOfferingId == courseOfferingId
                          && cg.IsActive
                          && !cg.IsDeleted)
                .OrderBy(cg => cg.GroupCode)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassGroup>> GetAvailableForStudentAsync(Guid studentId, int studentLevel)
        {
            var currentSemester = await _context.Semesters
                .FirstOrDefaultAsync(s => s.IsActive);

            if (currentSemester == null)
                return new List<ClassGroup>();

            var completedCourses = await _context.Enrollments
                .Include(e => e.ClassGroup)
                    .ThenInclude(cg => cg.CourseOffering)
                .Where(e => e.StudentId == studentId
                         && e.Status == EnrollmentStatus.Completed)
                .Select(e => e.ClassGroup.CourseOffering.CourseId)
                .ToListAsync();

            var currentEnrollments = await _context.Enrollments
                .Include(e => e.ClassGroup)
                    .ThenInclude(cg => cg.CourseOffering)
                .Where(e => e.StudentId == studentId
                         && (e.Status == EnrollmentStatus.Registered || e.Status == EnrollmentStatus.InProgress))
                .Select(e => e.ClassGroup.CourseOffering.CourseId)
                .ToListAsync();

            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(cg => cg.Instructor)
                    .ThenInclude(i => i.User)
                .Where(cg => cg.CourseOffering.SemesterId == currentSemester.Id
                          && cg.IsActive && !cg.IsDeleted
                          && cg.CurrentEnrollment < cg.Capacity
                          && cg.CourseOffering.Course.Level <= studentLevel
                          && !currentEnrollments.Contains(cg.CourseOffering.CourseId))
                .Where(cg => cg.CourseOffering.Course.PrerequisiteId == null
                          || completedCourses.Contains(cg.CourseOffering.Course.PrerequisiteId.Value))
                .OrderBy(cg => cg.CourseOffering.Course.Level)
                .ThenBy(cg => cg.CourseOffering.Course.CourseCode)
                .ToListAsync();
        }

        public async Task<bool> IsGroupFullAsync(Guid groupId)
        {
            var group = await _context.ClassGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(cg => cg.Id == groupId);

            return group != null && group.CurrentEnrollment >= group.Capacity;
        }

        public async Task<int> GetGroupEnrollmentCountAsync(Guid groupId)
        {
            return await _context.Enrollments
                .CountAsync(e => e.ClassGroupId == groupId
                              && (e.Status == EnrollmentStatus.Registered
                               || e.Status == EnrollmentStatus.InProgress));
        }

        public async Task IncrementEnrollmentAsync(Guid groupId)
        {
            var group = await _context.ClassGroups.FindAsync(groupId);
            if (group != null)
            {
                group.CurrentEnrollment++;
                group.UpdatedAt = DateTime.Now;
                _context.ClassGroups.Update(group);
            }
        }

        public async Task DecrementEnrollmentAsync(Guid groupId)
        {
            var group = await _context.ClassGroups.FindAsync(groupId);
            if (group != null && group.CurrentEnrollment > 0)
            {
                group.CurrentEnrollment--;
                group.UpdatedAt = DateTime.Now;
                _context.ClassGroups.Update(group);
            }
        }

        public async Task<IEnumerable<ClassGroup>> GetByStudentAsync(Guid studentId)
        {
            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(cg => cg.Instructor)
                    .ThenInclude(i => i.User)
                .Where(cg => cg.Enrollments.Any(e => e.StudentId == studentId
                                                   && (e.Status == EnrollmentStatus.Registered
                                                    || e.Status == EnrollmentStatus.InProgress)))
                .Where(cg => cg.IsActive && !cg.IsDeleted)
                .OrderBy(cg => cg.DayOfWeek)
                .ThenBy(cg => cg.StartTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassGroup>> GetByInstructorAsync(Guid instructorId)
        {
            return await _context.ClassGroups
                .Include(cg => cg.CourseOffering)
                    .ThenInclude(co => co.Course)
                .Include(cg => cg.Instructor)
                    .ThenInclude(i => i.User)
                .Where(cg => cg.InstructorId == instructorId
                          && cg.IsActive
                          && !cg.IsDeleted)
                .OrderBy(cg => cg.DayOfWeek)
                .ThenBy(cg => cg.StartTime)
                .ToListAsync();
        }

        public async Task<bool> CheckTimeConflictAsync(Guid studentId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, Guid? excludeGroupId = null)
        {
            var studentGroups = await _context.Enrollments
                .Include(e => e.ClassGroup)
                .Where(e => e.StudentId == studentId
                         && (e.Status == EnrollmentStatus.Registered
                          || e.Status == EnrollmentStatus.InProgress)
                         && (excludeGroupId == null || e.ClassGroupId != excludeGroupId))
                .Select(e => e.ClassGroup)
                .ToListAsync();

            foreach (var group in studentGroups)
            {
                if (group.DayOfWeek == dayOfWeek)
                {
                    if ((startTime >= group.StartTime && startTime < group.EndTime) ||
                        (endTime > group.StartTime && endTime <= group.EndTime) ||
                        (startTime <= group.StartTime && endTime >= group.EndTime))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

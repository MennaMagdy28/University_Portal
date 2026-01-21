using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class StudentRegistrationRepository : IStudentRegistrationRepository
    {
        private readonly HupDbContext _context;

        public StudentRegistrationRepository(HupDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasPassedPrerequisiteAsync(Guid studentId, Guid courseId)
        {
            var course = await _context.Courses
                .Include(c => c.Prerequisite)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course?.PrerequisiteId == null)
                return true; 

            var passed = await _context.Enrollments
                .Include(e => e.ClassGroup)
                    .ThenInclude(cg => cg.CourseOffering)
                .AnyAsync(e => e.StudentId == studentId
                            && e.ClassGroup.CourseOffering.CourseId == course.PrerequisiteId
                            && e.Status == EnrollmentStatus.Completed
                            && (e.ClassGrade + e.MidtermGrade + e.FinalGrade) >= 50); 

            return passed;
        }

        public async Task<bool> IsAlreadyEnrolledAsync(Guid studentId, Guid courseId)
        {
            var currentSemester = await _context.Semesters
                .FirstOrDefaultAsync(s => s.IsActive);

            if (currentSemester == null)
                return false;

            return await _context.Enrollments
                .Include(e => e.ClassGroup)
                    .ThenInclude(cg => cg.CourseOffering)
                .AnyAsync(e => e.StudentId == studentId
                            && e.ClassGroup.CourseOffering.CourseId == courseId
                            && e.ClassGroup.CourseOffering.SemesterId == currentSemester.Id
                            && (e.Status == EnrollmentStatus.Registered
                             || e.Status == EnrollmentStatus.InProgress));
        }

        public async Task<int> GetStudentLevelAsync(Guid studentId)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == studentId);

            return student?.Level ?? 1;
        }

        public async Task<List<Guid>> GetCompletedCoursesAsync(Guid studentId)
        {
            return await _context.Enrollments
                .Include(e => e.ClassGroup)
                    .ThenInclude(cg => cg.CourseOffering)
                .Where(e => e.StudentId == studentId
                         && e.Status == EnrollmentStatus.Completed
                         && (e.ClassGrade + e.MidtermGrade + e.FinalGrade) >= 60)
                .Select(e => e.ClassGroup.CourseOffering.CourseId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> CheckTimeConflictAsync(Guid studentId, Guid groupId)
        {
            var targetGroup = await _context.ClassGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(cg => cg.Id == groupId);

            if (targetGroup == null)
                return false;

            var studentGroups = await _context.Enrollments
                .Include(e => e.ClassGroup)
                .Where(e => e.StudentId == studentId
                         && (e.Status == EnrollmentStatus.Registered
                          || e.Status == EnrollmentStatus.InProgress)
                         && e.ClassGroupId != groupId)
                .Select(e => e.ClassGroup)
                .ToListAsync();

            foreach (var group in studentGroups)
            {
                if (group.DayOfWeek == targetGroup.DayOfWeek)
                {
                    bool timeConflict = (targetGroup.StartTime >= group.StartTime && targetGroup.StartTime < group.EndTime) ||
                                        (targetGroup.EndTime > group.StartTime && targetGroup.EndTime <= group.EndTime) ||
                                        (targetGroup.StartTime <= group.StartTime && targetGroup.EndTime >= group.EndTime);

                    if (timeConflict)
                        return true;
                }
            }

            return false;
        }

        public async Task<bool> CheckCourseCapacityAsync(Guid courseId, int maxStudentsPerCourse)
        {
            var currentSemester = await _context.Semesters
                .FirstOrDefaultAsync(s => s.IsActive);

            if (currentSemester == null)
                return false;

            var totalEnrolled = await _context.Enrollments
                .Include(e => e.ClassGroup)
                    .ThenInclude(cg => cg.CourseOffering)
                .Where(e => e.ClassGroup.CourseOffering.CourseId == courseId
                         && e.ClassGroup.CourseOffering.SemesterId == currentSemester.Id
                         && (e.Status == EnrollmentStatus.Registered
                          || e.Status == EnrollmentStatus.InProgress))
                .CountAsync();

            return totalEnrolled < maxStudentsPerCourse;
        }

        public async Task<Dictionary<Guid, int>> GetCourseEnrollmentCountsAsync(List<Guid> courseIds)
        {
            var currentSemester = await _context.Semesters
                .FirstOrDefaultAsync(s => s.IsActive);

            if (currentSemester == null)
                return new Dictionary<Guid, int>();

            var result = await _context.Enrollments
                .Include(e => e.ClassGroup)
                    .ThenInclude(cg => cg.CourseOffering)
                .Where(e => courseIds.Contains(e.ClassGroup.CourseOffering.CourseId)
                         && e.ClassGroup.CourseOffering.SemesterId == currentSemester.Id
                         && (e.Status == EnrollmentStatus.Registered
                          || e.Status == EnrollmentStatus.InProgress))
                .GroupBy(e => e.ClassGroup.CourseOffering.CourseId)
                .Select(g => new { CourseId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CourseId, x => x.Count);

            return result;
        }

        public async Task<List<Course>> GetPrerequisiteChainAsync(Guid courseId)
        {
            var chain = new List<Course>();
            var currentCourseId = courseId;

            while (currentCourseId != Guid.Empty)
            {
                var course = await _context.Courses
                    .Include(c => c.Prerequisite)
                    .FirstOrDefaultAsync(c => c.Id == currentCourseId);

                if (course == null)
                    break;

                chain.Add(course);
                currentCourseId = course.PrerequisiteId ?? Guid.Empty;
            }

            chain.Reverse();
            return chain;
        }
    }
}

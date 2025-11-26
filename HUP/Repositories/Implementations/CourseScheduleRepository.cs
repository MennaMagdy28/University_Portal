using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Data;
using HUP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class CourseScheduleRepository : ICourseScheduleRepository
    {
        private readonly HupDbContext _context;

        public CourseScheduleRepository(HupDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseSchedule>> GetByStudentAsync(Guid studentId)
        {
            var currentSemester = GetCurrentSemester();

            var studentEnrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId && e.Semester == currentSemester && e.IsActive &&
                           (e.Status == EnrollmentStatus.Registered || e.Status == EnrollmentStatus.InProgress))
                .Select(e => e.CourseId)
                .ToListAsync();

            if (!studentEnrollments.Any())
                return new List<CourseSchedule>();

            return await _context.CourseSchedules
                .Include(cs => cs.Course)
                .Include(cs => cs.Instructor)
                .ThenInclude(i => i.User)
                .Where(cs => studentEnrollments.Contains(cs.CourseID) &&
                            cs.Semester == currentSemester && cs.IsActive)
                .OrderBy(cs => cs.DayOfWeek)
                .ThenBy(cs => cs.StartTime)
                .ToListAsync();
        }

        private string GetCurrentSemester()
        {
            var now = DateTime.Now;
            var year = now.Year;
            var semester = now.Month switch
            {
                >= 1 and <= 5 => "Spring",
                >= 6 and <= 8 => "Summer",
                _ => "Fall"
            };
            return $"{semester} {year}";
        }
    }
}

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

        public async Task<CourseSchedule> GetByIdAsync(Guid id)
        {
            return await _context.CourseSchedules
                .Include(cs => cs.CourseOffering)
                .Include(cs => cs.Instructor)
                .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task<IEnumerable<CourseSchedule>> GetByStudentAsync(Guid studentId)
        {
            var currentSemester = GetCurrentSemester();

            var studentEnrollments = await _context.Enrollments
                .Include(e => e.CourseOffering)
                .Where(e => e.StudentId == studentId && e.CourseOffering.Semester.SemesterName == currentSemester &&
                           (e.Status == EnrollmentStatus.Registered || e.Status == EnrollmentStatus.InProgress))
                .Select(e => e.CourseOffering.CourseId)
                .ToListAsync();

            if (!studentEnrollments.Any())
                return new List<CourseSchedule>();

            return await _context.CourseSchedules
                .Include(cs => cs.CourseOffering)
                .Include(cs => cs.Instructor)
                .ThenInclude(i => i.User)
                .Where(cs => studentEnrollments.Contains(cs.CourseID) &&
                            cs.Semester == currentSemester && cs.IsActive)
                .OrderBy(cs => cs.DayOfWeek)
                .ThenBy(cs => cs.StartTime)
                .ToListAsync();
        }
        public async Task AddAsync(CourseSchedule schedule)
        {
            await _context.CourseSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseSchedule schedule)
        {
            schedule.UpdatedAt = DateTime.UtcNow;
            _context.CourseSchedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var schedule = await GetByIdAsync(id);
            if (schedule != null)
            {
                schedule.IsActive = false;
                schedule.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(schedule);
            }
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

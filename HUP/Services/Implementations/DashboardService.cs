using HUP.Core.DTOs.DashboardDTOs;
using HUP.Core.Enums;
using HUP.Data;
using HUP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HUP.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly HUPDbContext _context;

        public DashboardService(HUPDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var totalStudents = await _context.Students.CountAsync(s => s.IsActive);
            var totalInstructors = await _context.Instructors.CountAsync(i => i.IsActive);
            var totalCourses = await _context.Courses.CountAsync(c => c.IsActive);
            var activeEnrollments = await _context.Enrollments
                .CountAsync(e => e.Status == EnrollmentStatus.InProgress && e.IsActive);

            return new DashboardStatsDto
            {
                TotalStudents = totalStudents,
                TotalInstructors = totalInstructors,
                TotalCourses = totalCourses,
                ActiveEnrollments = activeEnrollments
            };
        }

        public async Task<IEnumerable<AcademicReportDto>> GetAcademicReportsAsync()
        {
            return await _context.Programs
                .Include(p => p.Students)
                .Where(p => p.IsActive)
                .Select(p => new AcademicReportDto
                {
                    ProgramName = p.ProgramName,
                    TotalStudents = p.Students.Count(s => s.IsActive),
                    AverageCGPA = p.Students.Where(s => s.IsActive).Average(s => s.CGPA),
                    GraduatedCount = p.Students.Count(s => s.IsActive && s.AcademicStatus == AcademicStatus.Graduated)
                })
                .ToListAsync();
        }
    }
}

using HUP.Repositories.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Data;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly HupDbContext _context;
        public EnrollmentRepository(HupDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Enrollment>> GetByStudentId(Guid studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId && !e.IsDeleted)
                .Include(e => e.CourseOffering)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetbySemester(Semester semester, Guid studentId)
        {
            return await _context.Enrollments
                .Where(e => e.EnrollmentDate >= semester.StartDate && e.EnrollmentDate <= semester.EndDate
                && e.StudentId == studentId && !e.IsDeleted)
                .Include(e => e.CourseOffering)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetExistingAsync(Guid studentId, Guid courseId)
        {
            return await _context.Enrollments.Where(e => e.StudentId == studentId
                                                         && e.CourseOfferingId == courseId && !e.IsDeleted)
                .FirstOrDefaultAsync(); 
        }

        public async Task AddAsync(Enrollment entity)
        {
            await _context.Enrollments.AddAsync(entity);
        }

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.CourseOffering)
                .Include(e => e.Student)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<Enrollment> GetByIdAsync(Guid id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            return enrollment;
        }

        public async Task RemoveAsync(Guid enrollmentId)
        {
            var enrollment = await _context.Enrollments.FindAsync(enrollmentId);

            if (enrollment != null) 
                _context.Enrollments.Remove(enrollment);
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

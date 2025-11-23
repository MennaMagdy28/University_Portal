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
        
        public async Task<IEnumerable<Enrollment>> GetbyStudentId(Guid studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetbySemster(Semester semester, Guid studentId)
        {
            return await _context.Enrollments
                .Where(e => e.EnrollmentDate >= semester.StartDate && e.EnrollmentDate <= semester.EndDate
                && e.StudentId == studentId)
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task AddAsync(Enrollment entity)
        {
            await _context.Enrollments.AddAsync(entity);
        }
        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToListAsync();
        }
        public async Task<Enrollment> GetByIdAsync(Guid id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == id);
            return enrollment;
        }

        public void Remove(Guid enrollmentId)
        {
            var enrollment = _context.Enrollments.Find(enrollmentId);
            if (enrollment != null) _context.Enrollments.Remove(enrollment);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void SoftDelete(Guid enrollmentId)
        {
            var enrollment = _context.Enrollments.Find(enrollmentId);
            if (enrollment != null)
            {
                enrollment.IsDeleted = true;
                enrollment.UpdatedAt = DateTime.UtcNow;
                _context.Enrollments.Update(enrollment);
            }
        }
        public void Update(Enrollment entity)
        {
            _context.Enrollments.Update(entity);
        }
    }
}

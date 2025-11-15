using HUP.Repositories.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Data;
using Microsoft.EntityFrameworkCore;

namespace HUP.Repositories.Implementations
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly HUPDbContext _context;
        public EnrollmentRepository(HUPDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Enrollment entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
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
        public async Task<IEnumerable<Enrollment>> GetByStudentAndCourseAsync(Guid studentId)
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId && !e.IsDeleted)
                .ToListAsync();
            return enrollments;
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
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Enrollments.Update(entity);
        }
    }
}

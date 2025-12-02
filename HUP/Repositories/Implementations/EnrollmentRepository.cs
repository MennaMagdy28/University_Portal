using HUP.Repositories.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Data;
using Microsoft.EntityFrameworkCore;
using HUP.Core.Models;

namespace HUP.Repositories.Implementations
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly HupDbContext _context;
        private readonly IStudentRepository _studentRepository;
        public EnrollmentRepository(HupDbContext context, IStudentRepository studentRepository)
        {
            _context = context;
            _studentRepository = studentRepository;

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

        public async Task<IEnumerable<Enrollment>> GetByStudentAndSemesterAsync(Guid studentId, string semester)
        {
            return await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId && e.Semester.SemesterName == semester && e.IsActive)
                .ToListAsync();
        }

        public async Task<List<SemesterGrades>> GetStudentSemesterGradeModelsAsync(Guid studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => new SemesterGrades
                {
                    SemesterId = e.SemesterId,
                    SemesterName = e.Semester.SemesterName,

                    CourseId = e.CourseId,
                    CourseName = e.Course.CourseName,
                    CourseCode = e.Course.CourseCode,
                    CourseCredits = e.Course.Credits,

                    ClassGrade = e.ClassGrade,
                    MidtermGrade = e.MidtermGrade,
                    FinalGrade = e.finalGrade
                })
                .ToListAsync();
        }
    }
}
